using MikTimer.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

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
        }

        private void button_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (this.listView_Timer.SelectedItems.Count < 1)
            {
                return;
            }

            IList selectedItems = this.listView_Timer.SelectedItems;
            List<TimerItem> marks = new List<TimerItem>();
            foreach (Object itemObject in selectedItems)
            {
                TimerItem item = itemObject as TimerItem;
                if (item == null)
                {
                    continue;
                }

                item.Stop();
                marks.Add(item);
            }

            foreach (TimerItem item in marks)
            {
                this.timers.Remove(item);
            }
        }

        private void listView_Timer_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.listView_Timer.SelectedIndex = -1;
        }

        #endregion
    }
}
