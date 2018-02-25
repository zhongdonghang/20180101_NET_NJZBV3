using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;//Please add references
namespace AMS.DAL
{
    /// <summary>
    /// 数据访问类:View_SlipCustomer
    /// </summary>
    public partial class View_SlipCustomer
    {
        public View_SlipCustomer()
        { }
        #region  Method



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(AMS.Model.View_SlipCustomer model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into View_SlipCustomer(");
            strSql.Append("CustomerNo,CompanyName,CustomerLinkWay,CustomerDescribe,Id,Number,SlipName,ImageUrl,SlipTemplate,CouponsXml,OperatorLonginId,OperatorPwd,OperatorBranchName,OperatorName,OperatorRemark,SlipCustomerDescribe,IsPrint,Type,EndDate,EffectDate,CampusNum,CustomerImage)");
            strSql.Append(" values (");
            strSql.Append("@CustomerNo,@CompanyName,@CustomerLinkWay,@CustomerDescribe,@Id,@Number,@SlipName,@ImageUrl,@SlipTemplate,@CouponsXml,@OperatorLonginId,@OperatorPwd,@OperatorBranchName,@OperatorName,@OperatorRemark,@SlipCustomerDescribe,@IsPrint,@Type,@EndDate,@EffectDate,@CampusNum,@CustomerImage)");
            SqlParameter[] parameters = {
					new SqlParameter("@CustomerNo", SqlDbType.NVarChar,20),
					new SqlParameter("@CompanyName", SqlDbType.NVarChar,20),
					new SqlParameter("@CustomerLinkWay", SqlDbType.NVarChar,500),
					new SqlParameter("@CustomerDescribe", SqlDbType.NVarChar,500),
					new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@Number", SqlDbType.NVarChar,50),
					new SqlParameter("@SlipName", SqlDbType.NVarChar,50),
					new SqlParameter("@ImageUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@SlipTemplate", SqlDbType.Text),
					new SqlParameter("@CouponsXml", SqlDbType.Text),
					new SqlParameter("@OperatorLonginId", SqlDbType.NVarChar,30),
					new SqlParameter("@OperatorPwd", SqlDbType.NVarChar,30),
					new SqlParameter("@OperatorBranchName", SqlDbType.NVarChar,30),
					new SqlParameter("@OperatorName", SqlDbType.NVarChar,30),
					new SqlParameter("@OperatorRemark", SqlDbType.NVarChar,50),
					new SqlParameter("@SlipCustomerDescribe", SqlDbType.NVarChar,200),
					new SqlParameter("@IsPrint", SqlDbType.Bit,1),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@EffectDate", SqlDbType.DateTime),
					new SqlParameter("@CampusNum", SqlDbType.NVarChar,50),
					new SqlParameter("@CustomerImage", SqlDbType.NVarChar,200)};
            parameters[0].Value = model.CustomerNo;
            parameters[1].Value = model.CompanyName;
            parameters[2].Value = model.CustomerLinkWay;
            parameters[3].Value = model.CustomerDescribe;
            parameters[4].Value = model.Id;
            parameters[5].Value = model.Number;
            parameters[6].Value = model.SlipName;
            parameters[7].Value = model.ImageUrl;
            parameters[8].Value = model.SlipTemplate;
            parameters[9].Value = model.CouponsXml;
            parameters[10].Value = model.OperatorLonginId;
            parameters[11].Value = model.OperatorPwd;
            parameters[12].Value = model.OperatorBranchName;
            parameters[13].Value = model.OperatorName;
            parameters[14].Value = model.OperatorRemark;
            parameters[15].Value = model.SlipCustomerDescribe;
            parameters[16].Value = model.IsPrint;
            parameters[17].Value = model.Type;
            parameters[18].Value = model.EndDate;
            parameters[19].Value = model.EffectDate;
            parameters[20].Value = model.CampusNum;
            parameters[21].Value = model.CustomerImage;

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
        public bool Update(AMS.Model.View_SlipCustomer model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update View_SlipCustomer set ");
            strSql.Append("CustomerNo=@CustomerNo,");
            strSql.Append("CompanyName=@CompanyName,");
            strSql.Append("CustomerLinkWay=@CustomerLinkWay,");
            strSql.Append("CustomerDescribe=@CustomerDescribe,");
            strSql.Append("Id=@Id,");
            strSql.Append("Number=@Number,");
            strSql.Append("SlipName=@SlipName,");
            strSql.Append("ImageUrl=@ImageUrl,");
            strSql.Append("SlipTemplate=@SlipTemplate,");
            strSql.Append("CouponsXml=@CouponsXml,");
            strSql.Append("OperatorLonginId=@OperatorLonginId,");
            strSql.Append("OperatorPwd=@OperatorPwd,");
            strSql.Append("OperatorBranchName=@OperatorBranchName,");
            strSql.Append("OperatorName=@OperatorName,");
            strSql.Append("OperatorRemark=@OperatorRemark,");
            strSql.Append("SlipCustomerDescribe=@SlipCustomerDescribe,");
            strSql.Append("IsPrint=@IsPrint,");
            strSql.Append("Type=@Type,");
            strSql.Append("EndDate=@EndDate,");
            strSql.Append("EffectDate=@EffectDate,");
            strSql.Append("CampusNum=@CampusNum,");
            strSql.Append("CustomerImage=@CustomerImage");
            strSql.Append(" where ");
            SqlParameter[] parameters = {
					new SqlParameter("@CustomerNo", SqlDbType.NVarChar,20),
					new SqlParameter("@CompanyName", SqlDbType.NVarChar,20),
					new SqlParameter("@CustomerLinkWay", SqlDbType.NVarChar,500),
					new SqlParameter("@CustomerDescribe", SqlDbType.NVarChar,500),
					new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@Number", SqlDbType.NVarChar,50),
					new SqlParameter("@SlipName", SqlDbType.NVarChar,50),
					new SqlParameter("@ImageUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@SlipTemplate", SqlDbType.Text),
					new SqlParameter("@CouponsXml", SqlDbType.Text),
					new SqlParameter("@OperatorLonginId", SqlDbType.NVarChar,30),
					new SqlParameter("@OperatorPwd", SqlDbType.NVarChar,30),
					new SqlParameter("@OperatorBranchName", SqlDbType.NVarChar,30),
					new SqlParameter("@OperatorName", SqlDbType.NVarChar,30),
					new SqlParameter("@OperatorRemark", SqlDbType.NVarChar,50),
					new SqlParameter("@SlipCustomerDescribe", SqlDbType.NVarChar,200),
					new SqlParameter("@IsPrint", SqlDbType.Bit,1),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@EffectDate", SqlDbType.DateTime),
					new SqlParameter("@CampusNum", SqlDbType.NVarChar,50),
					new SqlParameter("@CustomerImage", SqlDbType.NVarChar,200)};
            parameters[0].Value = model.CustomerNo;
            parameters[1].Value = model.CompanyName;
            parameters[2].Value = model.CustomerLinkWay;
            parameters[3].Value = model.CustomerDescribe;
            parameters[4].Value = model.Id;
            parameters[5].Value = model.Number;
            parameters[6].Value = model.SlipName;
            parameters[7].Value = model.ImageUrl;
            parameters[8].Value = model.SlipTemplate;
            parameters[9].Value = model.CouponsXml;
            parameters[10].Value = model.OperatorLonginId;
            parameters[11].Value = model.OperatorPwd;
            parameters[12].Value = model.OperatorBranchName;
            parameters[13].Value = model.OperatorName;
            parameters[14].Value = model.OperatorRemark;
            parameters[15].Value = model.SlipCustomerDescribe;
            parameters[16].Value = model.IsPrint;
            parameters[17].Value = model.Type;
            parameters[18].Value = model.EndDate;
            parameters[19].Value = model.EffectDate;
            parameters[20].Value = model.CampusNum;
            parameters[21].Value = model.CustomerImage;

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
        public bool Delete()
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from View_SlipCustomer ");
            strSql.Append(" where ");
            SqlParameter[] parameters = {
};

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
        /// 得到一个对象实体
        /// </summary>
        public AMS.Model.View_SlipCustomer GetModel()
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 CustomerNo,CompanyName,CustomerLinkWay,CustomerDescribe,Id,Number,SlipName,ImageUrl,SlipTemplate,CouponsXml,OperatorLonginId,OperatorPwd,OperatorBranchName,OperatorName,OperatorRemark,SlipCustomerDescribe,IsPrint,Type,EndDate,EffectDate,CampusNum,CustomerImage from View_SlipCustomer ");
            strSql.Append(" where ");
            SqlParameter[] parameters = {
};

            AMS.Model.View_SlipCustomer model = new AMS.Model.View_SlipCustomer();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["CustomerNo"] != null && ds.Tables[0].Rows[0]["CustomerNo"].ToString() != "")
                {
                    model.CustomerNo = ds.Tables[0].Rows[0]["CustomerNo"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CompanyName"] != null && ds.Tables[0].Rows[0]["CompanyName"].ToString() != "")
                {
                    model.CompanyName = ds.Tables[0].Rows[0]["CompanyName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CustomerLinkWay"] != null && ds.Tables[0].Rows[0]["CustomerLinkWay"].ToString() != "")
                {
                    model.CustomerLinkWay = ds.Tables[0].Rows[0]["CustomerLinkWay"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CustomerDescribe"] != null && ds.Tables[0].Rows[0]["CustomerDescribe"].ToString() != "")
                {
                    model.CustomerDescribe = ds.Tables[0].Rows[0]["CustomerDescribe"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Id"] != null && ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Number"] != null && ds.Tables[0].Rows[0]["Number"].ToString() != "")
                {
                    model.Number = ds.Tables[0].Rows[0]["Number"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SlipName"] != null && ds.Tables[0].Rows[0]["SlipName"].ToString() != "")
                {
                    model.SlipName = ds.Tables[0].Rows[0]["SlipName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ImageUrl"] != null && ds.Tables[0].Rows[0]["ImageUrl"].ToString() != "")
                {
                    model.ImageUrl = ds.Tables[0].Rows[0]["ImageUrl"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SlipTemplate"] != null && ds.Tables[0].Rows[0]["SlipTemplate"].ToString() != "")
                {
                    model.SlipTemplate = ds.Tables[0].Rows[0]["SlipTemplate"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CouponsXml"] != null && ds.Tables[0].Rows[0]["CouponsXml"].ToString() != "")
                {
                    model.CouponsXml = ds.Tables[0].Rows[0]["CouponsXml"].ToString();
                }
                if (ds.Tables[0].Rows[0]["OperatorLonginId"] != null && ds.Tables[0].Rows[0]["OperatorLonginId"].ToString() != "")
                {
                    model.OperatorLonginId = ds.Tables[0].Rows[0]["OperatorLonginId"].ToString();
                }
                if (ds.Tables[0].Rows[0]["OperatorPwd"] != null && ds.Tables[0].Rows[0]["OperatorPwd"].ToString() != "")
                {
                    model.OperatorPwd = ds.Tables[0].Rows[0]["OperatorPwd"].ToString();
                }
                if (ds.Tables[0].Rows[0]["OperatorBranchName"] != null && ds.Tables[0].Rows[0]["OperatorBranchName"].ToString() != "")
                {
                    model.OperatorBranchName = ds.Tables[0].Rows[0]["OperatorBranchName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["OperatorName"] != null && ds.Tables[0].Rows[0]["OperatorName"].ToString() != "")
                {
                    model.OperatorName = ds.Tables[0].Rows[0]["OperatorName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["OperatorRemark"] != null && ds.Tables[0].Rows[0]["OperatorRemark"].ToString() != "")
                {
                    model.OperatorRemark = ds.Tables[0].Rows[0]["OperatorRemark"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SlipCustomerDescribe"] != null && ds.Tables[0].Rows[0]["SlipCustomerDescribe"].ToString() != "")
                {
                    model.SlipCustomerDescribe = ds.Tables[0].Rows[0]["SlipCustomerDescribe"].ToString();
                }
                if (ds.Tables[0].Rows[0]["IsPrint"] != null && ds.Tables[0].Rows[0]["IsPrint"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsPrint"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsPrint"].ToString().ToLower() == "true"))
                    {
                        model.IsPrint = true;
                    }
                    else
                    {
                        model.IsPrint = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["Type"] != null && ds.Tables[0].Rows[0]["Type"].ToString() != "")
                {
                    model.Type = int.Parse(ds.Tables[0].Rows[0]["Type"].ToString());
                }
                if (ds.Tables[0].Rows[0]["EndDate"] != null && ds.Tables[0].Rows[0]["EndDate"].ToString() != "")
                {
                    model.EndDate = DateTime.Parse(ds.Tables[0].Rows[0]["EndDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["EffectDate"] != null && ds.Tables[0].Rows[0]["EffectDate"].ToString() != "")
                {
                    model.EffectDate = DateTime.Parse(ds.Tables[0].Rows[0]["EffectDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CampusNum"] != null && ds.Tables[0].Rows[0]["CampusNum"].ToString() != "")
                {
                    model.CampusNum = ds.Tables[0].Rows[0]["CampusNum"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CustomerImage"] != null && ds.Tables[0].Rows[0]["CustomerImage"].ToString() != "")
                {
                    model.CustomerImage = ds.Tables[0].Rows[0]["CustomerImage"].ToString();
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
            strSql.Append("select CustomerNo,CustomerId,CompanyName,CustomerLinkWay,CustomerDescribe,Id,Number,SlipName,ImageUrl,SlipTemplate,CouponsXml,OperatorLonginId,OperatorPwd,OperatorBranchName,OperatorName,OperatorRemark,SlipCustomerDescribe,IsPrint,Type,EndDate,EffectDate,CampusNum,CustomerImage ");
            strSql.Append(" FROM View_SlipCustomer ");
            if (!string.IsNullOrEmpty(strWhere))
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
            strSql.Append(" CustomerNo,CompanyName,CustomerLinkWay,CustomerDescribe,Id,Number,SlipName,ImageUrl,SlipTemplate,CouponsXml,OperatorLonginId,OperatorPwd,OperatorBranchName,OperatorName,OperatorRemark,SlipCustomerDescribe,IsPrint,Type,EndDate,EffectDate,CampusNum,CustomerImage ");
            strSql.Append(" FROM View_SlipCustomer ");
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
            parameters[0].Value = "View_SlipCustomer";
            parameters[1].Value = "id";
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

