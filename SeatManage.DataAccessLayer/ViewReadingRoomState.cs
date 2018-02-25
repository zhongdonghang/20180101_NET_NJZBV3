using System.Data;
using System.Data.SqlClient;
using System.Text;
using DBUtility;

namespace SeatManage.DataAccessLayer
{
    public class ViewReadingRoomState
    {
        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetList(string strWhere, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [BespeakCount],[UsingCount],[ReadingRoomNo],[ReadingRoomName],[ReadingSetting],[RoomSeat],[AreaInfo],[AreaName],[SchoolNo],[LibraryNo],[LibraryName],[SchoolName]");
            strSql.Append(" FROM ViewReadingRoomState ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }
    }
}
