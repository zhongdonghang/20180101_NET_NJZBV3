using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdvertManage.Model;
using System.Data.SqlClient;
using System.Data;
using DBUtility;

namespace AdvertManage.DAL
{
    /// <summary>
    /// 添加进出记录
    /// </summary>
    public class AMS_EnterOutLogDal
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(AMS_EnterOutLog model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AMS_EnterOutLog(");
            strSql.Append("[SchoolId] ,[CardNo] ,[EnterOutNo] ,[EnterOutState] ,[SeatNo],[EnterOutTime] ,[Operator] ,[EnterOutType] ,[ReadingRoomNum],[Remark])");
            strSql.Append(" values (");
            strSql.Append("@SchoolId ,@CardNo ,@EnterOutNo ,@EnterOutState ,@SeatNo,@EnterOutTime ,@Operator ,@EnterOutType ,@ReadingRoomNum,@Remark)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@SchoolId", SqlDbType.Int,4),
					new SqlParameter("@CardNo", SqlDbType.NVarChar,50),
					new SqlParameter("@EnterOutNo", SqlDbType.NVarChar,100),
					new SqlParameter("@EnterOutState", SqlDbType.Int,4),
					new SqlParameter("@SeatNo", SqlDbType.NVarChar,200),
					new SqlParameter("@EnterOutTime", SqlDbType.DateTime),
					new SqlParameter("@Operator", SqlDbType.Int,4),
                    new SqlParameter("@EnterOutType", SqlDbType.Int,4),
					new SqlParameter("@ReadingRoomNum", SqlDbType.NVarChar,50),
					new SqlParameter("@Remark", SqlDbType.NVarChar)};
            parameters[0].Value = model.SchoolId;
            parameters[1].Value = model.CardNo;
            parameters[2].Value = model.EnterOutNo;
            parameters[3].Value = (int)model.EnterOutState;
            parameters[4].Value = model.SeatNo;
            parameters[5].Value = model.EnterOutTime;
            parameters[6].Value =(int) model.Operator;
            parameters[7].Value =(int) model.EnterOutType;
            parameters[8].Value = model.ReadingRoomNum;
            parameters[9].Value = model.Remark;

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
    }
}
