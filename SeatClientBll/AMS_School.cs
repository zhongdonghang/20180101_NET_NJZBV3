/******************************************
 * 作者：罗晨阳
 * 创建时间：2013-5-21
 * 说明：AdvertisingManageSystemDB数据库中School表操作的BLL
 * 修改人：
 * 修改时间：
 * ****************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
using System.ServiceModel;

namespace SeatManage.Bll
{
    public class AMS_School
    {
        /// <summary>
        /// 获取学校列表
        /// </summary>
        /// <returns></returns>
        public List<SeatManage.ClassModel.Universities> GetSchoolList()
        {
            throw new NotImplementedException();
            //SeatManage.AMSDal.School school = new AMSDal.School();
            //try
            //{
            //    return school.GetSchool();
            //}
            //catch
            //{
            //    throw;
            //}
        }
        /// <summary>
        /// 根据学校ID获取学校信息
        /// </summary>
        /// <param name="Id">学校ID</param>
        /// <returns></returns>
        public Universities GetSchool(string Id)
        {
            throw new NotImplementedException();
            
            //SeatManage.AMSDal.School school = new AMSDal.School();
            //try
            //{
            //    return school.GetSchool(Id);
            //}
            //catch
            //{
            //    throw;
            //}
        }
        /// <summary>
        /// 设置页面访问量
        /// </summary>
        /// <param name="pageView"></param>
        /// <param name="id"></param>
        public void SetPageView(int pageView, string id)
        {
            throw new NotImplementedException();
            //SeatManage.AMSDal.School school = new AMSDal.School();
            //try
            //{
            //    school.SetPageView(pageView, id);
            //}
            //catch
            //{
            //    throw;
            //}
        }
    }
}
