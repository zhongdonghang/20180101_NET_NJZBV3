using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;
using SeatManage.ClassModel;
namespace SeatManage.DAL
{
	/// <summary>
	/// 数据访问类:T_SM_Library
	/// </summary>
	public partial class T_SM_Library
	{
        public T_SM_Library()
        { }
        #region  Method

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string LibraryNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_SM_Library");
            strSql.Append(" where LibraryNo=@LibraryNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@LibraryNo", SqlDbType.NVarChar,50)};
            parameters[0].Value = LibraryNo;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select LibraryNo,LibraryName,SchoolNo,AreaInfo ");
            strSql.Append(" FROM T_SM_Library ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" LibraryNo,LibraryName,SchoolNo,AreaInfo ");
            strSql.Append(" FROM T_SM_Library ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(SeatManage.ClassModel.LibraryInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_SM_Library(");
            strSql.Append("LibraryNo,LibraryName,SchoolNo,AreaInfo)");
            strSql.Append(" values (");
            strSql.Append("@LibraryNo,@LibraryName,@SchoolNo,@AreaInfo)");
            SqlParameter[] parameters = {
                    new SqlParameter("@LibraryNo", SqlDbType.NVarChar,50),
                    new SqlParameter("@LibraryName", SqlDbType.NVarChar,100),
                    new SqlParameter("@SchoolNo", SqlDbType.Int,4),
                    new SqlParameter("@AreaInfo", SqlDbType.Text)};
            parameters[0].Value = model.No;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.School.No;
            parameters[3].Value = model.AreaToXml();
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
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SeatManage.ClassModel.LibraryInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_SM_Library set ");
            strSql.Append("LibraryName=@LibraryName,");
            strSql.Append("SchoolNo=@SchoolNo,");
            strSql.Append("AreaInfo=@AreaInfo");
            strSql.Append(" where LibraryNo=@LibraryNo ");
            SqlParameter[] parameters = {
                    new SqlParameter("@LibraryName", SqlDbType.NVarChar,100),
                    new SqlParameter("@SchoolNo", SqlDbType.Int,4),
                    new SqlParameter("@AreaInfo", SqlDbType.Text),
                    new SqlParameter("@LibraryNo", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.Name;
            parameters[1].Value = model.School.No;
            parameters[2].Value = model.AreaToXml();
            parameters[3].Value = model.No;
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

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(SeatManage.ClassModel.LibraryInfo model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_SM_Library ");
            strSql.Append(" where LibraryNo=@LibraryNo ");
            SqlParameter[] parameters = {
                    new SqlParameter("@LibraryNo", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.No;

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
        ///// <summary>
        ///// 批量删除数据
        ///// </summary>
        //public bool DeleteList(string LibraryNolist )
        //{
        //    StringBuilder strSql=new StringBuilder();
        //    strSql.Append("delete from T_SM_Library ");
        //    strSql.Append(" where LibraryNo in ("+LibraryNolist + ")  ");
        //    int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
        //    if (rows > 0)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}


        ///// <summary>
        ///// 得到一个对象实体
        ///// </summary>
        //public SeatManage.Model.T_SM_Library GetModel(string LibraryNo)
        //{
			
        //    StringBuilder strSql=new StringBuilder();
        //    strSql.Append("select  top 1 LibraryNo,LibraryName,SchoolNo from T_SM_Library ");
        //    strSql.Append(" where LibraryNo=@LibraryNo ");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@LibraryNo", SqlDbType.NVarChar,50)};
        //    parameters[0].Value = LibraryNo;

        //    SeatManage.Model.T_SM_Library model=new SeatManage.Model.T_SM_Library();
        //    DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
        //    if(ds.Tables[0].Rows.Count>0)
        //    {
        //        if(ds.Tables[0].Rows[0]["LibraryNo"]!=null && ds.Tables[0].Rows[0]["LibraryNo"].ToString()!="")
        //        {
        //            model.LibraryNo=ds.Tables[0].Rows[0]["LibraryNo"].ToString();
        //        }
        //        if(ds.Tables[0].Rows[0]["LibraryName"]!=null && ds.Tables[0].Rows[0]["LibraryName"].ToString()!="")
        //        {
        //            model.LibraryName=ds.Tables[0].Rows[0]["LibraryName"].ToString();
        //        }
        //        if(ds.Tables[0].Rows[0]["SchoolNo"]!=null && ds.Tables[0].Rows[0]["SchoolNo"].ToString()!="")
        //        {
        //            model.SchoolNo=int.Parse(ds.Tables[0].Rows[0]["SchoolNo"].ToString());
        //        }
        //        return model;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

		

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
			parameters[0].Value = "T_SM_Library";
			parameters[1].Value = "LibraryNo";
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

