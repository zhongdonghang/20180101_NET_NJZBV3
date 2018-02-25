using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;//Please add references
namespace AMS.DAL
{
    /// <summary>
    /// 数据访问类:AMS_School
    /// </summary>
    public partial class AMS_School
    {
        public AMS_School()
        { }
        #region  Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("Id", "AMS_School");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from AMS_School");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(AMS.Model.AMS_School model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AMS_School(");
            strSql.Append(" Number,Name,DTUip,Describe,ConnectionString,LinkMan,LinkAddress,ProvinceID,CardInfo,InterfaceInfo,Flag,ExecuteProgress,InstallDate,InstallMan,AppOpen)");
            strSql.Append(" values (");
            strSql.Append(" @Number,@Name,@DTUip,@Describe,@ConnectionString,@LinkMan,@LinkAddress,@ProvinceID,@CardInfo,@InterfaceInfo,@Flag,@ExecuteProgress,@InstallDate,@InstallMan,@AppOpen)");
            SqlParameter[] parameters = { 
					new SqlParameter("@Number", SqlDbType.NVarChar,50),
					new SqlParameter("@Name", SqlDbType.NVarChar,30),
					new SqlParameter("@DTUip", SqlDbType.NVarChar,20),
					new SqlParameter("@Describe", SqlDbType.NVarChar,200),
					new SqlParameter("@ConnectionString", SqlDbType.NVarChar,200),
					new SqlParameter("@LinkMan", SqlDbType.NVarChar,300),
					new SqlParameter("@LinkAddress", SqlDbType.NVarChar,300),
					new SqlParameter("@ProvinceID", SqlDbType.Int,4),
					new SqlParameter("@CardInfo", SqlDbType.Text),
					new SqlParameter("@InterfaceInfo", SqlDbType.Text),
                    new SqlParameter("@Flag", SqlDbType.Int,4),
                    new SqlParameter("@ExecuteProgress",SqlDbType.NVarChar),
                    new SqlParameter("@InstallDate",SqlDbType.DateTime),
                    new SqlParameter("@InstallMan",SqlDbType.NVarChar),
                    new SqlParameter("@AppOpen",SqlDbType.Int)};
            parameters[0].Value = model.Number;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.DTUip;
            parameters[3].Value = model.Describe;
            parameters[4].Value = model.ConnectionString;
            parameters[5].Value = model.LinkMan;
            parameters[6].Value = model.LinkAddress;
            parameters[7].Value = model.ProvinceID;
            parameters[8].Value = model.CardInfo;
            parameters[9].Value = model.InterfaceInfo;
            parameters[10].Value = model.IsSeatBespeak ? 1 : 0;
            parameters[11].Value = model.ExecuteProgress;
            parameters[12].Value = model.InstallDate;
            parameters[13].Value = model.InstallMan;
            parameters[14].Value = model.AppOpen ? 1 : 0;
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
        public bool Update(AMS.Model.AMS_School model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AMS_School set ");
            strSql.Append("Number=@Number,");
            strSql.Append("Name=@Name,");
            strSql.Append("DTUip=@DTUip,");
            strSql.Append("Describe=@Describe,");
            strSql.Append("ConnectionString=@ConnectionString,");
            strSql.Append("LinkMan=@LinkMan,");
            strSql.Append("LinkAddress=@LinkAddress,");
            strSql.Append("ProvinceID=@ProvinceID,");
            strSql.Append("CardInfo=@CardInfo,");
            strSql.Append("InterfaceInfo=@InterfaceInfo,");
            strSql.Append("ExecuteProgress=@ExecuteProgress,");
            strSql.Append("InstallDate=@InstallDate,");
            strSql.Append("InstallMan=@InstallMan,");
            strSql.Append("Flag=@Flag,");
            strSql.Append("AppOpen=@AppOpen");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Number", SqlDbType.NVarChar,50),
					new SqlParameter("@Name", SqlDbType.NVarChar,30),
					new SqlParameter("@DTUip", SqlDbType.NVarChar,20),
					new SqlParameter("@Describe", SqlDbType.NVarChar,200),
					new SqlParameter("@ConnectionString", SqlDbType.NVarChar,200),
					new SqlParameter("@LinkMan", SqlDbType.NVarChar,300),
					new SqlParameter("@LinkAddress", SqlDbType.NVarChar,300),
					new SqlParameter("@ProvinceID", SqlDbType.Int,4),
					new SqlParameter("@CardInfo", SqlDbType.Text),
					new SqlParameter("@InterfaceInfo", SqlDbType.Text),
                    new SqlParameter("@Flag", SqlDbType.Int,4),
					new SqlParameter("@Id", SqlDbType.Int,4),
                    new SqlParameter("@ExecuteProgress",SqlDbType.NVarChar),
                    new SqlParameter("@InstallDate",SqlDbType.DateTime),
                    new SqlParameter("@InstallMan",SqlDbType.NVarChar),
                    new SqlParameter("@AppOpen",SqlDbType.Int)};
            parameters[0].Value = model.Number;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.DTUip;
            parameters[3].Value = model.Describe;
            parameters[4].Value = model.ConnectionString;
            parameters[5].Value = model.LinkMan;
            parameters[6].Value = model.LinkAddress;
            parameters[7].Value = model.ProvinceID;
            parameters[8].Value = model.CardInfo;
            parameters[9].Value = model.InterfaceInfo;
            parameters[10].Value = model.IsSeatBespeak ? 1 : 0;
            parameters[11].Value = model.Id;
            parameters[12].Value = model.ExecuteProgress;
            parameters[13].Value = model.InstallDate;
            parameters[14].Value = model.InstallMan;
            parameters[15].Value = model.AppOpen ? 1 : 0;
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
        public bool Delete(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from AMS_School ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

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
        public bool DeleteList(string Idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from AMS_School ");
            strSql.Append(" where Id in (" + Idlist + ")  ");
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
        public AMS.Model.AMS_School GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,Number,Name,DTUip,Describe,ConnectionString,LinkMan,LinkAddress,ProvinceID,CardInfo,InterfaceInfo,Flag from AMS_School ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            AMS.Model.AMS_School model = new AMS.Model.AMS_School();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"] != null && ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Number"] != null && ds.Tables[0].Rows[0]["Number"].ToString() != "")
                {
                    model.Number = ds.Tables[0].Rows[0]["Number"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Name"] != null && ds.Tables[0].Rows[0]["Name"].ToString() != "")
                {
                    model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DTUip"] != null && ds.Tables[0].Rows[0]["DTUip"].ToString() != "")
                {
                    model.DTUip = ds.Tables[0].Rows[0]["DTUip"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Describe"] != null && ds.Tables[0].Rows[0]["Describe"].ToString() != "")
                {
                    model.Describe = ds.Tables[0].Rows[0]["Describe"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ConnectionString"] != null && ds.Tables[0].Rows[0]["ConnectionString"].ToString() != "")
                {
                    model.ConnectionString = ds.Tables[0].Rows[0]["ConnectionString"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LinkMan"] != null && ds.Tables[0].Rows[0]["LinkMan"].ToString() != "")
                {
                    model.LinkMan = ds.Tables[0].Rows[0]["LinkMan"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LinkAddress"] != null && ds.Tables[0].Rows[0]["LinkAddress"].ToString() != "")
                {
                    model.LinkAddress = ds.Tables[0].Rows[0]["LinkAddress"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ProvinceID"] != null && ds.Tables[0].Rows[0]["ProvinceID"].ToString() != "")
                {
                    model.ProvinceID = int.Parse(ds.Tables[0].Rows[0]["ProvinceID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CardInfo"] != null && ds.Tables[0].Rows[0]["CardInfo"].ToString() != "")
                {
                    model.CardInfo = ds.Tables[0].Rows[0]["CardInfo"].ToString();
                }
                if (ds.Tables[0].Rows[0]["InterfaceInfo"] != null && ds.Tables[0].Rows[0]["InterfaceInfo"].ToString() != "")
                {
                    model.InterfaceInfo = ds.Tables[0].Rows[0]["InterfaceInfo"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Flag"] != null && ds.Tables[0].Rows[0]["Flag"].ToString() != "")
                {
                    model.IsSeatBespeak = ds.Tables[0].Rows[0]["Flag"].ToString() == "1" ? true : false;
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public AMS.Model.AMS_School GetModel(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,Number,Name,DTUip,Describe,ConnectionString,LinkMan,LinkAddress,ProvinceID,CardInfo,InterfaceInfo,Flag from AMS_School ");
            if (!string.IsNullOrEmpty(strWhere) && strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            AMS.Model.AMS_School model = new AMS.Model.AMS_School();
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"] != null && ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Number"] != null && ds.Tables[0].Rows[0]["Number"].ToString() != "")
                {
                    model.Number = ds.Tables[0].Rows[0]["Number"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Name"] != null && ds.Tables[0].Rows[0]["Name"].ToString() != "")
                {
                    model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DTUip"] != null && ds.Tables[0].Rows[0]["DTUip"].ToString() != "")
                {
                    model.DTUip = ds.Tables[0].Rows[0]["DTUip"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Describe"] != null && ds.Tables[0].Rows[0]["Describe"].ToString() != "")
                {
                    model.Describe = ds.Tables[0].Rows[0]["Describe"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ConnectionString"] != null && ds.Tables[0].Rows[0]["ConnectionString"].ToString() != "")
                {
                    model.ConnectionString = ds.Tables[0].Rows[0]["ConnectionString"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LinkMan"] != null && ds.Tables[0].Rows[0]["LinkMan"].ToString() != "")
                {
                    model.LinkMan = ds.Tables[0].Rows[0]["LinkMan"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LinkAddress"] != null && ds.Tables[0].Rows[0]["LinkAddress"].ToString() != "")
                {
                    model.LinkAddress = ds.Tables[0].Rows[0]["LinkAddress"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ProvinceID"] != null && ds.Tables[0].Rows[0]["ProvinceID"].ToString() != "")
                {
                    model.ProvinceID = int.Parse(ds.Tables[0].Rows[0]["ProvinceID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CardInfo"] != null && ds.Tables[0].Rows[0]["CardInfo"].ToString() != "")
                {
                    model.CardInfo = ds.Tables[0].Rows[0]["CardInfo"].ToString();
                }
                if (ds.Tables[0].Rows[0]["InterfaceInfo"] != null && ds.Tables[0].Rows[0]["InterfaceInfo"].ToString() != "")
                {
                    model.InterfaceInfo = ds.Tables[0].Rows[0]["InterfaceInfo"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Flag"] != null && ds.Tables[0].Rows[0]["Flag"].ToString() != "")
                {
                    model.IsSeatBespeak = ds.Tables[0].Rows[0]["Flag"].ToString() == "1" ? true : false;
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
            strSql.Append("select Id,Number,Name,DTUip,Describe,ConnectionString,LinkMan,LinkAddress,ProvinceID,CardInfo,InterfaceInfo,ExecuteProgress,InstallDate,InstallMan,IsSeatBespeak,Flag,AppOpen ");
            strSql.Append(" FROM AMS_School ");
            if (!string.IsNullOrEmpty(strWhere) && strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by Name");
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
            strSql.Append(" Id,Number,Name,DTUip,Describe,ConnectionString,LinkMan,LinkAddress,ProvinceID,CardInfo,InterfaceInfo,Flag.AppOpen ");
            strSql.Append(" FROM AMS_School ");
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
            parameters[0].Value = "AMS_School";
            parameters[1].Value = "Id";
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

