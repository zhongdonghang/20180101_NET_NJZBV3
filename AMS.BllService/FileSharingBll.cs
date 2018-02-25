using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AMS.IBllService;
using System.Data;

namespace AMS.BllService
{
    public partial class AdvertManageBllService : IAdvertManageBllService
    {
        DAL.FileSharingInfo dal_FileSharing = new DAL.FileSharingInfo();
        DAL.View_FileSharingInfo dal_FileSharingView = new DAL.View_FileSharingInfo();
        /// <summary>
        /// 获取共享文件列表
        /// </summary>
        /// <returns></returns>
        public List<Model.FileSharingInfo> GetSharingFileList()
        {
            try
            {
                List<Model.FileSharingInfo> modelList = new List<Model.FileSharingInfo>();
                DataSet ds = dal_FileSharingView.GetList(null);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    modelList.Add(DataRowToFileSharingInfoModel(ds.Tables[0].Rows[i]));
                }
                return modelList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 共享文件（新增）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string AddNewSharingFile(Model.FileSharingInfo model)
        {
            try
            {
                AMS.Model.FileSharingInfo sameModel = dal_FileSharing.GetModel(model.Name, model.FileType.Value);
                if (sameModel != null)
                {
                    return "此类同名文件已存在！";
                }
                if (!dal_FileSharing.Add(model))
                {
                    return "添加失败！";
                }
                return "";

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// 更新共享文件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string UpdateSharingFile(Model.FileSharingInfo model)
        {
            try
            {
                AMS.Model.FileSharingInfo sameModel = dal_FileSharing.GetModel(model.Name, model.FileType.Value);
                if (sameModel != null && sameModel.Id != model.Id)
                {
                    return "其他此类同名文件已存在！";
                }
                if (!dal_FileSharing.Update(model))
                {
                    return "更新失败！";
                }
                return "";

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// 删除共享文件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string DeleteSharingFile(Model.FileSharingInfo model)
        {
            try
            {
                if (!dal_FileSharing.Delete(model.Id))
                {
                    return "更新失败！";
                }
                return "";

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// 转换为model类型
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private AMS.Model.FileSharingInfo DataRowToFileSharingInfoModel(DataRow dr)
        {
            AMS.Model.FileSharingInfo model = new Model.FileSharingInfo();
            model.FilePath = dr["FilePath"].ToString();
            model.FileType = int.Parse(dr["FileType"].ToString());
            model.Id = int.Parse(dr["Id"].ToString());
            model.Name = dr["Name"].ToString();
            model.Remark = dr["Remark"].ToString();
            model.Size = dr["Size"].ToString();
            model.UpManID = int.Parse(dr["UpManID"].ToString());
            model.UpMan = dr["UserName"].ToString();
            return model;
        }
    }
}
