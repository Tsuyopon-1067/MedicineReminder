using System.Windows;
using System.Windows.Forms;


namespace MedicineReminder
{
    /// <summary>
    /// SettingsWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private MainWindow mw;
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="mw">MainWindowのインスタンス</param>
        public SettingsWindow(MainWindow mw)
        {
            InitializeComponent();
            this.SizeToContent = SizeToContent.Height;
            this.mw = mw;
            // トグルボタンの表示状態の読み込み
            asaCheck.IsChecked = Properties.Settings.Default.isAvailableAsa;
            hiruCheck.IsChecked = Properties.Settings.Default.isAvailableHiru;
            yoruCheck.IsChecked = Properties.Settings.Default.isAvailableYoru;
            // マウスカーソル位置にウィンドウを移動
            this.Left = System.Windows.Forms.Cursor.Position.X;
            this.Top = System.Windows.Forms.Cursor.Position.Y;
            if (this.Left > 100) this.Left -= 100;
            if (this.Top > 100) this.Top -= 100;
        }

        /// <summary>
        /// アプリケーションを終了するメソッド
        /// </summary>
        /// <param name="sender">おまじない イベントハンドラとして必要</param>
        /// <param name="e">おまじない イベントハンドラとして必要</param>
        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// チェックボックスの値を適用するメソッド
        /// </summary>
        /// <param name="sender">おまじない イベントハンドラとして必要</param>
        /// <param name="e">おまじない イベントハンドラとして必要</param>
        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            // メイン画面に設定を送る
            mw.isAvailableAsa = asaCheck.IsChecked;
            mw.isAvailableHiru = hiruCheck.IsChecked;
            mw.isAvailableYoru = yoruCheck.IsChecked;
            // 設定の変更を保存
            Properties.Settings.Default.isAvailableAsa = asaCheck.IsChecked;
            Properties.Settings.Default.isAvailableHiru = hiruCheck.IsChecked;
            Properties.Settings.Default.isAvailableYoru = yoruCheck.IsChecked;
            Properties.Settings.Default.Save();
            // トグルボタンの表示状態を適用
            mw.SetMedicineToggleVisiblity();
            QuitButton_Click(sender, e);
        }
    }
}
