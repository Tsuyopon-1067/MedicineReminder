using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MedicineReminder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


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
    }
}
