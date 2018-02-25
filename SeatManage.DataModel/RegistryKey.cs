using System.Collections.Generic;

namespace SeatManage.DataModel
{
    public class RegistryKey
    {
        public RegistryKey()
        {
            _RegistryList.Add("JuneberryReadingRoomInterfaceKey", "");
            _RegistryList.Add("JuneberryAccessInterfaceKey", "");
            _RegistryList.Add("JuneberryMediaReleaseKey", "");
        }

        private Dictionary<string, string> _RegistryList = new Dictionary<string, string>();
        /// <summary>
        /// 注册表内容
        /// </summary>
        public Dictionary<string, string> RegistryList
        {
            get { return _RegistryList; }
            set { _RegistryList = value; }
        }
    }
}
