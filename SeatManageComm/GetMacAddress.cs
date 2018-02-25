using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;

namespace SeatManage.SeatManageComm
{
    public class GetMacAddress
    {
        /// <summary>
        /// 获取本地网卡Mac地址
        /// </summary>
        /// <returns></returns>
        public static List<string> GetLocalAddress()
        {
            List<string> madAddrList = new List<string>();
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration"); 
            try
            {
                ManagementObjectCollection moc2 = mc.GetInstances(); 
                
                foreach (ManagementObject mo in moc2)
                {
                    if (Convert.ToBoolean(mo["IPEnabled"]) == true)
                    { 
                       string madAddr = mo["MacAddress"].ToString();
                        madAddr = madAddr.Replace(':', '-');
                        madAddrList.Add(madAddr);
                    }
                    mo.Dispose();
                }
            }
            catch (Exception ex)
            {
                SeatManageComm.WriteLog.Write(string.Format("执行mc.GetInstances() 遇到错误" + ex.Message));
                throw ex;
            }
            return madAddrList;
        }
    }
}
