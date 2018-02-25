using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AMS.ViewModel;
using System.IO;

namespace AdvertManageClient.FunPage.MediaEdit
{
    /// <summary>
    /// MediaPlayerTest.xaml 的交互逻辑
    /// </summary>
    public partial class MediaPlayerTest : Window
    {
        public MediaPlayerTest()
        {
            InitializeComponent();
            this.Left = 0;
            this.Top = 0;
            this.Width = 1080;
            this.Height = 960;
            vm_playlist.PlayVideo += new AMS.ViewModel.ViewModelMediaPlayerTest.PlayVideoEventHandler(get_PlayVideo);//通知播放时间
            vm_playlist.PlayListHandleEvent += (s, arges) => get_ShowMessage(s, arges);
            vm_playlist.PlayListHandleOver += (s, arges) => get_HandleOver(s, arges);
            this.DataContext = vm_playlist;
        }
        public void Start()
        {
            vm_playlist.StartPlay();
            mediaPlayHost.Height = double.Parse(vm_playlist.VideoImageHeight);
            myMediaPlayer.Height = int.Parse(vm_playlist.VideoImageHeight);
            imgCanv.Height = double.Parse(vm_playlist.VideoImageHeight);
        }
        public ViewModelMediaPlayerTest vm_playlist = new ViewModelMediaPlayerTest();
        System.Threading.Thread playListThread;
        private void get_HandleOver(object s, EventArgs arges)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                vm_playlist.ErrorMessage = "";
            }));
        }
        void get_PlayVideo(object sender, string e)
        {
            try
            {
                this.Dispatcher.Invoke(new Action(() => { mediaPlay(e); }));
                // this.txtWarning.Dispatcher.Invoke(new Action(() => { this.txtWarning.Visibility = System.Windows.Visibility.Collapsed; }));
            }
            catch (Exception ex)
            {
                vm_playlist.ErrorMessage=ex.Message;
            }
        }
        private void get_ShowMessage(object s, string arges)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                try
                {
                    vm_playlist.ErrorMessage = arges;
                }
                catch (Exception ex)
                {
                    vm_playlist.ErrorMessage=ex.Message;
                }
            }));
        }
        /// <summary>
        /// 播放文件
        /// </summary>
        /// <param name="e"></param>
        private void mediaPlay(string e)
        {

            GC.Collect();
            try
            {
                string fileType = e.Substring(e.LastIndexOf(".") + 1);
                string filename = e.Substring(e.LastIndexOf("\\") + 1);
                switch (fileType)
                {
                    case "JPG":
                    case "PNG":
                    case "BMP":
                    case "GIF":
                    case "jpg":
                    case "png":
                    case "bmp":
                    case "gif":
                        if (File.Exists(e))
                        {
                            myMediaPlayer.Player.Ctlcontrols.stop();
                            mediaPlayHost.Visibility = System.Windows.Visibility.Collapsed;
                            imgCanv.Visibility = System.Windows.Visibility.Visible;
                            BitmapImage img = ViewModelMediaPlayerTest.InitImage(e);
                            if (img != null)
                            {
                                imgCanv.Source = img;
                                img.Freeze();
                                vm_playlist.ErrorMessage = string.Format("当前播放：{0}", filename);
                            }
                            else
                            {
                                vm_playlist.ErrorMessage = "默认播放的文件不存在！";
                            }

                        }
                        else
                        {
                            vm_playlist.ErrorMessage = string.Format("文件{0}不存在", filename);
                        }

                        break;
                    default:
                        if (File.Exists(e))
                        {
                            imgCanv.Visibility = System.Windows.Visibility.Collapsed;
                            mediaPlayHost.Visibility = System.Windows.Visibility.Visible;
                            myMediaPlayer.Player.URL = e;
                            myMediaPlayer.Player.Ctlcontrols.play();
                            vm_playlist.ErrorMessage = string.Format("当前播放：{0}", filename);
                        }
                        else
                        {
                            vm_playlist.ErrorMessage = string.Format("文件{0}不存在", filename);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(ex.Message.TrimEnd('\0'));
            }
        }
        private void txtWarning_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            vm_playlist.Timer.Stop();
            vm_playlist.Timer.Dispose();
            myMediaPlayer.Dispose();
        }
        
    }
}
