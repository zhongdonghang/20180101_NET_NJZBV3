using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace SeatManage.SeatClient.Config.Code
{
    public class SyncUserAll
    {
        public delegate void EventHanleProgress(string message);
        public event EventHanleProgress Progress;
        private List<SeatManage.ClassModel.ReaderInfo> readerList;
        private string connectionString = "";
        public SyncUserAll(string newconn)
        {
            connectionString = newconn;
        }
        public void Run()
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
                string sqlstr = "SELECT [CardNo] ,[CardID] ,[ReaderName],[Sex],[ReaderTypeName],[ReaderDeptName],[ReaderProName],[Flag] FROM [dbo].[T_SM_Reader]";
                DataSet ds = Query(sqlstr, connectionString, null);
                allcount = ds.Tables[0].Rows.Count;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string isSameSQL = "SELECT [LoginID],[UsrName],[UsrPwd],[UsrType],[UsrEnabled],[Remark] FROM [dbo].[Users_ALL] WHERE [LoginID]='" + dr["CardNo"] + "'";
                    DataSet sds = Query(isSameSQL, connectionString, null);
                    if (sds.Tables[0].Rows.Count > 0)
                    {
                        if (Progress != null)
                        {
                            Progress(nownum + "/" + allcount + "  " + dr["CardNo"].ToString().Trim() + " " + dr["ReaderName"].ToString().Trim() + " 已存在");
                        }
                    }
                    else
                    {
                        SeatManage.ClassModel.UserInfo user = new ClassModel.UserInfo();
                        user.IsAdmin = false;
                        user.IsUsing = SeatManage.EnumType.LogStatus.Valid;
                        user.LoginId = dr["CardNo"].ToString();
                        user.Password = SeatManageComm.MD5Algorithm.GetMD5Str32(dr["CardNo"].ToString());
                        user.UserName = dr["ReaderName"].ToString();
                        user.UserType = SeatManage.EnumType.UserType.Reader;
                        user.Remark = "同步工具添加";
                        if (SeatManage.Bll.Users_ALL.AddNewUser(user))
                        {
                            okcount++;

                            if (Progress != null)
                            {
                                Progress(nownum + "/" + allcount + "  " + dr["CardNo"].ToString().Trim() + " " + dr["ReaderName"].ToString().Trim() + " 添加成功");
                            }
                        }
                        else
                        {
                            if (Progress != null)
                            {
                                Progress(nownum + "/" + allcount + "  " + dr["CardNo"].ToString().Trim() + " " + dr["ReaderName"].ToString().Trim() + " 添加失败");
                            }
                        }
                    }
                    nownum++;
                }
                if (Progress != null)
                {
                    Progress("导入完成，总共" + allcount + "条，添加" + okcount + "条");
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
