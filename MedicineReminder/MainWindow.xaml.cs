using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using System;
using System.ComponentModel;

namespace MedicineReminder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            this.SizeToContent = SizeToContent.Height;
            // ウィンドウ移動ハンドルの追加
            moveHandle.MouseLeftButtonDown += (o, e) => DragMove();
            toggleButtonStackPanel.MouseLeftButtonDown += (o, e) => DragMove();
            bottomBar.MouseLeftButtonDown += (o, e) => DragMove();
            // ウィンドウ位置の読み込み
            this.Left = Properties.Settings.Default.windowLeft;
            this.Top = Properties.Settings.Default.windowTop;
            // トグルボタンの表示状態の読み込み
            SetMedicineToggleVisiblity();
            // トグルボタンのチェック状態の読み込み
            medicineToggleAsa.IsChecked = Properties.Settings.Default.isCheckedAsa;
            medicineToggleHiru.IsChecked = Properties.Settings.Default.isCheckedHiru;
            medicineToggleYoru.IsChecked = Properties.Settings.Default.isCheckedYoru;
            // タイマを作成する
            SetupTimer();
        }

        /// <param>
        /// 朝のトグルスイッチが有効かどうか
        /// </param>
        public bool isAvailableAsa = Properties.Settings.Default.isAvailableAsa;
        /// <param>
        /// 昼のトグルスイッチが有効かどうか
        /// </param>
        public bool isAvailableHiru = Properties.Settings.Default.isAvailableHiru;
        /// <param>
        /// 夜のトグルスイッチが有効かどうか
        /// </param>
        public bool isAvailableYoru = Properties.Settings.Default.isAvailableYoru;
        /// <param>
        /// 日付の数値
        /// </param>
        private int dateNumber = Properties.Settings.Default.dateNumber;

        static private SolidColorBrush bottomBarColor = new SolidColorBrush(Color.FromArgb(0x00, 0xFF, 0x00, 0x00)); // 通常時の色(透明にしておく)
        static private SolidColorBrush bottomBarSelectedColor = new SolidColorBrush(Color.FromArgb(0xFF, 0xA9, 0xA9, 0xA9)); // 選択時の色 FF A9 A9 A9
        /// <summary>
        /// 下部のボタンにマウスが重なった時に色を変えるメソッド
        /// </summary>
        /// <remarks> 背景色を選択時の色に変更する </remarks>
        /// <param name="sender">おまじない イベントハンドラとして必要</param>
        /// <param name="e">おまじない イベントハンドラとして必要</param>
        private void ButtomButton_MouseEnter(object sender, MouseEventArgs e)
        {
            ((TextBlock)sender).Background = bottomBarSelectedColor;
        }
        /// <summary>
        /// 下部のボタンからマウスが離れた時に色を戻すメソッド
        /// </summary>
        /// <remarks> 背景色を透明にする </remarks>
        /// <param name="sender">おまじない イベントハンドラとして必要</param>
        /// <param name="e">おまじない イベントハンドラとして必要</param>
        private void ButtomButton_MouseLeave(object sender, MouseEventArgs e)
        {
            ((TextBlock)sender).Background = bottomBarColor;
        }

        /// <summary>
        /// 設定ボタンがクリックされた時に設定ウィンドウを表示するメソッド
        /// </summary>
        /// <param name="sender">おまじない イベントハンドラとして必要</param>
        /// <param name="e">おまじない イベントハンドラとして必要</param>
        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow w = new SettingsWindow(this);
            w.Show();
        }
        /// <summary>
        /// アプリケーションを終了するメソッド
        /// </summary>
        /// <param name="sender">おまじない イベントハンドラとして必要</param>
        /// <param name="e">おまじない イベントハンドラとして必要</param>
        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            // ウィンドウ座標を保存
            Properties.Settings.Default.windowLeft = this.Left;
            Properties.Settings.Default.windowTop = this.Top;
            // トグルボタンのチェック状態を保存
            SaveToggleState();
            Properties.Settings.Default.Save();
            this.Close();
        }
        /// <summary>
        /// トグルボタンの表示状態を適用するメソッド
        /// </summary>
        public void SetMedicineToggleVisiblity()
        {
            Visibility getVisiblity(bool x) => x ? Visibility.Visible : Visibility.Collapsed;
            medicineToggleAsa.Visibility = getVisiblity(isAvailableAsa);
            medicineToggleHiru.Visibility = getVisiblity(isAvailableHiru);
            medicineToggleYoru.Visibility = getVisiblity(isAvailableYoru);
        }

        /// <summary>
        /// マウスがトグルボタンコントロールから外れたらトグルボタンのチェック状態を保存するメソッド
        /// </summary>
        /// <param name="sender">おまじない イベントハンドラとして必要</param>
        /// <param name="e">おまじない イベントハンドラとして必要</param>

        private void MedicineToggle_MouseLeave(object sender, MouseEventArgs e)
        {
            SaveToggleState();
            Properties.Settings.Default.Save();
        }
        /// <summary>
        /// トグルボタンのチェック状態を保存するメソッド
        /// </summary>
        private void SaveToggleState()
        {
            Properties.Settings.Default.isCheckedAsa = medicineToggleAsa.IsChecked;
            Properties.Settings.Default.isCheckedHiru = medicineToggleHiru.IsChecked;
            Properties.Settings.Default.isCheckedYoru = medicineToggleYoru.IsChecked;
        }
        /// <summary>
        /// トグルボタンの選択状態をすべてfalseにするメソッド
        /// </summary>
        private void SetToggleButtonFalse()
        {
            medicineToggleAsa.IsChecked = false;
            medicineToggleHiru.IsChecked = false;
            medicineToggleYoru.IsChecked = false;
            SaveToggleState();
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// 日付の変更を監視するメソッド
        /// </summary>
        /// <param name="sender">おまじない イベントハンドラとして必要</param>
        /// <param name="e">おまじない イベントハンドラとして必要</param>
        private void ObserveDateChange(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            if (dateNumber != dt.Day) // 日付が変わっていたらチェックボックスをリセット
            {
                SetToggleButtonFalse();
                dateNumber = dt.Day; // 日付を更新する
                Properties.Settings.Default.dateNumber = dateNumber;
                Properties.Settings.Default.Save();
            }
            else if (dt.Hour < 23) // まだ23時代になっていない <=> 残り時間が1時間より長い => インターバルを1時間にする
            {
                _timer.Interval = TimeSpan.FromHours(1);
            }
            else if (dt.Minute < 59) // まだ59分代になっていない <=> 残り時間が1分より長い => インターバルを1分にする
            {
                _timer.Interval = TimeSpan.FromMinutes(1);
            }
            else if (dt.Second < 59) // まだ59秒代になっていない <=> 残り時間が1秒より長い => インターバルを1秒にする
            {
                _timer.Interval = TimeSpan.FromSeconds(1);
            }
            else // 残り1秒切り => インターバルを1/20秒にする
            {
                _timer.Interval = TimeSpan.FromMilliseconds(50);
            }
        }
        // タイマのインスタンス
        private DispatcherTimer _timer;
        /// <summary>
        /// タイマを作成するメソッド
        /// </summary>
        private void SetupTimer()
        {
            // タイマのインスタンスを生成
            _timer = new DispatcherTimer(); // 優先度はDispatcherPriority.Background
            _timer.Interval = TimeSpan.FromSeconds(1); // インターバルを1秒にする
            _timer.Tick += new EventHandler(ObserveDateChange); // タイマメソッドを設定
            _timer.Start(); // タイマを開始

            // 画面が閉じられるときに、タイマを停止
            this.Closing += new CancelEventHandler(StopTimer);
        }
        /// <summary>
        /// タイマを停止
        /// </summary>
        private void StopTimer(object sender, CancelEventArgs e)
        {
            _timer.Stop();
        }
    }
}
