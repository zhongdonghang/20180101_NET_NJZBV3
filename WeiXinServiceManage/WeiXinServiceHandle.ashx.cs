using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Xml;
using WeiXinJK;
using System.Configuration;
using SeatManage.IPocketBespeak;
using SeatManage.PocketBespeak;
using SeatManage.ClassModel;
using AMS.IBllService;
using WeiXinJK.Model;
using AMS.Model;
using AMS.ServiceProxy;
using SeatBespeakException;
using TcpClient_BespeakSeat;

namespace WeiXinServiceManage
{
    /// <summary>
    /// WeiXinServiceHandle 的摘要说明
    /// </summary>
    public class WeiXinServiceHandle : IHttpHandler
    {
        IWeiXinAdvertService ads = new WeiXinAdvertService();
        IWeixinService service = new WeiXinService();
        //IWeixinService  AdvertManage = null;
        /// <summary>
        /// 读者操作的相关功能。
        /// 暂离、释放座位、查看自己状态、查看阅览室座位使用状态
        /// </summary>
        //  IMainFunctionPageBll mainFunctionBll = null;
        IQueryLogs Loghandler = null;
        ILogin loginHandler = null;
        private string signature = "";//微信加密签名，signature结合了开发者填写的token参数和请求中的timestamp参数、nonce参数。 
        private string timestamp = "";//时间戳 
        private string nonce = "";//随机数 
        private string echostr = "";//随机字符串 
        private HttpContext context = null;
        private object lockObject = new object();//lock对象，用来执行互斥操作
        public void ProcessRequest(HttpContext context)
        {
            this.context = context;
            context.Response.ContentType = "text/plain";
            if (this.context.Request.HttpMethod == "POST")
            {
                //context.Session.Timeout = 60;
                //如果是POST请求，则响应请求内容
                ResponseMsg();
            }
            else if (this.context.Request.HttpMethod == "GET")//如果是Get请求，则是接入验证，返回随机字符串。
            {
                signature = context.Request.Params["signature"];
                timestamp = context.Request.Params["timestamp"];
                nonce = context.Request.Params["nonce"];
                echostr = context.Request.Params["echostr"];
                if (!service.CheckSignature(signature, timestamp, nonce))//验证请求是否微信发过来的。
                {//不是则结束响应
                    this.context.Response.End();
                }
                context.Response.Write(echostr);
            }
        }
        /// <summary>
        ///  获取post请求数据
        /// </summary>
        /// <returns></returns>
        private string PostInput()
        {
            Stream s = System.Web.HttpContext.Current.Request.InputStream;
            byte[] b = new byte[s.Length];
            s.Read(b, 0, (int)s.Length);
            return Encoding.UTF8.GetString(b);
        }
        private void ResponseMsg()// 服务器响应微信请求
        {
            service.LinkMenuClickEvent += new WeixinEventHandle(service_LinkMenuClickEvent);
            service.MenuClickEvent += new WeixinEventHandle(service_MenuClickEvent);
            service.ReceiveMsgEvent += new WeixinMsgHandle(service_ReceiveMsgEvent);
            service.SubscribeEvent += new WeixinEventHandle(service_SubscribeEvent);
            service.UnSubscribeEvent += new WeixinEventHandle(service_UnSubscribeEvent);
            string weixin = PostInput();
            service.MessageHandle(weixin);
        }
        //取消关注事件
        void service_UnSubscribeEvent(object sender, WeiXinMessageBase arge)
        {
            string WeixinTxt = WeiXinProxy.DeleteBind(arge.FromUserName);
        }

        // 关注事件
        void service_SubscribeEvent(object sender, WeiXinMessageBase arge)
        {
            string WeixinTxt = WeiXinProxy.GetResponse(EnumReplyMsgType.Subscribe);
            WeiXinTextMsg TexXml = new WeiXinTextMsg();
            TexXml.FromUserName = arge.ToUserName;
            TexXml.ToUserName = arge.FromUserName;
            TexXml.MsgType = EnumWeiXinMsgType.Text;
            TexXml.Content = WeixinTxt;
            context.Response.Write(TexXml.ToXML());
        }

        //接收消息
        void service_ReceiveMsgEvent(object sender, WeiXinMessageBase arge)
        {
            string WeixinTxt = "";
            switch (arge.MsgType)
            {
                case WeiXinJK.Model.EnumWeiXinMsgType.Text:
                    WeixinTransfer WXXml = new WeixinTransfer();
                    WXXml.ToUserName = arge.FromUserName;
                    WXXml.FromUserName = arge.ToUserName;
                    WXXml.MsgType = EnumWeiXinMsgType.transfer_customer_service;
                    context.Response.Write(WXXml.ToXML());
                    break;
                case WeiXinJK.Model.EnumWeiXinMsgType.Image:
                    WeixinTxt = WeiXinProxy.GetResponse(EnumReplyMsgType.ImgAutoReply);
                    break;
                case WeiXinJK.Model.EnumWeiXinMsgType.Voice:
                case WeiXinJK.Model.EnumWeiXinMsgType.Video:
                    WeixinTxt = WeiXinProxy.GetResponse(EnumReplyMsgType.VideoAutoReply);
                    break;
                case WeiXinJK.Model.EnumWeiXinMsgType.Location:
                    WeixinTxt = WeiXinProxy.GetResponse(EnumReplyMsgType.LocationAutoReply);
                    break;
                case WeiXinJK.Model.EnumWeiXinMsgType.Link:
                    WeixinTxt = WeiXinProxy.GetResponse(EnumReplyMsgType.LinkAutoReply);
                    break;
                default:
                    WeixinTxt = "哦！太博大精深了。但是我看不懂！！！";
                    break;
            }
            if (WeixinTxt != "")
            {
                WeiXinTextMsg TexXml = new WeiXinTextMsg();
                TexXml.ToUserName = arge.FromUserName;
                TexXml.FromUserName = arge.ToUserName;
                TexXml.MsgType = EnumWeiXinMsgType.Text;
                TexXml.Content = WeixinTxt;
                context.Response.Write(TexXml.ToXML());
            }
        }

        //菜单点击
        void service_MenuClickEvent(object sender, WeixinEventMsg arge)
        {
            WeiXinUsers user = WeiXinProxy.GetWeiXinUser(arge.FromUserName);//获取用户信息 
            WeiXinTextMsg responseTxt = new WeiXinTextMsg();//要回复的信息
            responseTxt.MsgType = EnumWeiXinMsgType.Text;
            responseTxt.FromUserName = arge.ToUserName;
            responseTxt.ToUserName = arge.FromUserName;
            string domain = ConfigurationManager.AppSettings["Domain"];
            string pocketdomain = ConfigurationManager.AppSettings["PocketDomain"];
            IMainFunctionPageBll mainFunctionBll = null;
            if (user == null)//连接地址未填写
            {
                context.Response.Flush();
                context.Response.Close();
                WeiXinAdvertse ad = new WeiXinAdvertse();
                ad.Title = "绑定学号";
                ad.Image = domain + "/Images/Bd.jpg";
                ad.Url = domain + "/BindUsers.aspx? WXID=" + arge.FromUserName;
                ad.Description = "请先绑定学号";
                //-------------------------------------------------
                ad.ContentXML = WeiXinJsonArticle.AdvertiseToJson(ad);
                //-------------------------------------------------
                List<WeiXinAdvertse> lw = new List<WeiXinAdvertse>();
                lw.Add(ad);
                ads.SendArticleMessage(arge.FromUserName, lw);
                return;
            }
            WeiXinJK.Model.WexinEventClickMenu click = (WeiXinJK.Model.WexinEventClickMenu)arge;
            string resultValue = "";
            switch (click.EventKey)
            {
                case EnumMenuKey.GetMyInfo:
                    break;
                case WeiXinJK.Model.EnumMenuKey.BindWeiXinId://修改绑定*******修改绑定连接未实现
                    #region 修改绑定
                    context.Response.Flush();
                    context.Response.Close();
                    WeiXinAdvertse ad = new WeiXinAdvertse();
                    ad.Title = "我的资料";
                    ad.Image = domain + "/Images/Bd.jpg";
                    ad.Url = domain + "/BindUsers.aspx?WXID=" + click.FromUserName;
                    ad.Description = "点击查看我的资料";
                    //---------------------------------------------------
                    ad.ContentXML = WeiXinJsonArticle.AdvertiseToJson(ad);
                    //---------------------------------------------------
                    List<WeiXinAdvertse> lw = new List<WeiXinAdvertse>();
                    lw.Add(ad);
                    ads.SendArticleMessage(arge.FromUserName, lw);
                    #endregion
                    break;
                case WeiXinJK.Model.EnumMenuKey.FreeSeat://释放座位 
                    #region 释放座位

                    lock (lockObject)
                    {
                        mainFunctionBll = new TcpClient_BespeakSeatAllMethod(user.SchoolInfo);
                        try
                        {
                            resultValue = mainFunctionBll.FreeSeat(user.SchoolInfo, new ReaderInfo() { CardNo = user.CardNo });
                        }
                        catch (Exception ex)
                        {
                            resultValue = ex.Message;
                        }
                        finally
                        {
                            (mainFunctionBll as TcpClient_BespeakSeatAllMethod).Dispose();
                        }
                    }
                    responseTxt.Content = resultValue;
                    context.Response.Write(responseTxt.ToXML());

                    #endregion
                    break;
                case WeiXinJK.Model.EnumMenuKey.GetBespeakLog://获取预约记录预留接口
                    #region 查看预约信息
                    List<BespeakLogInfo> li = new List<BespeakLogInfo>();
                    lock (lockObject)
                    {
                        try
                        {
                            Loghandler = new TcpClient_BespeakSeatAllMethod(user.SchoolInfo);

                            li = Loghandler.GetBookLogs(user.SchoolInfo, user.CardNo, null, 7);

                            (Loghandler as TcpClient_BespeakSeatAllMethod).Dispose();
                        }
                        catch (Exception ex)
                        {
                            responseTxt.Content = "执行出错误，请再次尝试";
                            context.Response.Write(responseTxt.ToXML());
                            SeatManage.SeatManageComm.WriteLog.Write(string.Format("获取预约信息失败：{0},异常来自：{1}", ex.Message, ex.Source));
                            return;
                        }
                    }
                    string content = "";
                    if (li.Count == 0)
                    {
                        content = "您暂无预约记录!";
                    }
                    else
                    {
                        content += "以下是您7天内的预约记录：" + Environment.NewLine;
                        //content += "地点".PadRight(5, ' ') + "座位号".PadRight(8, ' ') + "时间".PadRight(5, ' ') + Environment.NewLine;
                        foreach (BespeakLogInfo log in li)
                        {
                            content += "地点:" + Environment.NewLine;
                            content += log.ReadingRoomName + Environment.NewLine;
                            content += "座位号:" + log.ShortSeatNum + Environment.NewLine;
                            content += "时间：" + log.BsepeakTime.ToString("yy/MM/dd") + Environment.NewLine;
                        }
                    }
                    responseTxt.Content = content;
                    context.Response.Write(responseTxt.ToXML());
                    #endregion
                    break;
                case WeiXinJK.Model.EnumMenuKey.GetRoomUsedState://获取阅览室剩余座位
                    #region 获取阅览室剩余座位
                    context.Response.Flush();
                    context.Response.Close();

                    Dictionary<string, ReadingRoomSeatUsedState_Ex> dic = new Dictionary<string, ReadingRoomSeatUsedState_Ex>();
                    lock (lockObject)
                    {
                        mainFunctionBll = new TcpClient_BespeakSeatAllMethod(user.SchoolInfo);
                        try
                        {
                            dic = mainFunctionBll.GetAllRoomSeatUsedState(user.SchoolInfo);
                        }
                        catch (Exception ex)
                        {
                            ads.SendTxtMessage(responseTxt.ToUserName, "执行出错误，请再次尝试。");
                            SeatManage.SeatManageComm.WriteLog.Write(string.Format("获取可用座位数失败：{0},异常来自：{1}", ex.Message, ex.Source));
                        }
                        finally
                        {
                            (mainFunctionBll as TcpClient_BespeakSeatAllMethod).Dispose();
                        }
                    }
                    StringBuilder strtxt = new StringBuilder();
                    foreach (ReadingRoomSeatUsedState_Ex seatUsedState in dic.Values)
                    {
                        strtxt.Append(string.Format("{0}:{1}/{2}(已用/总共)", seatUsedState.ReadingRoom.Name, seatUsedState.SeatAmountUsed.ToString(), seatUsedState.SeatAmountAll.ToString()) + Environment.NewLine);
                    }
                    ads.SendTxtMessage(responseTxt.ToUserName, strtxt.ToString());

                    #endregion
                    break;
                case WeiXinJK.Model.EnumMenuKey.ShortLeave://设置暂离
                    #region 设置暂离
                    lock (lockObject)
                    {
                        mainFunctionBll = new TcpClient_BespeakSeatAllMethod(user.SchoolInfo);
                        try
                        {
                            resultValue = mainFunctionBll.SetShortLeave(user.SchoolInfo, new ReaderInfo() { CardNo = user.CardNo });
                            responseTxt.Content = resultValue;
                        }
                        catch (Exception ex)
                        {
                            responseTxt.Content = ex.Message;
                        }
                        finally
                        {
                            (mainFunctionBll as TcpClient_BespeakSeatAllMethod).Dispose();
                        }
                    }

                    context.Response.Write(responseTxt.ToXML());
                    #endregion
                    break;
                case WeiXinJK.Model.EnumMenuKey.ReserveSeat:
                    #region 预约座位

                    #endregion
                    break;
                case EnumMenuKey.BlackList:
                    #region 黑名单

                    #endregion
                    break;
                case EnumMenuKey.GetReaderState:
                    #region 读者状态
                    ReaderInfo readerIn = new ReaderInfo();
                    lock (lockObject)
                    {
                        try
                        {
                            mainFunctionBll = new TcpClient_BespeakSeatAllMethod(user.SchoolInfo);
                            readerIn = mainFunctionBll.GetReaderInfo(user.SchoolInfo, user.CardNo);
                        }
                        catch
                        {
                            responseTxt.Content = "执行出错误，请再次尝试";
                            context.Response.Write(responseTxt.ToXML());
                        }
                        finally
                        {
                            (mainFunctionBll as TcpClient_BespeakSeatAllMethod).Dispose();
                        }
                    }
                    string state = "";
                    if (readerIn.EnterOutLog == null)
                    {
                        state = "Leave";
                    }
                    else
                    {
                        state = readerIn.EnterOutLog.EnterOutState.ToString();
                    }

                    if (readerIn.BespeakLog.Count > 0)
                    {
                        state = "Booking";
                    }
                    string message = "";
                    switch (state)
                    {
                        case "SelectSeat":
                        case "ComeBack":
                        case "ContinuedTime":
                        case "WaitingSuccess":
                        case "BookingConfirmation":
                        case "ReselectSeat": message = "当前状态：在座"; break;
                        case "Leave": message = ""; break;
                        case "Booking": message = "今天有预约未确认"; break;
                        case "Waiting": message = "您正在等待座位"; break;
                        case "ShortLeave": message = "当前状态：暂离"; break;
                        default: message = "";
                            SeatManage.SeatManageComm.WriteLog.Write(string.Format("获取到读者信息，读者状态为：", state));
                            break;
                    }
                    if (readerIn.EnterOutLog != null && readerIn.EnterOutLog.EnterOutState != SeatManage.EnumType.EnterOutLogType.Leave)
                    {
                        string nowMessage = "";
                        //座位号 readerIn.EnterOutLog.ShortSeatNo;
                        if (message != "")
                        {
                            string seatNo = readerIn.EnterOutLog.ShortSeatNo;
                            nowMessage += string.Format("阅览室：{0}\n座位号：{1}\n学号：{2}\n{3}", readerIn.AtReadingRoom.Name, seatNo, readerIn.CardNo, message);
                            responseTxt.Content = nowMessage;
                            context.Response.Write(responseTxt.ToXML());
                        }
                    }
                    else
                    {
                        responseTxt.Content = "您当前还没有座位！";
                        context.Response.Write(responseTxt.ToXML());
                    }
                    #endregion
                    break;
                case EnumMenuKey.GetRules:
                    #region 查看规则
                    #endregion
                    break;
                case EnumMenuKey.ReservationService:
                    #region 预约多图文
                    context.Response.Flush();
                    context.Response.Close();
                    List<WeiXinAdvertse> ListWeiXin = new List<WeiXinAdvertse>();
                    WeiXinAdvertse adv = new WeiXinAdvertse();
                    adv.Title = "更多资讯";//标题
                    adv.Image = domain + "/Images/ysgg.jpg";//图片地址
                    adv.Url = "http://shanghai.longre.com/";//访问地址
                    //-------------------------------------------------
                    adv.ContentXML = WeiXinJsonArticle.AdvertiseToJson(adv);
                    ListWeiXin.Add(adv);
                    //---------------------------------------------------

                    adv = new WeiXinAdvertse();
                    adv.Title = "预约座位";
                    adv.Image = domain + "/Images/yyzw.jpg";
                    string para = SeatManage.SeatManageComm.AESAlgorithm.DESEncode(string.Format("cardNo={0}&schoolId={1}&operateKey={2}", user.CardNo, user.SchoolInfo.Id, Convert.ToInt32(EnumMenuKey.ReserveSeat)));
                    para = HttpUtility.UrlEncode(para);
                    adv.Url = pocketdomain + "/AutoLogin.aspx?parameters=" + para;
                    adv.ContentXML = WeiXinJsonArticle.AdvertiseToJson(adv);
                    ListWeiXin.Add(adv);
                    //------------------------------------------------------
                    adv = new WeiXinAdvertse();
                    adv.Title = "预约记录";
                    adv.Image = domain + "/Images/yyjl.jpg";
                    string par = SeatManage.SeatManageComm.AESAlgorithm.DESEncode(string.Format("cardNo={0}&schoolId={1}&operateKey={2}", user.CardNo, user.SchoolInfo.Id, Convert.ToInt32(EnumMenuKey.GetBespeakLog)));
                    par = HttpUtility.UrlEncode(par);
                    adv.Url = pocketdomain + "/AutoLogin.aspx?parameters=" + par;
                    adv.ContentXML = WeiXinJsonArticle.AdvertiseToJson(adv);
                    ListWeiXin.Add(adv);
                    ads.SendArticleMessage(arge.FromUserName, ListWeiXin);
                    break;
                    #endregion
                case EnumMenuKey.Weather:
                    context.Response.Flush();
                    context.Response.Close();
                    WeiXinAdvertse Weathers = new WeiXinAdvertse();
                    Weathers.Title = "回复城市+天气名可获取输入城市的实况天气\t如:南京天气";
                    Weathers.Image = domain + "/Images/Bd.jpg";
                    Weathers.Url = "";
                    //-------------------------------------------------
                    Weathers.ContentXML = WeiXinJsonArticle.AdvertiseToJson(Weathers);
                    //-------------------------------------------------
                    List<WeiXinAdvertse> ListWeather = new List<WeiXinAdvertse>();
                    ListWeather.Add(Weathers);
                    ads.SendArticleMessage(arge.FromUserName, ListWeather);
                    break;
                case EnumMenuKey.Service:
                    WeixinTransfer WXXml = new WeixinTransfer();
                    WXXml.ToUserName = arge.FromUserName;
                    WXXml.FromUserName = arge.ToUserName;
                    WXXml.MsgType = EnumWeiXinMsgType.transfer_customer_service;
                    context.Response.Write(WXXml.ToXML());
                    break;
                case EnumMenuKey.Press:
                    context.Response.Flush();
                    context.Response.Close();
                    List<WeiXinAdvertse> ListPress = new List<WeiXinAdvertse>();
                    WeiXinAdvertse press = new WeiXinAdvertse();
                    press.Title = "0";//标题
                    press.Image = domain + "";//图片地址
                    press.Url = "";//访问地址
                    //-------------------------------------------------
                    press.ContentXML = WeiXinJsonArticle.AdvertiseToJson(press);
                    ListPress.Add(press);
                    //---------------------------------------------------

                    press = new WeiXinAdvertse();
                    press.Title = "1";
                    press.Image = domain + "";
                    press.Url = "";
                    press.ContentXML = WeiXinJsonArticle.AdvertiseToJson(press);
                    ListPress.Add(press);
                    //------------------------------------------------------
                    press = new WeiXinAdvertse();
                    press.Title = "2";
                    press.Image = domain + "";
                    press.Url = "";
                    press.ContentXML = WeiXinJsonArticle.AdvertiseToJson(press);
                    ListPress.Add(press);
                    //------------------------------------------------------
                    press = new WeiXinAdvertse();
                    press.Title = "3";
                    press.Image = domain + "";
                    press.Url = "";
                    press.ContentXML = WeiXinJsonArticle.AdvertiseToJson(press);
                    ListPress.Add(press);
                    //------------------------------------------------------
                    press = new WeiXinAdvertse();
                    press.Title = "4";
                    press.Image = domain + "";
                    press.Url = "";
                    press.ContentXML = WeiXinJsonArticle.AdvertiseToJson(press);
                    ListPress.Add(press);
                    //------------------------------------------------------
                    press = new WeiXinAdvertse();
                    press.Title = "4";
                    press.Image = domain + "";
                    press.Url = "";
                    press.ContentXML = WeiXinJsonArticle.AdvertiseToJson(press);
                    ListPress.Add(press);
                    //------------------------------------------------------ 
                    ads.SendArticleMessage(arge.FromUserName, ListPress);
                    break;
            }
        }

        //菜单连接
        void service_LinkMenuClickEvent(object sender, WeixinEventMsg arge)
        {
            //throw new NotImplementedException();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}