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
    /// CheckBoxText.xaml の相互作用ロジック
    /// </summary>
    public partial class CheckBoxText : UserControl
    {
        public CheckBoxText()
        {
            InitializeComponent();
        }
        public string Text
        {
            get { return tb.Text; }
            set { tb.Text = value; }
        }
        public bool IsChecked
        {
            get { return (bool)check.IsChecked; }
            set { check.IsChecked = value; }
        }
    }
}
