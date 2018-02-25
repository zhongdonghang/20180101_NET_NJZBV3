using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;//Please add references
namespace AMS.DAL
{
	/// <summary>
	/// 数据访问类:View_CallBackErrorInfo
	/// </summary>
	public partial class View_CallBackErrorInfo
	{
		public View_CallBackErrorInfo()
		{}
		#region  Method



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(AMS.Model.View_CallBackErrorInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into View_CallBackErrorInfo(");
			strSql.Append("SchoolNum,SchoolName,SchoolDTUip,SchoolDescribe,SchoolCon,SchoolLinkMan,SchoolLinkAddress,SchoolCardInfo,SchoolInterfaceInfo,FbPerson,FbTime,SolveTime,SolveWay,ProblemType,FbDescribe,ID,SolveLoginId,SolveUserPwd,SolveRemark,SolveUserName,SolveBranchName,MarkBrandName,MarkUserPwd,MarkLoginId,MarkRemark,MarkUserName)");
			strSql.Append(" values (");
			strSql.Append("@SchoolNum,@SchoolName,@SchoolDTUip,@SchoolDescribe,@SchoolCon,@SchoolLinkMan,@SchoolLinkAddress,@SchoolCardInfo,@SchoolInterfaceInfo,@FbPerson,@FbTime,@SolveTime,@SolveWay,@ProblemType,@FbDescribe,@ID,@SolveLoginId,@SolveUserPwd,@SolveRemark,@SolveUserName,@SolveBranchName,@MarkBrandName,@MarkUserPwd,@MarkLoginId,@MarkRemark,@MarkUserName)");
			SqlParameter[] parameters = {
					new SqlParameter("@SchoolNum", SqlDbType.NVarChar,50),
					new SqlParameter("@SchoolName", SqlDbType.NVarChar,30),
					new SqlParameter("@SchoolDTUip", SqlDbType.NVarChar,20),
					new SqlParameter("@SchoolDescribe", SqlDbType.NVarChar,200),
					new SqlParameter("@SchoolCon", SqlDbType.NVarChar,200),
					new SqlParameter("@SchoolLinkMan", SqlDbType.NVarChar,300),
					new SqlParameter("@SchoolLinkAddress", SqlDbType.NVarChar,300),
					new SqlParameter("@SchoolCardInfo", SqlDbType.Text),
					new SqlParameter("@SchoolInterfaceInfo", SqlDbType.Text),
					new SqlParameter("@FbPerson", SqlDbType.NVarChar,20),
					new SqlParameter("@FbTime", SqlDbType.DateTime),
					new SqlParameter("@SolveTime", SqlDbType.DateTime),
					new SqlParameter("@SolveWay", SqlDbType.NVarChar,500),
					new SqlParameter("@ProblemType", SqlDbType.Int,4),
					new SqlParameter("@FbDescribe", SqlDbType.NVarChar,500),
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@SolveLoginId", SqlDbType.NVarChar,30),
					new SqlParameter("@SolveUserPwd", SqlDbType.NVarChar,30),
					new SqlParameter("@SolveRemark", SqlDbType.NVarChar,50),
					new SqlParameter("@SolveUserName", SqlDbType.NVarChar,30),
					new SqlParameter("@SolveBranchName", SqlDbType.NVarChar,30),
					new SqlParameter("@MarkBrandName", SqlDbType.NVarChar,30),
					new SqlParameter("@MarkUserPwd", SqlDbType.NVarChar,30),
					new SqlParameter("@MarkLoginId", SqlDbType.NVarChar,30),
					new SqlParameter("@MarkRemark", SqlDbType.NVarChar,50),
					new SqlParameter("@MarkUserName", SqlDbType.NVarChar,30)};
			parameters[0].Value = model.SchoolNum;
			parameters[1].Value = model.SchoolName;
			parameters[2].Value = model.SchoolDTUip;
			parameters[3].Value = model.SchoolDescribe;
			parameters[4].Value = model.SchoolCon;
			parameters[5].Value = model.SchoolLinkMan;
			parameters[6].Value = model.SchoolLinkAddress;
			parameters[7].Value = model.SchoolCardInfo;
			parameters[8].Value = model.SchoolInterfaceInfo;
			parameters[9].Value = model.FbPerson;
			parameters[10].Value = model.FbTime;
			parameters[11].Value = model.SolveTime;
			parameters[12].Value = model.SolveWay;
			parameters[13].Value = model.ProblemType;
			parameters[14].Value = model.FbDescribe;
			parameters[15].Value = model.ID;
			parameters[16].Value = model.SolveLoginId;
			parameters[17].Value = model.SolveUserPwd;
			parameters[18].Value = model.SolveRemark;
			parameters[19].Value = model.SolveUserName;
			parameters[20].Value = model.SolveBranchName;
			parameters[21].Value = model.MarkBrandName;
			parameters[22].Value = model.MarkUserPwd;
			parameters[23].Value = model.MarkLoginId;
			parameters[24].Value = model.MarkRemark;
			parameters[25].Value = model.MarkUserName;

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
		public bool Update(AMS.Model.View_CallBackErrorInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update View_CallBackErrorInfo set ");
			strSql.Append("SchoolNum=@SchoolNum,");
			strSql.Append("SchoolName=@SchoolName,");
			strSql.Append("SchoolDTUip=@SchoolDTUip,");
			strSql.Append("SchoolDescribe=@SchoolDescribe,");
			strSql.Append("SchoolCon=@SchoolCon,");
			strSql.Append("SchoolLinkMan=@SchoolLinkMan,");
			strSql.Append("SchoolLinkAddress=@SchoolLinkAddress,");
			strSql.Append("SchoolCardInfo=@SchoolCardInfo,");
			strSql.Append("SchoolInterfaceInfo=@SchoolInterfaceInfo,");
			strSql.Append("FbPerson=@FbPerson,");
			strSql.Append("FbTime=@FbTime,");
			strSql.Append("SolveTime=@SolveTime,");
			strSql.Append("SolveWay=@SolveWay,");
			strSql.Append("ProblemType=@ProblemType,");
			strSql.Append("FbDescribe=@FbDescribe,");
			strSql.Append("ID=@ID,");
			strSql.Append("SolveLoginId=@SolveLoginId,");
			strSql.Append("SolveUserPwd=@SolveUserPwd,");
			strSql.Append("SolveRemark=@SolveRemark,");
			strSql.Append("SolveUserName=@SolveUserName,");
			strSql.Append("SolveBranchName=@SolveBranchName,");
			strSql.Append("MarkBrandName=@MarkBrandName,");
			strSql.Append("MarkUserPwd=@MarkUserPwd,");
			strSql.Append("MarkLoginId=@MarkLoginId,");
			strSql.Append("MarkRemark=@MarkRemark,");
			strSql.Append("MarkUserName=@MarkUserName");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
					new SqlParameter("@SchoolNum", SqlDbType.NVarChar,50),
					new SqlParameter("@SchoolName", SqlDbType.NVarChar,30),
					new SqlParameter("@SchoolDTUip", SqlDbType.NVarChar,20),
					new SqlParameter("@SchoolDescribe", SqlDbType.NVarChar,200),
					new SqlParameter("@SchoolCon", SqlDbType.NVarChar,200),
					new SqlParameter("@SchoolLinkMan", SqlDbType.NVarChar,300),
					new SqlParameter("@SchoolLinkAddress", SqlDbType.NVarChar,300),
					new SqlParameter("@SchoolCardInfo", SqlDbType.Text),
					new SqlParameter("@SchoolInterfaceInfo", SqlDbType.Text),
					new SqlParameter("@FbPerson", SqlDbType.NVarChar,20),
					new SqlParameter("@FbTime", SqlDbType.DateTime),
					new SqlParameter("@SolveTime", SqlDbType.DateTime),
					new SqlParameter("@SolveWay", SqlDbType.NVarChar,500),
					new SqlParameter("@ProblemType", SqlDbType.Int,4),
					new SqlParameter("@FbDescribe", SqlDbType.NVarChar,500),
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@SolveLoginId", SqlDbType.NVarChar,30),
					new SqlParameter("@SolveUserPwd", SqlDbType.NVarChar,30),
					new SqlParameter("@SolveRemark", SqlDbType.NVarChar,50),
					new SqlParameter("@SolveUserName", SqlDbType.NVarChar,30),
					new SqlParameter("@SolveBranchName", SqlDbType.NVarChar,30),
					new SqlParameter("@MarkBrandName", SqlDbType.NVarChar,30),
					new SqlParameter("@MarkUserPwd", SqlDbType.NVarChar,30),
					new SqlParameter("@MarkLoginId", SqlDbType.NVarChar,30),
					new SqlParameter("@MarkRemark", SqlDbType.NVarChar,50),
					new SqlParameter("@MarkUserName", SqlDbType.NVarChar,30)};
			parameters[0].Value = model.SchoolNum;
			parameters[1].Value = model.SchoolName;
			parameters[2].Value = model.SchoolDTUip;
			parameters[3].Value = model.SchoolDescribe;
			parameters[4].Value = model.SchoolCon;
			parameters[5].Value = model.SchoolLinkMan;
			parameters[6].Value = model.SchoolLinkAddress;
			parameters[7].Value = model.SchoolCardInfo;
			parameters[8].Value = model.SchoolInterfaceInfo;
			parameters[9].Value = model.FbPerson;
			parameters[10].Value = model.FbTime;
			parameters[11].Value = model.SolveTime;
			parameters[12].Value = model.SolveWay;
			parameters[13].Value = model.ProblemType;
			parameters[14].Value = model.FbDescribe;
			parameters[15].Value = model.ID;
			parameters[16].Value = model.SolveLoginId;
			parameters[17].Value = model.SolveUserPwd;
			parameters[18].Value = model.SolveRemark;
			parameters[19].Value = model.SolveUserName;
			parameters[20].Value = model.SolveBranchName;
			parameters[21].Value = model.MarkBrandName;
			parameters[22].Value = model.MarkUserPwd;
			parameters[23].Value = model.MarkLoginId;
			parameters[24].Value = model.MarkRemark;
			parameters[25].Value = model.MarkUserName;

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
			strSql.Append("delete from View_CallBackErrorInfo ");
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
		public AMS.Model.View_CallBackErrorInfo GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 SchoolNum,SchoolName,SchoolDTUip,SchoolDescribe,SchoolCon,SchoolLinkMan,SchoolLinkAddress,SchoolCardInfo,SchoolInterfaceInfo,FbPerson,FbTime,SolveTime,SolveWay,ProblemType,FbDescribe,ID,SolveLoginId,SolveUserPwd,SolveRemark,SolveUserName,SolveBranchName,MarkBrandName,MarkUserPwd,MarkLoginId,MarkRemark,MarkUserName from View_CallBackErrorInfo ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
};

			AMS.Model.View_CallBackErrorInfo model=new AMS.Model.View_CallBackErrorInfo();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["SchoolNum"]!=null && ds.Tables[0].Rows[0]["SchoolNum"].ToString()!="")
				{
					model.SchoolNum=ds.Tables[0].Rows[0]["SchoolNum"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SchoolName"]!=null && ds.Tables[0].Rows[0]["SchoolName"].ToString()!="")
				{
					model.SchoolName=ds.Tables[0].Rows[0]["SchoolName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SchoolDTUip"]!=null && ds.Tables[0].Rows[0]["SchoolDTUip"].ToString()!="")
				{
					model.SchoolDTUip=ds.Tables[0].Rows[0]["SchoolDTUip"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SchoolDescribe"]!=null && ds.Tables[0].Rows[0]["SchoolDescribe"].ToString()!="")
				{
					model.SchoolDescribe=ds.Tables[0].Rows[0]["SchoolDescribe"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SchoolCon"]!=null && ds.Tables[0].Rows[0]["SchoolCon"].ToString()!="")
				{
					model.SchoolCon=ds.Tables[0].Rows[0]["SchoolCon"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SchoolLinkMan"]!=null && ds.Tables[0].Rows[0]["SchoolLinkMan"].ToString()!="")
				{
					model.SchoolLinkMan=ds.Tables[0].Rows[0]["SchoolLinkMan"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SchoolLinkAddress"]!=null && ds.Tables[0].Rows[0]["SchoolLinkAddress"].ToString()!="")
				{
					model.SchoolLinkAddress=ds.Tables[0].Rows[0]["SchoolLinkAddress"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SchoolCardInfo"]!=null && ds.Tables[0].Rows[0]["SchoolCardInfo"].ToString()!="")
				{
					model.SchoolCardInfo=ds.Tables[0].Rows[0]["SchoolCardInfo"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SchoolInterfaceInfo"]!=null && ds.Tables[0].Rows[0]["SchoolInterfaceInfo"].ToString()!="")
				{
					model.SchoolInterfaceInfo=ds.Tables[0].Rows[0]["SchoolInterfaceInfo"].ToString();
				}
				if(ds.Tables[0].Rows[0]["FbPerson"]!=null && ds.Tables[0].Rows[0]["FbPerson"].ToString()!="")
				{
					model.FbPerson=ds.Tables[0].Rows[0]["FbPerson"].ToString();
				}
				if(ds.Tables[0].Rows[0]["FbTime"]!=null && ds.Tables[0].Rows[0]["FbTime"].ToString()!="")
				{
					model.FbTime=DateTime.Parse(ds.Tables[0].Rows[0]["FbTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SolveTime"]!=null && ds.Tables[0].Rows[0]["SolveTime"].ToString()!="")
				{
					model.SolveTime=DateTime.Parse(ds.Tables[0].Rows[0]["SolveTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SolveWay"]!=null && ds.Tables[0].Rows[0]["SolveWay"].ToString()!="")
				{
					model.SolveWay=ds.Tables[0].Rows[0]["SolveWay"].ToString();
				}
				if(ds.Tables[0].Rows[0]["ProblemType"]!=null && ds.Tables[0].Rows[0]["ProblemType"].ToString()!="")
				{
					model.ProblemType=int.Parse(ds.Tables[0].Rows[0]["ProblemType"].ToString());
				}
				if(ds.Tables[0].Rows[0]["FbDescribe"]!=null && ds.Tables[0].Rows[0]["FbDescribe"].ToString()!="")
				{
					model.FbDescribe=ds.Tables[0].Rows[0]["FbDescribe"].ToString();
				}
				if(ds.Tables[0].Rows[0]["ID"]!=null && ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SolveLoginId"]!=null && ds.Tables[0].Rows[0]["SolveLoginId"].ToString()!="")
				{
					model.SolveLoginId=ds.Tables[0].Rows[0]["SolveLoginId"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SolveUserPwd"]!=null && ds.Tables[0].Rows[0]["SolveUserPwd"].ToString()!="")
				{
					model.SolveUserPwd=ds.Tables[0].Rows[0]["SolveUserPwd"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SolveRemark"]!=null && ds.Tables[0].Rows[0]["SolveRemark"].ToString()!="")
				{
					model.SolveRemark=ds.Tables[0].Rows[0]["SolveRemark"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SolveUserName"]!=null && ds.Tables[0].Rows[0]["SolveUserName"].ToString()!="")
				{
					model.SolveUserName=ds.Tables[0].Rows[0]["SolveUserName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SolveBranchName"]!=null && ds.Tables[0].Rows[0]["SolveBranchName"].ToString()!="")
				{
					model.SolveBranchName=ds.Tables[0].Rows[0]["SolveBranchName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["MarkBrandName"]!=null && ds.Tables[0].Rows[0]["MarkBrandName"].ToString()!="")
				{
					model.MarkBrandName=ds.Tables[0].Rows[0]["MarkBrandName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["MarkUserPwd"]!=null && ds.Tables[0].Rows[0]["MarkUserPwd"].ToString()!="")
				{
					model.MarkUserPwd=ds.Tables[0].Rows[0]["MarkUserPwd"].ToString();
				}
				if(ds.Tables[0].Rows[0]["MarkLoginId"]!=null && ds.Tables[0].Rows[0]["MarkLoginId"].ToString()!="")
				{
					model.MarkLoginId=ds.Tables[0].Rows[0]["MarkLoginId"].ToString();
				}
				if(ds.Tables[0].Rows[0]["MarkRemark"]!=null && ds.Tables[0].Rows[0]["MarkRemark"].ToString()!="")
				{
					model.MarkRemark=ds.Tables[0].Rows[0]["MarkRemark"].ToString();
				}
				if(ds.Tables[0].Rows[0]["MarkUserName"]!=null && ds.Tables[0].Rows[0]["MarkUserName"].ToString()!="")
				{
					model.MarkUserName=ds.Tables[0].Rows[0]["MarkUserName"].ToString();
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
            strSql.Append("select SchoolNum,SchoolName,SchoolDTUip,SchoolDescribe,SchoolCon,SchoolLinkMan,SchoolLinkAddress,SchoolCardInfo,SchoolInterfaceInfo,FbPerson,FbTime,SolveTime,SolveWay,ProblemType,FbDescribe,ID,SolveLoginId,SolveUserPwd,SolveRemark,SolveUserName,SolveBranchName,MarkBrandName,MarkUserPwd,MarkLoginId,MarkRemark,MarkUserName,Status,SolveManID,MarkManID,SchoolId ");
			strSql.Append(" FROM View_CallBackErrorInfo ");
            if (!string.IsNullOrEmpty(strWhere))
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
			strSql.Append(" SchoolNum,SchoolName,SchoolDTUip,SchoolDescribe,SchoolCon,SchoolLinkMan,SchoolLinkAddress,SchoolCardInfo,SchoolInterfaceInfo,FbPerson,FbTime,SolveTime,SolveWay,ProblemType,FbDescribe,ID,SolveLoginId,SolveUserPwd,SolveRemark,SolveUserName,SolveBranchName,MarkBrandName,MarkUserPwd,MarkLoginId,MarkRemark,MarkUserName ");
			strSql.Append(" FROM View_CallBackErrorInfo ");
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
			parameters[0].Value = "View_CallBackErrorInfo";
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

