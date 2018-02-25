/******************************************
 * 作者：王昊天
 * 创建时间：2013.5.20
 * 说明：等待记录的数据库操作
 * 修改人：
 * 修改时间：
 * ******************************************/
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;
using SeatManage.ClassModel;
namespace SeatManage.DAL
{
    /// <summary>
    /// 数据访问类:T_SM_SeatWaiting
    /// </summary>
    public partial class T_SM_SeatWaiting
    {
        public T_SM_SeatWaiting()
        { }
        #region  Method

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int SeatWaitingID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_SM_SeatWaiting");
            strSql.Append(" where SeatWaitingID=@SeatWaitingID ");
            SqlParameter[] parameters = {
					new SqlParameter("@SeatWaitingID", SqlDbType.Int,4)
                                        };
            parameters[0].Value = SeatWaitingID;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SeatWaitingID,CardNo,CardNoB,SeatNo,ReadingRoomNo,EnterOutLogID,SeatWaitTime,StateChangeTime,WaitingState,OperateType ");
            strSql.Append(" FROM ViewSeatWaiting ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by SeatWaitTime desc");
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder, SqlParameter[] patameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" SeatWaitingID,CardNo,CardNoB,SeatNo,ReadingRoomNo,EnterOutLogID,SeatWaitTime,StateChangeTime,WaitingState,OperateType ");
            strSql.Append(" FROM ViewSeatWaiting ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString(), patameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(WaitSeatLogInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_SM_SeatWaiting(");
            strSql.Append("CardNo,EnterOutLogID,WaitingState,OperateType,StateChangeTime)");
            strSql.Append(" values (");
            strSql.Append("@CardNo,@EnterOutLogID,@WaitingState,@OperateType,@StateChangeTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@CardNo", SqlDbType.NVarChar,50),
                    new SqlParameter("@EnterOutLogID", SqlDbType.Int,4),
                    new SqlParameter("@WaitingState", SqlDbType.Int,4),
                    new SqlParameter("@OperateType", SqlDbType.Int,4),
                    new SqlParameter("@StateChangeTime",SqlDbType.DateTime)
                                        };
            parameters[0].Value = model.CardNo;
            parameters[1].Value = model.EnterOutLogID;
            parameters[2].Value = model.WaitingState;
            parameters[3].Value = model.OperateType;
            parameters[4].Value = model.StatsChangeTime;
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
        /// 更新一条数据
        /// </summary>
        public bool Update(WaitSeatLogInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_SM_SeatWaiting set ");
            strSql.Append("CardNo=@CardNo,");
            strSql.Append("EnterOutLogID=@EnterOutLogID,");
            strSql.Append("StateChangeTime=@StateChangeTime,");
            strSql.Append("WaitingState=@WaitingState,");
            strSql.Append("OperateType=@OperateType");
            strSql.Append(" where SeatWaitingID=@SeatWaitingID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@CardNo", SqlDbType.NVarChar,50),
                    new SqlParameter("@EnterOutLogID", SqlDbType.Int,4),
                    new SqlParameter("@StateChangeTime", SqlDbType.DateTime),
                    new SqlParameter("@WaitingState", SqlDbType.Int,4),
                    new SqlParameter("@OperateType", SqlDbType.Int,4),
                    new SqlParameter("@SeatWaitingID", SqlDbType.Int,4)};
            parameters[0].Value = model.CardNo;
            parameters[1].Value = model.EnterOutLogID;
            parameters[2].Value = model.SeatWaitTime;
            parameters[3].Value = model.WaitingState;
            parameters[4].Value = model.OperateType;
            parameters[5].Value = model.SeatWaitingID;

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
        ///// 删除一条数据
        ///// </summary>
        //public bool Delete(int SeatWaitingID)
        //{

        //    StringBuilder strSql=new StringBuilder();
        //    strSql.Append("delete from T_SM_SeatWaiting ");
        //    strSql.Append(" where SeatWaitingID=@SeatWaitingID ");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@SeatWaitingID", SqlDbType.Int,4)};
        //    parameters[0].Value = SeatWaitingID;

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
        //public bool DeleteList(string SeatWaitingIDlist )
        //{
        //    StringBuilder strSql=new StringBuilder();
        //    strSql.Append("delete from T_SM_SeatWaiting ");
        //    strSql.Append(" where SeatWaitingID in ("+SeatWaitingIDlist + ")  ");
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
        //public SeatManage.Model.T_SM_SeatWaiting GetModel(int SeatWaitingID)
        //{

        //    StringBuilder strSql=new StringBuilder();
        //    strSql.Append("select  top 1 SeatWaitingID,CardNo,EnterOutLogID,SeatWaitTime,WaitingState,OperateType,NowState from T_SM_SeatWaiting ");
        //    strSql.Append(" where SeatWaitingID=@SeatWaitingID ");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@SeatWaitingID", SqlDbType.Int,4)};
        //    parameters[0].Value = SeatWaitingID;

        //    SeatManage.Model.T_SM_SeatWaiting model=new SeatManage.Model.T_SM_SeatWaiting();
        //    DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
        //    if(ds.Tables[0].Rows.Count>0)
        //    {
        //        if(ds.Tables[0].Rows[0]["SeatWaitingID"]!=null && ds.Tables[0].Rows[0]["SeatWaitingID"].ToString()!="")
        //        {
        //            model.SeatWaitingID=int.Parse(ds.Tables[0].Rows[0]["SeatWaitingID"].ToString());
        //        }
        //        if(ds.Tables[0].Rows[0]["CardNo"]!=null && ds.Tables[0].Rows[0]["CardNo"].ToString()!="")
        //        {
        //            model.CardNo=ds.Tables[0].Rows[0]["CardNo"].ToString();
        //        }
        //        if(ds.Tables[0].Rows[0]["EnterOutLogID"]!=null && ds.Tables[0].Rows[0]["EnterOutLogID"].ToString()!="")
        //        {
        //            model.EnterOutLogID=int.Parse(ds.Tables[0].Rows[0]["EnterOutLogID"].ToString());
        //        }
        //        if(ds.Tables[0].Rows[0]["SeatWaitTime"]!=null && ds.Tables[0].Rows[0]["SeatWaitTime"].ToString()!="")
        //        {
        //            model.SeatWaitTime=DateTime.Parse(ds.Tables[0].Rows[0]["SeatWaitTime"].ToString());
        //        }
        //        if(ds.Tables[0].Rows[0]["WaitingState"]!=null && ds.Tables[0].Rows[0]["WaitingState"].ToString()!="")
        //        {
        //            model.WaitingState=int.Parse(ds.Tables[0].Rows[0]["WaitingState"].ToString());
        //        }
        //        if(ds.Tables[0].Rows[0]["OperateType"]!=null && ds.Tables[0].Rows[0]["OperateType"].ToString()!="")
        //        {
        //            model.OperateType=int.Parse(ds.Tables[0].Rows[0]["OperateType"].ToString());
        //        }
        //        if(ds.Tables[0].Rows[0]["NowState"]!=null && ds.Tables[0].Rows[0]["NowState"].ToString()!="")
        //        {
        //            model.NowState=int.Parse(ds.Tables[0].Rows[0]["NowState"].ToString());
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
            parameters[0].Value = "T_SM_SeatWaiting";
            parameters[1].Value = "SeatWaitingID";
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

