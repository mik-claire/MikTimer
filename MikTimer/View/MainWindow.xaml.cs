using MikTimer.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace MikTimer.View
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer mainTimer = new DispatcherTimer();
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

            this.mainTimer.Interval = TimeSpan.FromMilliseconds(100);
            this.mainTimer.Tick += mainTimer_Tick;
            this.mainTimer.Start();
        }

        private void mainTimer_Tick(object sender, EventArgs e)
        {
            List<TimerItem> marks = new List<TimerItem>();
            foreach (TimerItem item in this.timers)
            {
                item.Remain = string.Empty;
                if (item.Remain != "00:00:00")
                {
                    continue;
                }

                marks.Add(item);
                NotifyWindow w = new NotifyWindow(item.Name);
                w.Owner = this;
                w.Show();
            }

            foreach (TimerItem item in marks)
            {
                this.timers.Remove(item);
            }
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

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                button_Delete_Click(null, null);
            }

            if (((Keyboard.GetKeyStates(Key.LeftCtrl) & KeyStates.Down) != KeyStates.Down) &&
                ((Keyboard.GetKeyStates(Key.RightCtrl) & KeyStates.Down) != KeyStates.Down))
            {
                return;
            }

            if (e.Key == Key.A)
            {
                this.listView_Timer.SelectAll();
            }

            if (e.Key == Key.N)
            {
                button_New_Click(null, null);
            }

            if (e.Key == Key.D)
            {
                button_Delete_Click(null, null);
            }
        }
    }
}
