using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Configuration;
namespace PocketBookOnline.WebService
{
    /// <summary>
    /// MediaService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://juneberry.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class MediaService : System.Web.Services.WebService
    {
       public  MySoapHeader myHeader = new MySoapHeader();
        private string soapUserName = ConfigurationManager.AppSettings["soapHeaderUserName"];
        private string soapPwd = ConfigurationManager.AppSettings["soapHeaderPwd"];
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        [SoapHeader("myHeader")]
        public string GetBespeakSeatSchoolList()
        {
            List<SeatManage.JsonModel.JM_SchoolModel> jm_schools = new List<SeatManage.JsonModel.JM_SchoolModel>();
            if (myHeader.UserName.Equals(soapUserName) && SeatManage.SeatManageComm.MD5Algorithm.GetMD5Str32(myHeader.PassWord).Equals(soapPwd))
            {
                List<AMS.Model.AMS_School> schoolList = new TcpClient_BespeakSeat.TcpClient_Login().GetAllSchoolFromLocal();
                for (int i = 0; i < schoolList.Count; i++)
                {
                    if (schoolList[i].IsSeatBespeak)
                    {
                        SeatManage.JsonModel.JM_SchoolModel jm_school = new SeatManage.JsonModel.JM_SchoolModel();
                        jm_school.SchoolId = schoolList[i].Id.ToString();
                        jm_school.SchoolName = schoolList[i].Name;
                        jm_school.SchoolNum = schoolList[i].ConnectionString;
                        jm_schools.Add(jm_school); 
                    }
                }
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(jm_schools);
            }
            else
            {
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(new SeatManage.JsonModel.JM_HandleResult() { Result = false, Msg = "没有权限！" });
            }
        }
    }
}
