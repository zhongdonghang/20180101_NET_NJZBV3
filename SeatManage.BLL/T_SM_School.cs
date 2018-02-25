using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
using SeatManage.SeatManageComm;
using System.ServiceModel;

namespace SeatManage.Bll
{
    [Serializable]
    public class T_SM_School
    {
        /// <summary>
        /// 添加学校
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddNewSchool(School model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.AddSchoolInfo(model);
            }
            catch (Exception ex)
            {
                error = true;
                WriteLog.Write("添加学校失败：" + ex.Message);
                return false;
            }
           
        }
        /// <summary>
        /// 更新学校信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpdataSchoolInfo(School model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.UpdateSchool(model);
            }
            catch (Exception ex)
            {
                error = true;
                WriteLog.Write("更新学校失败：" + ex.Message);
                return false;
            }
           
        }
        /// <summary>
        /// 删除学校信息，会连同下属图书馆阅览室以及座位信息一同删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool DeleteSchool(School model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.DeleteSchool(model);
            }
            catch (Exception ex)
            {
                error = true;
                WriteLog.Write("删除学校失败：" + ex.Message);
                return false;
            }
           
        }
        /// <summary>
        /// 获取学校信息
        /// </summary>
        /// <returns></returns>
        public static List<School> GetSchoolInfoList(string schoolno,string schoolname)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetSchoolList(schoolno, schoolname);
            }
            catch (Exception ex)
            {
                error = true;
                WriteLog.Write("获取学校列表失败：" + ex.Message);
                return new List<School>();
            }
          
        }
    }
}
