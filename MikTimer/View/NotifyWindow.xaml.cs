using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MikTimer.View
{
    /// <summary>
    /// NotifyWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class NotifyWindow : Window
    {
        public NotifyWindow(string name)
        {
            InitializeComponent();

            this.textBlcok_Name.Text = name;
        }

        #region Event Handler

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SystemSounds.Asterisk.Play();
        }

        private void button_OK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
