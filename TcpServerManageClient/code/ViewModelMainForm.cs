using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using AMS.TcpTransfersServer;
using System.Threading;
namespace TcpServerManageClient
{
    public class ViewModelMainForm : INotifyPropertyChanged
    {
        private ObservableCollection<ViewModelSchool> _SchoolList = new ObservableCollection<ViewModelSchool>();
        private ObservableCollection<string> _Logs = new ObservableCollection<string>();

        public ObservableCollection<string> Logs
        {
            get { return _Logs; }
            set
            {
                _Logs = value;
                OnPropertyChanged("Logs");
            }
        }
        private bool _ServerIsRuning = false;
        private int _MaxConcurrence;
        private int _Concurrence;
        /// <summary>
        /// 学校列表
        /// </summary>
        public ObservableCollection<ViewModelSchool> SchoolList
        {
            get { return _SchoolList; }
            set
            {
                _SchoolList = value;
                OnPropertyChanged("SchoolList");
            }
        }
        /// <summary>
        /// 服务是否运行
        /// </summary>
        public bool ServerIsRuning
        {
            get { return _ServerIsRuning; }
            set
            {
                _ServerIsRuning = value;
                OnPropertyChanged("ServerIsRuning");
            }
        }
        /// <summary>
        /// 最大连接数
        /// </summary>
        public int MaxConcurrence
        {
            get { return _MaxConcurrence; }
            set
            {
                _MaxConcurrence = value;
                OnPropertyChanged("MaxConcurrence");
            }
        }
        /// <summary>
        /// 最大并发数
        /// </summary>
        public int Concurrence
        {
            get { return _Concurrence; }
            set
            {
                _Concurrence = value;
                OnPropertyChanged("Concurrence");
            }
        }

        AMS.TcpTransfersServer.TcpTransfersServer server = new AMS.TcpTransfersServer.TcpTransfersServer();

        public AMS.TcpTransfersServer.TcpTransfersServer Server
        {
            get { return server; }
            set { server = value; }
        }

        public void Init()
        {
            server.OnReceivedMsg += new AMS.TcpTransfersServer.SocketMsgHandler(server_OnReceivedMsg);
            server.OnSchoolConnectionHandler += new AMS.TcpTransfersServer.SocketConnectionHandler(server_OnSchoolConnectionHandler);
            server.OnSchoolDisConnectionHandler += new AMS.TcpTransfersServer.SocketConnectionHandler(server_OnSchoolDisConnectionHandler);
            server.OnClientConnectioned += new EventHandler(server_OnClientConnectioned);
            GetSchoolList();
        }

        private void GetSchoolList()
        {
            try
            {
                List<AMS.Model.AMS_School> schoolModel = AMS.ServiceProxy.AMS_SchoolProxy.GetAllSchool();
                foreach (AMS.Model.AMS_School model in schoolModel)
                {
                    ViewModelSchool vm = new ViewModelSchool() { IsOnline = false, SchoolName = model.Name, SchoolNum = model.Number };
                    this.SchoolList.Add(vm);
                }
            }
            catch (Exception ex)
            {

            }
        }

        void server_OnClientConnectioned(object sender, EventArgs e)
        {
            AMS.TcpTransfersServer.TcpTransfersServer s = sender as AMS.TcpTransfersServer.TcpTransfersServer;
            Concurrence = s.Concurrence;
            MaxConcurrence = s.MaxConcurrence;
        }
        /// <summary>
        /// 学校断开连接
        /// </summary>
        /// <param name="schoolNum"></param>
        void server_OnSchoolDisConnectionHandler(string schoolNum, string ip)
        {
            foreach (ViewModelSchool school in SchoolList)
            {
                if (school.SchoolNum == schoolNum)
                {
                    school.IsOnline = false;
                    writeLog(string.Format("学校{0}断开连接", schoolNum));
                }
            }
        }
        /// <summary>
        /// 连接学校
        /// </summary>
        /// <param name="schoolNum"></param>
        void server_OnSchoolConnectionHandler(string schoolNum, string ip)
        {
            foreach (ViewModelSchool school in SchoolList)
            {
                if (school.SchoolNum == schoolNum)
                {
                    school.IsOnline = true;
                    school.Ip = ip;
                    writeLog(string.Format("学校{0}已连接", schoolNum));
                }
            }
        }
        /// <summary>
        /// 接收到请求消息
        /// </summary>
        /// <param name="msg"></param>
        void server_OnReceivedMsg(SocketMsgData.SocketMsgBase msg)
        {
            switch (msg.MsgType)
            {
                case SocketMsgData.TcpMsgDataType.Relay:
                    if (msg is SocketMsgData.SocketRequest)
                    {
                        string m = string.Format("客户端{0}向学校{1}发送{2}请求", msg.Sender, msg.Target, msg.MethodName);
                        writeLog(m);
                    }
                    else if (msg is SocketMsgData.SocketResponse)
                    {
                        string m = string.Format("学校{0}响应{1}客户端{2}的请求", msg.Sender, msg.Target, msg.MethodName);
                        writeLog(m);
                    }
                    break;
                case SocketMsgData.TcpMsgDataType.WeiXinNotice:
                    writeLog(string.Format("来自{0}的微信通知", msg.Sender));
                    break;


            }
        }
        public void Start()
        {
            server.Start();
            ServerIsRuning = true;
            writeLog("服务已启动");
        }

        private void writeLog(string msg)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
            {

                Logs.Add(string.Format("{0} {1}", DateTime.Now.ToLongTimeString(), msg));
            }));
            //ThreadPool.QueueUserWorkItem(delegate
            //{
            //    System.Threading.SynchronizationContext.SetSynchronizationContext(new System.Windows.Threading.DispatcherSynchronizationContext(System.Windows.Application.Current.Dispatcher));
            //});

            
            if (Logs.Count > 3000)
            {
                Logs.Clear();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
