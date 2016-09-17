using MikTimer.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace MikTimer.View
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private CurrentTime currentTime = new CurrentTime();
        private ObservableCollection<TimerItem> timers = new ObservableCollection<TimerItem>();

        public MainWindow()
        {
            InitializeComponent();
        }

        #region Event Handler

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.listView_Timer.DataContext = this.timers;
            this.textBlock_Now.DataContext = this.currentTime;
        }

        private void button_New_Click(object sender, RoutedEventArgs e)
        {
            TimerDetailWindow w = new TimerDetailWindow();
            bool? ok = w.ShowDialog();
            if (ok != true)
            {
                return;
            }

            TimerItem item = w.TimerItem;
            this.timers.Add(item);
            item.Start();
        }

        private void button_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (this.listView_Timer.SelectedItems.Count < 1)
            {
                return;
            }

            IList selectedItems = this.listView_Timer.SelectedItems;
            foreach (Object item in selectedItems)
            {
                TimerItem timerItem = item as TimerItem;
                if (timerItem == null)
                {
                    continue;
                }

                timerItem.Stop();
                this.timers.Remove(timerItem);
            }
        }

        private void listView_Timer_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.listView_Timer.SelectedIndex = -1;
        }

        #endregion

        private class Now
        {
            public string Time
            {
                get
                {
                    DateTime now = DateTime.Now;
                    return now.ToString("HH:mm:ss");
                }
            }
        }
    }
}
