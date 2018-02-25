/**  版本信息模板在安装目录下，可自行修改。
* T_SM_RoomUsageStatistics.cs
*
* 功 能： N/A
* 类 名： T_SM_RoomUsageStatistics
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2015/7/30 16:01:25   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;
using DBUtility;
using SeatManage.ClassModel;

//Please add references
namespace SeatManage.DAL
{
    /// <summary>
    /// 数据访问类:T_SM_RoomUsageStatistics
    /// </summary>
    public partial class T_SM_RoomUsageStatistics
    {
        public T_SM_RoomUsageStatistics()
        { }
        #region  BasicMethod



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(RoomUsageStatistics model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_SM_RoomUsageStatistics(");
            strSql.Append("ReadingRoomNo,StatisticsDate,OpenTime,CloseTime,RoomUsageTime,SeatAllCount,SeatUsageCount,SeatUsageTime,ReaderUsageCount,UsedReaderCount,RushCardOperatingCount,SelectSeatCount,SelectSeatByAdmin,SelectSeatByReader,SelectSeatInSeatClient,SelectSeatInOtherClient,ReselectSeatCount,ReselectSeatInSeatClient,ReselectSeatInOtherClient,CheckBespeakCount,CheckBespeakInSeatClient,CkeckBespeakInOtherClient,WaitSeatCount,ShortLeaveCount,ShortLeaveTime,ShortLeaveByAdmin,ShortLeaveByReader,ShortLeaveByOtherReader,ShortLeaveByService,ShortLeaveInSeatClient,ShortLeaveInOtherClient,LeaveCount,LeaveByAdmin,LeaveByReader,LeaveByService,LeaveInSeatClient,LeaveInOtherClient,ComeBackCount,ComeBackByAdmin,ComeBackByReader,ComeBackByOtherReader,ComeBackInSeatClient,ComeBackInOtherClient,ContinueTimeCount,ContinueTimeByReader,ContinueTimeByService,ContinueTimeInSeatClient,ContinueTimeInOtherClient,AllBespeakCount,BespeakCount,CanBesapeakSeat,BespeakedSeat,BespeakCancel,BespeakOverTime,BespeakCheck,NowDayBespeakCheck,NowDayBespeakCount,NowDayBespeakOverTime,NowDayBespeakCancel,ViolationRecordCount,VRBookingTimeOut,VRSeatOutTime,VRLeaveByAdmin,VRShortLeaveOutTime,VRShortLeaveByAdminOutTime,VRShortLeaveByReaderOutTime,VRShortLeaveByServiceOutTime,VRLeaveNotReadCard)");
            strSql.Append(" values (");
            strSql.Append("@ReadingRoomNo,@StatisticsDate,@OpenTime,@CloseTime,@RoomUsageTime,@SeatAllCount,@SeatUsageCount,@SeatUsageTime,@ReaderUsageCount,@UsedReaderCount,@RushCardOperatingCount,@SelectSeatCount,@SelectSeatByAdmin,@SelectSeatByReader,@SelectSeatInSeatClient,@SelectSeatInOtherClient,@ReselectSeatCount,@ReselectSeatInSeatClient,@ReselectSeatInOtherClient,@CheckBespeakCount,@CheckBespeakInSeatClient,@CkeckBespeakInOtherClient,@WaitSeatCount,@ShortLeaveCount,@ShortLeaveTime,@ShortLeaveByAdmin,@ShortLeaveByReader,@ShortLeaveByOtherReader,@ShortLeaveByService,@ShortLeaveInSeatClient,@ShortLeaveInOtherClient,@LeaveCount,@LeaveByAdmin,@LeaveByReader,@LeaveByService,@LeaveInSeatClient,@LeaveInOtherClient,@ComeBackCount,@ComeBackByAdmin,@ComeBackByReader,@ComeBackByOtherReader,@ComeBackInSeatClient,@ComeBackInOtherClient,@ContinueTimeCount,@ContinueTimeByReader,@ContinueTimeByService,@ContinueTimeInSeatClient,@ContinueTimeInOtherClient,@AllBespeakCount,@BespeakCount,@CanBesapeakSeat,@BespeakedSeat,@BespeakCancel,@BespeakOverTime,@BespeakCheck,@NowDayBespeakCheck,@NowDayBespeakCount,@NowDayBespeakOverTime,@NowDayBespeakCancel,@ViolationRecordCount,@VRBookingTimeOut,@VRSeatOutTime,@VRLeaveByAdmin,@VRShortLeaveOutTime,@VRShortLeaveByAdminOutTime,@VRShortLeaveByReaderOutTime,@VRShortLeaveByServiceOutTime,@VRLeaveNotReadCard)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ReadingRoomNo", SqlDbType.NVarChar,50),
					new SqlParameter("@StatisticsDate", SqlDbType.DateTime),
					new SqlParameter("@OpenTime", SqlDbType.DateTime),
					new SqlParameter("@CloseTime", SqlDbType.DateTime),
					new SqlParameter("@RoomUsageTime", SqlDbType.Int,4),
					new SqlParameter("@SeatAllCount", SqlDbType.Int,4),
					new SqlParameter("@SeatUsageCount", SqlDbType.Int,4),
					new SqlParameter("@SeatUsageTime", SqlDbType.Int,4),
					new SqlParameter("@ReaderUsageCount", SqlDbType.Int,4),
					new SqlParameter("@UsedReaderCount", SqlDbType.Int,4),
					new SqlParameter("@RushCardOperatingCount", SqlDbType.Int,4),
					new SqlParameter("@SelectSeatCount", SqlDbType.Int,4),
					new SqlParameter("@SelectSeatByAdmin", SqlDbType.Int,4),
					new SqlParameter("@SelectSeatByReader", SqlDbType.Int,4),
					new SqlParameter("@SelectSeatInSeatClient", SqlDbType.Int,4),
					new SqlParameter("@SelectSeatInOtherClient", SqlDbType.Int,4),
					new SqlParameter("@ReselectSeatCount", SqlDbType.Int,4),
					new SqlParameter("@ReselectSeatInSeatClient", SqlDbType.Int,4),
					new SqlParameter("@ReselectSeatInOtherClient", SqlDbType.Int,4),
					new SqlParameter("@CheckBespeakCount", SqlDbType.Int,4),
					new SqlParameter("@CheckBespeakInSeatClient", SqlDbType.Int,4),
					new SqlParameter("@CkeckBespeakInOtherClient", SqlDbType.Int,4),
					new SqlParameter("@WaitSeatCount", SqlDbType.Int,4),
					new SqlParameter("@ShortLeaveCount", SqlDbType.Int,4),
					new SqlParameter("@ShortLeaveTime", SqlDbType.Int,4),
					new SqlParameter("@ShortLeaveByAdmin", SqlDbType.Int,4),
					new SqlParameter("@ShortLeaveByReader", SqlDbType.Int,4),
					new SqlParameter("@ShortLeaveByOtherReader", SqlDbType.Int,4),
					new SqlParameter("@ShortLeaveByService", SqlDbType.Int,4),
					new SqlParameter("@ShortLeaveInSeatClient", SqlDbType.Int,4),
					new SqlParameter("@ShortLeaveInOtherClient", SqlDbType.Int,4),
					new SqlParameter("@LeaveCount", SqlDbType.Int,4),
					new SqlParameter("@LeaveByAdmin", SqlDbType.Int,4),
					new SqlParameter("@LeaveByReader", SqlDbType.Int,4),
					new SqlParameter("@LeaveByService", SqlDbType.Int,4),
					new SqlParameter("@LeaveInSeatClient", SqlDbType.Int,4),
					new SqlParameter("@LeaveInOtherClient", SqlDbType.Int,4),
					new SqlParameter("@ComeBackCount", SqlDbType.Int,4),
					new SqlParameter("@ComeBackByAdmin", SqlDbType.Int,4),
					new SqlParameter("@ComeBackByReader", SqlDbType.Int,4),
					new SqlParameter("@ComeBackByOtherReader", SqlDbType.Int,4),
					new SqlParameter("@ComeBackInSeatClient", SqlDbType.Int,4),
					new SqlParameter("@ComeBackInOtherClient", SqlDbType.Int,4),
					new SqlParameter("@ContinueTimeCount", SqlDbType.Int,4),
					new SqlParameter("@ContinueTimeByReader", SqlDbType.Int,4),
					new SqlParameter("@ContinueTimeByService", SqlDbType.Int,4),
					new SqlParameter("@ContinueTimeInSeatClient", SqlDbType.Int,4),
					new SqlParameter("@ContinueTimeInOtherClient", SqlDbType.Int,4),
					new SqlParameter("@AllBespeakCount", SqlDbType.Int,4),
					new SqlParameter("@BespeakCount", SqlDbType.Int,4),
					new SqlParameter("@CanBesapeakSeat", SqlDbType.Int,4),
					new SqlParameter("@BespeakedSeat", SqlDbType.Int,4),
					new SqlParameter("@BespeakCancel", SqlDbType.Int,4),
					new SqlParameter("@BespeakOverTime", SqlDbType.Int,4),
					new SqlParameter("@BespeakCheck", SqlDbType.Int,4),
					new SqlParameter("@NowDayBespeakCheck", SqlDbType.Int,4),
					new SqlParameter("@NowDayBespeakCount", SqlDbType.Int,4),
					new SqlParameter("@NowDayBespeakOverTime", SqlDbType.Int,4),
					new SqlParameter("@NowDayBespeakCancel", SqlDbType.Int,4),
					new SqlParameter("@ViolationRecordCount", SqlDbType.Int,4),
					new SqlParameter("@VRBookingTimeOut", SqlDbType.Int,4),
					new SqlParameter("@VRSeatOutTime", SqlDbType.Int,4),
					new SqlParameter("@VRLeaveByAdmin", SqlDbType.Int,4),
					new SqlParameter("@VRShortLeaveOutTime", SqlDbType.Int,4),
					new SqlParameter("@VRShortLeaveByAdminOutTime", SqlDbType.Int,4),
					new SqlParameter("@VRShortLeaveByReaderOutTime", SqlDbType.Int,4),
					new SqlParameter("@VRShortLeaveByServiceOutTime", SqlDbType.Int,4),
					new SqlParameter("@VRLeaveNotReadCard", SqlDbType.Int,4)};
            parameters[0].Value = model.ReadingRoomNo;
            parameters[1].Value = model.StatisticsDate;
            parameters[2].Value = model.OpenTime;
            parameters[3].Value = model.CloseTime;
            parameters[4].Value = model.RoomUsageTime;
            parameters[5].Value = model.SeatAllCount;
            parameters[6].Value = model.SeatUsageCount;
            parameters[7].Value = model.SeatUsageTime;
            parameters[8].Value = model.ReaderUsageCount;
            parameters[9].Value = model.UsedReaderCount;
            parameters[10].Value = model.RushCardOperatingCount;
            parameters[11].Value = model.SelectSeatCount;
            parameters[12].Value = model.SelectSeatByAdmin;
            parameters[13].Value = model.SelectSeatByReader;
            parameters[14].Value = model.SelectSeatInSeatClient;
            parameters[15].Value = model.SelectSeatInOtherClient;
            parameters[16].Value = model.ReselectSeatCount;
            parameters[17].Value = model.ReselectSeatInSeatClient;
            parameters[18].Value = model.ReselectSeatInOtherClient;
            parameters[19].Value = model.CheckBespeakCount;
            parameters[20].Value = model.CheckBespeakInSeatClient;
            parameters[21].Value = model.CkeckBespeakInOtherClient;
            parameters[22].Value = model.WaitSeatCount;
            parameters[23].Value = model.ShortLeaveCount;
            parameters[24].Value = model.ShortLeaveTime;
            parameters[25].Value = model.ShortLeaveByAdmin;
            parameters[26].Value = model.ShortLeaveByReader;
            parameters[27].Value = model.ShortLeaveByOtherReader;
            parameters[28].Value = model.ShortLeaveByService;
            parameters[29].Value = model.ShortLeaveInSeatClient;
            parameters[30].Value = model.ShortLeaveInOtherClient;
            parameters[31].Value = model.LeaveCount;
            parameters[32].Value = model.LeaveByAdmin;
            parameters[33].Value = model.LeaveByReader;
            parameters[34].Value = model.LeaveByService;
            parameters[35].Value = model.LeaveInSeatClient;
            parameters[36].Value = model.LeaveInOtherClient;
            parameters[37].Value = model.ComeBackCount;
            parameters[38].Value = model.ComeBackByAdmin;
            parameters[39].Value = model.ComeBackByReader;
            parameters[40].Value = model.ComeBackByOtherReader;
            parameters[41].Value = model.ComeBackInSeatClient;
            parameters[42].Value = model.ComeBackInOtherClient;
            parameters[43].Value = model.ContinueTimeCount;
            parameters[44].Value = model.ContinueTimeByReader;
            parameters[45].Value = model.ContinueTimeByService;
            parameters[46].Value = model.ContinueTimeInSeatClient;
            parameters[47].Value = model.ContinueTimeInOtherClient;
            parameters[48].Value = model.AllBespeakCount;
            parameters[49].Value = model.BespeakCount;
            parameters[50].Value = model.CanBesapeakSeat;
            parameters[51].Value = model.BespeakedSeat;
            parameters[52].Value = model.BespeakCancel;
            parameters[53].Value = model.BespeakOverTime;
            parameters[54].Value = model.BespeakCheck;
            parameters[55].Value = model.NowDayBespeakCheck;
            parameters[56].Value = model.NowDayBespeakCount;
            parameters[57].Value = model.NowDayBespeakOverTime;
            parameters[58].Value = model.NowDayBespeakCancel;
            parameters[59].Value = model.ViolationRecordCount;
            parameters[60].Value = model.VRBookingTimeOut;
            parameters[61].Value = model.VRSeatOutTime;
            parameters[62].Value = model.VRLeaveByAdmin;
            parameters[63].Value = model.VRShortLeaveOutTime;
            parameters[64].Value = model.VRShortLeaveByAdminOutTime;
            parameters[65].Value = model.VRShortLeaveByReaderOutTime;
            parameters[66].Value = model.VRShortLeaveByServiceOutTime;
            parameters[67].Value = model.VRLeaveNotReadCard;

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
        public bool Update(RoomUsageStatistics model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_SM_RoomUsageStatistics set ");
            strSql.Append("ReadingRoomNo=@ReadingRoomNo,");
            strSql.Append("StatisticsDate=@StatisticsDate,");
            strSql.Append("OpenTime=@OpenTime,");
            strSql.Append("CloseTime=@CloseTime,");
            strSql.Append("RoomUsageTime=@RoomUsageTime,");
            strSql.Append("SeatAllCount=@SeatAllCount,");
            strSql.Append("SeatUsageCount=@SeatUsageCount,");
            strSql.Append("SeatUsageTime=@SeatUsageTime,");
            strSql.Append("ReaderUsageCount=@ReaderUsageCount,");
            strSql.Append("UsedReaderCount=@UsedReaderCount,");
            strSql.Append("RushCardOperatingCount=@RushCardOperatingCount,");
            strSql.Append("SelectSeatCount=@SelectSeatCount,");
            strSql.Append("SelectSeatByAdmin=@SelectSeatByAdmin,");
            strSql.Append("SelectSeatByReader=@SelectSeatByReader,");
            strSql.Append("SelectSeatInSeatClient=@SelectSeatInSeatClient,");
            strSql.Append("SelectSeatInOtherClient=@SelectSeatInOtherClient,");
            strSql.Append("ReselectSeatCount=@ReselectSeatCount,");
            strSql.Append("ReselectSeatInSeatClient=@ReselectSeatInSeatClient,");
            strSql.Append("ReselectSeatInOtherClient=@ReselectSeatInOtherClient,");
            strSql.Append("CheckBespeakCount=@CheckBespeakCount,");
            strSql.Append("CheckBespeakInSeatClient=@CheckBespeakInSeatClient,");
            strSql.Append("CkeckBespeakInOtherClient=@CkeckBespeakInOtherClient,");
            strSql.Append("WaitSeatCount=@WaitSeatCount,");
            strSql.Append("ShortLeaveCount=@ShortLeaveCount,");
            strSql.Append("ShortLeaveTime=@ShortLeaveTime,");
            strSql.Append("ShortLeaveByAdmin=@ShortLeaveByAdmin,");
            strSql.Append("ShortLeaveByReader=@ShortLeaveByReader,");
            strSql.Append("ShortLeaveByOtherReader=@ShortLeaveByOtherReader,");
            strSql.Append("ShortLeaveByService=@ShortLeaveByService,");
            strSql.Append("ShortLeaveInSeatClient=@ShortLeaveInSeatClient,");
            strSql.Append("ShortLeaveInOtherClient=@ShortLeaveInOtherClient,");
            strSql.Append("LeaveCount=@LeaveCount,");
            strSql.Append("LeaveByAdmin=@LeaveByAdmin,");
            strSql.Append("LeaveByReader=@LeaveByReader,");
            strSql.Append("LeaveByService=@LeaveByService,");
            strSql.Append("LeaveInSeatClient=@LeaveInSeatClient,");
            strSql.Append("LeaveInOtherClient=@LeaveInOtherClient,");
            strSql.Append("ComeBackCount=@ComeBackCount,");
            strSql.Append("ComeBackByAdmin=@ComeBackByAdmin,");
            strSql.Append("ComeBackByReader=@ComeBackByReader,");
            strSql.Append("ComeBackByOtherReader=@ComeBackByOtherReader,");
            strSql.Append("ComeBackInSeatClient=@ComeBackInSeatClient,");
            strSql.Append("ComeBackInOtherClient=@ComeBackInOtherClient,");
            strSql.Append("ContinueTimeCount=@ContinueTimeCount,");
            strSql.Append("ContinueTimeByReader=@ContinueTimeByReader,");
            strSql.Append("ContinueTimeByService=@ContinueTimeByService,");
            strSql.Append("ContinueTimeInSeatClient=@ContinueTimeInSeatClient,");
            strSql.Append("ContinueTimeInOtherClient=@ContinueTimeInOtherClient,");
            strSql.Append("AllBespeakCount=@AllBespeakCount,");
            strSql.Append("BespeakCount=@BespeakCount,");
            strSql.Append("CanBesapeakSeat=@CanBesapeakSeat,");
            strSql.Append("BespeakedSeat=@BespeakedSeat,");
            strSql.Append("BespeakCancel=@BespeakCancel,");
            strSql.Append("BespeakOverTime=@BespeakOverTime,");
            strSql.Append("BespeakCheck=@BespeakCheck,");
            strSql.Append("NowDayBespeakCheck=@NowDayBespeakCheck,");
            strSql.Append("NowDayBespeakCount=@NowDayBespeakCount,");
            strSql.Append("NowDayBespeakOverTime=@NowDayBespeakOverTime,");
            strSql.Append("NowDayBespeakCancel=@NowDayBespeakCancel,");
            strSql.Append("ViolationRecordCount=@ViolationRecordCount,");
            strSql.Append("VRBookingTimeOut=@VRBookingTimeOut,");
            strSql.Append("VRSeatOutTime=@VRSeatOutTime,");
            strSql.Append("VRLeaveByAdmin=@VRLeaveByAdmin,");
            strSql.Append("VRShortLeaveOutTime=@VRShortLeaveOutTime,");
            strSql.Append("VRShortLeaveByAdminOutTime=@VRShortLeaveByAdminOutTime,");
            strSql.Append("VRShortLeaveByReaderOutTime=@VRShortLeaveByReaderOutTime,");
            strSql.Append("VRShortLeaveByServiceOutTime=@VRShortLeaveByServiceOutTime,");
            strSql.Append("VRLeaveNotReadCard=@VRLeaveNotReadCard");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@ReadingRoomNo", SqlDbType.NVarChar,50),
					new SqlParameter("@StatisticsDate", SqlDbType.DateTime),
					new SqlParameter("@OpenTime", SqlDbType.DateTime),
					new SqlParameter("@CloseTime", SqlDbType.DateTime),
					new SqlParameter("@RoomUsageTime", SqlDbType.Int,4),
					new SqlParameter("@SeatAllCount", SqlDbType.Int,4),
					new SqlParameter("@SeatUsageCount", SqlDbType.Int,4),
					new SqlParameter("@SeatUsageTime", SqlDbType.Int,4),
					new SqlParameter("@ReaderUsageCount", SqlDbType.Int,4),
					new SqlParameter("@UsedReaderCount", SqlDbType.Int,4),
					new SqlParameter("@RushCardOperatingCount", SqlDbType.Int,4),
					new SqlParameter("@SelectSeatCount", SqlDbType.Int,4),
					new SqlParameter("@SelectSeatByAdmin", SqlDbType.Int,4),
					new SqlParameter("@SelectSeatByReader", SqlDbType.Int,4),
					new SqlParameter("@SelectSeatInSeatClient", SqlDbType.Int,4),
					new SqlParameter("@SelectSeatInOtherClient", SqlDbType.Int,4),
					new SqlParameter("@ReselectSeatCount", SqlDbType.Int,4),
					new SqlParameter("@ReselectSeatInSeatClient", SqlDbType.Int,4),
					new SqlParameter("@ReselectSeatInOtherClient", SqlDbType.Int,4),
					new SqlParameter("@CheckBespeakCount", SqlDbType.Int,4),
					new SqlParameter("@CheckBespeakInSeatClient", SqlDbType.Int,4),
					new SqlParameter("@CkeckBespeakInOtherClient", SqlDbType.Int,4),
					new SqlParameter("@WaitSeatCount", SqlDbType.Int,4),
					new SqlParameter("@ShortLeaveCount", SqlDbType.Int,4),
					new SqlParameter("@ShortLeaveTime", SqlDbType.Int,4),
					new SqlParameter("@ShortLeaveByAdmin", SqlDbType.Int,4),
					new SqlParameter("@ShortLeaveByReader", SqlDbType.Int,4),
					new SqlParameter("@ShortLeaveByOtherReader", SqlDbType.Int,4),
					new SqlParameter("@ShortLeaveByService", SqlDbType.Int,4),
					new SqlParameter("@ShortLeaveInSeatClient", SqlDbType.Int,4),
					new SqlParameter("@ShortLeaveInOtherClient", SqlDbType.Int,4),
					new SqlParameter("@LeaveCount", SqlDbType.Int,4),
					new SqlParameter("@LeaveByAdmin", SqlDbType.Int,4),
					new SqlParameter("@LeaveByReader", SqlDbType.Int,4),
					new SqlParameter("@LeaveByService", SqlDbType.Int,4),
					new SqlParameter("@LeaveInSeatClient", SqlDbType.Int,4),
					new SqlParameter("@LeaveInOtherClient", SqlDbType.Int,4),
					new SqlParameter("@ComeBackCount", SqlDbType.Int,4),
					new SqlParameter("@ComeBackByAdmin", SqlDbType.Int,4),
					new SqlParameter("@ComeBackByReader", SqlDbType.Int,4),
					new SqlParameter("@ComeBackByOtherReader", SqlDbType.Int,4),
					new SqlParameter("@ComeBackInSeatClient", SqlDbType.Int,4),
					new SqlParameter("@ComeBackInOtherClient", SqlDbType.Int,4),
					new SqlParameter("@ContinueTimeCount", SqlDbType.Int,4),
					new SqlParameter("@ContinueTimeByReader", SqlDbType.Int,4),
					new SqlParameter("@ContinueTimeByService", SqlDbType.Int,4),
					new SqlParameter("@ContinueTimeInSeatClient", SqlDbType.Int,4),
					new SqlParameter("@ContinueTimeInOtherClient", SqlDbType.Int,4),
					new SqlParameter("@AllBespeakCount", SqlDbType.Int,4),
					new SqlParameter("@BespeakCount", SqlDbType.Int,4),
					new SqlParameter("@CanBesapeakSeat", SqlDbType.Int,4),
					new SqlParameter("@BespeakedSeat", SqlDbType.Int,4),
					new SqlParameter("@BespeakCancel", SqlDbType.Int,4),
					new SqlParameter("@BespeakOverTime", SqlDbType.Int,4),
					new SqlParameter("@BespeakCheck", SqlDbType.Int,4),
					new SqlParameter("@NowDayBespeakCheck", SqlDbType.Int,4),
					new SqlParameter("@NowDayBespeakCount", SqlDbType.Int,4),
					new SqlParameter("@NowDayBespeakOverTime", SqlDbType.Int,4),
					new SqlParameter("@NowDayBespeakCancel", SqlDbType.Int,4),
					new SqlParameter("@ViolationRecordCount", SqlDbType.Int,4),
					new SqlParameter("@VRBookingTimeOut", SqlDbType.Int,4),
					new SqlParameter("@VRSeatOutTime", SqlDbType.Int,4),
					new SqlParameter("@VRLeaveByAdmin", SqlDbType.Int,4),
					new SqlParameter("@VRShortLeaveOutTime", SqlDbType.Int,4),
					new SqlParameter("@VRShortLeaveByAdminOutTime", SqlDbType.Int,4),
					new SqlParameter("@VRShortLeaveByReaderOutTime", SqlDbType.Int,4),
					new SqlParameter("@VRShortLeaveByServiceOutTime", SqlDbType.Int,4),
					new SqlParameter("@VRLeaveNotReadCard", SqlDbType.Int,4),
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = model.ReadingRoomNo;
            parameters[1].Value = model.StatisticsDate;
            parameters[2].Value = model.OpenTime;
            parameters[3].Value = model.CloseTime;
            parameters[4].Value = model.RoomUsageTime;
            parameters[5].Value = model.SeatAllCount;
            parameters[6].Value = model.SeatUsageCount;
            parameters[7].Value = model.SeatUsageTime;
            parameters[8].Value = model.ReaderUsageCount;
            parameters[9].Value = model.UsedReaderCount;
            parameters[10].Value = model.RushCardOperatingCount;
            parameters[11].Value = model.SelectSeatCount;
            parameters[12].Value = model.SelectSeatByAdmin;
            parameters[13].Value = model.SelectSeatByReader;
            parameters[14].Value = model.SelectSeatInSeatClient;
            parameters[15].Value = model.SelectSeatInOtherClient;
            parameters[16].Value = model.ReselectSeatCount;
            parameters[17].Value = model.ReselectSeatInSeatClient;
            parameters[18].Value = model.ReselectSeatInOtherClient;
            parameters[19].Value = model.CheckBespeakCount;
            parameters[20].Value = model.CheckBespeakInSeatClient;
            parameters[21].Value = model.CkeckBespeakInOtherClient;
            parameters[22].Value = model.WaitSeatCount;
            parameters[23].Value = model.ShortLeaveCount;
            parameters[24].Value = model.ShortLeaveTime;
            parameters[25].Value = model.ShortLeaveByAdmin;
            parameters[26].Value = model.ShortLeaveByReader;
            parameters[27].Value = model.ShortLeaveByOtherReader;
            parameters[28].Value = model.ShortLeaveByService;
            parameters[29].Value = model.ShortLeaveInSeatClient;
            parameters[30].Value = model.ShortLeaveInOtherClient;
            parameters[31].Value = model.LeaveCount;
            parameters[32].Value = model.LeaveByAdmin;
            parameters[33].Value = model.LeaveByReader;
            parameters[34].Value = model.LeaveByService;
            parameters[35].Value = model.LeaveInSeatClient;
            parameters[36].Value = model.LeaveInOtherClient;
            parameters[37].Value = model.ComeBackCount;
            parameters[38].Value = model.ComeBackByAdmin;
            parameters[39].Value = model.ComeBackByReader;
            parameters[40].Value = model.ComeBackByOtherReader;
            parameters[41].Value = model.ComeBackInSeatClient;
            parameters[42].Value = model.ComeBackInOtherClient;
            parameters[43].Value = model.ContinueTimeCount;
            parameters[44].Value = model.ContinueTimeByReader;
            parameters[45].Value = model.ContinueTimeByService;
            parameters[46].Value = model.ContinueTimeInSeatClient;
            parameters[47].Value = model.ContinueTimeInOtherClient;
            parameters[48].Value = model.AllBespeakCount;
            parameters[49].Value = model.BespeakCount;
            parameters[50].Value = model.CanBesapeakSeat;
            parameters[51].Value = model.BespeakedSeat;
            parameters[52].Value = model.BespeakCancel;
            parameters[53].Value = model.BespeakOverTime;
            parameters[54].Value = model.BespeakCheck;
            parameters[55].Value = model.NowDayBespeakCheck;
            parameters[56].Value = model.NowDayBespeakCount;
            parameters[57].Value = model.NowDayBespeakOverTime;
            parameters[58].Value = model.NowDayBespeakCancel;
            parameters[59].Value = model.ViolationRecordCount;
            parameters[60].Value = model.VRBookingTimeOut;
            parameters[61].Value = model.VRSeatOutTime;
            parameters[62].Value = model.VRLeaveByAdmin;
            parameters[63].Value = model.VRShortLeaveOutTime;
            parameters[64].Value = model.VRShortLeaveByAdminOutTime;
            parameters[65].Value = model.VRShortLeaveByReaderOutTime;
            parameters[66].Value = model.VRShortLeaveByServiceOutTime;
            parameters[67].Value = model.VRLeaveNotReadCard;
            parameters[68].Value = model.id;

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
        public bool Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_SM_RoomUsageStatistics ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

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
        public bool DeleteList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_SM_RoomUsageStatistics ");
            strSql.Append(" where id in (" + idlist + ")  ");
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
        public RoomUsageStatistics GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,ReadingRoomNo,StatisticsDate,OpenTime,CloseTime,RoomUsageTime,SeatAllCount,SeatUsageCount,SeatUsageTime,ReaderUsageCount,UsedReaderCount,RushCardOperatingCount,SelectSeatCount,SelectSeatByAdmin,SelectSeatByReader,SelectSeatInSeatClient,SelectSeatInOtherClient,ReselectSeatCount,ReselectSeatInSeatClient,ReselectSeatInOtherClient,CheckBespeakCount,CheckBespeakInSeatClient,CkeckBespeakInOtherClient,WaitSeatCount,ShortLeaveCount,ShortLeaveTime,ShortLeaveByAdmin,ShortLeaveByReader,ShortLeaveByOtherReader,ShortLeaveByService,ShortLeaveInSeatClient,ShortLeaveInOtherClient,LeaveCount,LeaveByAdmin,LeaveByReader,LeaveByService,LeaveInSeatClient,LeaveInOtherClient,ComeBackCount,ComeBackByAdmin,ComeBackByReader,ComeBackByOtherReader,ComeBackInSeatClient,ComeBackInOtherClient,ContinueTimeCount,ContinueTimeByReader,ContinueTimeByService,ContinueTimeInSeatClient,ContinueTimeInOtherClient,AllBespeakCount,BespeakCount,CanBesapeakSeat,BespeakedSeat,BespeakCancel,BespeakOverTime,BespeakCheck,NowDayBespeakCheck,NowDayBespeakCount,NowDayBespeakOverTime,NowDayBespeakCancel,ViolationRecordCount,VRBookingTimeOut,VRSeatOutTime,VRLeaveByAdmin,VRShortLeaveOutTime,VRShortLeaveByAdminOutTime,VRShortLeaveByReaderOutTime,VRShortLeaveByServiceOutTime,VRLeaveNotReadCard from T_SM_RoomUsageStatistics ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            RoomUsageStatistics model = new RoomUsageStatistics();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public RoomUsageStatistics DataRowToModel(DataRow row)
        {
            RoomUsageStatistics model = new RoomUsageStatistics();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["ReadingRoomNo"] != null)
                {
                    model.ReadingRoomNo = row["ReadingRoomNo"].ToString();
                }
                if (row["StatisticsDate"] != null && row["StatisticsDate"].ToString() != "")
                {
                    model.StatisticsDate = DateTime.Parse(row["StatisticsDate"].ToString());
                }
                if (row["OpenTime"] != null && row["OpenTime"].ToString() != "")
                {
                    model.OpenTime = DateTime.Parse(row["OpenTime"].ToString());
                }
                if (row["CloseTime"] != null && row["CloseTime"].ToString() != "")
                {
                    model.CloseTime = DateTime.Parse(row["CloseTime"].ToString());
                }
                if (row["RoomUsageTime"] != null && row["RoomUsageTime"].ToString() != "")
                {
                    model.RoomUsageTime = int.Parse(row["RoomUsageTime"].ToString());
                }
                if (row["SeatAllCount"] != null && row["SeatAllCount"].ToString() != "")
                {
                    model.SeatAllCount = int.Parse(row["SeatAllCount"].ToString());
                }
                if (row["SeatUsageCount"] != null && row["SeatUsageCount"].ToString() != "")
                {
                    model.SeatUsageCount = int.Parse(row["SeatUsageCount"].ToString());
                }
                if (row["SeatUsageTime"] != null && row["SeatUsageTime"].ToString() != "")
                {
                    model.SeatUsageTime = int.Parse(row["SeatUsageTime"].ToString());
                }
                if (row["ReaderUsageCount"] != null && row["ReaderUsageCount"].ToString() != "")
                {
                    model.ReaderUsageCount = int.Parse(row["ReaderUsageCount"].ToString());
                }
                if (row["UsedReaderCount"] != null && row["UsedReaderCount"].ToString() != "")
                {
                    model.UsedReaderCount = int.Parse(row["UsedReaderCount"].ToString());
                }
                if (row["RushCardOperatingCount"] != null && row["RushCardOperatingCount"].ToString() != "")
                {
                    model.RushCardOperatingCount = int.Parse(row["RushCardOperatingCount"].ToString());
                }
                if (row["SelectSeatCount"] != null && row["SelectSeatCount"].ToString() != "")
                {
                    model.SelectSeatCount = int.Parse(row["SelectSeatCount"].ToString());
                }
                if (row["SelectSeatByAdmin"] != null && row["SelectSeatByAdmin"].ToString() != "")
                {
                    model.SelectSeatByAdmin = int.Parse(row["SelectSeatByAdmin"].ToString());
                }
                if (row["SelectSeatByReader"] != null && row["SelectSeatByReader"].ToString() != "")
                {
                    model.SelectSeatByReader = int.Parse(row["SelectSeatByReader"].ToString());
                }
                if (row["SelectSeatInSeatClient"] != null && row["SelectSeatInSeatClient"].ToString() != "")
                {
                    model.SelectSeatInSeatClient = int.Parse(row["SelectSeatInSeatClient"].ToString());
                }
                if (row["SelectSeatInOtherClient"] != null && row["SelectSeatInOtherClient"].ToString() != "")
                {
                    model.SelectSeatInOtherClient = int.Parse(row["SelectSeatInOtherClient"].ToString());
                }
                if (row["ReselectSeatCount"] != null && row["ReselectSeatCount"].ToString() != "")
                {
                    model.ReselectSeatCount = int.Parse(row["ReselectSeatCount"].ToString());
                }
                if (row["ReselectSeatInSeatClient"] != null && row["ReselectSeatInSeatClient"].ToString() != "")
                {
                    model.ReselectSeatInSeatClient = int.Parse(row["ReselectSeatInSeatClient"].ToString());
                }
                if (row["ReselectSeatInOtherClient"] != null && row["ReselectSeatInOtherClient"].ToString() != "")
                {
                    model.ReselectSeatInOtherClient = int.Parse(row["ReselectSeatInOtherClient"].ToString());
                }
                if (row["CheckBespeakCount"] != null && row["CheckBespeakCount"].ToString() != "")
                {
                    model.CheckBespeakCount = int.Parse(row["CheckBespeakCount"].ToString());
                }
                if (row["CheckBespeakInSeatClient"] != null && row["CheckBespeakInSeatClient"].ToString() != "")
                {
                    model.CheckBespeakInSeatClient = int.Parse(row["CheckBespeakInSeatClient"].ToString());
                }
                if (row["CkeckBespeakInOtherClient"] != null && row["CkeckBespeakInOtherClient"].ToString() != "")
                {
                    model.CkeckBespeakInOtherClient = int.Parse(row["CkeckBespeakInOtherClient"].ToString());
                }
                if (row["WaitSeatCount"] != null && row["WaitSeatCount"].ToString() != "")
                {
                    model.WaitSeatCount = int.Parse(row["WaitSeatCount"].ToString());
                }
                if (row["ShortLeaveCount"] != null && row["ShortLeaveCount"].ToString() != "")
                {
                    model.ShortLeaveCount = int.Parse(row["ShortLeaveCount"].ToString());
                }
                if (row["ShortLeaveTime"] != null && row["ShortLeaveTime"].ToString() != "")
                {
                    model.ShortLeaveTime = int.Parse(row["ShortLeaveTime"].ToString());
                }
                if (row["ShortLeaveByAdmin"] != null && row["ShortLeaveByAdmin"].ToString() != "")
                {
                    model.ShortLeaveByAdmin = int.Parse(row["ShortLeaveByAdmin"].ToString());
                }
                if (row["ShortLeaveByReader"] != null && row["ShortLeaveByReader"].ToString() != "")
                {
                    model.ShortLeaveByReader = int.Parse(row["ShortLeaveByReader"].ToString());
                }
                if (row["ShortLeaveByOtherReader"] != null && row["ShortLeaveByOtherReader"].ToString() != "")
                {
                    model.ShortLeaveByOtherReader = int.Parse(row["ShortLeaveByOtherReader"].ToString());
                }
                if (row["ShortLeaveByService"] != null && row["ShortLeaveByService"].ToString() != "")
                {
                    model.ShortLeaveByService = int.Parse(row["ShortLeaveByService"].ToString());
                }
                if (row["ShortLeaveInSeatClient"] != null && row["ShortLeaveInSeatClient"].ToString() != "")
                {
                    model.ShortLeaveInSeatClient = int.Parse(row["ShortLeaveInSeatClient"].ToString());
                }
                if (row["ShortLeaveInOtherClient"] != null && row["ShortLeaveInOtherClient"].ToString() != "")
                {
                    model.ShortLeaveInOtherClient = int.Parse(row["ShortLeaveInOtherClient"].ToString());
                }
                if (row["LeaveCount"] != null && row["LeaveCount"].ToString() != "")
                {
                    model.LeaveCount = int.Parse(row["LeaveCount"].ToString());
                }
                if (row["LeaveByAdmin"] != null && row["LeaveByAdmin"].ToString() != "")
                {
                    model.LeaveByAdmin = int.Parse(row["LeaveByAdmin"].ToString());
                }
                if (row["LeaveByReader"] != null && row["LeaveByReader"].ToString() != "")
                {
                    model.LeaveByReader = int.Parse(row["LeaveByReader"].ToString());
                }
                if (row["LeaveByService"] != null && row["LeaveByService"].ToString() != "")
                {
                    model.LeaveByService = int.Parse(row["LeaveByService"].ToString());
                }
                if (row["LeaveInSeatClient"] != null && row["LeaveInSeatClient"].ToString() != "")
                {
                    model.LeaveInSeatClient = int.Parse(row["LeaveInSeatClient"].ToString());
                }
                if (row["LeaveInOtherClient"] != null && row["LeaveInOtherClient"].ToString() != "")
                {
                    model.LeaveInOtherClient = int.Parse(row["LeaveInOtherClient"].ToString());
                }
                if (row["ComeBackCount"] != null && row["ComeBackCount"].ToString() != "")
                {
                    model.ComeBackCount = int.Parse(row["ComeBackCount"].ToString());
                }
                if (row["ComeBackByAdmin"] != null && row["ComeBackByAdmin"].ToString() != "")
                {
                    model.ComeBackByAdmin = int.Parse(row["ComeBackByAdmin"].ToString());
                }
                if (row["ComeBackByReader"] != null && row["ComeBackByReader"].ToString() != "")
                {
                    model.ComeBackByReader = int.Parse(row["ComeBackByReader"].ToString());
                }
                if (row["ComeBackByOtherReader"] != null && row["ComeBackByOtherReader"].ToString() != "")
                {
                    model.ComeBackByOtherReader = int.Parse(row["ComeBackByOtherReader"].ToString());
                }
                if (row["ComeBackInSeatClient"] != null && row["ComeBackInSeatClient"].ToString() != "")
                {
                    model.ComeBackInSeatClient = int.Parse(row["ComeBackInSeatClient"].ToString());
                }
                if (row["ComeBackInOtherClient"] != null && row["ComeBackInOtherClient"].ToString() != "")
                {
                    model.ComeBackInOtherClient = int.Parse(row["ComeBackInOtherClient"].ToString());
                }
                if (row["ContinueTimeCount"] != null && row["ContinueTimeCount"].ToString() != "")
                {
                    model.ContinueTimeCount = int.Parse(row["ContinueTimeCount"].ToString());
                }
                if (row["ContinueTimeByReader"] != null && row["ContinueTimeByReader"].ToString() != "")
                {
                    model.ContinueTimeByReader = int.Parse(row["ContinueTimeByReader"].ToString());
                }
                if (row["ContinueTimeByService"] != null && row["ContinueTimeByService"].ToString() != "")
                {
                    model.ContinueTimeByService = int.Parse(row["ContinueTimeByService"].ToString());
                }
                if (row["ContinueTimeInSeatClient"] != null && row["ContinueTimeInSeatClient"].ToString() != "")
                {
                    model.ContinueTimeInSeatClient = int.Parse(row["ContinueTimeInSeatClient"].ToString());
                }
                if (row["ContinueTimeInOtherClient"] != null && row["ContinueTimeInOtherClient"].ToString() != "")
                {
                    model.ContinueTimeInOtherClient = int.Parse(row["ContinueTimeInOtherClient"].ToString());
                }
                if (row["AllBespeakCount"] != null && row["AllBespeakCount"].ToString() != "")
                {
                    model.AllBespeakCount = int.Parse(row["AllBespeakCount"].ToString());
                }
                if (row["BespeakCount"] != null && row["BespeakCount"].ToString() != "")
                {
                    model.BespeakCount = int.Parse(row["BespeakCount"].ToString());
                }
                if (row["CanBesapeakSeat"] != null && row["CanBesapeakSeat"].ToString() != "")
                {
                    model.CanBesapeakSeat = int.Parse(row["CanBesapeakSeat"].ToString());
                }
                if (row["BespeakedSeat"] != null && row["BespeakedSeat"].ToString() != "")
                {
                    model.BespeakedSeat = int.Parse(row["BespeakedSeat"].ToString());
                }
                if (row["BespeakCancel"] != null && row["BespeakCancel"].ToString() != "")
                {
                    model.BespeakCancel = int.Parse(row["BespeakCancel"].ToString());
                }
                if (row["BespeakOverTime"] != null && row["BespeakOverTime"].ToString() != "")
                {
                    model.BespeakOverTime = int.Parse(row["BespeakOverTime"].ToString());
                }
                if (row["BespeakCheck"] != null && row["BespeakCheck"].ToString() != "")
                {
                    model.BespeakCheck = int.Parse(row["BespeakCheck"].ToString());
                }
                if (row["NowDayBespeakCheck"] != null && row["NowDayBespeakCheck"].ToString() != "")
                {
                    model.NowDayBespeakCheck = int.Parse(row["NowDayBespeakCheck"].ToString());
                }
                if (row["NowDayBespeakCount"] != null && row["NowDayBespeakCount"].ToString() != "")
                {
                    model.NowDayBespeakCount = int.Parse(row["NowDayBespeakCount"].ToString());
                }
                if (row["NowDayBespeakOverTime"] != null && row["NowDayBespeakOverTime"].ToString() != "")
                {
                    model.NowDayBespeakOverTime = int.Parse(row["NowDayBespeakOverTime"].ToString());
                }
                if (row["NowDayBespeakCancel"] != null && row["NowDayBespeakCancel"].ToString() != "")
                {
                    model.NowDayBespeakCancel = int.Parse(row["NowDayBespeakCancel"].ToString());
                }
                if (row["ViolationRecordCount"] != null && row["ViolationRecordCount"].ToString() != "")
                {
                    model.ViolationRecordCount = int.Parse(row["ViolationRecordCount"].ToString());
                }
                if (row["VRBookingTimeOut"] != null && row["VRBookingTimeOut"].ToString() != "")
                {
                    model.VRBookingTimeOut = int.Parse(row["VRBookingTimeOut"].ToString());
                }
                if (row["VRSeatOutTime"] != null && row["VRSeatOutTime"].ToString() != "")
                {
                    model.VRSeatOutTime = int.Parse(row["VRSeatOutTime"].ToString());
                }
                if (row["VRLeaveByAdmin"] != null && row["VRLeaveByAdmin"].ToString() != "")
                {
                    model.VRLeaveByAdmin = int.Parse(row["VRLeaveByAdmin"].ToString());
                }
                if (row["VRShortLeaveOutTime"] != null && row["VRShortLeaveOutTime"].ToString() != "")
                {
                    model.VRShortLeaveOutTime = int.Parse(row["VRShortLeaveOutTime"].ToString());
                }
                if (row["VRShortLeaveByAdminOutTime"] != null && row["VRShortLeaveByAdminOutTime"].ToString() != "")
                {
                    model.VRShortLeaveByAdminOutTime = int.Parse(row["VRShortLeaveByAdminOutTime"].ToString());
                }
                if (row["VRShortLeaveByReaderOutTime"] != null && row["VRShortLeaveByReaderOutTime"].ToString() != "")
                {
                    model.VRShortLeaveByReaderOutTime = int.Parse(row["VRShortLeaveByReaderOutTime"].ToString());
                }
                if (row["VRShortLeaveByServiceOutTime"] != null && row["VRShortLeaveByServiceOutTime"].ToString() != "")
                {
                    model.VRShortLeaveByServiceOutTime = int.Parse(row["VRShortLeaveByServiceOutTime"].ToString());
                }
                if (row["VRLeaveNotReadCard"] != null && row["VRLeaveNotReadCard"].ToString() != "")
                {
                    model.VRLeaveNotReadCard = int.Parse(row["VRLeaveNotReadCard"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,ReadingRoomNo,StatisticsDate,OpenTime,CloseTime,RoomUsageTime,SeatAllCount,SeatUsageCount,SeatUsageTime,ReaderUsageCount,UsedReaderCount,RushCardOperatingCount,SelectSeatCount,SelectSeatByAdmin,SelectSeatByReader,SelectSeatInSeatClient,SelectSeatInOtherClient,ReselectSeatCount,ReselectSeatInSeatClient,ReselectSeatInOtherClient,CheckBespeakCount,CheckBespeakInSeatClient,CkeckBespeakInOtherClient,WaitSeatCount,ShortLeaveCount,ShortLeaveTime,ShortLeaveByAdmin,ShortLeaveByReader,ShortLeaveByOtherReader,ShortLeaveByService,ShortLeaveInSeatClient,ShortLeaveInOtherClient,LeaveCount,LeaveByAdmin,LeaveByReader,LeaveByService,LeaveInSeatClient,LeaveInOtherClient,ComeBackCount,ComeBackByAdmin,ComeBackByReader,ComeBackByOtherReader,ComeBackInSeatClient,ComeBackInOtherClient,ContinueTimeCount,ContinueTimeByReader,ContinueTimeByService,ContinueTimeInSeatClient,ContinueTimeInOtherClient,AllBespeakCount,BespeakCount,CanBesapeakSeat,BespeakedSeat,BespeakCancel,BespeakOverTime,BespeakCheck,NowDayBespeakCheck,NowDayBespeakCount,NowDayBespeakOverTime,NowDayBespeakCancel,ViolationRecordCount,VRBookingTimeOut,VRSeatOutTime,VRLeaveByAdmin,VRShortLeaveOutTime,VRShortLeaveByAdminOutTime,VRShortLeaveByReaderOutTime,VRShortLeaveByServiceOutTime,VRLeaveNotReadCard ");
            strSql.Append(" FROM T_SM_RoomUsageStatistics ");
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
            strSql.Append(" id,ReadingRoomNo,StatisticsDate,OpenTime,CloseTime,RoomUsageTime,SeatAllCount,SeatUsageCount,SeatUsageTime,ReaderUsageCount,UsedReaderCount,RushCardOperatingCount,SelectSeatCount,SelectSeatByAdmin,SelectSeatByReader,SelectSeatInSeatClient,SelectSeatInOtherClient,ReselectSeatCount,ReselectSeatInSeatClient,ReselectSeatInOtherClient,CheckBespeakCount,CheckBespeakInSeatClient,CkeckBespeakInOtherClient,WaitSeatCount,ShortLeaveCount,ShortLeaveTime,ShortLeaveByAdmin,ShortLeaveByReader,ShortLeaveByOtherReader,ShortLeaveByService,ShortLeaveInSeatClient,ShortLeaveInOtherClient,LeaveCount,LeaveByAdmin,LeaveByReader,LeaveByService,LeaveInSeatClient,LeaveInOtherClient,ComeBackCount,ComeBackByAdmin,ComeBackByReader,ComeBackByOtherReader,ComeBackInSeatClient,ComeBackInOtherClient,ContinueTimeCount,ContinueTimeByReader,ContinueTimeByService,ContinueTimeInSeatClient,ContinueTimeInOtherClient,AllBespeakCount,BespeakCount,CanBesapeakSeat,BespeakedSeat,BespeakCancel,BespeakOverTime,BespeakCheck,NowDayBespeakCheck,NowDayBespeakCount,NowDayBespeakOverTime,NowDayBespeakCancel,ViolationRecordCount,VRBookingTimeOut,VRSeatOutTime,VRLeaveByAdmin,VRShortLeaveOutTime,VRShortLeaveByAdminOutTime,VRShortLeaveByReaderOutTime,VRShortLeaveByServiceOutTime,VRLeaveNotReadCard ");
            strSql.Append(" FROM T_SM_RoomUsageStatistics ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM T_SM_RoomUsageStatistics ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
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
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.id desc");
            }
            strSql.Append(")AS Row, T.*  from T_SM_RoomUsageStatistics T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
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
            parameters[0].Value = "T_SM_RoomUsageStatistics";
            parameters[1].Value = "id";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}

