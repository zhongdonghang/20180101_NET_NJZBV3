using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;//Please add references
namespace SeatManage.DAL
{
	/// <summary>
	/// 数据访问类:ViewStudyBookingLog
	/// </summary>
	public partial class ViewStudyBookingLog
	{
		public ViewStudyBookingLog()
		{}
		#region  Method



//        /// <summary>
//        /// 增加一条数据
//        /// </summary>
//        public bool Add(SeatManage.DataModel.ViewStudyBookingLog model)
//        {
//            StringBuilder strSql=new StringBuilder();
//            strSql.Append("insert into ViewStudyBookingLog(");
//            strSql.Append(")");
//            strSql.Append(" values (");
//            strSql.Append(")");
//            SqlParameter[] parameters = {
//};

//            int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
//            if (rows > 0)
//            {
//                return true;
//            }
//            else
//            {
//                return false;
//            }
//        }
//        /// <summary>
//        /// 更新一条数据
//        /// </summary>
//        public bool Update(SeatManage.DataModel.ViewStudyBookingLog model)
//        {
//            StringBuilder strSql=new StringBuilder();
//            strSql.Append("update ViewStudyBookingLog set ");
//#warning 系统发现缺少更新的字段，请手工确认如此更新是否正确！ 
//            strSql.Append(" where ");
//            SqlParameter[] parameters = {
//};

//            int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
//            if (rows > 0)
//            {
//                return true;
//            }
//            else
//            {
//                return false;
//            }
//        }

//        /// <summary>
//        /// 删除一条数据
//        /// </summary>
//        public bool Delete()
//        {
//            //该表无主键信息，请自定义主键/条件字段
//            StringBuilder strSql=new StringBuilder();
//            strSql.Append("delete from ViewStudyBookingLog ");
//            strSql.Append(" where ");
//            SqlParameter[] parameters = {
//};

//            int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
//            if (rows > 0)
//            {
//                return true;
//            }
//            else
//            {
//                return false;
//            }
//        }


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SeatManage.ClassModel.ViewStudyBookingLog GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select  top 1 [CardNo],[StudyID],[SubmitTime],[CheckTime],[BespeakTime],[UseTime],[CheckState],[ChecklPerson],[Remark],[StudyRoomNo],[StudyRoomName],[StudyRoomSetting],[ReaderName],[ApplicationTable] from ViewStudyBookingLog ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
};

			SeatManage.ClassModel.ViewStudyBookingLog model=new SeatManage.ClassModel.ViewStudyBookingLog();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				return model;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select [CardNo],[StudyID],[SubmitTime],[CheckTime],[BespeakTime],[UseTime],[CheckState],[ChecklPerson],[Remark],[StudyRoomNo],[StudyRoomName],[StudyRoomSetting],[ReaderName],[ApplicationTable] ");
			strSql.Append(" FROM ViewStudyBookingLog ");
			if(strWhere != null && strWhere.Trim() != "")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
            strSql.Append(" [CardNo],[StudyID],[SubmitTime],[CheckTime],[BespeakTime],[UseTime],[CheckState],[ChecklPerson],[Remark],[StudyRoomNo],[StudyRoomName],[StudyRoomSetting],[ReaderName],[ApplicationTable]");
			strSql.Append(" FROM ViewStudyBookingLog ");
			if(strWhere != null && strWhere.Trim() != "")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
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
			parameters[0].Value = "ViewStudyBookingLog";
			parameters[1].Value = "";
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

