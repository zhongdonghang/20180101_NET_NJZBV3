using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;//Please add references
using SeatManage.ClassModel;
namespace SeatManage.DAL
{
    /// <summary>
    /// 数据访问类:AMS_DeviceStatus
    /// </summary>
    public partial class AMS_DeviceStatus
    {
        public AMS_DeviceStatus()
        { }
        #region  Method

        /// <summary>
        ///  是否存在该记录
        /// </summary>
        /// <param name="DeviceNum">设备编号</param>
        /// <returns></returns>
        public bool Exists(string DeviceNum)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from AMS_Terminal");
            strSql.Append(" where DeviceNum=@DeviceNum ");
            SqlParameter[] parameters = {
					new SqlParameter("@DeviceNum", SqlDbType.NVarChar,50)};
            parameters[0].Value = DeviceNum;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(TerminalInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AMS_Terminal(");
            strSql.Append("DeviceNum,TerminalMacAddress,EmpowerLoseEfficacyTime,Status,ScreenshotPath,Date,IsUpdatePlayList,DeviceSetting,Describe)");
            strSql.Append(" values (");
            strSql.Append("@DeviceNum,@TerminalMacAddress,@EmpowerLoseEfficacyTime,@Status,@ScreenshotPath,@Date,@IsUpdatePlayList,@DeviceSetting,@Describe)");
            SqlParameter[] parameters = {
					new SqlParameter("@DeviceNum", SqlDbType.NVarChar,50),
                    new SqlParameter("@TerminalMacAddress",SqlDbType.NVarChar),
                    new SqlParameter("@EmpowerLoseEfficacyTime",SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.NVarChar,2),
					new SqlParameter("@ScreenshotPath", SqlDbType.NVarChar,100),
					new SqlParameter("@Date", SqlDbType.DateTime),
					new SqlParameter("@IsUpdatePlayList", SqlDbType.Bit,1),
					new SqlParameter("@DeviceSetting", SqlDbType.Text),
					new SqlParameter("@Describe", SqlDbType.NVarChar,500)};
            parameters[0].Value = model.ClientNo;
            parameters[1].Value = model.TerminalMacAddress;
            parameters[2].Value = model.EmpowerLoseEfficacyTime;
            parameters[3].Value = model.Status;
            parameters[4].Value = model.ScreenshotPath;
            parameters[5].Value = model.StatusUpdateTime;
            parameters[6].Value = model.IsUpdatePlayList;
            parameters[7].Value = model.DeviceSetting.ToString();
            parameters[8].Value = model.Describe;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return rows;
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(TerminalInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AMS_Terminal set ");
            strSql.Append("Status=@Status,");
            strSql.Append("TerminalMacAddress=@TerminalMacAddress,");
            strSql.Append("ScreenshotPath=@ScreenshotPath,");
            strSql.Append("Date=@Date,");
            strSql.Append("IsUpdatePlayList=@IsUpdatePlayList,");
            strSql.Append("DeviceSetting=@DeviceSetting,");
            strSql.Append("Describe=@Describe,");
            strSql.Append("EmpowerLoseEfficacyTime=@EmpowerLoseEfficacyTime,");
            strSql.Append("LastPrintTimes=@LastPrintTimes,");
            strSql.Append("PrintedTimes=@PrintedTimes,");
            strSql.Append("PrinterStatus=@PrinterStatus ");
            strSql.Append(" where DeviceNum=@DeviceNum ");

            SqlParameter[] parameters = {
					new SqlParameter("@Status", SqlDbType.NVarChar,2),
                    new SqlParameter("@TerminalMacAddress",SqlDbType.NVarChar),
					new SqlParameter("@ScreenshotPath", SqlDbType.NVarChar,100),
					new SqlParameter("@Date", SqlDbType.DateTime),
					new SqlParameter("@IsUpdatePlayList", SqlDbType.Bit,1),
					new SqlParameter("@DeviceSetting", SqlDbType.Text),
					new SqlParameter("@Describe", SqlDbType.NVarChar,500),
                    new SqlParameter("@LastPrintTimes", SqlDbType.Int),
                    new SqlParameter("@PrintedTimes", SqlDbType.Int),
                    new SqlParameter("@PrinterStatus", SqlDbType.Bit),
					new SqlParameter("@DeviceNum", SqlDbType.NVarChar,50),
                                        new SqlParameter("@EmpowerLoseEfficacyTime",SqlDbType.DateTime)
        };
            parameters[0].Value = model.Status;
            parameters[1].Value = model.TerminalMacAddress;
            parameters[2].Value = model.ScreenshotPath;
            parameters[3].Value = model.StatusUpdateTime;
            parameters[4].Value = model.IsUpdatePlayList;
            parameters[5].Value = model.DeviceSetting.ToString();
            parameters[6].Value = model.Describe;
            parameters[7].Value = model.LastPrintTimes;
            parameters[8].Value = model.PrintedTimes;
            parameters[9].Value = model.PrinterStatus;
            parameters[10].Value = model.ClientNo;
            parameters[11].Value = model.EmpowerLoseEfficacyTime;
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return rows;
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(TerminalInfoV2 model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AMS_Terminal(");
            strSql.Append("DeviceNum,TerminalMacAddress,EmpowerLoseEfficacyTime,Status,ScreenshotPath,Date,IsUpdatePlayList,DeviceSetting,Describe)");
            strSql.Append(" values (");
            strSql.Append("@DeviceNum,@TerminalMacAddress,@EmpowerLoseEfficacyTime,@Status,@ScreenshotPath,@Date,@IsUpdatePlayList,@DeviceSetting,@Describe)");
            SqlParameter[] parameters = {
					new SqlParameter("@DeviceNum", SqlDbType.NVarChar,50),
                    new SqlParameter("@TerminalMacAddress",SqlDbType.NVarChar),
                    new SqlParameter("@EmpowerLoseEfficacyTime",SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.NVarChar,2),
					new SqlParameter("@ScreenshotPath", SqlDbType.NVarChar,100),
					new SqlParameter("@Date", SqlDbType.DateTime),
					new SqlParameter("@IsUpdatePlayList", SqlDbType.Bit,1),
					new SqlParameter("@DeviceSetting", SqlDbType.Text),
					new SqlParameter("@Describe", SqlDbType.NVarChar,500)};
            parameters[0].Value = model.ClientNo;
            parameters[1].Value = model.TerminalMacAddress;
            parameters[2].Value = model.EmpowerLoseEfficacyTime;
            parameters[3].Value = model.Status;
            parameters[4].Value = model.ScreenshotPath;
            parameters[5].Value = model.StatusUpdateTime;
            parameters[6].Value = model.IsUpdatePlayList;
            parameters[7].Value = model.DeviceSetting.ToString();
            parameters[8].Value = model.Describe;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return rows;
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(TerminalInfoV2 model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AMS_Terminal set ");
            strSql.Append("Status=@Status,");
            strSql.Append("TerminalMacAddress=@TerminalMacAddress,");
            strSql.Append("ScreenshotPath=@ScreenshotPath,");
            strSql.Append("Date=@Date,");
            strSql.Append("IsUpdatePlayList=@IsUpdatePlayList,");
            strSql.Append("DeviceSetting=@DeviceSetting,");
            strSql.Append("Describe=@Describe,");
            strSql.Append("EmpowerLoseEfficacyTime=@EmpowerLoseEfficacyTime,");
            strSql.Append("LastPrintTimes=@LastPrintTimes,");
            strSql.Append("PrintedTimes=@PrintedTimes,");
            strSql.Append("PrinterStatus=@PrinterStatus ");
            strSql.Append(" where DeviceNum=@DeviceNum ");

            SqlParameter[] parameters = {
					new SqlParameter("@Status", SqlDbType.NVarChar,2),
                    new SqlParameter("@TerminalMacAddress",SqlDbType.NVarChar),
					new SqlParameter("@ScreenshotPath", SqlDbType.NVarChar,100),
					new SqlParameter("@Date", SqlDbType.DateTime),
					new SqlParameter("@IsUpdatePlayList", SqlDbType.Bit,1),
					new SqlParameter("@DeviceSetting", SqlDbType.Text),
					new SqlParameter("@Describe", SqlDbType.NVarChar,500),
                    new SqlParameter("@LastPrintTimes", SqlDbType.Int),
                    new SqlParameter("@PrintedTimes", SqlDbType.Int),
                    new SqlParameter("@PrinterStatus", SqlDbType.Bit),
					new SqlParameter("@DeviceNum", SqlDbType.NVarChar,50),
                                        new SqlParameter("@EmpowerLoseEfficacyTime",SqlDbType.DateTime)
        };
            parameters[0].Value = model.Status;
            parameters[1].Value = model.TerminalMacAddress;
            parameters[2].Value = model.ScreenshotPath;
            parameters[3].Value = model.StatusUpdateTime;
            parameters[4].Value = model.IsUpdatePlayList;
            parameters[5].Value = model.DeviceSetting.ToString();
            parameters[6].Value = model.Describe;
            parameters[7].Value = model.LastPrintTimes;
            parameters[8].Value = model.PrintedTimes;
            parameters[9].Value = model.PrinterStatus;
            parameters[10].Value = model.ClientNo;
            parameters[11].Value = model.EmpowerLoseEfficacyTime;
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return rows;
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(string DeviceNum)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from AMS_Terminal ");
            strSql.Append(" where DeviceNum=@DeviceNum ");
            SqlParameter[] parameters = {
					new SqlParameter("@DeviceNum", SqlDbType.NVarChar,50)};
            parameters[0].Value = DeviceNum;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return rows;

        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public int DeleteList(string DeviceNumlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from AMS_Terminal ");
            strSql.Append(" where DeviceNum in (" + DeviceNumlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            return rows;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="parameters">条件中的参数</param>
        /// <returns></returns>
        public DataSet GetList(string strWhere, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select DeviceNum,TerminalMacAddress,EmpowerLoseEfficacyTime,Status,ScreenshotPath,Date,IsUpdatePlayList,DeviceSetting,Describe,LastPrintTimes,PrintedTimes,PrinterStatus ");
            strSql.Append(" FROM AMS_Terminal ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString(), parameters);
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
            parameters[0].Value = "AMS_DeviceStatus";
            parameters[1].Value = "DeviceNum";
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

