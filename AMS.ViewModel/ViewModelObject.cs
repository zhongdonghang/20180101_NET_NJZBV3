using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using AMS.Model;

namespace AMS.ViewModel
{
    public class ViewModelObject:INotifyPropertyChanged
    {
        private static AMS_UserInfo user;

        public static AMS_UserInfo User
        {
            get { return user; }
            set { user = value;
          // OnPropertyChanged("User");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
