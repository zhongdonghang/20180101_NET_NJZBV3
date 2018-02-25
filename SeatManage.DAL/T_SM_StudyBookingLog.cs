using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;//Please add references
namespace SeatManage.DAL
{
    /// <summary>
    /// 数据访问类:T_SM_StudyBookingLog
    /// </summary>
    public partial class T_SM_StudyBookingLog
    {
        public T_SM_StudyBookingLog()
        { }
        #region  Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("StudyID", "T_SM_StudyBookingLog");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int StudyID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_SM_StudyBookingLog");
            strSql.Append(" where StudyID=@StudyID");
            SqlParameter[] parameters = {
					new SqlParameter("@StudyID", SqlDbType.Int,4)
};
            parameters[0].Value = StudyID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SeatManage.ClassModel.StudyBookingLog model)
        {
            //ApplicationTable
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_SM_StudyBookingLog(");
            strSql.Append("CardNo,StudyRoomNo,SubmitTime,CheckTime,BespeakTime,UseTime,CheckState,ChecklPerson,Remark,ApplicationTable)");
            strSql.Append(" values (");
            strSql.Append("@CardNo,@StudyRoomNo,@SubmitTime,@CheckTime,@BespeakTime,@UseTime,@CheckState,@ChecklPerson,@Remark,@ApplicationTable)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@CardNo", SqlDbType.NVarChar,50),
					new SqlParameter("@StudyRoomNo", SqlDbType.NVarChar,50),
					new SqlParameter("@SubmitTime", SqlDbType.DateTime),
					new SqlParameter("@CheckTime", SqlDbType.DateTime),
					new SqlParameter("@BespeakTime", SqlDbType.DateTime),
					new SqlParameter("@UseTime", SqlDbType.Int,4),
					new SqlParameter("@CheckState", SqlDbType.Int,4),
					new SqlParameter("@ChecklPerson", SqlDbType.NVarChar,50),
					new SqlParameter("@Remark", SqlDbType.Text),
                    new SqlParameter("@ApplicationTable", SqlDbType.Text)};
            parameters[0].Value = model.CardNo;
            parameters[1].Value = model.StudyRoomNo;
            parameters[2].Value = model.SubmitTime;
            parameters[3].Value = model.CheckTime;
            parameters[4].Value = model.BespeakTime;
            parameters[5].Value = model.UseTime;
            parameters[6].Value = (int)model.CheckState;
            parameters[7].Value = model.ChecklPerson;
            parameters[8].Value = model.Remark;
            parameters[9].Value = model.Application.ToString();
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
        public bool Update(SeatManage.ClassModel.StudyBookingLog model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_SM_StudyBookingLog set ");
            strSql.Append("CardNo=@CardNo,");
            strSql.Append("StudyRoomNo=@StudyRoomNo,");
            strSql.Append("SubmitTime=@SubmitTime,");
            strSql.Append("CheckTime=@CheckTime,");
            strSql.Append("BespeakTime=@BespeakTime,");
            strSql.Append("UseTime=@UseTime,");
            strSql.Append("CheckState=@CheckState,");
            strSql.Append("ChecklPerson=@ChecklPerson,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("ApplicationTable=@ApplicationTable");
            strSql.Append(" where StudyID=@StudyID");
            SqlParameter[] parameters = {
					new SqlParameter("@CardNo", SqlDbType.NVarChar,50),
					new SqlParameter("@StudyRoomNo", SqlDbType.NVarChar,50),
					new SqlParameter("@SubmitTime", SqlDbType.DateTime),
					new SqlParameter("@CheckTime", SqlDbType.DateTime),
					new SqlParameter("@BespeakTime", SqlDbType.DateTime),
					new SqlParameter("@UseTime", SqlDbType.Int,4),
					new SqlParameter("@CheckState", SqlDbType.Int,4),
					new SqlParameter("@ChecklPerson", SqlDbType.NVarChar,50),
					new SqlParameter("@Remark", SqlDbType.Text),
                    new SqlParameter("@ApplicationTable", SqlDbType.Text),
					new SqlParameter("@StudyID", SqlDbType.Int,4)};
            parameters[0].Value = model.CardNo;
            parameters[1].Value = model.StudyRoomNo;
            parameters[2].Value = model.SubmitTime;
            parameters[3].Value = model.CheckTime;
            parameters[4].Value = model.BespeakTime;
            parameters[5].Value = model.UseTime;
            parameters[6].Value = (int)model.CheckState;
            parameters[7].Value = model.ChecklPerson;
            parameters[8].Value = model.Remark;
            parameters[9].Value = model.Application.ToString();
            parameters[10].Value = model.StudyID;

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
        public bool Delete(int StudyID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_SM_StudyBookingLog ");
            strSql.Append(" where StudyID=@StudyID");
            SqlParameter[] parameters = {
					new SqlParameter("@StudyID", SqlDbType.Int,4)
};
            parameters[0].Value = StudyID;

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
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string StudyIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_SM_StudyBookingLog ");
            strSql.Append(" where StudyID in (" + StudyIDlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
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
        public SeatManage.ClassModel.StudyBookingLog GetModel(int StudyID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 StudyID,CardNo,StudyRoomNo,SubmitTime,CheckTime,BespeakTime,UseTime,CheckState,ChecklPerson,Remark,ApplicationTable from T_SM_StudyBookingLog ");
            strSql.Append(" where StudyID=@StudyID");
            SqlParameter[] parameters = {
					new SqlParameter("@StudyID", SqlDbType.Int,4)
};
            parameters[0].Value = StudyID;

            SeatManage.ClassModel.StudyBookingLog model = new SeatManage.ClassModel.StudyBookingLog();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["StudyID"] != null && ds.Tables[0].Rows[0]["StudyID"].ToString() != "")
                {
                    model.StudyID = int.Parse(ds.Tables[0].Rows[0]["StudyID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CardNo"] != null && ds.Tables[0].Rows[0]["CardNo"].ToString() != "")
                {
                    model.CardNo = ds.Tables[0].Rows[0]["CardNo"].ToString();
                }
                if (ds.Tables[0].Rows[0]["StudyRoomNo"] != null && ds.Tables[0].Rows[0]["StudyRoomNo"].ToString() != "")
                {
                    model.StudyRoomNo = ds.Tables[0].Rows[0]["StudyRoomNo"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SubmitTime"] != null && ds.Tables[0].Rows[0]["SubmitTime"].ToString() != "")
                {
                    model.SubmitTime = DateTime.Parse(ds.Tables[0].Rows[0]["SubmitTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CheckTime"] != null && ds.Tables[0].Rows[0]["CheckTime"].ToString() != "")
                {
                    model.CheckTime = DateTime.Parse(ds.Tables[0].Rows[0]["CheckTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BespeakTime"] != null && ds.Tables[0].Rows[0]["BespeakTime"].ToString() != "")
                {
                    model.BespeakTime = DateTime.Parse(ds.Tables[0].Rows[0]["BespeakTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UseTime"] != null && ds.Tables[0].Rows[0]["UseTime"].ToString() != "")
                {
                    model.UseTime = int.Parse(ds.Tables[0].Rows[0]["UseTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CheckState"] != null && ds.Tables[0].Rows[0]["CheckState"].ToString() != "")
                {
                    model.CheckState = (SeatManage.EnumType.CheckStatus)int.Parse(ds.Tables[0].Rows[0]["CheckState"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ChecklPerson"] != null && ds.Tables[0].Rows[0]["ChecklPerson"].ToString() != "")
                {
                    model.ChecklPerson = ds.Tables[0].Rows[0]["ChecklPerson"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Remark"] != null && ds.Tables[0].Rows[0]["Remark"].ToString() != "")
                {
                    model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select StudyID,CardNo,StudyRoomNo,SubmitTime,CheckTime,BespeakTime,UseTime,CheckState,ChecklPerson,Remark,ApplicationTable ");
            strSql.Append(" FROM T_SM_StudyBookingLog ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" StudyID,CardNo,StudyRoomNo,SubmitTime,CheckTime,BespeakTime,UseTime,CheckState,ChecklPerson,Remark,ApplicationTable ");
            strSql.Append(" FROM T_SM_StudyBookingLog ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
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
            parameters[0].Value = "T_SM_StudyBookingLog";
            parameters[1].Value = "StudyID";
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

