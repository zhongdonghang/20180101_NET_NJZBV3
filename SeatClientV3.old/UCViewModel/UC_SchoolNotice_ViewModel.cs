using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace SeatClientV3.UCViewModel
{
    public class UC_SchoolNotice_ViewModel : INotifyPropertyChanged
    {
        #region 属性
        private System.Windows.Media.Imaging.BitmapImage _SchoolNoteImage = new System.Windows.Media.Imaging.BitmapImage();
        /// <summary>
        /// 校园通知图片
        /// </summary>
        public System.Windows.Media.Imaging.BitmapImage SchoolNoteImage
        {
            get { return _SchoolNoteImage; }
            set { _SchoolNoteImage = value; OnPropertyChanged("SchoolNoteImage"); }
        }
        private System.Windows.Media.Imaging.BitmapImage _NowShowImage;
        /// <summary>
        /// 当前显示图片
        /// </summary>
        public System.Windows.Media.Imaging.BitmapImage NowShowImage
        {
            get { return _NowShowImage; }
            set { _NowShowImage = value; OnPropertyChanged("NowShowImage"); }
        }
        /// <summary>
        /// 向左按钮隐藏
        /// </summary>
        private string _LeftBtn = "Collapsed";
        public string LeftBtn
        {
            get { return _LeftBtn; }
            set { _LeftBtn = value; OnPropertyChanged("LeftBtn"); }
        }

        /// <summary>
        /// 向右按钮隐藏
        /// </summary>
        private string _RightBtn = "Collapsed";
        public string RightBtn
        {
            get { return _RightBtn; }
            set { _RightBtn = value; OnPropertyChanged("RightBtn"); }
        }
        private string Apppath = AppDomain.CurrentDomain.BaseDirectory + "images\\AdImage\\";
        int noticeNum;
        private List<SeatManage.ClassModel.SchoolNoteInfo> SchoolNotices;
        //public static readonly DependencyProperty SchoolNoticeCount = DependencyProperty.Register("SchoolNoticeCount", typeof(int), typeof( MyUserControl.UC_SchoolNotice));
        #endregion

        #region 图片切换
        SeatManage.SeatManageComm.TimeLoop ImgTime = null;
        SeatManage.SeatManageComm.TimeLoop ImgTimeStop = null;
        /// <summary>
        /// 执行图片切换
        /// </summary>
        public void ImageChangeRun()
        {
            SchoolNotices = OperateResult.SystemObject.GetInstance().SchoolNote;
            ViewModel.MainWindow_ViewModel.SchoolNotices = SchoolNotices.Count();
            noticeNum = 0;
            if (SchoolNotices.Count > 0)
            {
                NowShowImage = new System.Windows.Media.Imaging.BitmapImage(new Uri(Apppath + "NoteImage\\" + SchoolNotices[noticeNum].NoteImagePath, UriKind.RelativeOrAbsolute));
            }
            BtnVisible();
            ImgTime = new SeatManage.SeatManageComm.TimeLoop(10 * 1000);
            ImgTime.TimeTo += new EventHandler(ImgTime_TimeTo);
            ImgTimeStop = new SeatManage.SeatManageComm.TimeLoop(10 * 1000);
            ImgTimeStop.TimeTo += new EventHandler(ImgTimeStop_TimeTo);
            ImgTime.TimeStrat();
        }
        public void ImageChangeStop()
        {
            ImgTime.TimeStop();
            ImgTimeStop.TimeStop();
            ImgTimeStop.TimeStrat();
        }
        /// <summary>
        /// 图片停止切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ImgTimeStop_TimeTo(object sender, EventArgs e)
        {
            ImgTimeStop.TimeStop();
            ImgTime.TimeStrat();
        }
        /// <summary>
        /// 图片切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ImgTime_TimeTo(object sender, EventArgs e)
        {
            try
            {
                ImgTime.TimeStop();
                if (SchoolNotices.Count > 0)
                {
                    if (noticeNum >= SchoolNotices.Count - 1)
                    {
                        noticeNum = -1;
                    }
                    System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        noticeNum++;
                        NowShowImage = new System.Windows.Media.Imaging.BitmapImage(new Uri(Apppath + "NoteImage\\" + SchoolNotices[noticeNum].NoteImagePath, UriKind.RelativeOrAbsolute));
                        BtnVisible();

                        if (ImageChange != null && SchoolNotices.Count > 1)
                        {
                            ImageChange(this, new EventArgs());
                        }
                    }));
                }
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("查询进出记录遇到异常" + ex.Message);
            }
            finally
            {
                ImgTime.TimeStrat();
            }
        }
        //图片向左
        public bool ImageLeft()
        {
            try
            {
                if (noticeNum > 0)
                {
                    noticeNum--;
                    NowShowImage = new System.Windows.Media.Imaging.BitmapImage(new Uri(Apppath + "NoteImage\\" + SchoolNotices[noticeNum].NoteImagePath, UriKind.RelativeOrAbsolute));
                    BtnVisible();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("切换图片遇到异常" + ex.Message);
                return false;
            }
        }
        //图片向右
        public bool ImageRight()
        {
            try
            {
                if (noticeNum < SchoolNotices.Count - 1)
                {
                    noticeNum++;
                    NowShowImage = new System.Windows.Media.Imaging.BitmapImage(new Uri(Apppath + "NoteImage\\" + SchoolNotices[noticeNum].NoteImagePath, UriKind.RelativeOrAbsolute));
                    BtnVisible();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("切换图片遇到异常" + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 按钮显示
        /// </summary>
        private void BtnVisible()
        {
            if (noticeNum > 0)
            {
                LeftBtn = "Visible";
            }
            else
            {
                LeftBtn = "Collapsed";
            }
            if (noticeNum < SchoolNotices.Count - 1)
            {
                RightBtn = "Visible";
            }
            else
            {
                RightBtn = "Collapsed";
            }
        }
        #endregion

        public event EventHandler ImageChange;
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
