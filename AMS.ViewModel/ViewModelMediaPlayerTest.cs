using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.IO;
using System.Xml;
using System.Timers;

namespace AMS.ViewModel
{
    public class ViewModelMediaPlayerTest : ViewModelObject
    {
        private static readonly string CLASSNAME = "ViewModelMediaPlayerTest";
        private string loaclPath = AppDomain.CurrentDomain.BaseDirectory + "\\TestTemp\\" + AMS.Model.Enum.SeatManageSubsystem.MediaFiles.ToString() + "\\";
        /// <summary>
        /// 定时器，定时扫描播放列表
        /// </summary>
        private Timer timer = new Timer(300);
        /// <summary>
        /// time控件
        /// </summary>
        public Timer Timer
        {
            get { return timer; }
            set { timer = value; }
        }
        #region 事件
        public delegate void PlayVideoEventHandler(object sender, string message);
        /// <summary>
        /// 通知播放事件
        /// </summary>
        public event PlayVideoEventHandler PlayVideo;
        /// <summary>
        /// 下载事件
        /// </summary>
        public event PlayVideoEventHandler PlayListHandleEvent;
        /// <summary>
        /// 初始化结束
        /// </summary>
        public event EventHandler PlayListHandleOver;
        #endregion
        private ViewModelProgressBar _vm_Progressbar = new ViewModelProgressBar();

        /// <summary>
        /// 进度条
        /// </summary>
        public ViewModelProgressBar Vm_Progressbar
        {
            get { return _vm_Progressbar; }
            set { _vm_Progressbar = value; }
        }
        private AMS.Model.AMS_PlayList _PlayModel;
        /// <summary>
        /// model
        /// </summary>
        public AMS.Model.AMS_PlayList PlayModel
        {
            get { return _PlayModel; }
            set { _PlayModel = value; OnPropertyChanged("PlayModel"); }
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

        /// <summary>
        /// 视频播放窗口高度
        /// </summary>
        public string VideoImageHeight
        {
            get
            {
                if (_IsShowSlip)
                {
                    return "607";
                }
                else
                {
                    return "895";
                }
            }
        }
        public string SlipShow
        {
            get
            {
                if (_IsShowSlip)
                {
                    return "Visible";
                }
                else
                {
                    return "Collapsed";
                }
            }
        }

        private bool _IsShowSlip = false;
        /// <summary>
        /// 是否显示优惠券
        /// </summary>
        public bool IsShowSlip
        {
            get { return _IsShowSlip; }
            set
            {
                _IsShowSlip = value;
                OnPropertyChanged("PlayModel");
                OnPropertyChanged("VideoImageHeight");
                OnPropertyChanged("SlipShow");
            }
        }
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <returns></returns>
        public bool DownLoadFile()
        {
            string functionName = "DownLoadFile";
            try
            {
                string resultstr = "";
                AMS.ServiceProxy.FileOperate download = new ServiceProxy.FileOperate();
                download.HandleProgress += new ServiceProxy.EventHandleFileTransport(download_HandleProgress);
                Vm_Progressbar.ProgressType = "媒体文件下载";
                Vm_Progressbar.FullProgress = _PlayModel.MediaFiles.Count;
                foreach (AMS.Model.AMS_VideoItem item in _PlayModel.MediaFiles)
                {
                    Vm_Progressbar.ProgressName = "正在下载\"" + item.Name + "\"……";
                    resultstr = download.FileDownLoad(loaclPath + item.Name, item.ReRelativeUrl, SeatManage.EnumType.SeatManageSubsystem.MediaFiles);
                    if (!string.IsNullOrEmpty(resultstr))
                    {
                        ErrorMessage = resultstr;
                        return false;
                    }
                }
                return true;
            }
            catch (AMS.Model.CustomerException ex)
            {
                ErrorMessage = string.Format("{0} 出自{1}.{2}", ex.Message, ex.ErrorSourcesClass, ex.ErrorSourcesFunction);
                return false;
            }
            catch (Exception ex)
            {
                ErrorMessage = string.Format("{0} 出自{1}.{2}", ex.Message, CLASSNAME, functionName);
                return false;
            }
        }

        void download_HandleProgress(int message)
        {
            Vm_Progressbar.NowProgress = message.ToString();
        }

        public void StartPlay()
        {
            string functionName = "StartPlay";
            try
            {
                LoadPlayList();
                timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
                //播放列表初始化结束，触发事件
                PlayListHandleOver(null, null);
                timer.Start();
            }
            catch (AMS.Model.CustomerException ex)
            {
                ErrorMessage = string.Format("{0} 出自{1}.{2}", ex.Message, ex.ErrorSourcesClass, ex.ErrorSourcesFunction);
            }
            catch (Exception ex)
            {
                ErrorMessage = string.Format("{0} 出自{1}.{2}", ex.Message, CLASSNAME, functionName);
            }
        }
        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            timer.Stop();
            getPlayFilePath();
            timer.Start();
        }
        /// <summary>
        /// 从本地载入播放列表
        /// </summary>
        public bool LoadPlayList()
        {
            try
            {
                //计算整个播放列表播放时间的间隔加上循环间隔时长
                playListTimeLength = _PlayModel.PlayListTimeLength;
                //TODO:根据当前时间重新排列播放列表
                RearrangePlayList();
                return true;
            }
            catch
            {
                PlayListHandleEvent(this, "载入播放列表失败！");
                return false;
            }
        }
        /// <summary>
        /// 重新排列当前播放列表
        /// </summary>
        private void RearrangePlayList()
        {
            PlayListHandleEvent(this, "播放列表排列");

            int i = 1;
            do
            {
                //当前项播放时间
                DateTime playTime;
                if (_PlayModel.PlayFileList.Count > 1)
                {
                    //当前项播放时间
                    playTime = DateTime.Parse(DateTime.Now.ToShortDateString() + " " + _PlayModel.PlayFileList[i].PlayTime);
                }
                else
                {
                    playTime = DateTime.Parse(DateTime.Now.ToShortDateString() + " " + _PlayModel.PlayFileList[0].PlayTime);
                }
                //当前时间
                DateTime dt = DateTime.Now;
                if (dt.CompareTo(playTime) >= 0)
                {
                    MoveVideo(_PlayModel.PlayFileList[0]);
                }
                else
                {
                    return;
                }

            } while (true);

        }

        /// <summary>
        /// 获取当前时间播放的媒体文件路径
        /// </summary>
        /// <returns></returns>
        public void getPlayFilePath()
        {
            DateTime NowDate = DateTime.Now;//当前时间
            if (_PlayModel.PlayFileList.Count > 0)
            {
                DateTime filePlayTime = DateTime.Parse(DateTime.Now.ToShortDateString() + " " + _PlayModel.PlayFileList[0].PlayTime);
                //判断当前时间是不是大于第一个文件的播放时间。
                if (NowDate.CompareTo(filePlayTime) > 0)
                {
                    //获取路径
                    string relativurl = loaclPath + _PlayModel.PlayFileList[0].Name;
                    string md5Value = _PlayModel.PlayFileList[0].MD5Value;
                    //更新播放时间
                    MoveVideo(_PlayModel.PlayFileList[0]);
                    //md5校验
                    if (!string.IsNullOrEmpty(md5Value))
                    {
                        string MediaMd5 = SeatManage.SeatManageComm.SeatComm.GetMD5HashFromFile(relativurl);
                        if (MediaMd5.Equals(md5Value))
                        {
                            //触发播放视频的事件
                            PlayVideo(this, relativurl);
                        }
                        else
                        {
                            PlayListHandleEvent(this, string.Format("文件{0} MD5校验失败！", relativurl));
                        }
                    }
                    else
                    {
                        PlayVideo(this, relativurl);
                    }

                }
            }
        }


        int playListTimeLength = 0;
        /// <summary>
        /// 把传递过来的项移动到最后一项
        /// </summary>
        /// <param name="videoItem"></param>
        private void MoveVideo(AMS.Model.AMS_VideoItem videoItem)
        {
            //计算当前项下次播放的时间
            DateTime dt = DateTime.Parse(DateTime.Now.ToShortDateString() + " " + videoItem.PlayTime).AddSeconds(playListTimeLength);
            //修改当前项播放的时间
            videoItem.PlayTime = dt.ToLongTimeString();
            //加到结尾
            _PlayModel.PlayFileList.Add(videoItem);
            //移除第一项
            _PlayModel.PlayFileList.RemoveAt(0);

        }
        /// <summary>
        /// 把图片文件读到内存中
        /// </summary>
        /// <param name="imgPath"></param>
        /// <returns></returns>
        public static BitmapImage InitImage(string imgPath)
        {
            if (File.Exists(imgPath))
            {
                using (BinaryReader reader = new BinaryReader(File.Open(imgPath, FileMode.Open)))
                {
                    BitmapImage bitmapImage;
                    FileInfo fi = new FileInfo(imgPath);
                    byte[] bytes = reader.ReadBytes((int)fi.Length);
                    reader.Close();
                    bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = new MemoryStream(bytes);
                    bitmapImage.EndInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    return bitmapImage;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
