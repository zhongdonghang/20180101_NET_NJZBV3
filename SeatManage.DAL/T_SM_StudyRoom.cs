using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;//Please add references
namespace SeatManage.DAL
{
    /// <summary>
    /// 数据访问类:T_SM_StudyRoom
    /// </summary>
    public partial class T_SM_StudyRoom
    {
        public T_SM_StudyRoom()
        { }
        #region  Method

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string StudyRoomNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_SM_StudyRoom");
            strSql.Append(" where StudyRoomNo=@StudyRoomNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@StudyRoomNo", SqlDbType.NVarChar,50)};
            parameters[0].Value = StudyRoomNo;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(SeatManage.ClassModel.StudyRoomInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_SM_StudyRoom(");
            strSql.Append("StudyRoomNo,StudyRoomName,StudyRoomSetting,Remark,RoomImage)");
            strSql.Append(" values (");
            strSql.Append("@StudyRoomNo,@StudyRoomName,@StudyRoomSetting,@Remark,@RoomImage)");
            SqlParameter[] parameters = {
					new SqlParameter("@StudyRoomNo", SqlDbType.NVarChar,50),
					new SqlParameter("@StudyRoomName", SqlDbType.NVarChar,50),
					new SqlParameter("@StudyRoomSetting", SqlDbType.Text),
					new SqlParameter("@Remark", SqlDbType.Text),
                    new SqlParameter("@RoomImage", SqlDbType.Text)};
            parameters[0].Value = model.StudyRoomNo;
            parameters[1].Value = model.StudyRoomName;
            parameters[2].Value = model.Setting.ToString();
            parameters[3].Value = model.Remark;
            parameters[4].Value = model.RoomImage;
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
        /// 更新一条数据
        /// </summary>
        public bool Update(SeatManage.ClassModel.StudyRoomInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_SM_StudyRoom set ");
            strSql.Append("StudyRoomName=@StudyRoomName,");
            strSql.Append("StudyRoomSetting=@StudyRoomSetting,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("RoomImage=@RoomImage");
            strSql.Append(" where StudyRoomNo=@StudyRoomNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@StudyRoomName", SqlDbType.NVarChar,50),
					new SqlParameter("@StudyRoomSetting", SqlDbType.Text),
					new SqlParameter("@Remark", SqlDbType.Text),
                    new SqlParameter("@RoomImage", SqlDbType.Text),
					new SqlParameter("@StudyRoomNo", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.StudyRoomName;
            parameters[1].Value = model.Setting.ToString();
            parameters[2].Value = model.Remark;
            parameters[3].Value = model.RoomImage;
            parameters[4].Value = model.StudyRoomNo;

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
        public bool Delete(string StudyRoomNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_SM_StudyRoom ");
            strSql.Append(" where StudyRoomNo=@StudyRoomNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@StudyRoomNo", SqlDbType.NVarChar,50)};
            parameters[0].Value = StudyRoomNo;

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
        public bool DeleteList(string StudyRoomNolist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_SM_StudyRoom ");
            strSql.Append(" where StudyRoomNo in (" + StudyRoomNolist + ")  ");
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
        public SeatManage.ClassModel.StudyRoomInfo GetModel(string StudyRoomNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 StudyRoomNo,StudyRoomName,StudyRoomSetting,Remark,RoomImage from T_SM_StudyRoom ");
            strSql.Append(" where StudyRoomNo=@StudyRoomNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@StudyRoomNo", SqlDbType.NVarChar,50)};
            parameters[0].Value = StudyRoomNo;

            SeatManage.ClassModel.StudyRoomInfo model = new SeatManage.ClassModel.StudyRoomInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["StudyRoomNo"] != null && ds.Tables[0].Rows[0]["StudyRoomNo"].ToString() != "")
                {
                    model.StudyRoomNo = ds.Tables[0].Rows[0]["StudyRoomNo"].ToString();
                }
                if (ds.Tables[0].Rows[0]["StudyRoomName"] != null && ds.Tables[0].Rows[0]["StudyRoomName"].ToString() != "")
                {
                    model.StudyRoomName = ds.Tables[0].Rows[0]["StudyRoomName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["StudyRoomSetting"] != null && ds.Tables[0].Rows[0]["StudyRoomSetting"].ToString() != "")
                {
                    model.Setting = model.Setting = new ClassModel.StudyRoomSetting(ds.Tables[0].Rows[0]["StudyRoomSetting"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Remark"] != null && ds.Tables[0].Rows[0]["Remark"].ToString() != "")
                {
                    model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RoomImage"] != null && ds.Tables[0].Rows[0]["RoomImage"].ToString() != "")
                {
                    model.RoomImage = ds.Tables[0].Rows[0]["RoomImage"].ToString();
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
            strSql.Append("select StudyRoomNo,StudyRoomName,StudyRoomSetting,Remark,RoomImage ");
            strSql.Append(" FROM T_SM_StudyRoom ");
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
            strSql.Append(" StudyRoomNo,StudyRoomName,StudyRoomSetting,Remark,RoomImage ");
            strSql.Append(" FROM T_SM_StudyRoom ");
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
            parameters[0].Value = "T_SM_StudyRoom";
            parameters[1].Value = "StudyRoomNo";
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

