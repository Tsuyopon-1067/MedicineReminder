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
    /// MedicineToggle.xaml の相互作用ロジック
    /// </summary>
    public partial class MedicineToggle : UserControl
    {
        public MedicineToggle()
        {
            InitializeComponent();
        }

        bool isChecked = false;
        public string Text
        {
            get { return tb.Text; }
            set { tb.Text = value; }
        }
        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                isChecked = value;
                SetMaruBatu(isChecked);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            isChecked = !isChecked;
            SetMaruBatu(isChecked);
        }
        private void SetMaruBatu(bool x)
        {
            if (x)
            {
                maru.Visibility = Visibility.Visible;
                batu.Visibility = Visibility.Hidden;
            }
            else
            {
                maru.Visibility = Visibility.Hidden;
                batu.Visibility = Visibility.Visible;
            }
        }
    }
}
