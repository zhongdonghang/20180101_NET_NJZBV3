using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;
using SeatManage.ClassModel;
using SeatManage.EnumType;
using System.Collections.Generic;
namespace SeatManage.DAL
{
    /// <summary>
    /// 数据访问类:T_SM_Seat
    /// </summary>
    public partial class T_SM_Seat
    {
        public T_SM_Seat()
        { }
        #region  Method

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string SeatNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_SM_Seat");
            strSql.Append(" where SeatNo=@SeatNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@SeatNo", SqlDbType.NVarChar,100)};
            parameters[0].Value = SeatNo;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SeatNo,ReadingRoomNo,IsUsing,SeatUI,IsLock,LockTime,TimePart ");
            strSql.Append(" FROM T_SM_Seat ");
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
            strSql.Append(" SeatNo,ReadingRoomNo,IsUsing,SeatUI,IsLock,LockTime,TimePart ");
            strSql.Append(" FROM T_SM_Seat ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 座位加锁
        /// </summary>
        /// <param name="seatNo"></param>
        /// <returns></returns>
        public SeatManage.EnumType.SeatLockState SeatLocked(string seatNo, DateTime lockTime)
        {
            if (!string.IsNullOrEmpty(seatNo))
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@ExcResult", SqlDbType.Int);
                parameters[0].Direction = ParameterDirection.Output;
                parameters[1] = new SqlParameter("@SeatNo", seatNo);
                parameters[2] = new SqlParameter("@LockTime", lockTime);
                DbHelperSQL.Execute_Proc("Proc_LockSeat", parameters);
                return (SeatLockState)(int)parameters[0].Value;
            }
            return SeatLockState.None;
        }
        /// <summary>
        /// 座位解锁
        /// </summary>
        /// <param name="seatNo"></param>
        /// <returns></returns>
        public SeatManage.EnumType.SeatLockState SeatUnLocked(string seatNo)
        {
            if (!string.IsNullOrEmpty(seatNo))
            {
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@ExcResult", SqlDbType.Int);
                parameters[0].Direction = ParameterDirection.Output;
                parameters[1] = new SqlParameter("@SeatNo", seatNo);
                DbHelperSQL.Execute_Proc("Proc_UnLockSeat", parameters);
                return (SeatLockState)(int)parameters[0].Value;
            }
            return SeatLockState.None;
        }

        /// <summary>
        /// 获取读者信息
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public DataSet ReaderUsedSeat(string cardNo, int days, List<string> roomNums, DateTime serviceDateTime)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(cardNo))
            {
                strSql.Append("select SeatNo,count(SeatNo) as SeatCount,ReadingRoomNo  from (");
                strSql.Append(" select seatNo,EnterOutLogNo ,ReadingRoomNo ");

                strSql.Append(string.Format(" from dbo.T_SM_EnterOutLog  where cardNo='{0}' and datediff(day,EnterOutTime,getdate())< {1}  ", cardNo, days));
                strSql.Append("and seatNo not in ");
                strSql.Append(string.Format("	(select seatNo from dbo.T_SM_SeatBespeak where  BespeakState={0} and   datediff(day, BespeakTime,'{1}')=0  ) ", (int)BookingStatus.Waiting, serviceDateTime.ToString()));
                strSql.Append(" and seatNo not in");
                strSql.Append(string.Format("	(select seatNo from T_SM_EnterOutLog where EnterOutState<>{0} and EnterOutType={1}  )", (int)EnterOutLogType.Leave, (int)LogStatus.Valid));
                StringBuilder rooms = new StringBuilder();
                if (roomNums != null && roomNums.Count > 0)
                {

                    for (int i = 0; i < roomNums.Count; i++)
                    {
                        if (i < roomNums.Count - 1)
                        {
                            rooms.Append(string.Format("'{0}',", roomNums[i]));
                        }
                        else
                        {
                            rooms.Append(string.Format("'{0}'", roomNums[i]));
                        }
                    }
                    strSql.Append(string.Format(" and ReadingRoomNo in  ( {0} )", rooms.ToString()));
                }
                strSql.Append(" group by EnterOutLogNo,SeatNo,ReadingRoomNo ) as temp");
                strSql.Append(" group by SeatNo,ReadingRoomNo order by SeatCount desc ");
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 随机分配座位
        /// </summary>
        /// <param name="reandingRoomNum">所在阅览室编号</param>
        /// <returns></returns>
        public DataSet RandomAllotSeat(string roomNum, DateTime datetime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(string.Format("  select top(1) SeatNo from dbo.T_SM_Seat where ReadingRoomNo = '{0}' and IsLock=0 ", roomNum));
            strSql.Append(string.Format(" and  not EXISTS ( select t_sm_EnterOutLog.SeatNo from t_sm_EnterOutLog where  T_SM_Seat.SeatNo=t_sm_EnterOutLog.SeatNo and EnterOutType={0} and EnterOutState<>{1})", (int)LogStatus.Valid, (int)EnterOutLogType.Leave));
            strSql.Append(string.Format(" and  not EXISTS (select T_SM_SeatBespeak.SeatNo from T_SM_SeatBespeak where T_SM_Seat.SeatNo =T_SM_SeatBespeak.SeatNo and T_SM_SeatBespeak.BespeakState={0} and  datediff(day, BespeakTime,'{1}')=0) ", (int)BookingStatus.Waiting, datetime.ToString()));
            strSql.Append(" order by newid()");
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 删除原来的座位信息
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public bool Delete(string strWhere, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_SM_Seat ");
            if (string.IsNullOrEmpty(strWhere.ToString()))
            {
                throw new Exception("删除座位失败，条件不能为空");
            }
            strSql.AppendFormat(" where {0}", strWhere);

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
        /// 增加一条数据
        /// </summary>
        public bool Add(SeatManage.ClassModel.Seat model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_SM_Seat(");
            strSql.Append("SeatNo,ReadingRoomNo,IsUsing,SeatUI,IsLock)");
            strSql.Append(" values (");
            strSql.Append("@SeatNo,@ReadingRoomNo,@IsUsing,@SeatUI,@IsLock)");
            SqlParameter[] parameters = {
                    new SqlParameter("@SeatNo", SqlDbType.NVarChar,100),
                    new SqlParameter("@ReadingRoomNo", SqlDbType.NVarChar,100),
                    new SqlParameter("@IsUsing", SqlDbType.Bit),
                    new SqlParameter("@SeatUI", SqlDbType.NVarChar,100),
                    new SqlParameter("@IsLock", SqlDbType.Int,4)};
            parameters[0].Value = model.SeatNo;
            parameters[1].Value = model.ReadingRoomNum;
            parameters[2].Value = false;
            parameters[3].Value = string.Format("{0},{1}", model.PositionX, model.PositionY);
            parameters[4].Value = false;

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
        ///// 更新一条数据
        ///// </summary>
        //public bool Update(SeatManage.Model.T_SM_Seat model)
        //{
        //    StringBuilder strSql=new StringBuilder();
        //    strSql.Append("update T_SM_Seat set ");
        //    strSql.Append("ReadingRoomNo=@ReadingRoomNo,");
        //    strSql.Append("SeatSetting=@SeatSetting,");
        //    strSql.Append("SeatUI=@SeatUI,");
        //    strSql.Append("IsLock=@IsLock,");
        //    strSql.Append("LockTime=@LockTime,");
        //    strSql.Append("TimePart=@TimePart");
        //    strSql.Append(" where SeatNo=@SeatNo ");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@ReadingRoomNo", SqlDbType.Int,4),
        //            new SqlParameter("@SeatSetting", SqlDbType.NVarChar,50),
        //            new SqlParameter("@SeatUI", SqlDbType.NVarChar,100),
        //            new SqlParameter("@IsLock", SqlDbType.Int,4),
        //            new SqlParameter("@LockTime", SqlDbType.DateTime),
        //            new SqlParameter("@TimePart", SqlDbType.NVarChar,100),
        //            new SqlParameter("@SeatNo", SqlDbType.NVarChar,100)};
        //    parameters[0].Value = model.ReadingRoomNo;
        //    parameters[1].Value = model.SeatSetting;
        //    parameters[2].Value = model.SeatUI;
        //    parameters[3].Value = model.IsLock;
        //    parameters[4].Value = model.LockTime;
        //    parameters[5].Value = model.TimePart;
        //    parameters[6].Value = model.SeatNo;

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
        //public bool Delete(string SeatNo)
        //{

        //    StringBuilder strSql=new StringBuilder();
        //    strSql.Append("delete from T_SM_Seat ");
        //    strSql.Append(" where SeatNo=@SeatNo ");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@SeatNo", SqlDbType.NVarChar,100)};
        //    parameters[0].Value = SeatNo;

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
        //public bool DeleteList(string SeatNolist )
        //{
        //    StringBuilder strSql=new StringBuilder();
        //    strSql.Append("delete from T_SM_Seat ");
        //    strSql.Append(" where SeatNo in ("+SeatNolist + ")  ");
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
        //public SeatManage.Model.T_SM_Seat GetModel(string SeatNo)
        //{

        //    StringBuilder strSql=new StringBuilder();
        //    strSql.Append("select  top 1 SeatNo,ReadingRoomNo,SeatSetting,SeatUI,IsLock,LockTime,TimePart from T_SM_Seat ");
        //    strSql.Append(" where SeatNo=@SeatNo ");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@SeatNo", SqlDbType.NVarChar,100)};
        //    parameters[0].Value = SeatNo;

        //    SeatManage.Model.T_SM_Seat model=new SeatManage.Model.T_SM_Seat();
        //    DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
        //    if(ds.Tables[0].Rows.Count>0)
        //    {
        //        if(ds.Tables[0].Rows[0]["SeatNo"]!=null && ds.Tables[0].Rows[0]["SeatNo"].ToString()!="")
        //        {
        //            model.SeatNo=ds.Tables[0].Rows[0]["SeatNo"].ToString();
        //        }
        //        if(ds.Tables[0].Rows[0]["ReadingRoomNo"]!=null && ds.Tables[0].Rows[0]["ReadingRoomNo"].ToString()!="")
        //        {
        //            model.ReadingRoomNo=int.Parse(ds.Tables[0].Rows[0]["ReadingRoomNo"].ToString());
        //        }
        //        if(ds.Tables[0].Rows[0]["SeatSetting"]!=null && ds.Tables[0].Rows[0]["SeatSetting"].ToString()!="")
        //        {
        //            model.SeatSetting=ds.Tables[0].Rows[0]["SeatSetting"].ToString();
        //        }
        //        if(ds.Tables[0].Rows[0]["SeatUI"]!=null && ds.Tables[0].Rows[0]["SeatUI"].ToString()!="")
        //        {
        //            model.SeatUI=ds.Tables[0].Rows[0]["SeatUI"].ToString();
        //        }
        //        if(ds.Tables[0].Rows[0]["IsLock"]!=null && ds.Tables[0].Rows[0]["IsLock"].ToString()!="")
        //        {
        //            model.IsLock=int.Parse(ds.Tables[0].Rows[0]["IsLock"].ToString());
        //        }
        //        if(ds.Tables[0].Rows[0]["LockTime"]!=null && ds.Tables[0].Rows[0]["LockTime"].ToString()!="")
        //        {
        //            model.LockTime=DateTime.Parse(ds.Tables[0].Rows[0]["LockTime"].ToString());
        //        }
        //        if(ds.Tables[0].Rows[0]["TimePart"]!=null && ds.Tables[0].Rows[0]["TimePart"].ToString()!="")
        //        {
        //            model.TimePart=ds.Tables[0].Rows[0]["TimePart"].ToString();
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
            parameters[0].Value = "T_SM_Seat";
            parameters[1].Value = "SeatNo";
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

