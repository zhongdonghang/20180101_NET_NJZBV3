using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;
using SeatManage.ClassModel;
namespace SeatManage.DAL
{
    /// <summary>
    /// 数据访问类:SlipCustomer
    /// </summary>
    public partial class SlipCustomer
    {
        public SlipCustomer()
        { }
        #region  Method

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string No)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SlipCustomer");
            strSql.Append(" where No=@No ");
            SqlParameter[] parameters = {
					new SqlParameter("@No", SqlDbType.NVarChar,20)};
            parameters[0].Value = No;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, No,Name,ImageUrl,SlipTemplate,EffectDate,EndDate,Type,PrintAmount,LookOverAmount,CustomerImage,CampusNum,IsPrint,Num ");
            strSql.Append(" FROM SlipCustomer ");
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
            strSql.Append(" id, No,Name,ImageUrl,SlipTemplate,EffectDate,EndDate,Type,PrintAmount,CustomerImage,CampusNum,IsPrint,Num ");
            strSql.Append(" FROM SlipCustomer ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        ///// <summary>
        ///// 增加一条数据
        ///// </summary>
        public bool Add(SeatManage.ClassModel.AMS_SlipCustomer model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SlipCustomer(");
            strSql.Append("No,Name,ImageUrl,SlipTemplate,EffectDate,EndDate,Type,PrintAmount,LookOverAmount,CustomerImage,CampusNum,IsPrint,Num)");
            strSql.Append(" values (");
            strSql.Append("@No,@Name,@ImageUrl,@SlipTemplate,@EffectDate,@EndDate,@Type,@PrintAmount,@LookOverAmount,@CustomerImage,@CampusNum,@IsPrint,@Num)");
            SqlParameter[] parameters = {
                    new SqlParameter("@No", SqlDbType.NVarChar,20),
                    new SqlParameter("@Name", SqlDbType.NVarChar,50),
                    new SqlParameter("@ImageUrl", SqlDbType.NVarChar,200),
                    new SqlParameter("@SlipTemplate", SqlDbType.Text),
                    new SqlParameter("@EffectDate", SqlDbType.DateTime),
                    new SqlParameter("@EndDate", SqlDbType.DateTime),
                    new SqlParameter("@Type", SqlDbType.Int,4),
                                       new SqlParameter("@PrintAmount",SqlDbType.Int,4),
                                        new SqlParameter("@LookOverAmount",SqlDbType.Int,4),
                                        new SqlParameter("@CustomerImage",SqlDbType.NVarChar,200),
                                        new SqlParameter("@CampusNum",SqlDbType.NVarChar,50),
                                         new SqlParameter("@IsPrint",SqlDbType.Bit),
                                         new SqlParameter("@Num",model.Num)
                                        };
            parameters[0].Value = model.No;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.ImageName;
            parameters[3].Value = model.SlipTemplate;
            parameters[4].Value = model.EffectDate;
            parameters[5].Value = model.EndDate;
            parameters[6].Value = model.Type;
            parameters[7].Value = model.PrintAmount;
            parameters[8].Value = model.LookOverAmount;
            parameters[9].Value = model.CustomerLogo;
            parameters[10].Value = model.CampusNum;
            parameters[11].Value = model.IsPrint;
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
        public bool Update(SeatManage.ClassModel.AMS_SlipCustomer model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SlipCustomer set ");
            strSql.Append("Name=@Name,");
            strSql.Append("ImageUrl=@ImageUrl,");
            strSql.Append("SlipTemplate=@SlipTemplate,");
            strSql.Append("EffectDate=@EffectDate,");
            strSql.Append("EndDate=@EndDate,");
            strSql.Append("Type=@Type,");
            strSql.Append("PrintAmount=@PrintAmount,");
            strSql.Append("LookOverAmount=@LookOverAmount,");
            strSql.Append("CustomerImage=@CustomerImage,");
            strSql.Append("CampusNum=@CampusNum, ");
            strSql.Append("IsPrint=@IsPrint  ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Name", SqlDbType.NVarChar,50),
                    new SqlParameter("@ImageUrl", SqlDbType.NVarChar,200),
                    new SqlParameter("@SlipTemplate", SqlDbType.Text),
                    new SqlParameter("@EffectDate", SqlDbType.DateTime),
                    new SqlParameter("@EndDate", SqlDbType.DateTime),
                    new SqlParameter("@Type", SqlDbType.Int,4),
                    new SqlParameter("@Id", SqlDbType.NVarChar,20),
                     new SqlParameter("@PrintAmount",SqlDbType.Int,4),
                     new SqlParameter("@LookOverAmount",SqlDbType.Int,4),
                    new SqlParameter("@CustomerImage",SqlDbType.NVarChar,200),
                    new SqlParameter("@CampusNum",SqlDbType.NVarChar,50),
                     new SqlParameter("@IsPrint",SqlDbType.Bit)
                                        };
            parameters[0].Value = model.Name;
            parameters[1].Value = model.ImageName;
            parameters[2].Value = model.SlipTemplate;
            parameters[3].Value = model.EffectDate;
            parameters[4].Value = model.EndDate;
            parameters[5].Value = model.Type;
            parameters[6].Value = model.Id;
            parameters[7].Value = model.PrintAmount;
            parameters[8].Value = model.LookOverAmount;
            parameters[9].Value = model.CustomerLogo;
            parameters[10].Value = model.CampusNum;
            parameters[11].Value = model.IsPrint;
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
            strSql.Append("delete from SlipCustomer ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int)};
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
        ///// <summary>
        ///// 批量删除数据
        ///// </summary>
        //public bool DeleteList(string Nolist )
        //{
        //    StringBuilder strSql=new StringBuilder();
        //    strSql.Append("delete from SlipCustomer ");
        //    strSql.Append(" where No in ("+Nolist + ")  ");
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
        //public SeatManage.Model.SlipCustomer GetModel(string No)
        //{

        //    StringBuilder strSql=new StringBuilder();
        //    strSql.Append("select  top 1 No,Name,ImageUrl,SlipTemplate,EffectDate,EndDate,Type from SlipCustomer ");
        //    strSql.Append(" where No=@No ");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@No", SqlDbType.NVarChar,20)};
        //    parameters[0].Value = No;

        //    SeatManage.Model.SlipCustomer model=new SeatManage.Model.SlipCustomer();
        //    DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
        //    if(ds.Tables[0].Rows.Count>0)
        //    {
        //        if(ds.Tables[0].Rows[0]["No"]!=null && ds.Tables[0].Rows[0]["No"].ToString()!="")
        //        {
        //            model.No=ds.Tables[0].Rows[0]["No"].ToString();
        //        }
        //        if(ds.Tables[0].Rows[0]["Name"]!=null && ds.Tables[0].Rows[0]["Name"].ToString()!="")
        //        {
        //            model.Name=ds.Tables[0].Rows[0]["Name"].ToString();
        //        }
        //        if(ds.Tables[0].Rows[0]["ImageUrl"]!=null && ds.Tables[0].Rows[0]["ImageUrl"].ToString()!="")
        //        {
        //            model.ImageUrl=ds.Tables[0].Rows[0]["ImageUrl"].ToString();
        //        }
        //        if(ds.Tables[0].Rows[0]["SlipTemplate"]!=null && ds.Tables[0].Rows[0]["SlipTemplate"].ToString()!="")
        //        {
        //            model.SlipTemplate=ds.Tables[0].Rows[0]["SlipTemplate"].ToString();
        //        }
        //        if(ds.Tables[0].Rows[0]["EffectDate"]!=null && ds.Tables[0].Rows[0]["EffectDate"].ToString()!="")
        //        {
        //            model.EffectDate=DateTime.Parse(ds.Tables[0].Rows[0]["EffectDate"].ToString());
        //        }
        //        if(ds.Tables[0].Rows[0]["EndDate"]!=null && ds.Tables[0].Rows[0]["EndDate"].ToString()!="")
        //        {
        //            model.EndDate=DateTime.Parse(ds.Tables[0].Rows[0]["EndDate"].ToString());
        //        }
        //        if(ds.Tables[0].Rows[0]["Type"]!=null && ds.Tables[0].Rows[0]["Type"].ToString()!="")
        //        {
        //            model.Type=int.Parse(ds.Tables[0].Rows[0]["Type"].ToString());
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
            parameters[0].Value = "SlipCustomer";
            parameters[1].Value = "No";
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

