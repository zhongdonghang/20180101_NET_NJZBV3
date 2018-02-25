/********************************************
 * 作者：王昊天
 * 创建时间：2013-6-3
 * 说明：功能页面BLL
 * 修改人：
 * 修改：
 * ******************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.SeatManageComm;
using System.ServiceModel;

namespace SeatManage.Bll
{
    public class SysFuncDic
    {
        /// <summary>
        /// 获取功能页面列表
        /// </summary>
        /// <param name="Order">根据分类查询，输入null默认查询全部</param>
        /// <param name="Num">根据编号查询，输入null默认查询全部</param>
        /// <returns>返回一个功能页面list</returns>
        public List<SeatManage.ClassModel.SysFuncDicInfo> GetFuncPage(string Order, string Num)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetFuncPage(Order, Num);
            }
            catch (Exception ex)
            {
                error = true;
                WriteLog.Write("获取功能列表失败：" + ex.Message);
                return new List<ClassModel.SysFuncDicInfo>();
            }
            
        }
        /// <summary>
        /// 添加功能页面
        /// </summary>
        /// <param name="model">添加的实体类</param>
        /// <returns>返回空值，说明成功，其他返回错误信息</returns>
        public string AddNewFuncPage(SeatManage.ClassModel.SysFuncDicInfo model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.AddNewFuncPage(model);
            }
            catch (Exception ex)
            {
                error = true;
                WriteLog.Write("添加功能页面失败：" + ex.Message);
                return ex.Message;
            }
           
        }
        /// <summary>
        /// 修改功能页面
        /// </summary>
        /// <param name="model">需要修改的实体类</param>
        /// <returns>返回Ture说明成功，False失败</returns>
        public bool UpdateFuncPage(SeatManage.ClassModel.SysFuncDicInfo model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.UpdateFuncPage(model);
            }
            catch (Exception ex)
            {
                error = true;
                WriteLog.Write("修改功能页面失败：" + ex.Message);
                return false;
            }
            
        }
        /// <summary>
        /// 删除功能页面，连表删除，会把菜单以及菜单相关联的权限删除
        /// </summary>
        /// <param name="model">需要删除的实体类</param>
        /// <returns>返回Ture说明成功，False失败</returns>
        public bool DeleteFuncPage(SeatManage.ClassModel.SysFuncDicInfo model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.DeleteFuncPage(model);
            }
            catch (Exception ex)
            {
                error = true;
                WriteLog.Write("删除功能页面失败：" + ex.Message);
                return false;
            }
           
        }
    }
}
