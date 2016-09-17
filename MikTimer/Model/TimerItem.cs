using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;

namespace MikTimer.Model
{
    public class TimerItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private DispatcherTimer timer = new DispatcherTimer();

        private string name = string.Empty;
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        private DateTime end = new DateTime();
        public DateTime End
        {
            get { return this.end; }
            set { this.end = value; }
        }

        public string EndTime
        {
            get
            {
                string time = this.end.ToString("HH:mm:ss");
                return time;
            }
        }

        public string Remain
        {
            get
            {
                DateTime now = DateTime.Now;
                TimeSpan remain = this.end - now;
                string time = remain.ToString(@"hh\:mm\:ss");
                return time;
            }
            set
            {
                OnPropertyChanged("Remain");
            }
        }

        public TimerItem(string name, DateTime end)
        {
            this.name = name;
            this.end = end;

            start();
        }

        public TimerItem(string name, TimeSpan remain)
        {
            this.name = name;

            DateTime now = DateTime.Now;
            DateTime end = now + remain;
            this.end = end;

            start();
        }

        private void start()
        {
            this.timer.Interval = TimeSpan.FromSeconds(1);
            this.timer.Tick += timer_Tick;
            this.timer.Start();
        }

        public void Stop()
        {
            this.timer.Stop();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.Remain = string.Empty;

            if (this.Remain != "00:00:00")
            {
                return;
            }

            this.timer.Stop();
            MessageBox.Show(this.name,
                "It's time now.",
                MessageBoxButton.OK,
                MessageBoxImage.Information,
                MessageBoxResult.OK,
                MessageBoxOptions.DefaultDesktopOnly);
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler == null)
            {
                return;
            }

            handler(this, new PropertyChangedEventArgs(name));
        }
    }
}
