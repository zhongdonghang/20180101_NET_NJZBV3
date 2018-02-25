using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading;

namespace SeatManage.SeatClient.Config.Code
{
    public class SyncOldData
    {
        private string Old_connectionString = "";
        private string New_connectionString = "";
        private int schoolcount = 0;
        private int libcount = 0;
        private int roomcount = 0;
        public SyncOldData(string oldconn, string newconn)
        {
            Old_connectionString = oldconn;
            New_connectionString = newconn;
        }
        public void Update()
        {
            try
            {
                if (Progress != null)
                {
                    Progress("数据获取中……");
                }
                GetSchools();
                if (Progress != null)
                {
                    Progress("同步了" + schoolcount + "个校区，" + libcount + "个图书馆，" + roomcount + "个阅览室");
                }
            }
            catch (Exception e)
            {
                List<SeatManage.ClassModel.School> schools = SeatManage.Bll.T_SM_School.GetSchoolInfoList(null, null);
                foreach (SeatManage.ClassModel.School sc in schools)
                {
                    SeatManage.Bll.T_SM_School.DeleteSchool(sc);
                }
                if (Progress != null)
                {
                    Progress("导入旧数据出错！请手动同步-_-|||");
                }
            }
        }
        /// <summary>
        /// 获取学校列表
        /// </summary>
        /// <returns></returns>
        private void GetSchools()
        {
            try
            {
                string sqlstr = "SELECT [SchoolID],[SchoolNo],[SchoolName] ,[SchoolOrder] FROM [T_SM_School]";
                DataSet ds = Query(sqlstr, Old_connectionString, null);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ClassModel.School sc = new ClassModel.School();
                    sc.No = dr["SchoolNo"].ToString();
                    sc.Name = dr["SchoolName"].ToString();
                    if (Progress != null)
                    {
                        Progress("正在导入校区:" + dr["SchoolName"].ToString() + "……");
                    }
                    if (!SeatManage.Bll.T_SM_School.AddNewSchool(sc))
                    {
                        throw new Exception("添加校区出错！");
                    }
                    else
                    {
                        if (Progress != null)
                        {
                            Progress("校区:" + dr["SchoolName"].ToString() + "导入成功！");
                        }
                    }
                    schoolcount++;
                    GetLibs(sc, dr["SchoolID"].ToString());
                }
            }
            catch
            {
                throw;
            }
        }
        private void GetLibs(ClassModel.School school, string schoolid)
        {
            try
            {
                string sqlstr = "SELECT [LibraryID],[LibraryNo],[LibraryName],[LibraryOrder],[SchoolID] FROM [T_SM_Library] WHERE [SchoolID]='" + schoolid + "'";
                DataSet ds = Query(sqlstr, Old_connectionString, null);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ClassModel.LibraryInfo lib = new ClassModel.LibraryInfo();
                    lib.No = (libcount + 1).ToString("X2");
                    lib.Name = dr["LibraryName"].ToString();
                    lib.School = school;
                    if (Progress != null)
                    {
                        Progress("正在导入图书馆:" + dr["LibraryName"].ToString() + "……");
                    }
                    if (!SeatManage.Bll.T_SM_Library.AddNewLibrary(lib))
                    {
                        throw new Exception("添加图书馆出错！");
                    }
                    else
                    {
                        if (Progress != null)
                        {
                            Progress("图书馆:" + dr["LibraryName"].ToString() + "导入成功！");
                        }
                    }
                    libcount++;
                    GetReadingRooms(lib, dr["LibraryID"].ToString());
                }
            }
            catch
            {
                throw;
            }
        }
        private void GetReadingRooms(ClassModel.LibraryInfo libinfo, string libid)
        {
            try
            {
                string sqlstr = "SELECT [ReadingRoomID],[ReadingRoomNo],[ReadingRoomName],[ReadingRoomOrder],[LibraryID],[ReadingSetting],[RoomSeat] FROM [T_SM_ReadingRoom] WHERE [LibraryID]='" + libid + "'";
                DataSet ds = Query(sqlstr, Old_connectionString, null);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ClassModel.ReadingRoomInfo room = new ClassModel.ReadingRoomInfo();
                    room.Libaray = libinfo;
                    room.Name = dr["ReadingRoomName"].ToString();
                    ClassModel.SeatLayout sl = ClassModel.SeatLayout.GetSeatLayout(dr["RoomSeat"].ToString());
                    sl.RoomNo = sl.Seats.Keys.ElementAt(0).Substring(0, 6);
                    room.No = sl.RoomNo;
                    room.SeatList = sl;
                    room.Setting = new ClassModel.ReadingRoomSetting();
                    if (Progress != null)
                    {
                        Progress("正在导入阅览室:" + dr["ReadingRoomName"].ToString() + "……");
                    }
                    if (!SeatManage.Bll.T_SM_ReadingRoom.AddNewReadingRoom(room))
                    {
                        throw new Exception("添加阅览室失败！");
                    }
                    else
                    {
                        if (Progress != null)
                        {
                            Progress("阅览室:" + dr["ReadingRoomName"].ToString() + "导入成功！");
                        }
                    }
                    roomcount++;

                    if (SeatManage.Bll.T_SM_ReadingRoom.UpdateSeatLayout(room.SeatList) == SeatManage.EnumType.HandleResult.Failed)
                    {
                        throw new Exception("添加座位失败！");
                    }
                    else
                    {
                        if (Progress != null)
                        {
                            Progress("阅览室:" + dr["ReadingRoomName"].ToString() + "座位导入成功！");
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }


        int alllogcount;
        public delegate void EventHanleProgress(string message);
        public event EventHanleProgress Progress;
        public void GetOldEOlog()
        {
            if (Progress != null)
            {
                Progress("数据获取中……");
            }
            alllogcount = 0;
            string sqlstr = "SELECT [CardNo],[EnterOutLogNo],SUBSTRING([SeatID],0,7) as ReadingRoomNo,[SeatID] as SeatNo,'' as TerminalNum,"
                          + "case [EnterOutState] when 0 then 0 when 3 then 0 when 1 then 1 when 2 then 8 when 4 then 5 else 0 end as EnterOutState, "
                          + "[EnterOutTime],[EnterOutType],"
                          + "case [EnterFlag] when 0 then 1 when 1 then 0 when 2 then 2 else 1 end as EnterFlag,"
                          + "'旧数据同步' as Remark, '1900-1-1 0:00:00' as [MarkTime] FROM [T_SM_EnterOutLog] Where [EnterOutTime]<'" + DateTime.Now.ToShortDateString() + "' ";
            DataSet ds = Query(sqlstr, Old_connectionString, null);
            alllogcount = ds.Tables[0].Rows.Count;
            SqlBulkCopy sbc = new SqlBulkCopy(New_connectionString);
            sbc.DestinationTableName = "[T_SM_EnterOutLog]";
            sbc.BatchSize = 100;
            sbc.NotifyAfter = 100;
            sbc.SqlRowsCopied += new SqlRowsCopiedEventHandler(sbc_SqlRowsCopied);
            sbc.ColumnMappings.Add(0, 1);
            sbc.ColumnMappings.Add(1, 2);
            sbc.ColumnMappings.Add(2, 3);
            sbc.ColumnMappings.Add(3, 4);
            sbc.ColumnMappings.Add(4, 5);
            sbc.ColumnMappings.Add(5, 6);
            sbc.ColumnMappings.Add(6, 7);
            sbc.ColumnMappings.Add(7, 8);
            sbc.ColumnMappings.Add(8, 9);
            sbc.ColumnMappings.Add(9, 10);
            sbc.ColumnMappings.Add(10, 11);
            sbc.WriteToServer(ds.Tables[0]);
            if (Progress != null)
            {
                Progress("导入完成！");
            }
        }


        void sbc_SqlRowsCopied(object sender, SqlRowsCopiedEventArgs e)
        {
            if (Progress != null)
            {
                Progress("数据导入中…… " + e.RowsCopied + "/" + alllogcount);
            }
        }
        public void BookUserData()
        {

            try
            {
                if (Progress != null)
                {
                    Progress("数据获取中……");
                }
                int allcount = 0;
                int nownum = 0;
                int okcount = 0;
                string sqlstr = "SELECT [UsrName],[UsrPwd],[LoginID],[IsDel],[UsrType] FROM [Users_ALL] where [UsrType]='s' and [IsDel]='N'";
                DataSet ds = Query(sqlstr, Old_connectionString, null);
                allcount = ds.Tables[0].Rows.Count;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SeatManage.ClassModel.UserInfo user = new ClassModel.UserInfo();
                    user.IsAdmin = false;
                    user.IsUsing = SeatManage.EnumType.LogStatus.Valid;
                    user.LoginId = dr["LoginID"].ToString();
                    user.Password = SeatManageComm.MD5Algorithm.GetMD5Str32(dr["UsrPwd"].ToString());
                    user.UserName = dr["UsrName"].ToString();
                    user.UserType = SeatManage.EnumType.UserType.Reader;
                    if (SeatManage.Bll.Users_ALL.AddNewUser(user))
                    {
                        okcount++;
                    }
                    nownum++;
                    if (Progress != null)
                    {
                        Progress("同步中…… " + nownum + "/" + allcount);
                    }
                }
                if (Progress != null)
                {
                    Progress("导入完成，总共" + allcount + "条，成功" + (okcount) + "条");
                }

            }
            catch
            {
                if (Progress != null)
                {
                    Progress("导入失败！");
                }
            }
        }
        #region 数据库操作
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public DataSet Query(string SQLString, string connectionString, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    return ds;
                }
            }
        }
        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public int ExecuteSql(string SQLString, string connectionString, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        int rows = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        return rows;
                    }
                    catch (System.Data.SqlClient.SqlException E)
                    {
                        throw new Exception(E.Message);
                    }
                }
            }
        }
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {


                foreach (SqlParameter parameter in cmdParms)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
        }
        #endregion
    }
}
