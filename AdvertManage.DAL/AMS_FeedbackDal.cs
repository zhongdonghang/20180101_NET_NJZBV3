using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DBUtility;
using System.Data;

namespace AdvertManage.DAL
{
    public class AMS_FeedbackDal
    {
        /// <summary>
        /// 插入反馈意见
        /// </summary>
        /// <param name="cardNo">学号</param>
        /// <param name="remark">反馈意见</param>
        /// <param name="loginId"></param>
        /// <returns></returns>
        public int AddFeedback(Model.AMS_FeedbackModel model)
        {
            string sql = "INSERT INTO [dbo].[AMS_Feedback] ([CardNo],[SchoolId],[Remark],[SubmitTime]) VALUES (@cardNo,@schoolId,@remark,GETDATE())";
            SqlParameter[] parameters = {
                                        new SqlParameter("@cardNo",SqlDbType.NVarChar,100),
                                        new SqlParameter("@remark",SqlDbType.NVarChar,800),
                                        new SqlParameter("@schoolId",SqlDbType.NVarChar,50)
                                      };
            parameters[0].Value = model.CardNo;
            parameters[1].Value = model.Remark;
            parameters[2].Value = model.SchoolId;
            object obj = DbHelperSQL.ExecuteSql(sql.ToString(), parameters);
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
        /// 捕获异常信息并插入数据库
        /// </summary>
        /// <param name="error">异常信息</param>
        public void AddErrorMessage(string error)
        {
            SqlParameter[] parameters = { new SqlParameter("@error", error) };
            string sql = "INSERT INTO [dbo].[AMS_ErrorLogs]([Remark],[SubmitTime]) VALUES(@error,getdate())";
            try
            {
                DbHelperSQL.ExecuteSql(sql, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 网站访问量
        /// </summary>
        /// <param name="id"></param>
        public void SetPageView(string id)
        {
            SqlParameter[] patameter = { new SqlParameter("@id", SqlDbType.Int) };
            patameter[0].Value = int.Parse(id);
            string sqlSel = "SELECT [PageView] FROM [dbo].[AMS_School] WHERE [Id]=@id";
            try
            {
                DataSet ds = DbHelperSQL.Query(sqlSel, patameter);
                if (ds != null)
                {
                    int pageView = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                    pageView += 1;
                    string sqlUpdate = "UPDATE [dbo].[AMS_School] SET [PageView] = '" + pageView + "' WHERE Id='" + id + "'";
                    DbHelperSQL.ExecuteSql(sqlUpdate);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
