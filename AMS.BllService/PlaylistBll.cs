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
        AMS.DAL.View_PlayList dal_PlaylistView = new DAL.View_PlayList();
        AMS.DAL.AMS_PlayList dal_Playlist = new DAL.AMS_PlayList();
        public List<Model.AMS_PlayList> GetPlayListList()
        {
            try
            {
                List<Model.AMS_PlayList> modellist = new List<Model.AMS_PlayList>();
                DataSet ds = dal_PlaylistView.GetList(null);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    modellist.Add(DataRowToAMS_PlayListModel(ds.Tables[0].Rows[i]));
                }
                return modellist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string AddNewPlayList(Model.AMS_PlayList model)
        {
            try
            {
                AMS.Model.AMS_PlayList sameModel = dal_Playlist.GetModel(model.Number);
                if (sameModel != null)
                {
                    return "播放列表编号重复！";
                }
                model.PlayList = Model.AMS_PlayList.ToXml(model);
                if (dal_Playlist.Add(model) == 0)
                {
                    return "添加播放列表失败！";
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string UpdatePlayList(Model.AMS_PlayList model)
        {
            try
            {
                AMS.Model.AMS_PlayList sameModel = dal_Playlist.GetModel(model.Number);
                if (sameModel != null && sameModel.Id != model.Id)
                {
                    return "播放列表编号重复！";
                }
                model.PlayList = Model.AMS_PlayList.ToXml(model);
                if (!dal_Playlist.Update(model))
                {
                    return "修改播放列表失败！";
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string DeletePlayList(Model.AMS_PlayList model)
        {
            try
            {
                if (!dal_Playlist.Delete(model.Id))
                {
                    return "修改播放列表失败！";
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// 转换playlist的model
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private AMS.Model.AMS_PlayList DataRowToAMS_PlayListModel(DataRow dr)
        {
            AMS.Model.AMS_PlayList model = new Model.AMS_PlayList();
            model = AMS.Model.AMS_PlayList.Parse(dr["PlayList"].ToString());
            model.Describe = dr["Describe"].ToString();
            model.Id = int.Parse(dr["Id"].ToString());
            if (dr.Table.Columns.Contains("OperatorName"))
            {
                model.OperatorName = dr["OperatorName"].ToString();
            }
            model.PlayListName = dr["PlayListName"].ToString();
            model.ReleaseDate = DateTime.Parse(dr["ReleaseDate"].ToString());
            return model;
        }


        public Model.AMS_PlayList GetPlayListByID(int id)
        {
            try
            {
                Model.AMS_PlayList modellist = new Model.AMS_PlayList();
                StringBuilder sql = new StringBuilder();
                sql.AppendFormat("ID={0}", id);
                DataSet ds = dal_PlaylistView.GetList(sql.ToString());
                modellist = DataRowToAMS_PlayListModel(ds.Tables[0].Rows[0]);
                return modellist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
