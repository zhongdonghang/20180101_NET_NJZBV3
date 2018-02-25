using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using SeatManage.ClassModel;
using System.ServiceModel;

namespace SeatManage.Bll
{
    public class T_SM_Reader
    {
        /// <summary>
        /// 根据学号获取读者的登录信息，并判断与输入的密码是否匹配
        /// </summary>
        /// <param name="loginId">登录Id</param>
        /// <param name="passWord">登录密码</param>
        /// <param name="school">读者所在学校</param>
        /// <returns></returns>
        public string CheckUser(string loginId, string passWord, ClassModel.Universities universities)
        {
            IWCFService.ISeatManageService seatService = WcfAccessProxy.ServiceProxy.CreateChannelSeatManageService(universities.ConnectionString);
            bool error = false;
            try
            {
                return seatService.CheckUser(loginId, passWord);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("密码验证失败：" + ex.Message);
                return null;
            }
          
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="school">所属学校</param>
        /// <returns></returns>
        public ReaderInfo GetReader(string loginId)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            string error = "";
            try
            {
                ReaderInfo reader = seatService.GetReader(loginId, true);
                //UserInfo reader = seatService.GetUserInfo(loginId);
                return reader;
            }
            catch (FaultException ex)
            {
                error = "产生异常：" + ex.Code.Name + ",错误原因：" + ex.Reason;
                //SeatManage.IPocketBookOnlineBll.IErrorPageBll bllErrorPage = new SeatManage.PocketBookOnLine.Bll.ErrorPageBll();
                //bllErrorPage.AddErrorMessage(error);
                SeatManageComm.WriteLog.Write("获取读者信息错误：" + error);
                return null;
            }
           
        }
        /// <summary>
        /// 获取读者类型列表
        /// </summary>
        /// <returns></returns>
        public List<string> GetReaderType()
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetReaderType();
            }
            catch (FaultException ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取读者类型错误：" + ex.Message);
                return null;
            }
           
        }
        /// <summary>
        ///根据物理ID获取读者信息
        /// </summary>
        /// <param name="cardId"></param>
        /// <returns></returns>
        public static ReaderInfo GetReaderByCardId(string cardId)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetReaderByCardId(cardId);
            }
            catch (FaultException ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("根据卡片物理ID获取读者信息失败：" + ex.Message);
                return null;
            }
           
        }

        /// <summary>
        ///根据卡列号从源数据获取读者信息
        /// </summary>
        /// <param name="cardId"></param>
        /// <returns></returns>
        public static ReaderInfo GetReaderByCardIdFromSource(string cardId)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetReaderByCardIdFromSource(cardId);
            }
            catch (FaultException ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("根据卡列号从源读者库获取读者信息失败：" + ex.Message);
                return null;
            }
           
        }

        /// <summary>
        ///根据学号从源数据获取读者信息
        /// </summary>
        /// <param name="cardId"></param>
        /// <returns></returns>
        public static ReaderInfo GetReaderByCardNoFromSource(string cardNo)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetReaderByCardNoFromSource(cardNo);
            }
            catch (FaultException ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("根据学号从源读者库获取读者信息失败：" + ex.Message);
                return null;
            }
          
        }
        /// <summary>
        /// 清空读者信息库
        /// </summary>
        public static void Clear()
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                  seatService.ClearReaderInfo();
            }
            catch (FaultException ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("清空读者信息库失败：" + ex.Message); 
            }
            
        }
        /// <summary>
        /// 添加一条读者信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static SeatManage.EnumType.HandleResult Add(SeatManage.ClassModel.ReaderInfo model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
               return seatService.Add(model);
            }
            catch (FaultException ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("添加读者信息失败：" + ex.Message);
                return EnumType.HandleResult.Failed;
            }
          
        }
        /// <summary>
        /// 批量添加读者信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static SeatManage.EnumType.HandleResult AddReaderList(List<SeatManage.ClassModel.ReaderInfo> modelList)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.AddLotSize(modelList);
            }
            catch (FaultException ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("添加读者信息失败：" + ex.Message);
                return EnumType.HandleResult.Failed;
            }
          
        }
        /// <summary>
        /// 删除读者信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static SeatManage.EnumType.HandleResult DeleteReader(SeatManage.ClassModel.ReaderInfo model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.DeleteReaderByCardNo(model);
            }
            catch (FaultException ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("删除读者信息失败：" + ex.Message);
                return EnumType.HandleResult.Failed;
            }
           
        }
        /// <summary>
        /// 更新读者信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static SeatManage.EnumType.HandleResult Update(SeatManage.ClassModel.ReaderInfo model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.UpdateReaderInfo(model);
            }
            catch (FaultException ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("添加读者信息失败：" + ex.Message);
                return EnumType.HandleResult.Failed;
            }
          
        }

        /// <summary>
        /// 根据卡列号更新读者信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static SeatManage.EnumType.HandleResult UpdateByCardId(SeatManage.ClassModel.ReaderInfo model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.UpdateReaderInfoByCardId(model);
            }
            catch (FaultException ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("根据卡列号更新读者信息：" + ex.Message);
                return EnumType.HandleResult.Failed;
            }
        }


    }
}
