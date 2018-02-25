using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.ClassModel
{
    [Serializable]
    public class ServerIp
    {
        string _IP;

        public string IP
        {
            get { return _IP; }
        }
        int _Port;

        public int Port
        {
            get { return _Port; }
        }
        public ServerIp(string address)
        {
            if (!string.IsNullOrEmpty(address))
            {
                string[] addressInfo = address.Split(':');
                _IP = addressInfo[0];
                int.TryParse(addressInfo[1], out _Port);
            }
        }

    } 
}
