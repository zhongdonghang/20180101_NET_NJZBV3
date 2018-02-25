using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace SeatClientV2.ViewModel
{
    public class LoadingUC_ViewModel : INotifyPropertyChanged
    {
        public LoadingUC_ViewModel()
        {
            RT = new System.Timers.Timer(100);
            for (int i = 0; i < 8; i++)
            {
                RotateAngles.Add(i * 40);
                DirectionAngles.Add(i * 40 + 315);
            }
            RT.Elapsed += new System.Timers.ElapsedEventHandler(RT_Elapsed);
            RT.Start();
        }

        void RT_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            RT.Stop();
            for (int i = 0; i < 8; i++)
            {
                RotateAngles[i] += 40;
                if (RotateAngles[i] > 360)
                {
                    RotateAngles[i] -= 360;
                }
                DirectionAngles[i] += 40;
                if (DirectionAngles[i] > 360)
                {
                    DirectionAngles[i] -= 360;
                }
            }
            RT.Start();
        }
        public void TimeClose()
        {
            RT.Stop();
            RT.Dispose();
        }
        private System.Timers.Timer RT;
        private ObservableCollection<int> _RotateAngles = new ObservableCollection<int>();
        /// <summary>
        /// 旋转角度
        /// </summary>
        public ObservableCollection<int> RotateAngles
        {
            get { return _RotateAngles; }
            set { _RotateAngles = value; OnPropertyChanged("RotateAngles"); }
        }
        private ObservableCollection<int> _DirectionAngles = new ObservableCollection<int>();
        /// <summary>
        /// 阴影角度
        /// </summary>
        public ObservableCollection<int> DirectionAngles
        {
            get { return _DirectionAngles; }
            set { _DirectionAngles = value; OnPropertyChanged("DirectionAngles"); }
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
