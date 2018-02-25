using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;
using SeatManage.ClassModel;
namespace SeatManage.DAL
{
    /// <summary>
    /// 数据访问类:T_SM_EnterOutLog
    /// </summary>
    public partial class T_SM_EnterOutLog
    {
        public T_SM_EnterOutLog()
        { }
        #region  Method



        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int EnterOutLogID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_SM_EnterOutLog");
            strSql.Append(" where EnterOutLogID=@EnterOutLogID ");
            SqlParameter[] parameters = {
					new SqlParameter("@EnterOutLogID", SqlDbType.Int,4)};
            parameters[0].Value = EnterOutLogID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere, SqlParameter[] parameters)
        {
            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("select EnterOutLogID,CardNo,EnterOutLogNo,ReadingRoomNo,SeatNo,EnterOutState,EnterOutTime,EnterOutType,EnterFlag,Remark,MarkTime ");
            //strSql.Append(" FROM T_SM_EnterOutLog ");
            //if (!string.IsNullOrEmpty(strWhere))
            //{
            //    strSql.Append(" where " + strWhere);
            //}
            //return DbHelperSQL.Query(strSql.ToString(), parameters);

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SchoolName,LibraryName,ReadingRoomName,EnterOutTime,EnterOutType,EnterOutLogNo,EnterOutLogID,SchoolNo,LibraryNo,ReadingRoomNo,EnterOutState,SeatNo,ReaderName,ReaderTypeName,ReaderDeptName,EnterFlag,Remark,MarkTime,CardNo,ReadingSetting,TerminalNum ");
            strSql.Append(" FROM ViewEnterOutLog ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetCount(string strWhere, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(EnterOutLogID) ");
            strSql.Append(" FROM ViewEnterOutLog ");
            if (strWhere.Trim() != "")
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
            strSql.Append(" SchoolName,LibraryName,ReadingRoomName,EnterOutTime,EnterOutType,EnterOutLogNo,EnterOutLogID,SchoolNo,LibraryNo,ReadingRoomNo,EnterOutState,SeatNo,ReaderName,ReaderTypeName,ReaderDeptName,EnterFlag,Remark,MarkTime,CardNo,ReadingSetting,TerminalNum");
            strSql.Append(" FROM ViewEnterOutLog ");
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
        public int Add(EnterOutLogInfo model, ref int newLogId)
        {
            //TODO:不跟据阅览室状态添加进出记录
            SqlParameter[] parameters = new SqlParameter[11];
            parameters[0] = new SqlParameter("@ExcResult", SqlDbType.Int);
            parameters[0].Direction = ParameterDirection.Output;

            parameters[1] = new SqlParameter("@CardNo", model.CardNo);
            parameters[2] = new SqlParameter("@EnterOutLogNo", model.EnterOutLogNo);
            parameters[3] = new SqlParameter("@EnterOutState", (int)model.EnterOutState);
            parameters[4] = new SqlParameter("@EnterOutTime", model.EnterOutTime);
            parameters[5] = new SqlParameter("@EnterFlag", (int)model.Flag);
            parameters[6] = new SqlParameter("@ReadingRoomNo", model.ReadingRoomNo);
            parameters[7] = new SqlParameter("@Remark", model.Remark);
            parameters[8] = new SqlParameter("@SeatNo", model.SeatNo);
            parameters[9] = new SqlParameter("@TerminalNum", model.TerminalNum);
            parameters[10] = new SqlParameter("RETURN_VALUE", SqlDbType.Int);
            parameters[10].Direction = ParameterDirection.ReturnValue;
            DbHelperSQL.Execute_Proc("Proc_AddNewEnterOutLog", parameters);
            string id = parameters[10].Value.ToString();
            if (!string.IsNullOrEmpty(id))
            {
                newLogId = int.Parse(id);
            }
            return (int)parameters[0].Value;
        }
        /// <summary>
        /// 插入计时时间
        /// </summary>
        /// <param name="EnterOutLogID">进出记录编号</param>
        /// <param name="MarkTime">计时时间</param>
        /// <returns></returns>
        public bool UpdateMarkTime(string EnterOutLogID, DateTime MarkTime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_SM_EnterOutLog set ");
            strSql.Append("MarkTime=@MarkTime");
            strSql.Append(" where EnterOutLogID=@EnterOutLogID ");
            SqlParameter[] parameters = {
                new SqlParameter("@EnterOutLogID", SqlDbType.Int,4),
                new SqlParameter("@MarkTime", SqlDbType.DateTime)};
            parameters[0].Value = EnterOutLogID;
            parameters[1].Value = MarkTime;
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
        /// 获取阅览室某一天的进出人次
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int GetSeatPersonTimes(DateTime dt,string roomNum)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(times) from(select count(EnterOutLogNo) times from dbo.T_SM_EnterOutLog ");
            strSql.AppendFormat(" where datediff(day,@dt, EnterOutTime)=0 and ReadingRoomNo=@roomNum group by EnterOutLogNo");
            strSql.Append(") as tempTable "); 
            SqlParameter[] parameters = {
                new SqlParameter("@dt", SqlDbType.DateTime),
                new SqlParameter("@roomNum", SqlDbType.NVarChar)};
            parameters[0].Value = dt;
            parameters[1].Value = roomNum;
            int rows =(int) DbHelperSQL.GetSingle(strSql.ToString(), parameters);
             
           return rows;
            
        }

        ///// <summary>
        ///// 更新一条数据
        ///// </summary>
        //public bool Update(SeatManage.Model.T_SM_EnterOutLog model)
        //{
        //    StringBuilder strSql=new StringBuilder();
        //    strSql.Append("update T_SM_EnterOutLog set ");
        //    strSql.Append("CardNo=@CardNo,");
        //    strSql.Append("EnterOutLogNo=@EnterOutLogNo,");
        //    strSql.Append("ReadingRoomNo=@ReadingRoomNo,");
        //    strSql.Append("SeatNo=@SeatNo,");
        //    strSql.Append("EnterOutState=@EnterOutState,");
        //    strSql.Append("EnterOutTime=@EnterOutTime,");
        //    strSql.Append("EnterOutType=@EnterOutType,");
        //    strSql.Append("EnterFlag=@EnterFlag,");
        //    strSql.Append("Remark=@Remark,");
        //    strSql.Append("MarkTime=@MarkTime");
        //    strSql.Append(" where EnterOutLogID=@EnterOutLogID ");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@CardNo", SqlDbType.NVarChar,100),
        //            new SqlParameter("@EnterOutLogNo", SqlDbType.NVarChar,100),
        //            new SqlParameter("@ReadingRoomNo", SqlDbType.NVarChar,50),
        //            new SqlParameter("@SeatNo", SqlDbType.NVarChar,100),
        //            new SqlParameter("@EnterOutState", SqlDbType.Int,4),
        //            new SqlParameter("@EnterOutTime", SqlDbType.DateTime),
        //            new SqlParameter("@EnterOutType", SqlDbType.Int,4),
        //            new SqlParameter("@EnterFlag", SqlDbType.Int,4),
        //            new SqlParameter("@Remark", SqlDbType.NVarChar,500),
        //            new SqlParameter("@MarkTime", SqlDbType.DateTime),
        //            new SqlParameter("@EnterOutLogID", SqlDbType.Int,4)};
        //    parameters[0].Value = model.CardNo;
        //    parameters[1].Value = model.EnterOutLogNo;
        //    parameters[2].Value = model.ReadingRoomNo;
        //    parameters[3].Value = model.SeatNo;
        //    parameters[4].Value = model.EnterOutState;
        //    parameters[5].Value = model.EnterOutTime;
        //    parameters[6].Value = model.EnterOutType;
        //    parameters[7].Value = model.EnterFlag;
        //    parameters[8].Value = model.Remark;
        //    parameters[9].Value = model.MarkTime;
        //    parameters[10].Value = model.EnterOutLogID;

        //    int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
        ///// 删除一条数据
        ///// </summary>
        //public bool Delete(int EnterOutLogID)
        //{

        //    StringBuilder strSql=new StringBuilder();
        //    strSql.Append("delete from T_SM_EnterOutLog ");
        //    strSql.Append(" where EnterOutLogID=@EnterOutLogID ");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@EnterOutLogID", SqlDbType.Int,4)};
        //    parameters[0].Value = EnterOutLogID;

        //    int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
        ///// 批量删除数据
        ///// </summary>
        //public bool DeleteList(string EnterOutLogIDlist )
        //{
        //    StringBuilder strSql=new StringBuilder();
        //    strSql.Append("delete from T_SM_EnterOutLog ");
        //    strSql.Append(" where EnterOutLogID in ("+EnterOutLogIDlist + ")  ");
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
        //public SeatManage.Model.T_SM_EnterOutLog GetModel(int EnterOutLogID)
        //{

        //    StringBuilder strSql=new StringBuilder();
        //    strSql.Append("select  top 1 EnterOutLogID,CardNo,EnterOutLogNo,ReadingRoomNo,SeatNo,EnterOutState,EnterOutTime,EnterOutType,EnterFlag,Remark,MarkTime from T_SM_EnterOutLog ");
        //    strSql.Append(" where EnterOutLogID=@EnterOutLogID ");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@EnterOutLogID", SqlDbType.Int,4)};
        //    parameters[0].Value = EnterOutLogID;

        //    SeatManage.Model.T_SM_EnterOutLog model=new SeatManage.Model.T_SM_EnterOutLog();
        //    DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
        //    if(ds.Tables[0].Rows.Count>0)
        //    {
        //        if(ds.Tables[0].Rows[0]["EnterOutLogID"]!=null && ds.Tables[0].Rows[0]["EnterOutLogID"].ToString()!="")
        //        {
        //            model.EnterOutLogID=int.Parse(ds.Tables[0].Rows[0]["EnterOutLogID"].ToString());
        //        }
        //        if(ds.Tables[0].Rows[0]["CardNo"]!=null && ds.Tables[0].Rows[0]["CardNo"].ToString()!="")
        //        {
        //            model.CardNo=ds.Tables[0].Rows[0]["CardNo"].ToString();
        //        }
        //        if(ds.Tables[0].Rows[0]["EnterOutLogNo"]!=null && ds.Tables[0].Rows[0]["EnterOutLogNo"].ToString()!="")
        //        {
        //            model.EnterOutLogNo=ds.Tables[0].Rows[0]["EnterOutLogNo"].ToString();
        //        }
        //        if(ds.Tables[0].Rows[0]["ReadingRoomNo"]!=null && ds.Tables[0].Rows[0]["ReadingRoomNo"].ToString()!="")
        //        {
        //            model.ReadingRoomNo=ds.Tables[0].Rows[0]["ReadingRoomNo"].ToString();
        //        }
        //        if(ds.Tables[0].Rows[0]["SeatNo"]!=null && ds.Tables[0].Rows[0]["SeatNo"].ToString()!="")
        //        {
        //            model.SeatNo=ds.Tables[0].Rows[0]["SeatNo"].ToString();
        //        }
        //        if(ds.Tables[0].Rows[0]["EnterOutState"]!=null && ds.Tables[0].Rows[0]["EnterOutState"].ToString()!="")
        //        {
        //            model.EnterOutState=int.Parse(ds.Tables[0].Rows[0]["EnterOutState"].ToString());
        //        }
        //        if(ds.Tables[0].Rows[0]["EnterOutTime"]!=null && ds.Tables[0].Rows[0]["EnterOutTime"].ToString()!="")
        //        {
        //            model.EnterOutTime=DateTime.Parse(ds.Tables[0].Rows[0]["EnterOutTime"].ToString());
        //        }
        //        if(ds.Tables[0].Rows[0]["EnterOutType"]!=null && ds.Tables[0].Rows[0]["EnterOutType"].ToString()!="")
        //        {
        //            model.EnterOutType=int.Parse(ds.Tables[0].Rows[0]["EnterOutType"].ToString());
        //        }
        //        if(ds.Tables[0].Rows[0]["EnterFlag"]!=null && ds.Tables[0].Rows[0]["EnterFlag"].ToString()!="")
        //        {
        //            model.EnterFlag=int.Parse(ds.Tables[0].Rows[0]["EnterFlag"].ToString());
        //        }
        //        if(ds.Tables[0].Rows[0]["Remark"]!=null && ds.Tables[0].Rows[0]["Remark"].ToString()!="")
        //        {
        //            model.Remark=ds.Tables[0].Rows[0]["Remark"].ToString();
        //        }
        //        if(ds.Tables[0].Rows[0]["MarkTime"]!=null && ds.Tables[0].Rows[0]["MarkTime"].ToString()!="")
        //        {
        //            model.MarkTime=DateTime.Parse(ds.Tables[0].Rows[0]["MarkTime"].ToString());
        //        }
        //        return model;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}


 
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string cardNo)
        { 
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT SchoolName,ReaderName,TerminalNum,LibraryName,ReadingRoomName,EnterOutTime,EnterOutType,EnterOutLogNo,EnterOutLogID,SchoolNo,LibraryNo,ReadingRoomNo,EnterOutState,SeatNo,  EnterFlag,Remark,MarkTime,ReadingSetting,CardNo FROM");
            strSql.AppendFormat(" (SELECT ROW_NUMBER() OVER (ORDER BY EnterOutTime DESC) AS SerialNumber,SchoolName,TerminalNum,ReaderName,LibraryName,ReadingRoomName,EnterOutTime,EnterOutType,EnterOutLogNo,EnterOutLogID,SchoolNo,LibraryNo,ReadingRoomNo,EnterOutState,SeatNo,  EnterFlag,Remark,MarkTime,ReadingSetting,CardNo FROM ViewEnterOutLog where cardNo='{0}' ) AS T", cardNo);
            strSql.AppendFormat(" WHERE T.SerialNumber >{0}  and T.SerialNumber <= {1}", PageIndex * PageSize, (PageIndex + 1) * PageSize);

            return DbHelperSQL.Query(strSql.ToString());
        } 

        #endregion  Method
    }
}

