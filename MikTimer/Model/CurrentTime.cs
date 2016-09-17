using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;

namespace MikTimer.Model
{
    public class CurrentTime : INotifyPropertyChanged
    {
        private DispatcherTimer timer = new DispatcherTimer();

        public CurrentTime()
        {
            this.timer.Interval = TimeSpan.FromMilliseconds(100);
            this.timer.Tick += timer_Tick;
            this.timer.Start();
        }

        public string Now
        {
            get
            {
                return DateTime.Now.ToString("HH:mm:ss");
            }
            set
            {
                OnPropertyChanged("Now");
            }
        }
                
        private void timer_Tick(object sender, EventArgs e)
        {
            this.Now = DateTime.Now.ToString("HH:mm:ss");
        }

        public event PropertyChangedEventHandler PropertyChanged;
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
