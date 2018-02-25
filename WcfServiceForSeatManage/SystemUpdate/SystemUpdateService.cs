using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.IWCFService;
using System.ServiceModel;
using SeatManage.ClassModel;
using SeatManage.EnumType;
using SeatManage.DAL;
using System.Data;

namespace WcfServiceForSeatManage
{
    public partial class SeatManageDateService : ISeatManageService
    { 
        AutoUpdater updaterDal = new AutoUpdater();
        /// <summary>
        /// 获取参数中包含的文件信息
        /// </summary>
        /// <param name="FilesName">文件名称</param>
        /// <returns></returns> 
        public List<FileSimpleInfo> GetFilesInfo(List<string> filesName, SeatManageSubsystem system)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 获取更新信息
        /// </summary>
        /// <returns></returns>
        public FileUpdateInfo GetUpdateInfo(SeatManageSubsystem system)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(string.Format(" Application ={0}", (int)system));
            try
            {
                DataSet ds = updaterDal.GetList(strWhere.ToString(), null);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return DataRowToFileUpdaterInfo(ds.Tables[0].Rows[0]);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 修改自动更新信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateFileInfo(FileUpdateInfo model)
        {
            try
            {
                return updaterDal.Update(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 添加自动更新信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns> 
        public bool AddUpdaterFileInfo(FileUpdateInfo model)
        {
            try
            {
                return updaterDal.Add(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        

        private FileUpdateInfo DataRowToFileUpdaterInfo(DataRow dr)
        {
            string updateFileXml = dr["AutoUpdaterXml"].ToString();
            FileUpdateInfo fileUpdate = FileUpdateInfo.Convert(updateFileXml); ;
            return fileUpdate;
        }
    }
}
