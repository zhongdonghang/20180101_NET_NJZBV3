using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TcpClient_BespeakSeat
{
    public class TcpClient_Login : SeatManage.IPocketBespeak.ILogin
    {

        public List<AMS.Model.AMS_School> GetAllSchoolFromLocal()
        {
            return AMS.ServiceProxy.AMS_SchoolProxy.GetAllSchool();
        }

        public SeatManage.ClassModel.ReaderInfo CheckAndGetReaderInfo(SeatManage.ClassModel.UserInfo user, AMS.Model.AMS_School school)
        {
            SMS.BespeakServerProxy.BespeakServerProxy bespeakProxy = new SMS.BespeakServerProxy.BespeakServerProxy(school.ConnectionString);
            return bespeakProxy.CheckAndGetReaderInfo(user);
        }

        public SeatManage.ClassModel.ReaderInfo GetReaderInfoByCardNo(string cardNo, AMS.Model.AMS_School school)
        {
            SMS.BespeakServerProxy.BespeakServerProxy bespeakProxy = new SMS.BespeakServerProxy.BespeakServerProxy(school.ConnectionString);
            return bespeakProxy.GetReaderInfoByCardNo(cardNo);
        }

        public AMS.Model.AMS_School GetSingleSchoolInfo(string schoolId)
        {
            return AMS.ServiceProxy.AMS_SchoolProxy.GetSchoolById(int.Parse(schoolId));
        }

        public SeatManage.ClassModel.ReaderInfo GetReaderInfoByCardNofalse(string cardNo, AMS.Model.AMS_School school)
        {
            SMS.BespeakServerProxy.BespeakServerProxy bespeakProxy = new SMS.BespeakServerProxy.BespeakServerProxy(school.ConnectionString);
            return bespeakProxy.GetReaderInfoByCardNofalse(cardNo);
        }
    }
}
