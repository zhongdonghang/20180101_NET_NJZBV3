using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Timers;
using System.IO;
using System.Configuration;
using System.Drawing;
using System.Windows.Media.Imaging;
using SeatManage.ClassModel;

namespace AMS.MediaPlayer.Code
{
    //public delegate void PlayVideoEventHandler(object sender, string message);
    /// <summary>
    /// 
    /// </summary>
    public class GetPlaylist
    {
        #region 事件
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

        public GetPlaylist()
        {
        }

        #region 成员变量
        /// <summary>
        /// 定时器，定时扫描播放列表
        /// </summary>
        private Timer timer = new Timer(300);

        /// <summary>
        /// 定时器，计重试时长；
        /// </summary>
        private Timer timer1;
        /// <summary>
        /// 当前要播放的视频文件地址
        /// </summary>
        private string _videoPath = "";
        /// <summary>
        /// 文件播放列表
        /// </summary>
        private List<PlaylistItemInfo> plists = new List<PlaylistItemInfo>();
        /// <summary>
        /// 错误日志文件夹路径
        /// </summary>
        private DirectoryInfo errorDir;
        #endregion

        /// <summary>
        /// 获取当前应该播放的媒体信息
        /// </summary>
        /// <param name="isOffline">是否脱机运行</param>
        public void Run()
        {
            if (PlayerSetting.IsOffline == "1")
            {

                //从服务器上获取新的播放列表以及媒体文件并且保存到本地
                // get.Run();
                SeatManage.ClassModel.PlaylistInfo playlistModel = new PlaylistInfo();
                //从服务器上获取新的播放列表
                timer1 = new Timer(1000);
                timer1.Elapsed += new ElapsedEventHandler(timer1_Elapsed);
                timer1.Start();
                try
                {
                    if (DownloadPlaylist(ref playlistModel))
                    {
                        //把新获取的播放列表写入文件
                        if (WritePlayListToFile(playlistModel))
                        {
                            SeatManage.SeatManageComm.WriteLog.Write("播放列表文件写入成功，准备下载……");
                            if (string.IsNullOrEmpty(PlayerSetting.DeviceNo))
                            {
                                SeatManage.SeatManageComm.WriteLog.Write("终端编号为空");
                            }
                            else
                            {
                                TerminalInfoV2 terminal = SeatManage.Bll.TerminalOperatorService.GetTeminalSetting(PlayerSetting.DeviceNo);
                                if (terminal != null)
                                {
                                    terminal.IsUpdatePlayList = false;
                                    SeatManage.Bll.TerminalOperatorService.UpdateTeminalSetting(terminal);
                                }
                            }
                            //删除无用的文件
                            DeleteNullFile();
                            DownloadFile(playlistModel.ImageFilePath);

                        }
                        else
                        {
                            SeatManage.SeatManageComm.WriteLog.Write("播放列表写入失败");
                        }
                    }
                    else
                    {
                        //TODO：没有新的播放列表
                    }
                }
                catch (Exception ex)
                {
                    SeatManage.SeatManageComm.WriteLog.Write("网络未连接" + ex.Message);
                }
                finally
                {
                    timer1.Stop();
                }
            }
            //载入本地的播放列表
            if (LoadPlayList())
            {
                timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
                //播放列表初始化结束，触发事件
                PlayListHandleOver(null, null);
                timer.Start();
            }
        }
        int s = 0;
        void timer1_Elapsed(object sender, ElapsedEventArgs e)
        {
            s += 1;
        }
        /// <summary>
        /// 获取播放列表
        /// </summary>
        private bool DownloadPlaylist(ref PlaylistInfo model)
        {
            while (true)
            {
                //获取今天的播放列表
                model = null;

                try
                {
                    List<AMS_Advertisement> advert = SeatManage.Bll.AdvertisementOperation.GetAdList(false, SeatManage.EnumType.AdType.PlaylistAd);
                    if (advert.Count < 1)
                    {
                        PlayListHandleEvent(this, "没有有效的播放列表");
                        return false;
                    }
                    List<PlaylistInfo> pllist = new List<PlaylistInfo>();
                    foreach (var pl in advert)
                    {
                        PlaylistInfo plmodel = PlaylistInfo.ToModel(pl.AdContent);
                        plmodel.ID = advert[0].ID;
                        pllist.Add(plmodel);
                    }
                    model = pllist[0];
                    for (int i = 1; i < pllist.Count; i++)
                    {
                        foreach (string filepath in pllist[i].ImageFilePath)
                        {
                            if (model.ImageFilePath.FindAll(u => u == filepath).Count > 0)
                            {
                                continue;
                            }
                            model.ImageFilePath.Add(filepath);
                        }
                        foreach (var media in pllist[i].MediaPlayList)
                        {
                            model.MediaPlayList.Add(media);
                        }
                    }
                    timer1.Stop();
                    timer1.Dispose();
                    return true;
                }
                catch (Exception ex)
                {
                    PlayListHandleEvent(this, "服务器连接失败，正在重试……");
                    System.Threading.Thread.Sleep(2000);
                    if (s > 300)
                    {
                        timer1.Stop();
                        timer1.Dispose();
                        return false;
                    }

                }

                //if (model != null)
                //{
                //    timer1.Stop();
                //    timer1.Dispose();
                //    return true;
                //}
                //else
                //{
                //    SeatManage.SeatManageComm.WriteLog.Write(ex.Message);
                //    return false;
                //}
            }
        }

        /// <summary>
        /// 更新播放列表，并写入文件
        /// </summary>
        /// <param name="model">播放列表</param>
        /// <returns>成功写入返回true，否则返回false</returns>
        private bool WritePlayListToFile(PlaylistInfo model)
        {

            XmlDocument xmlDoc = new XmlDocument();
            //获取应用程序所在文件夹
            string FilePath = PlayerSetting.DefaultVideosPath + "PlayList.xml";

            try
            {

                if (model == null)
                {
                    return false;
                }
                xmlDoc.LoadXml(model.ToXml());
                DirectoryInfo d = new DirectoryInfo(PlayerSetting.DefaultVideosPath);
                if (!d.Exists)
                {
                    d.Create();
                }
                xmlDoc.Save(FilePath);
                return true;
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// 解析播放列表并下载视频文件
        /// </summary>
        /// <param name="model">下载的文件地址</param>
        private List<string> GetDownloadVideoFile(AMS_PlayListMd5 model)
        {
            //存放待下载的媒体文件
            List<string> downloadFiles = new List<string>();

            //播放列表存放的文件夹
            DirectoryInfo direct = new DirectoryInfo(PlayerSetting.DefaultVideosPath);
            if (!direct.Exists)
            {
                direct.Create();
            }
            //下载所有的媒体文件。
            foreach (AMS_VideoMd5Item vm in model.VideoFiles)
            {
                downloadFiles.Add(vm.RelativeUrl);
            }
            return downloadFiles;
        }

        /// <summary>
        /// 下载视频文件
        /// </summary>
        /// <param name="vm">视频文件相对路径</param>
        public void DownloadFile(List<string> videoFilePaths)
        {
            SeatManage.Bll.FileOperate fi = new SeatManage.Bll.FileOperate();
            fi.DownloadError += new SeatManage.Bll.EventHandleFileOperateError(fi_DownloadError);
            //执行下载操作
            for (int i = 0; i < videoFilePaths.Count; i++)
            {

                string path = videoFilePaths[i];
                string fullPath = string.Format("{0}{1}", PlayerSetting.DefaultVideosPath, path);
                try
                {
                    PlayListHandleEvent(this, string.Format("正在下载{0}", path));
                    fi.FileDownLoad(fullPath, path, SeatManage.EnumType.SeatManageSubsystem.PlaylistAd);
                }
                catch (Exception ex)
                {
                    SeatManage.SeatManageComm.WriteLog.Write(ex.Message);
                }

            }


        }

        void fi_DownloadError(string message)
        {
            if (PlayListHandleEvent != null)
            {
                PlayListHandleEvent(this, message);
            }
        }

        /// <summary>
        /// 删除没用的视频文件
        /// </summary>
        private bool DeleteNullFile()
        {
            try
            {
                DirectoryInfo direct = new DirectoryInfo(PlayerSetting.DefaultVideosPath);
                string[] dvs = PlayerSetting.DefaultVideo.Split(';');
                //string dv = PlayerSetting.DefaultVideo.Substring(PlayerSetting.DefaultVideo.LastIndexOf("\\") + 1);
                bool flag = false;
                //遍历文件夹中的文件
                foreach (FileInfo NextFile in direct.GetFiles())
                {
                    flag = false;
                    //名字不等于播放列表，或者名字不等于默认视频文件
                    if (NextFile.Name == "PlayList.xml" || dvs.Contains(NextFile.Name))
                    {
                        //标记为不删除
                        flag = true;
                    }
                    if (!flag)
                    {
                        PlayListHandleEvent(this, string.Format("删除无用的视频文件{0}", NextFile.Name));
                        File.Delete(NextFile.FullName);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(ex.Message);
                return false;
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
                string xmlDocPath = PlayerSetting.DefaultVideosPath + "PlayList.xml";
                if (System.IO.File.Exists(xmlDocPath))
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(xmlDocPath);
                    PlaylistInfo plm = PlaylistInfo.ToModel(xmlDoc.OuterXml);
                    //播放列表赋值
                    plists = plm.MediaPlayList;
                    //计算整个播放列表播放时间的间隔加上循环间隔时长
                    playListTimeLength = 0;
                    //foreach (PlaylistItemInfo item in plists)
                    //{

                    //    playListTimeLength += item.PlayTime;
                    //}
                    for (int i = 0; i < plists.Count; i++)
                    {
                        string relativurl = plists[i].MediaFileName;
                        string path = PlayerSetting.DefaultVideosPath + relativurl;
                        string md5Value = plists[i].MD5Key;
                        if (!string.IsNullOrEmpty(md5Value))
                        {
                            string MediaMd5 = SeatManage.SeatManageComm.SeatComm.GetMD5HashFromFile(path);
                            if (MediaMd5.Equals(md5Value))
                            {
                                playListTimeLength += plists[i].PlayTime;
                            }
                            else
                            {
                                PlayListHandleEvent(this, string.Format("文件{0} MD5校验失败！", relativurl));
                                plists.RemoveAt(i);
                                i--;
                            }
                        }

                    }
                    //TODO:根据当前时间重新排列播放列表
                    RearrangePlayList();
                    return true;
                }
                else if (!string.IsNullOrEmpty((PlayerSetting.DefaultVideo)))
                {
                    string[] fileNames = PlayerSetting.DefaultVideo.Split(';');
                    plists = new List<PlaylistItemInfo>();
                    foreach (string name in fileNames)
                    {
                        if (string.IsNullOrEmpty(name))
                        {
                            continue;
                        }
                        PlaylistItemInfo item = new PlaylistItemInfo();
                        item.MD5Key = SeatManage.SeatManageComm.SeatComm.GetMD5HashFromFile(string.Format("{0}{1}", PlayerSetting.DefaultVideosPath, name));
                        item.MediaFileName = name;
                        item.PlayTime = 30;
                        plists.Add(item);
                        playListTimeLength += 30;
                    }
                    if (plists.Count < 1)
                    {
                        SeatManage.SeatManageComm.WriteLog.Write("播放列表不存在");
                        PlayListHandleEvent(this, "本地播放列表不存在！");
                        return false;
                    }
                    RearrangePlayList();
                    return true;

                }
                else
                {
                    SeatManage.SeatManageComm.WriteLog.Write("播放列表不存在");
                    PlayListHandleEvent(this, "本地播放列表不存在！");
                    return false;
                }
            }
            catch
            {
                SeatManage.SeatManageComm.WriteLog.Write("载入播放列表失败");
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
            if (plists.Count == 1)
            {
                PlaylistItemInfo item = new PlaylistItemInfo();
                item.MD5Key = plists[0].MD5Key;
                item.MediaFileName = plists[0].MediaFileName;
                item.PlayTime = plists[0].PlayTime;
            }
            plists[0].StartTime = DateTime.Now;
            for (int i = 1; i < plists.Count; i++)
            {
                plists[i].StartTime = plists[i - 1].StartTime.AddSeconds(plists[i - 1].PlayTime);
            }
        }

        /// <summary>
        /// 获取当前时间播放的媒体文件路径
        /// </summary>
        /// <returns></returns>
        public void getPlayFilePath()
        {
            if (plists.Count > 0)
            {
                //判断当前时间是不是大于第一个文件的播放时间。
                if (DateTime.Now.CompareTo(plists[0].StartTime) > 0)
                {
                    //获取路径
                    string relativurl = plists[0].MediaFileName;
                    string path = PlayerSetting.DefaultVideosPath + relativurl;
                    //string md5Value = plists[0].MD5Key;
                    //更新播放时间
                    MoveVideo(plists[0]);
                    //md5校验
                    //if (!string.IsNullOrEmpty(md5Value))
                    //{
                    //    string MediaMd5 = SeatManage.SeatManageComm.SeatComm.GetMD5HashFromFile(path);
                    //    if (MediaMd5.Equals(md5Value))
                    //    {
                    //        //触发播放视频的事件
                    //        PlayVideo(this, path);
                    //    }
                    //    else
                    //    {
                    //        PlayListHandleEvent(this, string.Format("文件{0} MD5校验失败！", relativurl));
                    //    }
                    //}
                    //else
                    //{
                    PlayVideo(this, path);
                    //}

                }
            }
        }


        int playListTimeLength = 0;
        /// <summary>
        /// 把传递过来的项移动到最后一项
        /// </summary>
        /// <param name="videoItem"></param>
        private void MoveVideo(PlaylistItemInfo videoItem)
        {
            //修改当前项播放的时间
            videoItem.StartTime = videoItem.StartTime.AddSeconds(playListTimeLength);
            //加到结尾
            plists.Add(videoItem);
            //移除第一项
            plists.RemoveAt(0);

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
