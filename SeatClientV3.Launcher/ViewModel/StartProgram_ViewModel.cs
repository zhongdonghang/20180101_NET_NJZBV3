using System;
using SeatManage.EnumType;

namespace SeatClientV3.Launcher.ViewModel
{
    public class StartProgram_ViewModel
    {
        private SeatManageSubsystem _subsystemType;
        private string _drictortyPath;
        private string _StartProgramClient;
        private string _processName;
        /// <summary>
        /// 程序类型
        /// </summary>
        public SeatManageSubsystem SubsystemType
        {
            get { return _subsystemType; }
            set { _subsystemType = value; }
        }
        /// <summary>
        /// 文件夹路径
        /// </summary>
        public string DrictortyPath
        {
            get { return _drictortyPath; }
            set { _drictortyPath = value; }
        }
        /// <summary>
        /// 启动文件。
        /// </summary>
        public string StartProgramClient
        {
            get { return _StartProgramClient; }
            set { _StartProgramClient = value; }
        }
        /// <summary>
        /// 配置文件
        /// </summary>
        public string ProcessName
        {
            get { return _processName; }
            set { _processName = value; }
        }
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="configStr"></param>
        /// <returns></returns>
        public static StartProgram_ViewModel Parse(string configStr)
        {
            try
            {
                StartProgram_ViewModel viewModel = new StartProgram_ViewModel();
                string type = configStr.Split(':')[0];
                string path = configStr.Split(':')[1];
                viewModel.DrictortyPath = path.Split(',')[0].Substring(0, path.Split(',')[0].IndexOf("\\"));
                viewModel.SubsystemType = (SeatManageSubsystem) System.Enum.Parse(typeof (SeatManageSubsystem), type);
                viewModel.StartProgramClient = path.Split(',')[0].Substring(path.Split(',')[0].IndexOf("\\"));
                viewModel.ProcessName = path.Split(',')[1];
                return viewModel;
            }
            catch
            {
                return null;
            }
        }
    }
}
