using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;
using AdvertManage.Model;//Please add references
namespace AdvertManage.DAL
{
	/// <summary>
	/// 数据访问类:AMS_CommandList
	/// </summary>
	public partial class AMS_CommandListDal
	{
		public AMS_CommandListDal()
		{}
		#region  Method 
 


		/// <summary>
		/// 增加一条数据
		/// </summary>
        public int Add(AMS_CommandListModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into AMS_CommandList(");
			strSql.Append("SchoolId,Command,CommandId,ReleaseTime,FinishTime,FinishFlag)");
			strSql.Append(" values (");
			strSql.Append("@SchoolId,@Command,@CommandId,@ReleaseTime,@FinishTime,@FinishFlag)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					    new SqlParameter("@SchoolId", SqlDbType.Int,4),
					    new SqlParameter("@Command", SqlDbType.Int,4),
					    new SqlParameter("@CommandId", SqlDbType.Int,4),
					    new SqlParameter("@ReleaseTime", SqlDbType.DateTime),
					    new SqlParameter("@FinishTime", SqlDbType.DateTime),
					    new SqlParameter("@FinishFlag", SqlDbType.Bit,1)
                                        };
			parameters[0].Value = model.SchoolId;
			parameters[1].Value =(int) model.Command;
			parameters[2].Value = model.CommandId;
			parameters[3].Value = model.ReleaseTime;
			parameters[4].Value = model.FinishTime;
			parameters[5].Value = (int)model.FinishFlag;

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
        public bool Update(AMS_CommandListModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update AMS_CommandList set ");
			strSql.Append("SchoolId=@SchoolId,");
			strSql.Append("Command=@Command,");
			strSql.Append("CommandId=@CommandId,");
			strSql.Append("ReleaseTime=@ReleaseTime,");
			strSql.Append("FinishTime=@FinishTime,");
			strSql.Append("FinishFlag=@FinishFlag");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@SchoolId", SqlDbType.Int,4),
					new SqlParameter("@Command", SqlDbType.Int,4),
					new SqlParameter("@CommandId", SqlDbType.Int,4),
					new SqlParameter("@ReleaseTime", SqlDbType.DateTime),
					new SqlParameter("@FinishTime", SqlDbType.DateTime),
					new SqlParameter("@FinishFlag", SqlDbType.Int,4),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.SchoolId;
			parameters[1].Value = (int)model.Command;
			parameters[2].Value = model.CommandId;
			parameters[3].Value = model.ReleaseTime;
			parameters[4].Value = model.FinishTime;
			parameters[5].Value = (int)model.FinishFlag;
			parameters[6].Value = model.ID;

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
		public bool Delete(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from AMS_CommandList ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
                                        };
			parameters[0].Value = ID;

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
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere,SqlParameter[] parameters)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,SchoolId,Command,CommandId,ReleaseTime,FinishTime,FinishFlag,SchoolNum,SchoolName,SchoolConnectionString,SchoolDescribe,SchoolDTUip ");
            strSql.Append(" FROM View_CommandList ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString(), parameters);
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
			parameters[0].Value = "AMS_CommandList";
			parameters[1].Value = "ID";
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

