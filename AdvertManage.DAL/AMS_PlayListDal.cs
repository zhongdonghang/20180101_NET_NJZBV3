using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using  DBUtility;
using AdvertManage.Model;//Please add references
namespace AdvertManage.DAL
{
	/// <summary>
	/// 数据访问类:AMS_PlayList
	/// </summary>
	public partial class AMS_PlayListDal
	{
		public AMS_PlayListDal()
		{}
		#region  Method
          

		/// <summary>
		/// 增加一条数据
		/// </summary>
        public int Add(AMS_PlayListModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into AMS_PlayList(");
			strSql.Append("Number,PlayList,ReleaseDate,EffectDate,EndDate)");
			strSql.Append(" values (");
			strSql.Append("@Number,@PlayList,@ReleaseDate,@EffectDate,@EndDate)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Number", SqlDbType.NVarChar,50),
					new SqlParameter("@PlayList", SqlDbType.Text),
					new SqlParameter("@ReleaseDate", SqlDbType.DateTime),
					new SqlParameter("@EffectDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime)};
			parameters[0].Value = model.PlayListNo;
            parameters[1].Value = model.ToXml();
			parameters[2].Value = model.ReleaseDate;
			parameters[3].Value = model.EffectDate;
			parameters[4].Value = model.EndDate;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
        public bool Update(AMS_PlayListModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update AMS_PlayList set ");
			strSql.Append("Number=@Number,");
			strSql.Append("PlayList=@PlayList,");
			strSql.Append("ReleaseDate=@ReleaseDate,");
			strSql.Append("EffectDate=@EffectDate,");
			strSql.Append("EndDate=@EndDate");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Number", SqlDbType.NVarChar,50),
					new SqlParameter("@PlayList", SqlDbType.Text),
					new SqlParameter("@ReleaseDate", SqlDbType.DateTime),
					new SqlParameter("@EffectDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@Id", SqlDbType.Int,4)};
			parameters[0].Value = model.PlayListNo;
			parameters[1].Value = model.ToXml();
			parameters[2].Value = model.ReleaseDate;
			parameters[3].Value = model.EffectDate;
			parameters[4].Value = model.EndDate;
			parameters[5].Value = model.Id;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from AMS_PlayList ");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
};
			parameters[0].Value = Id;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string Idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from AMS_PlayList ");
			strSql.Append(" where Id in ("+Idlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
         
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere,SqlParameter[] parameters)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Id,Number,PlayList,ReleaseDate,EffectDate,EndDate ");
			strSql.Append(" FROM AMS_PlayList ");
			if(!string.IsNullOrEmpty(strWhere))
			{
				strSql.Append(" where "+strWhere);
			}
            return DbHelperSQL.Query(strSql.ToString(), parameters);
		}


        /// <summary>
        /// 增加一条包含MD5值的数据
        /// </summary>
        public int AddMd5(AMS_PlayListMd5Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AMS_PlayList(");
            strSql.Append("Number,PlayList,ReleaseDate,EffectDate,EndDate)");
            strSql.Append(" values (");
            strSql.Append("@Number,@PlayList,@ReleaseDate,@EffectDate,@EndDate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Number", SqlDbType.NVarChar,50),
					new SqlParameter("@PlayList", SqlDbType.Text),
					new SqlParameter("@ReleaseDate", SqlDbType.DateTime),
					new SqlParameter("@EffectDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime)};
            parameters[0].Value = model.PlayListNo;
            parameters[1].Value = model.ToXml();
            parameters[2].Value = model.ReleaseDate;
            parameters[3].Value = model.EffectDate;
            parameters[4].Value = model.EndDate;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条包含MD5值的数据
        /// </summary>
        public bool UpdateMd5(AMS_PlayListMd5Model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AMS_PlayList set ");
            strSql.Append("Number=@Number,");
            strSql.Append("PlayList=@PlayList,");
            strSql.Append("ReleaseDate=@ReleaseDate,");
            strSql.Append("EffectDate=@EffectDate,");
            strSql.Append("EndDate=@EndDate");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Number", SqlDbType.NVarChar,50),
					new SqlParameter("@PlayList", SqlDbType.Text),
					new SqlParameter("@ReleaseDate", SqlDbType.DateTime),
					new SqlParameter("@EffectDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = model.PlayListNo;
            parameters[1].Value = model.ToXml();
            parameters[2].Value = model.ReleaseDate;
            parameters[3].Value = model.EffectDate;
            parameters[4].Value = model.EndDate;
            parameters[5].Value = model.Id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@fldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "AMS_PlayList";
			parameters[1].Value = "Id";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  Method
	}
}

