﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Xml;
using System.IO;

namespace UpdateOperate.Code
{
    public class AddBookKey
    {
        string connectionString = "";
        public string BookKeyon()
        {

            try
            {
                string sqlstr = "SELECT [BespeakID],[CardNo],[ReadingRoomNo],[SeatNo],[BespeakTime],[BespeakState],[SubmitTime],[CancelTime],[BespeakCancelPerson],[Remark],[BespeakFlag] FROM [T_SM_SeatBespeak] WHERE [BespeakFlag] is null";
                DataSet ds = Query(sqlstr, connectionString, null);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string instr = "UPDATE [T_SM_SeatBespeak]" +
                                   " SET [BespeakFlag] = '" +
                                   SeatManage.SeatManageComm.MD5Algorithm.GetMD5Str32WithListKey(new List<string>() { Convert.ToDateTime(dr["BespeakTime"]).ToString(), Convert.ToDateTime(dr["SubmitTime"]).ToString(), dr["SeatNo"].ToString(), dr["CardNo"].ToString(), "Bespeak" }) +
                                   "' WHERE BespeakID=" + dr["BespeakID"].ToString();
                    ExecuteSql(instr, connectionString, null);
                }
                return "处理完成！";
            }
            catch (Exception ex)
            {
                return "处理出错！";
            }
        }
        public string GetCommstr()
        {
           XmlDocument doc = new XmlDocument();
            string fileDircetoryPath = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = string.Format("{0}WindowsServiceHost.exe.config", fileDircetoryPath);
            if (File.Exists(filePath))
            {
                doc.Load(filePath);
                XmlNodeList nodes = doc.SelectNodes("//configuration/connectionStrings/add");
                foreach (XmlNode node in nodes)
                {
                    if (node.Attributes["name"].Value == "ConnectionString")
                    {
                        connectionString = node.Attributes["connectionString"].Value;
                        return "";
                    }
                }
                return "找不到配置文件！";
            }
            else
            {
                return "找不到配置文件！";
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
    }
}
        #endregion