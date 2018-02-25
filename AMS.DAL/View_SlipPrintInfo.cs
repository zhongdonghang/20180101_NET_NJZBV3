using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;//Please add references
namespace AMS.DAL
{
	/// <summary>
	/// 数据访问类:View_SlipPrintInfo
	/// </summary>
	public partial class View_SlipPrintInfo
	{
		public View_SlipPrintInfo()
		{}
		#region  Method



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(AMS.Model.View_SlipPrintInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into View_SlipPrintInfo(");
			strSql.Append("SlipCustomerId,SlipCustomerNum,Date,PrintAmount,LookOverAmount,SchoolNumber,SchoolName,CampusId,CampusNum,CampusName,SlipName)");
			strSql.Append(" values (");
			strSql.Append("@SlipCustomerId,@SlipCustomerNum,@Date,@PrintAmount,@LookOverAmount,@SchoolNumber,@SchoolName,@CampusId,@CampusNum,@CampusName,@SlipName)");
			SqlParameter[] parameters = {
					new SqlParameter("@SlipCustomerId", SqlDbType.Int,4),
					new SqlParameter("@SlipCustomerNum", SqlDbType.NVarChar,50),
					new SqlParameter("@Date", SqlDbType.DateTime),
					new SqlParameter("@PrintAmount", SqlDbType.Int,4),
					new SqlParameter("@LookOverAmount", SqlDbType.Int,4),
					new SqlParameter("@SchoolNumber", SqlDbType.NVarChar,50),
					new SqlParameter("@SchoolName", SqlDbType.NVarChar,30),
					new SqlParameter("@CampusId", SqlDbType.Int,4),
					new SqlParameter("@CampusNum", SqlDbType.NVarChar,50),
					new SqlParameter("@CampusName", SqlDbType.NVarChar,50),
					new SqlParameter("@SlipName", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.SlipCustomerId;
			parameters[1].Value = model.SlipCustomerNum;
			parameters[2].Value = model.Date;
			parameters[3].Value = model.PrintAmount;
			parameters[4].Value = model.LookOverAmount;
			parameters[5].Value = model.SchoolNumber;
			parameters[6].Value = model.SchoolName;
			parameters[7].Value = model.CampusId;
			parameters[8].Value = model.CampusNum;
			parameters[9].Value = model.CampusName;
			parameters[10].Value = model.SlipName;

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
		/// 更新一条数据
		/// </summary>
		public bool Update(AMS.Model.View_SlipPrintInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update View_SlipPrintInfo set ");
			strSql.Append("SlipCustomerId=@SlipCustomerId,");
			strSql.Append("SlipCustomerNum=@SlipCustomerNum,");
			strSql.Append("Date=@Date,");
			strSql.Append("PrintAmount=@PrintAmount,");
			strSql.Append("LookOverAmount=@LookOverAmount,");
			strSql.Append("SchoolNumber=@SchoolNumber,");
			strSql.Append("SchoolName=@SchoolName,");
			strSql.Append("CampusId=@CampusId,");
			strSql.Append("CampusNum=@CampusNum,");
			strSql.Append("CampusName=@CampusName,");
			strSql.Append("SlipName=@SlipName");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
					new SqlParameter("@SlipCustomerId", SqlDbType.Int,4),
					new SqlParameter("@SlipCustomerNum", SqlDbType.NVarChar,50),
					new SqlParameter("@Date", SqlDbType.DateTime),
					new SqlParameter("@PrintAmount", SqlDbType.Int,4),
					new SqlParameter("@LookOverAmount", SqlDbType.Int,4),
					new SqlParameter("@SchoolNumber", SqlDbType.NVarChar,50),
					new SqlParameter("@SchoolName", SqlDbType.NVarChar,30),
					new SqlParameter("@CampusId", SqlDbType.Int,4),
					new SqlParameter("@CampusNum", SqlDbType.NVarChar,50),
					new SqlParameter("@CampusName", SqlDbType.NVarChar,50),
					new SqlParameter("@SlipName", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.SlipCustomerId;
			parameters[1].Value = model.SlipCustomerNum;
			parameters[2].Value = model.Date;
			parameters[3].Value = model.PrintAmount;
			parameters[4].Value = model.LookOverAmount;
			parameters[5].Value = model.SchoolNumber;
			parameters[6].Value = model.SchoolName;
			parameters[7].Value = model.CampusId;
			parameters[8].Value = model.CampusNum;
			parameters[9].Value = model.CampusName;
			parameters[10].Value = model.SlipName;

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
		public bool Delete()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from View_SlipPrintInfo ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
};

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
		/// 得到一个对象实体
		/// </summary>
		public AMS.Model.View_SlipPrintInfo GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 SlipCustomerId,SlipCustomerNum,Date,PrintAmount,LookOverAmount,SchoolNumber,SchoolName,CampusId,CampusNum,CampusName,SlipName from View_SlipPrintInfo ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
};

			AMS.Model.View_SlipPrintInfo model=new AMS.Model.View_SlipPrintInfo();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["SlipCustomerId"]!=null && ds.Tables[0].Rows[0]["SlipCustomerId"].ToString()!="")
				{
					model.SlipCustomerId=int.Parse(ds.Tables[0].Rows[0]["SlipCustomerId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SlipCustomerNum"]!=null && ds.Tables[0].Rows[0]["SlipCustomerNum"].ToString()!="")
				{
					model.SlipCustomerNum=ds.Tables[0].Rows[0]["SlipCustomerNum"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Date"]!=null && ds.Tables[0].Rows[0]["Date"].ToString()!="")
				{
					model.Date=DateTime.Parse(ds.Tables[0].Rows[0]["Date"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PrintAmount"]!=null && ds.Tables[0].Rows[0]["PrintAmount"].ToString()!="")
				{
					model.PrintAmount=int.Parse(ds.Tables[0].Rows[0]["PrintAmount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["LookOverAmount"]!=null && ds.Tables[0].Rows[0]["LookOverAmount"].ToString()!="")
				{
					model.LookOverAmount=int.Parse(ds.Tables[0].Rows[0]["LookOverAmount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SchoolNumber"]!=null && ds.Tables[0].Rows[0]["SchoolNumber"].ToString()!="")
				{
					model.SchoolNumber=ds.Tables[0].Rows[0]["SchoolNumber"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SchoolName"]!=null && ds.Tables[0].Rows[0]["SchoolName"].ToString()!="")
				{
					model.SchoolName=ds.Tables[0].Rows[0]["SchoolName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["CampusId"]!=null && ds.Tables[0].Rows[0]["CampusId"].ToString()!="")
				{
					model.CampusId=int.Parse(ds.Tables[0].Rows[0]["CampusId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CampusNum"]!=null && ds.Tables[0].Rows[0]["CampusNum"].ToString()!="")
				{
					model.CampusNum=ds.Tables[0].Rows[0]["CampusNum"].ToString();
				}
				if(ds.Tables[0].Rows[0]["CampusName"]!=null && ds.Tables[0].Rows[0]["CampusName"].ToString()!="")
				{
					model.CampusName=ds.Tables[0].Rows[0]["CampusName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SlipName"]!=null && ds.Tables[0].Rows[0]["SlipName"].ToString()!="")
				{
					model.SlipName=ds.Tables[0].Rows[0]["SlipName"].ToString();
				}
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
			strSql.Append("select SlipCustomerId,SlipCustomerNum,Date,PrintAmount,LookOverAmount,SchoolNumber,SchoolName,CampusId,CampusNum,CampusName,SlipName ");
			strSql.Append(" FROM View_SlipPrintInfo ");
			if(strWhere.Trim()!="")
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
			strSql.Append(" SlipCustomerId,SlipCustomerNum,Date,PrintAmount,LookOverAmount,SchoolNumber,SchoolName,CampusId,CampusNum,CampusName,SlipName ");
			strSql.Append(" FROM View_SlipPrintInfo ");
			if(strWhere.Trim()!="")
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
			parameters[0].Value = "View_SlipPrintInfo";
			parameters[1].Value = "id";
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

