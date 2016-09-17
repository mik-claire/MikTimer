using MikTimer.Model;
using System;
using System.Windows;
using System.Windows.Input;

namespace MikTimer.View
{
    /// <summary>
    /// TimerDetailWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class TimerDetailWindow : Window
    {
        private TimerItem timerItem;
        public TimerItem TimerItem
        {
            get { return this.timerItem; }
            // set { this.timerItem = value; }
        }

        public TimerDetailWindow()
        {
            InitializeComponent();
        }

        private void button_OK_Click(object sender, RoutedEventArgs e)
        {
            string name = this.textBox_Name.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                name = "# no_name";
            }

            string endTime = this.textBox_End.Text.Trim();
            DateTime end = new DateTime();
            if (string.IsNullOrEmpty(endTime))
            {
                string remainTime = string.Format("{0}:{1}:{2}",
                    this.textBox_RemainHour.Text.Trim(),
                    this.textBox_RemainMinute.Text.Trim(),
                    this.textBox_RemainSecond.Text.Trim()
                    );
                TimeSpan remain = new TimeSpan();
                if (!TimeSpan.TryParse(remainTime, out remain))
                {
                    MessageBox.Show("Invalid Input: Remain");
                    return;
                }

                this.timerItem = new TimerItem(name, remain);
            }
            else
            {
                if (!DateTime.TryParse(endTime, out end))
                {
                    MessageBox.Show("Invalid Input: End");
                    return;
                }

                this.timerItem = new TimerItem(name, end);
            }

            this.DialogResult = true;
        }

        private void button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter)
            {
                return;
            }

            button_OK_Click(null, null);
        }
    }
}
