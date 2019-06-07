using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personalised_News_Feed.Classes.UI
{
    public class SideBarItem : INotifyPropertyChanged
    {
        public string ItemTitle { get; set; }

        private string IsActive_;

        public string IsActive {
            get
            {
                return IsActive_;
            }
            set
            {
                IsActive_ = value;
                OnPropertyChanged("IsActive");
            }
        }

        public string IconPath { get; set; }

        public string ItemId { get; set; }

        private void OnPropertyChanged(string v)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(v));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
