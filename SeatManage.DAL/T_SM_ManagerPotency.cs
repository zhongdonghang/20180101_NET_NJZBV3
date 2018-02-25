/********************************
 * 作者：王昊天
 * 创建时间：2013-6-3
 * 说明：角色的操作dal
 * 修改人：
 * 修改时间：
 * ******************************/
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;
using SeatManage.ClassModel;
namespace SeatManage.DAL
{
	/// <summary>
	/// 数据访问类:T_SM_ManagerPotency
	/// </summary>
	public partial class T_SM_ManagerPotency
	{
		public T_SM_ManagerPotency()
		{}
		#region  Method

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere,SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select LoginID, UsrName, SchoolNo, SchoolName, LibraryNo, LibraryName, ReadingRoomNo, ReadingRoomName,ReadingSetting,RoomSeat");
            strSql.Append(" FROM ViewUserRoomRight ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" select LoginID, UsrName, SchoolNo, SchoolName, LibraryNo, LibraryName, ReadingRoomNo, ReadingRoomName,ReadingSetting,RoomSeat");
            strSql.Append(" FROM ViewUserRoomRight ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool AddUpdate(ManagerPotency model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete T_SM_ManagerPotency ");
            strSql.Append(" where LoginID=@LoginID");
            SqlParameter[] parameters = 
            {
                    new SqlParameter("@LoginID", model.LoginID)
            };
            try
            {
                int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
                rows = 0;
                //插入新数据
                foreach (ReadingRoomInfo room in model.RightRoomList)
                {
                    strSql = new StringBuilder();
                    strSql.Append("insert into T_SM_ManagerPotency(");
                    strSql.Append("RoomNo,LoginID)");
                    strSql.Append(" values (");
                    strSql.Append("@RoomNo,@LoginID)");
                    SqlParameter[] parameters_new = {
                    new SqlParameter("@RoomNo", room.No),
                    new SqlParameter("@LoginID", model.LoginID)};
                    rows += DbHelperSQL.ExecuteSql(strSql.ToString(), parameters_new);
                }
                if (model.RightRoomList.Count == rows)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                throw;
            }
        }
//        /// <summary>
//        /// 更新一条数据
//        /// </summary>
//        public bool Update(SeatManage.Model.T_SM_ManagerPotency model)
//        {
//            StringBuilder strSql=new StringBuilder();
//            strSql.Append("update T_SM_ManagerPotency set ");
//            strSql.Append("ManagerPotencyID=@ManagerPotencyID,");
//            strSql.Append("ManagerPotencyType=@ManagerPotencyType,");
//            strSql.Append("ReadingRoomID=@ReadingRoomID,");
//            strSql.Append("UserID=@UserID");
//            strSql.Append(" where ");
//            SqlParameter[] parameters = {
//                    new SqlParameter("@ManagerPotencyID", SqlDbType.Int,4),
//                    new SqlParameter("@ManagerPotencyType", SqlDbType.Int,4),
//                    new SqlParameter("@ReadingRoomID", SqlDbType.Int,4),
//                    new SqlParameter("@UserID", SqlDbType.VarChar,10)};
//            parameters[0].Value = model.ManagerPotencyID;
//            parameters[1].Value = model.ManagerPotencyType;
//            parameters[2].Value = model.ReadingRoomID;
//            parameters[3].Value = model.UserID;

//            int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
//            if (rows > 0)
//            {
//                return true;
//            }
//            else
//            {
//                return false;
//            }
//        }

//        /// <summary>
//        /// 删除一条数据
//        /// </summary>
//        public bool Delete()
//        {
//            //该表无主键信息，请自定义主键/条件字段
//            StringBuilder strSql=new StringBuilder();
//            strSql.Append("delete from T_SM_ManagerPotency ");
//            strSql.Append(" where ");
//            SqlParameter[] parameters = {
//};

//            int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
//            if (rows > 0)
//            {
//                return true;
//            }
//            else
//            {
//                return false;
//            }
//        }


//        /// <summary>
//        /// 得到一个对象实体
//        /// </summary>
//        public SeatManage.Model.T_SM_ManagerPotency GetModel()
//        {
//            //该表无主键信息，请自定义主键/条件字段
//            StringBuilder strSql=new StringBuilder();
//            strSql.Append("select  top 1 ManagerPotencyID,ManagerPotencyType,ReadingRoomID,UserID from T_SM_ManagerPotency ");
//            strSql.Append(" where ");
//            SqlParameter[] parameters = {
//};

//            SeatManage.Model.T_SM_ManagerPotency model=new SeatManage.Model.T_SM_ManagerPotency();
//            DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
//            if(ds.Tables[0].Rows.Count>0)
//            {
//                if(ds.Tables[0].Rows[0]["ManagerPotencyID"]!=null && ds.Tables[0].Rows[0]["ManagerPotencyID"].ToString()!="")
//                {
//                    model.ManagerPotencyID=int.Parse(ds.Tables[0].Rows[0]["ManagerPotencyID"].ToString());
//                }
//                if(ds.Tables[0].Rows[0]["ManagerPotencyType"]!=null && ds.Tables[0].Rows[0]["ManagerPotencyType"].ToString()!="")
//                {
//                    model.ManagerPotencyType=int.Parse(ds.Tables[0].Rows[0]["ManagerPotencyType"].ToString());
//                }
//                if(ds.Tables[0].Rows[0]["ReadingRoomID"]!=null && ds.Tables[0].Rows[0]["ReadingRoomID"].ToString()!="")
//                {
//                    model.ReadingRoomID=int.Parse(ds.Tables[0].Rows[0]["ReadingRoomID"].ToString());
//                }
//                if(ds.Tables[0].Rows[0]["UserID"]!=null && ds.Tables[0].Rows[0]["UserID"].ToString()!="")
//                {
//                    model.UserID=ds.Tables[0].Rows[0]["UserID"].ToString();
//                }
//                return model;
//            }
//            else
//            {
//                return null;
//            }
//        }

		

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
			parameters[0].Value = "T_SM_ManagerPotency";
			parameters[1].Value = "LibraryNo";
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

