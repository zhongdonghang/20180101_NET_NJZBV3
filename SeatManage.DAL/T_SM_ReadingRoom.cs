using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;
using SeatManage.ClassModel;
namespace SeatManage.DAL
{
	/// <summary>
	/// 数据访问类:T_SM_ReadingRoom
	/// </summary>
	public partial class T_SM_ReadingRoom
	{
		public T_SM_ReadingRoom()
		{}
        #region  Method

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string ReadingRoomNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_SM_ReadingRoom");
            strSql.Append(" where ReadingRoomNo=@ReadingRoomNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@ReadingRoomNo", SqlDbType.NVarChar,50)};
            parameters[0].Value = ReadingRoomNo;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetList(string strWhere, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ReadingRoomNo,ReadingRoomName,ReadingSetting,RoomSeat,SchoolName,LibraryName,LibraryNo,SchoolNo,AreaName,AreaInfo ");
            strSql.Append(" FROM ViewReadingRoom ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        /// <param name="Top"></param>
        /// <param name="strWhere"></param>
        /// <param name="filedOrder"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DataSet GetList(int Top, string strWhere, string filedOrder, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" ReadingRoomNo,ReadingRoomName,ReadingSetting,RoomSeat,SchoolName,LibraryName,LibraryNo,SchoolNo,AreaName ");
            strSql.Append(" FROM ViewReadingRoom ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(SeatManage.ClassModel.ReadingRoomInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_SM_ReadingRoom(");
            strSql.Append("ReadingRoomNo,ReadingRoomName,LibraryNo,ReadingSetting,RoomSeat,AreaName)");
            strSql.Append(" values (");
            strSql.Append("@ReadingRoomNo,@ReadingRoomName,@LibraryNo,@ReadingSetting,@RoomSeat,@AreaName)");
            SqlParameter[] parameters = {
                    new SqlParameter("@ReadingRoomNo", SqlDbType.NVarChar,50),
                    new SqlParameter("@ReadingRoomName", SqlDbType.NVarChar,100),
                    new SqlParameter("@LibraryNo", SqlDbType.NVarChar,50),
                    new SqlParameter("@ReadingSetting", SqlDbType.Text),
                    new SqlParameter("@RoomSeat", SqlDbType.Text),
                    new SqlParameter("@AreaName", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.No;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.Libaray.No;
            parameters[3].Value = model.Setting.ToString();
            parameters[4].Value = model.SeatList.ToString();
            parameters[5].Value = model.Area.AreaName;
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
        public bool Update(SeatManage.ClassModel.ReadingRoomInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_SM_ReadingRoom set ");
            strSql.Append("ReadingRoomName=@ReadingRoomName,");
            strSql.Append("LibraryNo=@LibraryNo,");
            strSql.Append("ReadingSetting=@ReadingSetting,");
            strSql.Append("RoomSeat=@RoomSeat,");
            strSql.Append("AreaName=@AreaName");
            strSql.Append(" where ReadingRoomNo=@ReadingRoomNo ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ReadingRoomName", SqlDbType.NVarChar,100),
                    new SqlParameter("@LibraryNo", SqlDbType.NVarChar,50),
                    new SqlParameter("@ReadingSetting", SqlDbType.Text),
                    new SqlParameter("@RoomSeat", SqlDbType.Text),
                    new SqlParameter("@AreaName", SqlDbType.NVarChar,50),
                    new SqlParameter("@ReadingRoomNo", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.Name;
            parameters[1].Value = model.Libaray.No;
            parameters[2].Value = model.Setting.ToString();
            parameters[3].Value = model.SeatList.ToString();
            parameters[4].Value = model.Area.AreaName;
            parameters[5].Value = model.No;

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
        public bool Delete(SeatManage.ClassModel.ReadingRoomInfo model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_SM_ReadingRoom ");
            strSql.Append(" where ReadingRoomNo=@ReadingRoomNo ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ReadingRoomNo", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.No;

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
        ///// 批量删除数据
        ///// </summary>
        //public bool DeleteList(string ReadingRoomNolist )
        //{
        //    StringBuilder strSql=new StringBuilder();
        //    strSql.Append("delete from T_SM_ReadingRoom ");
        //    strSql.Append(" where ReadingRoomNo in ("+ReadingRoomNolist + ")  ");
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
        //public SeatManage.Model.T_SM_ReadingRoom GetModel(string ReadingRoomNo)
        //{

        //    StringBuilder strSql=new StringBuilder();
        //    strSql.Append("select  top 1 ReadingRoomNo,ReadingRoomName,LibraryNo,ReadingSetting,RoomSeat from T_SM_ReadingRoom ");
        //    strSql.Append(" where ReadingRoomNo=@ReadingRoomNo ");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@ReadingRoomNo", SqlDbType.NVarChar,50)};
        //    parameters[0].Value = ReadingRoomNo;

        //    SeatManage.Model.T_SM_ReadingRoom model=new SeatManage.Model.T_SM_ReadingRoom();
        //    DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
        //    if(ds.Tables[0].Rows.Count>0)
        //    {
        //        if(ds.Tables[0].Rows[0]["ReadingRoomNo"]!=null && ds.Tables[0].Rows[0]["ReadingRoomNo"].ToString()!="")
        //        {
        //            model.ReadingRoomNo=ds.Tables[0].Rows[0]["ReadingRoomNo"].ToString();
        //        }
        //        if(ds.Tables[0].Rows[0]["ReadingRoomName"]!=null && ds.Tables[0].Rows[0]["ReadingRoomName"].ToString()!="")
        //        {
        //            model.ReadingRoomName=ds.Tables[0].Rows[0]["ReadingRoomName"].ToString();
        //        }
        //        if(ds.Tables[0].Rows[0]["LibraryNo"]!=null && ds.Tables[0].Rows[0]["LibraryNo"].ToString()!="")
        //        {
        //            model.LibraryNo=int.Parse(ds.Tables[0].Rows[0]["LibraryNo"].ToString());
        //        }
        //        if(ds.Tables[0].Rows[0]["ReadingSetting"]!=null && ds.Tables[0].Rows[0]["ReadingSetting"].ToString()!="")
        //        {
        //            model.ReadingSetting=ds.Tables[0].Rows[0]["ReadingSetting"].ToString();
        //        }
        //        if(ds.Tables[0].Rows[0]["RoomSeat"]!=null && ds.Tables[0].Rows[0]["RoomSeat"].ToString()!="")
        //        {
        //            model.RoomSeat=ds.Tables[0].Rows[0]["RoomSeat"].ToString();
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
            parameters[0].Value = "T_SM_ReadingRoom";
            parameters[1].Value = "ReadingRoomNo";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/


        public ReadingRoomInfo DataRowToReadingRoomInfo(DataRow dr)
        {
            ReadingRoomInfo roomInfo = new ReadingRoomInfo();
            roomInfo.No = dr["ReadingRoomNo"].ToString();
            roomInfo.Name = dr["ReadingRoomName"].ToString();
            if (!String.IsNullOrEmpty(dr["ReadingSetting"].ToString()))
            {
                roomInfo.Setting = new ReadingRoomSetting(dr["ReadingSetting"].ToString());
            }
            else
            {
                roomInfo.Setting = new ReadingRoomSetting();
            }
            if (!string.IsNullOrEmpty(dr["RoomSeat"].ToString()))
            {
                roomInfo.SeatList = SeatLayout.GetSeatLayout(dr["RoomSeat"].ToString());
            }
            else
            {
                roomInfo.SeatList = new SeatLayout();
            }
            roomInfo.Libaray.No = dr["LibraryNo"].ToString();
            roomInfo.Libaray.Name = dr["LibraryName"].ToString();
            roomInfo.Libaray.School.No = dr["SchoolNo"].ToString();
            roomInfo.Libaray.School.Name = dr["SchoolName"].ToString();
            roomInfo.Libaray.AreaList = roomInfo.Libaray.ToList(dr["AreaInfo"].ToString());
            if (dr["AreaName"] != null && !string.IsNullOrEmpty(dr["AreaName"].ToString()))
            {
                foreach (AreaInfo item in roomInfo.Libaray.AreaList)
                {
                    if (dr["AreaName"].ToString() == item.AreaName)
                    {
                        roomInfo.Area = item;
                        break;
                    }
                }
            }
            return roomInfo;
        }
        #endregion  Method
	}
}

