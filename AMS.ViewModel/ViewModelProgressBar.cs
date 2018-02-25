using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMS.ViewModel
{
    public class ViewModelProgressBar : ViewModelObject
    {
        private static readonly string CLASSNAME = "ViewModelProgressBar";
        public delegate void ProgressBarClose();
        /// <summary>
        /// 完成事件
        /// </summary>
        public event ProgressBarClose ProgressFinish;
        /// <summary>
        /// 刷新事件
        /// </summary>
        public event ProgressBarClose ProgressRefresh;
        private string _ProgressType = "";
        /// <summary>
        /// 进度类型
        /// </summary>
        public string ProgressType
        {
            get { return _ProgressType; }
            set { _ProgressType = value; OnPropertyChanged("ProgressType"); }
        }

        private string _ErrorMessage = "";
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; OnPropertyChanged("ErrorMessage"); }
        }
        private string _ProgressName = "";
        /// <summary>
        /// 进度名称
        /// </summary>
        public string ProgressName
        {
            get { return _ProgressName; }
            set { _ProgressName = value; OnPropertyChanged("ProgressName"); }
        }

        private int _NowProgress = 0;
        /// <summary>
        /// 当前进度
        /// </summary>
        public string NowProgress
        {
            get { return _NowProgress.ToString() + "%"; }
            set
            {
                _NowProgress = int.Parse(value);
                OnPropertyChanged("NowProgress");
                OnPropertyChanged("Progress");
                OnPropertyChanged("ProgressFull");
                OnPropertyChanged("NowFullProgress");
            }
        }
        public double Progress
        {
            get { return (double)_NowProgress; }
        }

        private int _NowFullProgress = 0;
        private int _FullProgress = 1;

        public int FullProgress
        {
            get { return _FullProgress; }
            set { _FullProgress = value; OnPropertyChanged("NowFullProgress"); }
        }
        public double ProgressFull
        {
            get
            {
                if (_NowFullProgress == _FullProgress)
                {
                    IsFinish();
                }
                if (_NowProgress == 100)
                {
                    _NowFullProgress++;
                    return (((double)_NowFullProgress) / (double)_FullProgress) * 100;
                }
                else
                {
                    return (((double)_NowFullProgress) / (double)_FullProgress) * 100 + Progress / (double)_FullProgress;
                }
            }
        }
        /// <summary>
        /// 总进度
        /// </summary>
        public string NowFullProgress
        {
            get
            {
                if (_NowProgress == 100)
                {
                    return ((((double)_NowFullProgress) / (double)_FullProgress) * 100).ToString().Split('.')[0] + "%";
                }
                else
                {
                    return ((((double)_NowFullProgress) / (double)_FullProgress) * 100 + Progress / (double)_FullProgress).ToString().Split('.')[0] + "%";
                }
            }
        }
        /// <summary>
        /// 触发错误事件
        /// </summary>
        public void Refresh()
        {
            _NowFullProgress = 0;
        }
        /// <summary>
        /// 触发完成事件
        /// </summary>
        public void IsFinish()
        {
            if (ProgressFinish != null)
            {
                ProgressFinish();
            }
        }
        public ViewModelProgressBar ToCype()
        {
            ViewModelProgressBar newVM = new ViewModelProgressBar();
            newVM._FullProgress = this._FullProgress;
            newVM._NowFullProgress = this._NowFullProgress;
            newVM._NowProgress = this._NowProgress;
            newVM._ProgressName = this._ProgressName;
            newVM._ProgressType = this._ProgressType;
            return newVM;
        }
    }
}
