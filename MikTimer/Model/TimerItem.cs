using System;
using System.ComponentModel;
using System.Windows;

namespace MikTimer.Model
{
    public class TimerItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

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
        }

        public TimerItem(string name, TimeSpan remain)
        {
            this.name = name;

            DateTime now = DateTime.Now;
            DateTime end = now + remain;
            this.end = end;
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
