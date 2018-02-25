using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;//Please add references
namespace AMS.DAL
{
	/// <summary>
	/// 数据访问类:View_CommandList
	/// </summary>
	public partial class View_CommandList
	{
		public View_CommandList()
		{}
		#region  Method



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(AMS.Model.View_CommandList model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into View_CommandList(");
			strSql.Append("ID,Command,CommandId,ReleaseTime,FinishTime,FinishFlag,SchoolNum,SchoolName,SchoolConnectionString,SchoolDescribe,SchoolDTUip,SchoolLinkMan,SchoolAddress,SchoolProvince,SchoolCardInfo,SchoolInterfaceInfo,OperatorLoginId,OperatorPwd,OperatorBranchName,OperatorName,OperatorRemark)");
			strSql.Append(" values (");
			strSql.Append("@ID,@Command,@CommandId,@ReleaseTime,@FinishTime,@FinishFlag,@SchoolNum,@SchoolName,@SchoolConnectionString,@SchoolDescribe,@SchoolDTUip,@SchoolLinkMan,@SchoolAddress,@SchoolProvince,@SchoolCardInfo,@SchoolInterfaceInfo,@OperatorLoginId,@OperatorPwd,@OperatorBranchName,@OperatorName,@OperatorRemark)");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@Command", SqlDbType.Int,4),
					new SqlParameter("@CommandId", SqlDbType.Int,4),
					new SqlParameter("@ReleaseTime", SqlDbType.DateTime),
					new SqlParameter("@FinishTime", SqlDbType.DateTime),
					new SqlParameter("@FinishFlag", SqlDbType.Int,4),
					new SqlParameter("@SchoolNum", SqlDbType.NVarChar,50),
					new SqlParameter("@SchoolName", SqlDbType.NVarChar,30),
					new SqlParameter("@SchoolConnectionString", SqlDbType.NVarChar,200),
					new SqlParameter("@SchoolDescribe", SqlDbType.NVarChar,200),
					new SqlParameter("@SchoolDTUip", SqlDbType.NVarChar,20),
					new SqlParameter("@SchoolLinkMan", SqlDbType.NVarChar,300),
					new SqlParameter("@SchoolAddress", SqlDbType.NVarChar,300),
					new SqlParameter("@SchoolProvince", SqlDbType.Int,4),
					new SqlParameter("@SchoolCardInfo", SqlDbType.Text),
					new SqlParameter("@SchoolInterfaceInfo", SqlDbType.Text),
					new SqlParameter("@OperatorLoginId", SqlDbType.NVarChar,30),
					new SqlParameter("@OperatorPwd", SqlDbType.NVarChar,30),
					new SqlParameter("@OperatorBranchName", SqlDbType.NVarChar,30),
					new SqlParameter("@OperatorName", SqlDbType.NVarChar,30),
					new SqlParameter("@OperatorRemark", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.Command;
			parameters[2].Value = model.CommandId;
			parameters[3].Value = model.ReleaseTime;
			parameters[4].Value = model.FinishTime;
			parameters[5].Value = model.FinishFlag;
			parameters[6].Value = model.SchoolNum;
			parameters[7].Value = model.SchoolName;
			parameters[8].Value = model.SchoolConnectionString;
			parameters[9].Value = model.SchoolDescribe;
			parameters[10].Value = model.SchoolDTUip;
			parameters[11].Value = model.SchoolLinkMan;
			parameters[12].Value = model.SchoolAddress;
			parameters[13].Value = model.SchoolProvince;
			parameters[14].Value = model.SchoolCardInfo;
			parameters[15].Value = model.SchoolInterfaceInfo;
			parameters[16].Value = model.OperatorLoginId;
			parameters[17].Value = model.OperatorPwd;
			parameters[18].Value = model.OperatorBranchName;
			parameters[19].Value = model.OperatorName;
			parameters[20].Value = model.OperatorRemark;

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
		public bool Update(AMS.Model.View_CommandList model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update View_CommandList set ");
			strSql.Append("ID=@ID,");
			strSql.Append("Command=@Command,");
			strSql.Append("CommandId=@CommandId,");
			strSql.Append("ReleaseTime=@ReleaseTime,");
			strSql.Append("FinishTime=@FinishTime,");
			strSql.Append("FinishFlag=@FinishFlag,");
			strSql.Append("SchoolNum=@SchoolNum,");
			strSql.Append("SchoolName=@SchoolName,");
			strSql.Append("SchoolConnectionString=@SchoolConnectionString,");
			strSql.Append("SchoolDescribe=@SchoolDescribe,");
			strSql.Append("SchoolDTUip=@SchoolDTUip,");
			strSql.Append("SchoolLinkMan=@SchoolLinkMan,");
			strSql.Append("SchoolAddress=@SchoolAddress,");
			strSql.Append("SchoolProvince=@SchoolProvince,");
			strSql.Append("SchoolCardInfo=@SchoolCardInfo,");
			strSql.Append("SchoolInterfaceInfo=@SchoolInterfaceInfo,");
			strSql.Append("OperatorLoginId=@OperatorLoginId,");
			strSql.Append("OperatorPwd=@OperatorPwd,");
			strSql.Append("OperatorBranchName=@OperatorBranchName,");
			strSql.Append("OperatorName=@OperatorName,");
			strSql.Append("OperatorRemark=@OperatorRemark");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@Command", SqlDbType.Int,4),
					new SqlParameter("@CommandId", SqlDbType.Int,4),
					new SqlParameter("@ReleaseTime", SqlDbType.DateTime),
					new SqlParameter("@FinishTime", SqlDbType.DateTime),
					new SqlParameter("@FinishFlag", SqlDbType.Int,4),
					new SqlParameter("@SchoolNum", SqlDbType.NVarChar,50),
					new SqlParameter("@SchoolName", SqlDbType.NVarChar,30),
					new SqlParameter("@SchoolConnectionString", SqlDbType.NVarChar,200),
					new SqlParameter("@SchoolDescribe", SqlDbType.NVarChar,200),
					new SqlParameter("@SchoolDTUip", SqlDbType.NVarChar,20),
					new SqlParameter("@SchoolLinkMan", SqlDbType.NVarChar,300),
					new SqlParameter("@SchoolAddress", SqlDbType.NVarChar,300),
					new SqlParameter("@SchoolProvince", SqlDbType.Int,4),
					new SqlParameter("@SchoolCardInfo", SqlDbType.Text),
					new SqlParameter("@SchoolInterfaceInfo", SqlDbType.Text),
					new SqlParameter("@OperatorLoginId", SqlDbType.NVarChar,30),
					new SqlParameter("@OperatorPwd", SqlDbType.NVarChar,30),
					new SqlParameter("@OperatorBranchName", SqlDbType.NVarChar,30),
					new SqlParameter("@OperatorName", SqlDbType.NVarChar,30),
					new SqlParameter("@OperatorRemark", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.Command;
			parameters[2].Value = model.CommandId;
			parameters[3].Value = model.ReleaseTime;
			parameters[4].Value = model.FinishTime;
			parameters[5].Value = model.FinishFlag;
			parameters[6].Value = model.SchoolNum;
			parameters[7].Value = model.SchoolName;
			parameters[8].Value = model.SchoolConnectionString;
			parameters[9].Value = model.SchoolDescribe;
			parameters[10].Value = model.SchoolDTUip;
			parameters[11].Value = model.SchoolLinkMan;
			parameters[12].Value = model.SchoolAddress;
			parameters[13].Value = model.SchoolProvince;
			parameters[14].Value = model.SchoolCardInfo;
			parameters[15].Value = model.SchoolInterfaceInfo;
			parameters[16].Value = model.OperatorLoginId;
			parameters[17].Value = model.OperatorPwd;
			parameters[18].Value = model.OperatorBranchName;
			parameters[19].Value = model.OperatorName;
			parameters[20].Value = model.OperatorRemark;

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
			strSql.Append("delete from View_CommandList ");
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
		public AMS.Model.View_CommandList GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,Command,CommandId,ReleaseTime,FinishTime,FinishFlag,SchoolNum,SchoolName,SchoolConnectionString,SchoolDescribe,SchoolDTUip,SchoolLinkMan,SchoolAddress,SchoolProvince,SchoolCardInfo,SchoolInterfaceInfo,OperatorLoginId,OperatorPwd,OperatorBranchName,OperatorName,OperatorRemark from View_CommandList ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
};

			AMS.Model.View_CommandList model=new AMS.Model.View_CommandList();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"]!=null && ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Command"]!=null && ds.Tables[0].Rows[0]["Command"].ToString()!="")
				{
                    model.Command = (AMS.Model.Enum.CommandType)int.Parse(ds.Tables[0].Rows[0]["Command"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CommandId"]!=null && ds.Tables[0].Rows[0]["CommandId"].ToString()!="")
				{
					model.CommandId=int.Parse(ds.Tables[0].Rows[0]["CommandId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ReleaseTime"]!=null && ds.Tables[0].Rows[0]["ReleaseTime"].ToString()!="")
				{
					model.ReleaseTime=DateTime.Parse(ds.Tables[0].Rows[0]["ReleaseTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["FinishTime"]!=null && ds.Tables[0].Rows[0]["FinishTime"].ToString()!="")
				{
					model.FinishTime=DateTime.Parse(ds.Tables[0].Rows[0]["FinishTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["FinishFlag"]!=null && ds.Tables[0].Rows[0]["FinishFlag"].ToString()!="")
				{
					model.FinishFlag=int.Parse(ds.Tables[0].Rows[0]["FinishFlag"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SchoolNum"]!=null && ds.Tables[0].Rows[0]["SchoolNum"].ToString()!="")
				{
					model.SchoolNum=ds.Tables[0].Rows[0]["SchoolNum"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SchoolName"]!=null && ds.Tables[0].Rows[0]["SchoolName"].ToString()!="")
				{
					model.SchoolName=ds.Tables[0].Rows[0]["SchoolName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SchoolConnectionString"]!=null && ds.Tables[0].Rows[0]["SchoolConnectionString"].ToString()!="")
				{
					model.SchoolConnectionString=ds.Tables[0].Rows[0]["SchoolConnectionString"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SchoolDescribe"]!=null && ds.Tables[0].Rows[0]["SchoolDescribe"].ToString()!="")
				{
					model.SchoolDescribe=ds.Tables[0].Rows[0]["SchoolDescribe"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SchoolDTUip"]!=null && ds.Tables[0].Rows[0]["SchoolDTUip"].ToString()!="")
				{
					model.SchoolDTUip=ds.Tables[0].Rows[0]["SchoolDTUip"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SchoolLinkMan"]!=null && ds.Tables[0].Rows[0]["SchoolLinkMan"].ToString()!="")
				{
					model.SchoolLinkMan=ds.Tables[0].Rows[0]["SchoolLinkMan"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SchoolAddress"]!=null && ds.Tables[0].Rows[0]["SchoolAddress"].ToString()!="")
				{
					model.SchoolAddress=ds.Tables[0].Rows[0]["SchoolAddress"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SchoolProvince"]!=null && ds.Tables[0].Rows[0]["SchoolProvince"].ToString()!="")
				{
					model.SchoolProvince=int.Parse(ds.Tables[0].Rows[0]["SchoolProvince"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SchoolCardInfo"]!=null && ds.Tables[0].Rows[0]["SchoolCardInfo"].ToString()!="")
				{
					model.SchoolCardInfo=ds.Tables[0].Rows[0]["SchoolCardInfo"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SchoolInterfaceInfo"]!=null && ds.Tables[0].Rows[0]["SchoolInterfaceInfo"].ToString()!="")
				{
					model.SchoolInterfaceInfo=ds.Tables[0].Rows[0]["SchoolInterfaceInfo"].ToString();
				}
				if(ds.Tables[0].Rows[0]["OperatorLoginId"]!=null && ds.Tables[0].Rows[0]["OperatorLoginId"].ToString()!="")
				{
					model.OperatorLoginId=ds.Tables[0].Rows[0]["OperatorLoginId"].ToString();
				}
				if(ds.Tables[0].Rows[0]["OperatorPwd"]!=null && ds.Tables[0].Rows[0]["OperatorPwd"].ToString()!="")
				{
					model.OperatorPwd=ds.Tables[0].Rows[0]["OperatorPwd"].ToString();
				}
				if(ds.Tables[0].Rows[0]["OperatorBranchName"]!=null && ds.Tables[0].Rows[0]["OperatorBranchName"].ToString()!="")
				{
					model.OperatorBranchName=ds.Tables[0].Rows[0]["OperatorBranchName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["OperatorName"]!=null && ds.Tables[0].Rows[0]["OperatorName"].ToString()!="")
				{
					model.OperatorName=ds.Tables[0].Rows[0]["OperatorName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["OperatorRemark"]!=null && ds.Tables[0].Rows[0]["OperatorRemark"].ToString()!="")
				{
					model.OperatorRemark=ds.Tables[0].Rows[0]["OperatorRemark"].ToString();
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
            strSql.Append("select ID,Command,CommandId,ReleaseTime,FinishTime,FinishFlag,SchoolNum,SchoolName,SchoolConnectionString,SchoolDescribe,SchoolDTUip,SchoolLinkMan,SchoolAddress,SchoolProvince,SchoolCardInfo,SchoolInterfaceInfo,OperatorLoginId,OperatorPwd,OperatorBranchName,OperatorName,OperatorRemark,SchoolId ");
			strSql.Append(" FROM View_CommandList ");
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
			strSql.Append(" ID,Command,CommandId,ReleaseTime,FinishTime,FinishFlag,SchoolNum,SchoolName,SchoolConnectionString,SchoolDescribe,SchoolDTUip,SchoolLinkMan,SchoolAddress,SchoolProvince,SchoolCardInfo,SchoolInterfaceInfo,OperatorLoginId,OperatorPwd,OperatorBranchName,OperatorName,OperatorRemark ");
			strSql.Append(" FROM View_CommandList ");
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
			parameters[0].Value = "View_CommandList";
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

