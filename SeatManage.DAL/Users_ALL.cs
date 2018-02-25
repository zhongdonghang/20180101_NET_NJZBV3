using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;
using SeatManage.ClassModel;
using SeatManage.EnumType;
namespace SeatManage.DAL
{
	/// <summary>
	/// 数据访问类:Users_ALL
	/// </summary>
	public partial class Users_ALL
	{
		public Users_ALL()
		{}
		#region  Method

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere,SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select LoginID,UsrName,UsrPwd,UsrEnabled,UsrType,Remark,IPLockIPAdress ");
            strSql.Append(" FROM Users_ALL ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString(),parameters);
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder,SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" select LoginID,UsrName,UsrPwd,UsrEnabled,UsrType,Remark,IPLockIPAdress ");
            strSql.Append(" FROM Users_ALL ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 添加新的用户并且授权。
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public HandleResult Add(SeatManage.ClassModel.UserInfo model)
        {
            SqlParameter[] parameters = new SqlParameter[7]{
                                        new SqlParameter("@UsrName",model.UserName),
                                        new SqlParameter("@LoginID",model.LoginId),
                                        new SqlParameter("@UsrPwd",model.Password),
                                        new SqlParameter("@UsrEnabled",(int)model.IsUsing),
                                        new SqlParameter("@UsrType",(int)model.UserType), 
                                        new SqlParameter("@Remark",model.Remark), 
                                        new SqlParameter("@ExcResult",SqlDbType.Int)
                                        };
            parameters[6].Direction = ParameterDirection.Output;
            if (string.IsNullOrEmpty(model.UserName))
            {
                parameters[0].SqlValue = DBNull.Value;
            }
            DbHelperSQL.Execute_Proc("[Proc_AddUserAll]", parameters);
            string id = parameters[1].Value.ToString();
            if (string.IsNullOrEmpty(id))
            {
                return HandleResult.Failed;
            }
            else
            {
                return HandleResult.Successed;
            }
        }
        /// <summary>
        /// 更新读者信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(SeatManage.ClassModel.UserInfo model)
        {
            SqlParameter[] parameters = new SqlParameter[7]{
                                        new SqlParameter("@UsrName",model.UserName),
                                        new SqlParameter("@LoginID",model.LoginId),
                                        new SqlParameter("@UsrPwd",model.Password),
                                        new SqlParameter("@UsrEnabled",(int)model.IsUsing),
                                        new SqlParameter("@UsrType",(int)model.UserType) ,
                                        new SqlParameter("@Remark",model.Remark),
                                        new SqlParameter("@IPLockIPAdress",model.LockIPAdress)
                                        };
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Users_ALL ");
            strSql.Append(" set UsrName=@UsrName,UsrPwd=@UsrPwd,LoginID=@LoginID  ,UsrEnabled=@UsrEnabled, UsrType=@UsrType,Remark=@Remark,IPLockIPAdress=@IPLockIPAdress ");
            strSql.Append(" where LoginID=@LoginID");
            try
            {
                int i = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception Ex)
            {
                throw Ex;
            }
        }
        /// <summary>
        /// 删除读者信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool delete(SeatManage.ClassModel.UserInfo model)
        {
            SqlParameter[] parameters = new SqlParameter[1]{
                                        new SqlParameter("@LoginID",model.LoginId)};
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete Users_ALL ");
            strSql.Append(" where LoginID=@LoginID");
            try
            {
                int i = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        /// <summary>
        /// 获取读者角色
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetUserRoleList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select LoginID, UsrName, UsrPwd, UsrEnabled, Remark, RoleID, IsLock, ROLENAME, UserType ");
            strSql.Append(" FROM ViewUserRole ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }
            try
            {
                return DbHelperSQL.Query(strSql.ToString());
            }
            catch(Exception e)
            {
                throw e;
            }
        }

//        /// <summary>
//        /// 增加一条数据
//        /// </summary>
//        public bool Add(SeatManage.Model.Users_ALL model)
//        {
//            StringBuilder strSql=new StringBuilder();
//            strSql.Append("insert into Users_ALL(");
//            strSql.Append("UserID,CONO,DepNo,UserNo,UsrName,UsrPwd,LoginID,NickName,UsrSex,Birthday,Nation,NativePlace,Postalcode,UsrAddr,UsrPic,UsrEMail,UsrCardID,MobileTel,UsrTelNum,UsrFaxNum,UsrExt,UsrSignPic,IsDel,UsrEnabled,UserState,OrderSeq,GradeNo,UsrType,Remark,LogIp,IsDefault)");
//            strSql.Append(" values (");
//            strSql.Append("@UserID,@CONO,@DepNo,@UserNo,@UsrName,@UsrPwd,@LoginID,@NickName,@UsrSex,@Birthday,@Nation,@NativePlace,@Postalcode,@UsrAddr,@UsrPic,@UsrEMail,@UsrCardID,@MobileTel,@UsrTelNum,@UsrFaxNum,@UsrExt,@UsrSignPic,@IsDel,@UsrEnabled,@UserState,@OrderSeq,@GradeNo,@UsrType,@Remark,@LogIp,@IsDefault)");
//            SqlParameter[] parameters = {
//                    new SqlParameter("@UserID", SqlDbType.Int,4),
//                    new SqlParameter("@CONO", SqlDbType.VarChar,10),
//                    new SqlParameter("@DepNo", SqlDbType.VarChar,10),
//                    new SqlParameter("@UserNo", SqlDbType.VarChar,20),
//                    new SqlParameter("@UsrName", SqlDbType.VarChar,60),
//                    new SqlParameter("@UsrPwd", SqlDbType.VarChar,500),
//                    new SqlParameter("@LoginID", SqlDbType.VarChar,60),
//                    new SqlParameter("@NickName", SqlDbType.VarChar,255),
//                    new SqlParameter("@UsrSex", SqlDbType.Char,1),
//                    new SqlParameter("@Birthday", SqlDbType.DateTime),
//                    new SqlParameter("@Nation", SqlDbType.VarChar,50),
//                    new SqlParameter("@NativePlace", SqlDbType.VarChar,255),
//                    new SqlParameter("@Postalcode", SqlDbType.Char,10),
//                    new SqlParameter("@UsrAddr", SqlDbType.VarChar,255),
//                    new SqlParameter("@UsrPic", SqlDbType.Image),
//                    new SqlParameter("@UsrEMail", SqlDbType.VarChar,255),
//                    new SqlParameter("@UsrCardID", SqlDbType.VarChar,20),
//                    new SqlParameter("@MobileTel", SqlDbType.Char,20),
//                    new SqlParameter("@UsrTelNum", SqlDbType.VarChar,60),
//                    new SqlParameter("@UsrFaxNum", SqlDbType.VarChar,20),
//                    new SqlParameter("@UsrExt", SqlDbType.VarChar,30),
//                    new SqlParameter("@UsrSignPic", SqlDbType.Image),
//                    new SqlParameter("@IsDel", SqlDbType.Char,1),
//                    new SqlParameter("@UsrEnabled", SqlDbType.Char,1),
//                    new SqlParameter("@UserState", SqlDbType.Char,1),
//                    new SqlParameter("@OrderSeq", SqlDbType.VarChar,10),
//                    new SqlParameter("@GradeNo", SqlDbType.VarChar,10),
//                    new SqlParameter("@UsrType", SqlDbType.Char,1),
//                    new SqlParameter("@Remark", SqlDbType.VarChar,255),
//                    new SqlParameter("@LogIp", SqlDbType.VarChar,25),
//                    new SqlParameter("@IsDefault", SqlDbType.Char,1)};
//            parameters[0].Value = model.UserID;
//            parameters[1].Value = model.CONO;
//            parameters[2].Value = model.DepNo;
//            parameters[3].Value = model.UserNo;
//            parameters[4].Value = model.UsrName;
//            parameters[5].Value = model.UsrPwd;
//            parameters[6].Value = model.LoginID;
//            parameters[7].Value = model.NickName;
//            parameters[8].Value = model.UsrSex;
//            parameters[9].Value = model.Birthday;
//            parameters[10].Value = model.Nation;
//            parameters[11].Value = model.NativePlace;
//            parameters[12].Value = model.Postalcode;
//            parameters[13].Value = model.UsrAddr;
//            parameters[14].Value = model.UsrPic;
//            parameters[15].Value = model.UsrEMail;
//            parameters[16].Value = model.UsrCardID;
//            parameters[17].Value = model.MobileTel;
//            parameters[18].Value = model.UsrTelNum;
//            parameters[19].Value = model.UsrFaxNum;
//            parameters[20].Value = model.UsrExt;
//            parameters[21].Value = model.UsrSignPic;
//            parameters[22].Value = model.IsDel;
//            parameters[23].Value = model.UsrEnabled;
//            parameters[24].Value = model.UserState;
//            parameters[25].Value = model.OrderSeq;
//            parameters[26].Value = model.GradeNo;
//            parameters[27].Value = model.UsrType;
//            parameters[28].Value = model.Remark;
//            parameters[29].Value = model.LogIp;
//            parameters[30].Value = model.IsDefault;

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
//        public bool Update(SeatManage.Model.Users_ALL model)
//        {
//            StringBuilder strSql=new StringBuilder();
//            strSql.Append("update Users_ALL set ");
//            strSql.Append("UserID=@UserID,");
//            strSql.Append("CONO=@CONO,");
//            strSql.Append("DepNo=@DepNo,");
//            strSql.Append("UserNo=@UserNo,");
//            strSql.Append("UsrName=@UsrName,");
//            strSql.Append("UsrPwd=@UsrPwd,");
//            strSql.Append("LoginID=@LoginID,");
//            strSql.Append("NickName=@NickName,");
//            strSql.Append("UsrSex=@UsrSex,");
//            strSql.Append("Birthday=@Birthday,");
//            strSql.Append("Nation=@Nation,");
//            strSql.Append("NativePlace=@NativePlace,");
//            strSql.Append("Postalcode=@Postalcode,");
//            strSql.Append("UsrAddr=@UsrAddr,");
//            strSql.Append("UsrPic=@UsrPic,");
//            strSql.Append("UsrEMail=@UsrEMail,");
//            strSql.Append("UsrCardID=@UsrCardID,");
//            strSql.Append("MobileTel=@MobileTel,");
//            strSql.Append("UsrTelNum=@UsrTelNum,");
//            strSql.Append("UsrFaxNum=@UsrFaxNum,");
//            strSql.Append("UsrExt=@UsrExt,");
//            strSql.Append("UsrSignPic=@UsrSignPic,");
//            strSql.Append("IsDel=@IsDel,");
//            strSql.Append("UsrEnabled=@UsrEnabled,");
//            strSql.Append("UserState=@UserState,");
//            strSql.Append("OrderSeq=@OrderSeq,");
//            strSql.Append("GradeNo=@GradeNo,");
//            strSql.Append("UsrType=@UsrType,");
//            strSql.Append("Remark=@Remark,");
//            strSql.Append("LogIp=@LogIp,");
//            strSql.Append("IsDefault=@IsDefault");
//            strSql.Append(" where ");
//            SqlParameter[] parameters = {
//                    new SqlParameter("@UserID", SqlDbType.Int,4),
//                    new SqlParameter("@CONO", SqlDbType.VarChar,10),
//                    new SqlParameter("@DepNo", SqlDbType.VarChar,10),
//                    new SqlParameter("@UserNo", SqlDbType.VarChar,20),
//                    new SqlParameter("@UsrName", SqlDbType.VarChar,60),
//                    new SqlParameter("@UsrPwd", SqlDbType.VarChar,500),
//                    new SqlParameter("@LoginID", SqlDbType.VarChar,60),
//                    new SqlParameter("@NickName", SqlDbType.VarChar,255),
//                    new SqlParameter("@UsrSex", SqlDbType.Char,1),
//                    new SqlParameter("@Birthday", SqlDbType.DateTime),
//                    new SqlParameter("@Nation", SqlDbType.VarChar,50),
//                    new SqlParameter("@NativePlace", SqlDbType.VarChar,255),
//                    new SqlParameter("@Postalcode", SqlDbType.Char,10),
//                    new SqlParameter("@UsrAddr", SqlDbType.VarChar,255),
//                    new SqlParameter("@UsrPic", SqlDbType.Image),
//                    new SqlParameter("@UsrEMail", SqlDbType.VarChar,255),
//                    new SqlParameter("@UsrCardID", SqlDbType.VarChar,20),
//                    new SqlParameter("@MobileTel", SqlDbType.Char,20),
//                    new SqlParameter("@UsrTelNum", SqlDbType.VarChar,60),
//                    new SqlParameter("@UsrFaxNum", SqlDbType.VarChar,20),
//                    new SqlParameter("@UsrExt", SqlDbType.VarChar,30),
//                    new SqlParameter("@UsrSignPic", SqlDbType.Image),
//                    new SqlParameter("@IsDel", SqlDbType.Char,1),
//                    new SqlParameter("@UsrEnabled", SqlDbType.Char,1),
//                    new SqlParameter("@UserState", SqlDbType.Char,1),
//                    new SqlParameter("@OrderSeq", SqlDbType.VarChar,10),
//                    new SqlParameter("@GradeNo", SqlDbType.VarChar,10),
//                    new SqlParameter("@UsrType", SqlDbType.Char,1),
//                    new SqlParameter("@Remark", SqlDbType.VarChar,255),
//                    new SqlParameter("@LogIp", SqlDbType.VarChar,25),
//                    new SqlParameter("@IsDefault", SqlDbType.Char,1)};
//            parameters[0].Value = model.UserID;
//            parameters[1].Value = model.CONO;
//            parameters[2].Value = model.DepNo;
//            parameters[3].Value = model.UserNo;
//            parameters[4].Value = model.UsrName;
//            parameters[5].Value = model.UsrPwd;
//            parameters[6].Value = model.LoginID;
//            parameters[7].Value = model.NickName;
//            parameters[8].Value = model.UsrSex;
//            parameters[9].Value = model.Birthday;
//            parameters[10].Value = model.Nation;
//            parameters[11].Value = model.NativePlace;
//            parameters[12].Value = model.Postalcode;
//            parameters[13].Value = model.UsrAddr;
//            parameters[14].Value = model.UsrPic;
//            parameters[15].Value = model.UsrEMail;
//            parameters[16].Value = model.UsrCardID;
//            parameters[17].Value = model.MobileTel;
//            parameters[18].Value = model.UsrTelNum;
//            parameters[19].Value = model.UsrFaxNum;
//            parameters[20].Value = model.UsrExt;
//            parameters[21].Value = model.UsrSignPic;
//            parameters[22].Value = model.IsDel;
//            parameters[23].Value = model.UsrEnabled;
//            parameters[24].Value = model.UserState;
//            parameters[25].Value = model.OrderSeq;
//            parameters[26].Value = model.GradeNo;
//            parameters[27].Value = model.UsrType;
//            parameters[28].Value = model.Remark;
//            parameters[29].Value = model.LogIp;
//            parameters[30].Value = model.IsDefault;

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
//            strSql.Append("delete from Users_ALL ");
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
//        /// 得到一个对象实体
//        /// </summary>
//        public SeatManage.Model.Users_ALL GetModel()
//        {
//            //该表无主键信息，请自定义主键/条件字段
//            StringBuilder strSql=new StringBuilder();
//            strSql.Append("select  top 1 UserID,CONO,DepNo,UserNo,UsrName,UsrPwd,LoginID,NickName,UsrSex,Birthday,Nation,NativePlace,Postalcode,UsrAddr,UsrPic,UsrEMail,UsrCardID,MobileTel,UsrTelNum,UsrFaxNum,UsrExt,UsrSignPic,IsDel,UsrEnabled,UserState,OrderSeq,GradeNo,UsrType,Remark,LogIp,IsDefault from Users_ALL ");
//            strSql.Append(" where ");
//            SqlParameter[] parameters = {
//};

//            SeatManage.Model.Users_ALL model=new SeatManage.Model.Users_ALL();
//            DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
//            if(ds.Tables[0].Rows.Count>0)
//            {
//                if(ds.Tables[0].Rows[0]["UserID"]!=null && ds.Tables[0].Rows[0]["UserID"].ToString()!="")
//                {
//                    model.UserID=int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
//                }
//                if(ds.Tables[0].Rows[0]["CONO"]!=null && ds.Tables[0].Rows[0]["CONO"].ToString()!="")
//                {
//                    model.CONO=ds.Tables[0].Rows[0]["CONO"].ToString();
//                }
//                if(ds.Tables[0].Rows[0]["DepNo"]!=null && ds.Tables[0].Rows[0]["DepNo"].ToString()!="")
//                {
//                    model.DepNo=ds.Tables[0].Rows[0]["DepNo"].ToString();
//                }
//                if(ds.Tables[0].Rows[0]["UserNo"]!=null && ds.Tables[0].Rows[0]["UserNo"].ToString()!="")
//                {
//                    model.UserNo=ds.Tables[0].Rows[0]["UserNo"].ToString();
//                }
//                if(ds.Tables[0].Rows[0]["UsrName"]!=null && ds.Tables[0].Rows[0]["UsrName"].ToString()!="")
//                {
//                    model.UsrName=ds.Tables[0].Rows[0]["UsrName"].ToString();
//                }
//                if(ds.Tables[0].Rows[0]["UsrPwd"]!=null && ds.Tables[0].Rows[0]["UsrPwd"].ToString()!="")
//                {
//                    model.UsrPwd=ds.Tables[0].Rows[0]["UsrPwd"].ToString();
//                }
//                if(ds.Tables[0].Rows[0]["LoginID"]!=null && ds.Tables[0].Rows[0]["LoginID"].ToString()!="")
//                {
//                    model.LoginID=ds.Tables[0].Rows[0]["LoginID"].ToString();
//                }
//                if(ds.Tables[0].Rows[0]["NickName"]!=null && ds.Tables[0].Rows[0]["NickName"].ToString()!="")
//                {
//                    model.NickName=ds.Tables[0].Rows[0]["NickName"].ToString();
//                }
//                if(ds.Tables[0].Rows[0]["UsrSex"]!=null && ds.Tables[0].Rows[0]["UsrSex"].ToString()!="")
//                {
//                    model.UsrSex=ds.Tables[0].Rows[0]["UsrSex"].ToString();
//                }
//                if(ds.Tables[0].Rows[0]["Birthday"]!=null && ds.Tables[0].Rows[0]["Birthday"].ToString()!="")
//                {
//                    model.Birthday=DateTime.Parse(ds.Tables[0].Rows[0]["Birthday"].ToString());
//                }
//                if(ds.Tables[0].Rows[0]["Nation"]!=null && ds.Tables[0].Rows[0]["Nation"].ToString()!="")
//                {
//                    model.Nation=ds.Tables[0].Rows[0]["Nation"].ToString();
//                }
//                if(ds.Tables[0].Rows[0]["NativePlace"]!=null && ds.Tables[0].Rows[0]["NativePlace"].ToString()!="")
//                {
//                    model.NativePlace=ds.Tables[0].Rows[0]["NativePlace"].ToString();
//                }
//                if(ds.Tables[0].Rows[0]["Postalcode"]!=null && ds.Tables[0].Rows[0]["Postalcode"].ToString()!="")
//                {
//                    model.Postalcode=ds.Tables[0].Rows[0]["Postalcode"].ToString();
//                }
//                if(ds.Tables[0].Rows[0]["UsrAddr"]!=null && ds.Tables[0].Rows[0]["UsrAddr"].ToString()!="")
//                {
//                    model.UsrAddr=ds.Tables[0].Rows[0]["UsrAddr"].ToString();
//                }
//                if(ds.Tables[0].Rows[0]["UsrPic"]!=null && ds.Tables[0].Rows[0]["UsrPic"].ToString()!="")
//                {
//                    model.UsrPic=(byte[])ds.Tables[0].Rows[0]["UsrPic"];
//                }
//                if(ds.Tables[0].Rows[0]["UsrEMail"]!=null && ds.Tables[0].Rows[0]["UsrEMail"].ToString()!="")
//                {
//                    model.UsrEMail=ds.Tables[0].Rows[0]["UsrEMail"].ToString();
//                }
//                if(ds.Tables[0].Rows[0]["UsrCardID"]!=null && ds.Tables[0].Rows[0]["UsrCardID"].ToString()!="")
//                {
//                    model.UsrCardID=ds.Tables[0].Rows[0]["UsrCardID"].ToString();
//                }
//                if(ds.Tables[0].Rows[0]["MobileTel"]!=null && ds.Tables[0].Rows[0]["MobileTel"].ToString()!="")
//                {
//                    model.MobileTel=ds.Tables[0].Rows[0]["MobileTel"].ToString();
//                }
//                if(ds.Tables[0].Rows[0]["UsrTelNum"]!=null && ds.Tables[0].Rows[0]["UsrTelNum"].ToString()!="")
//                {
//                    model.UsrTelNum=ds.Tables[0].Rows[0]["UsrTelNum"].ToString();
//                }
//                if(ds.Tables[0].Rows[0]["UsrFaxNum"]!=null && ds.Tables[0].Rows[0]["UsrFaxNum"].ToString()!="")
//                {
//                    model.UsrFaxNum=ds.Tables[0].Rows[0]["UsrFaxNum"].ToString();
//                }
//                if(ds.Tables[0].Rows[0]["UsrExt"]!=null && ds.Tables[0].Rows[0]["UsrExt"].ToString()!="")
//                {
//                    model.UsrExt=ds.Tables[0].Rows[0]["UsrExt"].ToString();
//                }
//                if(ds.Tables[0].Rows[0]["UsrSignPic"]!=null && ds.Tables[0].Rows[0]["UsrSignPic"].ToString()!="")
//                {
//                    model.UsrSignPic=(byte[])ds.Tables[0].Rows[0]["UsrSignPic"];
//                }
//                if(ds.Tables[0].Rows[0]["IsDel"]!=null && ds.Tables[0].Rows[0]["IsDel"].ToString()!="")
//                {
//                    model.IsDel=ds.Tables[0].Rows[0]["IsDel"].ToString();
//                }
//                if(ds.Tables[0].Rows[0]["UsrEnabled"]!=null && ds.Tables[0].Rows[0]["UsrEnabled"].ToString()!="")
//                {
//                    model.UsrEnabled=ds.Tables[0].Rows[0]["UsrEnabled"].ToString();
//                }
//                if(ds.Tables[0].Rows[0]["UserState"]!=null && ds.Tables[0].Rows[0]["UserState"].ToString()!="")
//                {
//                    model.UserState=ds.Tables[0].Rows[0]["UserState"].ToString();
//                }
//                if(ds.Tables[0].Rows[0]["OrderSeq"]!=null && ds.Tables[0].Rows[0]["OrderSeq"].ToString()!="")
//                {
//                    model.OrderSeq=ds.Tables[0].Rows[0]["OrderSeq"].ToString();
//                }
//                if(ds.Tables[0].Rows[0]["GradeNo"]!=null && ds.Tables[0].Rows[0]["GradeNo"].ToString()!="")
//                {
//                    model.GradeNo=ds.Tables[0].Rows[0]["GradeNo"].ToString();
//                }
//                if(ds.Tables[0].Rows[0]["UsrType"]!=null && ds.Tables[0].Rows[0]["UsrType"].ToString()!="")
//                {
//                    model.UsrType=ds.Tables[0].Rows[0]["UsrType"].ToString();
//                }
//                if(ds.Tables[0].Rows[0]["Remark"]!=null && ds.Tables[0].Rows[0]["Remark"].ToString()!="")
//                {
//                    model.Remark=ds.Tables[0].Rows[0]["Remark"].ToString();
//                }
//                if(ds.Tables[0].Rows[0]["LogIp"]!=null && ds.Tables[0].Rows[0]["LogIp"].ToString()!="")
//                {
//                    model.LogIp=ds.Tables[0].Rows[0]["LogIp"].ToString();
//                }
//                if(ds.Tables[0].Rows[0]["IsDefault"]!=null && ds.Tables[0].Rows[0]["IsDefault"].ToString()!="")
//                {
//                    model.IsDefault=ds.Tables[0].Rows[0]["IsDefault"].ToString();
//                }
//                return model;
//            }
//            else
//            {
//                return null;
//            }
//        }

		

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
			parameters[0].Value = "Users_ALL";
			parameters[1].Value = "violateID";
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

