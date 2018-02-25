using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.IWCFService;
using System.Data;

namespace WcfServiceForSeatManage
{
    public partial class SeatManageDateService : ISeatManageService
    {
        SeatManage.DAL.AMS_PlayList playlistDal = new SeatManage.DAL.AMS_PlayList();
        /// <summary>
        /// 根据状态获取播放列表
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public List<SeatManage.ClassModel.AMS_PlayList> GetPlayListByStatus(SeatManage.EnumType.LogStatus status)
        {
             List<SeatManage.ClassModel.AMS_PlayList> list = new List<SeatManage.ClassModel.AMS_PlayList>();
            StringBuilder strWhere = new StringBuilder();
            switch (status)
            {
                case SeatManage.EnumType.LogStatus.Valid: 
                    strWhere.AppendFormat(" datediff(day,EffectDate,'{0}')>=0 and  datediff(day,EndDate,'{0}')<=0 order by EffectDate desc",GetServerDateTime());
                    break;
                case SeatManage.EnumType.LogStatus.None:
                    break;
            }
            try
            {
                DataSet ds = playlistDal.GetList(strWhere.ToString(), null);
                for(int i=0;i<ds.Tables[0].Rows.Count;i++)
                {
                    list.Add(DataRowToPlayListModel(ds.Tables[0].Rows[i]));
                }
                return list;
            }
            catch (Exception ex){
                throw ex;
            }
        }
        /// <summary>
        /// 根据是否失效获取播放列表
        /// </summary>
        /// <param name="status">播放列表状态，None为查询所有的</param>
        /// <returns></returns>
        public List<SeatManage.ClassModel.AMS_PlayList> GetPlayListOverTime(SeatManage.EnumType.LogStatus status)
        {
            List<SeatManage.ClassModel.AMS_PlayList> list = new List<SeatManage.ClassModel.AMS_PlayList>();
            StringBuilder strWhere = new StringBuilder();
            switch (status)
            {
                case SeatManage.EnumType.LogStatus.Fail:
                    strWhere.AppendFormat("  datediff(day,EndDate,'{0}')>0", GetServerDateTime());
                    break;
                case SeatManage.EnumType.LogStatus.None:
                    break;
            }
            try
            {
                DataSet ds = playlistDal.GetList(strWhere.ToString(), null);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    list.Add(DataRowToPlayListModel(ds.Tables[0].Rows[i]));
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 添加新的播放列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public SeatManage.EnumType.HandleResult AddPlaylist(SeatManage.ClassModel.AMS_PlayList model)
        {
            if (playlistDal.Add(model))
            {
                return SeatManage.EnumType.HandleResult.Successed;
            }
            else
            {
                return SeatManage.EnumType.HandleResult.Failed;
            }
        }
        /// <summary>
        /// 更新播放列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public SeatManage.EnumType.HandleResult UpdatePlaylist(SeatManage.ClassModel.AMS_PlayList model)
        {
            if (playlistDal.Update(model))
            {
                return SeatManage.EnumType.HandleResult.Successed;
            }
            else
            {
                return SeatManage.EnumType.HandleResult.Failed;
            }
        }
        /// <summary>
        /// 删除播放列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public SeatManage.EnumType.HandleResult DeletePlaylist(SeatManage.ClassModel.AMS_PlayList model)
        {
            if (playlistDal.Delete(model.PlayListNo))
            {
                return SeatManage.EnumType.HandleResult.Successed;
            }
            else
            {
                return SeatManage.EnumType.HandleResult.Failed;
            }
        }
        /// <summary>
        /// 根据编号获取播放列表
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public SeatManage.ClassModel.AMS_PlayList GetPlayListByNum(string num)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat(" Number='{0}' ",num);
            DataSet ds = playlistDal.GetList(strWhere.ToString(),null);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToPlayListModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 行转换为Model
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private SeatManage.ClassModel.AMS_PlayList DataRowToPlayListModel(DataRow dr)
        {
            if (dr != null)
            {
                string strPlayList = dr["PlayList"].ToString();
                if (!string.IsNullOrEmpty(strPlayList))
                {
                    return SeatManage.ClassModel.AMS_PlayList.Parse(strPlayList);
                } 
            }
            return null;
        }


        /// <summary>
        /// 根据状态获取mD5播放列表
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public List<SeatManage.ClassModel.AMS_PlayListMd5> GetMd5PlayListByStatus(SeatManage.EnumType.LogStatus status)
        {
            List<SeatManage.ClassModel.AMS_PlayListMd5> list = new List<SeatManage.ClassModel.AMS_PlayListMd5>();
            StringBuilder strWhere = new StringBuilder();
            switch (status)
            {
                case SeatManage.EnumType.LogStatus.Valid:
                    strWhere.AppendFormat(" datediff(day,EffectDate,'{0}')>=0 and  datediff(day,EndDate,'{0}')<=0 order by EffectDate desc", GetServerDateTime());
                    break;
                case SeatManage.EnumType.LogStatus.None:
                    break;
            }
            try
            {
                DataSet ds = playlistDal.GetList(strWhere.ToString(), null);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    list.Add(DataRowToMd5PlayListModel(ds.Tables[0].Rows[i]));
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 根据是否失效获取Md5播放列表
        /// </summary>
        /// <param name="status">播放列表状态，None为查询所有的</param>
        /// <returns></returns>
        public List<SeatManage.ClassModel.AMS_PlayListMd5> GetMd5PlayListOverTime(SeatManage.EnumType.LogStatus status)
        {
            List<SeatManage.ClassModel.AMS_PlayListMd5> list = new List<SeatManage.ClassModel.AMS_PlayListMd5>();
            StringBuilder strWhere = new StringBuilder();
            switch (status)
            {
                case SeatManage.EnumType.LogStatus.Fail:
                    strWhere.AppendFormat("  datediff(day,EndDate,'{0}')>0", GetServerDateTime());
                    break;
                case SeatManage.EnumType.LogStatus.None:
                    break;
            }
            try
            {
                DataSet ds = playlistDal.GetList(strWhere.ToString(), null);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    list.Add(DataRowToMd5PlayListModel(ds.Tables[0].Rows[i]));
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 添加新的Md5播放列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public SeatManage.EnumType.HandleResult AddMd5Playlist(SeatManage.ClassModel.AMS_PlayListMd5 model)
        {
            if (playlistDal.AddMd5(model))
            {
                return SeatManage.EnumType.HandleResult.Successed;
            }
            else
            {
                return SeatManage.EnumType.HandleResult.Failed;
            }
        }
        /// <summary>
        /// 更新Md5播放列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public SeatManage.EnumType.HandleResult UpdateMd5Playlist(SeatManage.ClassModel.AMS_PlayListMd5 model)
        {
            if (playlistDal.UpdateMd5(model))
            {
                return SeatManage.EnumType.HandleResult.Successed;
            }
            else
            {
                return SeatManage.EnumType.HandleResult.Failed;
            }
        }
        /// <summary>
        /// 删除Md5播放列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public SeatManage.EnumType.HandleResult DeleteMd5Playlist(SeatManage.ClassModel.AMS_PlayListMd5 model)
        {
            if (playlistDal.Delete(model.PlayListNo))
            {
                return SeatManage.EnumType.HandleResult.Successed;
            }
            else
            {
                return SeatManage.EnumType.HandleResult.Failed;
            }
        }
        /// <summary>
        /// 根据编号获取mD5播放列表
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public SeatManage.ClassModel.AMS_PlayListMd5 GetMd5PlayListByNum(string num)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat(" Number='{0}' ", num);
            DataSet ds = playlistDal.GetList(strWhere.ToString(), null);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToMd5PlayListModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 行转换为包含MD5的Model
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private SeatManage.ClassModel.AMS_PlayListMd5 DataRowToMd5PlayListModel(DataRow dr)
        {
            if (dr != null)
            {
                string strPlayList = dr["PlayList"].ToString();
                if (!string.IsNullOrEmpty(strPlayList))
                {
                    return SeatManage.ClassModel.AMS_PlayListMd5.Parse(strPlayList);
                }
            }
            return null;
        }
    }
}
