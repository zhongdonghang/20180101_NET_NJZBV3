using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdvertManage.IWCFService;
using System.Data;

namespace AdvertManage.WCFService
{
    public partial class AdvertManageService : IAdvertManageService
    {
        DAL.AMS_PlayListDal playListDal = new DAL.AMS_PlayListDal();
        /// <summary>
        /// 根据Id获取播放列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns> 
        public AdvertManage.Model.AMS_PlayListModel GetPlaylistById(int id)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat(" Id={0}", id);
            try
            {
                DataSet ds = playListDal.GetList(strWhere.ToString(), null);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    AdvertManage.Model.AMS_PlayListModel model = new Model.AMS_PlayListModel();
                    model = AdvertManage.Model.AMS_PlayListModel.Parse(ds.Tables[0].Rows[0]["PlayList"].ToString());
                    model.Id = ds.Tables[0].Rows[0]["id"].ToString();
                    model.ReleaseDate = DateTime.Parse(ds.Tables[0].Rows[0]["ReleaseDate"].ToString());
                    return model;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 根据编号获取播放列表
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public Model.AMS_PlayListModel GetPlaylistByNum(string number)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat(" Number='{0}'", number);
            try
            {
                DataSet ds = playListDal.GetList(strWhere.ToString(), null);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    AdvertManage.Model.AMS_PlayListModel model = new Model.AMS_PlayListModel();
                    model = AdvertManage.Model.AMS_PlayListModel.Parse(ds.Tables[0].Rows[0]["PlayList"].ToString());
                    model.Id = ds.Tables[0].Rows[0]["id"].ToString();
                    return model;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 添加视频播放列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Model.Enum.HandleResult AddPlayList(Model.AMS_PlayListModel model)
        {
            try
            {
                if (playListDal.Add(model) > 0)
                {
                    return Model.Enum.HandleResult.Successed;
                }
                else
                {
                    return Model.Enum.HandleResult.Failed;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Model.Enum.HandleResult UpdatePlayList(Model.AMS_PlayListModel model)
        {
            try
            {
                if (playListDal.Update(model) )
                {
                    return Model.Enum.HandleResult.Successed;
                }
                else
                {
                    return Model.Enum.HandleResult.Failed;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Model.Enum.HandleResult DeletePlayList(int id)
        {
            try
            {
                if (playListDal.Delete(id))
                {
                    return Model.Enum.HandleResult.Successed;
                }
                else
                {
                    return Model.Enum.HandleResult.Failed;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 获取播放列表
        /// </summary>
        /// <returns></returns>
        public List<Model.AMS_PlayListModel> GetPlaylist()
        {
            try
            {
                List<Model.AMS_PlayListModel> modelList = new List<Model.AMS_PlayListModel>();
                DataSet ds = playListDal.GetList(null, null);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    AdvertManage.Model.AMS_PlayListModel model = AdvertManage.Model.AMS_PlayListModel.Parse(ds.Tables[0].Rows[i]["PlayList"].ToString());
                    model.ReleaseDate = DateTime.Parse(ds.Tables[0].Rows[i]["ReleaseDate"].ToString());
                    model.Id = ds.Tables[0].Rows[i]["id"].ToString();
                    modelList.Add(model);
                }
                return modelList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 根据Id获取Md5播放列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns> 
        public AdvertManage.Model.AMS_PlayListMd5Model GetMD5PlaylistById(int id)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat(" Id={0}", id);
            try
            {
                DataSet ds = playListDal.GetList(strWhere.ToString(), null);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    AdvertManage.Model.AMS_PlayListMd5Model model = new Model.AMS_PlayListMd5Model();
                    model = AdvertManage.Model.AMS_PlayListMd5Model.Parse(ds.Tables[0].Rows[0]["PlayList"].ToString());
                    model.Id = ds.Tables[0].Rows[0]["id"].ToString();
                    model.ReleaseDate = DateTime.Parse(ds.Tables[0].Rows[0]["ReleaseDate"].ToString());
                    return model;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 根据编号获取Md5播放列表
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public Model.AMS_PlayListMd5Model GetMd5PlaylistByNum(string number)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat(" Number='{0}'", number);
            try
            {
                DataSet ds = playListDal.GetList(strWhere.ToString(), null);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    AdvertManage.Model.AMS_PlayListMd5Model model = new Model.AMS_PlayListMd5Model();
                    model = AdvertManage.Model.AMS_PlayListMd5Model.Parse(ds.Tables[0].Rows[0]["PlayList"].ToString());
                    model.Id = ds.Tables[0].Rows[0]["id"].ToString();
                    return model;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 添加Md5视频播放列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Model.Enum.HandleResult AddMd5PlayList(Model.AMS_PlayListMd5Model model)
        {
            try
            {
                if (playListDal.AddMd5(model) > 0)
                {
                    return Model.Enum.HandleResult.Successed;
                }
                else
                {
                    return Model.Enum.HandleResult.Failed;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 更新一条包含MD5校验值的播放列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Model.Enum.HandleResult UpdateMd5PlayList(Model.AMS_PlayListMd5Model model)
        {
            try
            {
                if (playListDal.UpdateMd5(model))
                {
                    return Model.Enum.HandleResult.Successed;
                }
                else
                {
                    return Model.Enum.HandleResult.Failed;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 获取MD5播放列表
        /// </summary>
        /// <returns></returns>
        public List<Model.AMS_PlayListMd5Model> GetMd5Playlist()
        {
            try
            {
                List<Model.AMS_PlayListMd5Model> modelList = new List<Model.AMS_PlayListMd5Model>();
                DataSet ds = playListDal.GetList(null, null);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    AdvertManage.Model.AMS_PlayListMd5Model model = AdvertManage.Model.AMS_PlayListMd5Model.Parse(ds.Tables[0].Rows[i]["PlayList"].ToString());
                    model.ReleaseDate = DateTime.Parse(ds.Tables[0].Rows[i]["ReleaseDate"].ToString());
                    model.Id = ds.Tables[0].Rows[i]["id"].ToString();
                    modelList.Add(model);
                }
                return modelList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
