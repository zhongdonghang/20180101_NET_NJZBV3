using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;
using SeatManage.ClassModel;
namespace SeatManage.DAL
{
    /// <summary>
    /// 数据访问类:T_SM_ReaderNotes
    /// </summary>
    public partial class T_SM_ReaderNotice
    {
        public T_SM_ReaderNotice()
        { }
        #region  Method

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int NoticeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_SM_ReaderNotice");
            strSql.Append(" where NoticeId=@NoticeId");
            SqlParameter[] parameters = {
					new SqlParameter("@NoticeId", SqlDbType.Int,4)
                                        };
            parameters[0].Value = NoticeId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select NoticeId,NoticeTitle,CardNo,AddTime,NoticeContent,IsRead ");
            strSql.Append(" FROM T_SM_ReaderNotice ");
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
            strSql.Append(" NoticeId,CardNo,NoticeTitle,NoticeContent,IsRead ");
            strSql.Append(" FROM T_SM_ReaderNotice ");
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
        public int Add(ReaderNoticeInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_SM_ReaderNotice(");
            strSql.Append("CardNo,AddTime,NoticeTitle,NoticeContent,IsRead)");
            strSql.Append(" values (");
            strSql.Append("@CardNo,@AddTime,@NoticeTitle,@NoticeContent,@IsRead)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                            new SqlParameter("@CardNo", SqlDbType.NVarChar,20),
                            new SqlParameter("@AddTime",SqlDbType.DateTime), 
                            new SqlParameter("@NoticeTitle",SqlDbType.NVarChar,200),
                            new SqlParameter("@NoticeContent", SqlDbType.Text),
                            new SqlParameter("@IsRead", SqlDbType.Int,4)};
            parameters[0].Value = model.CardNo;
            parameters[1].Value = model.AddTime;
            parameters[2].Value = ((int)model.Type).ToString();
            parameters[3].Value = model.Note;
            parameters[4].Value = (int)model.IsRead;

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
        public bool Update( ReaderNoticeInfo model)
                {
                    StringBuilder strSql=new StringBuilder();
                    strSql.Append("update T_SM_ReaderNotice set ");
                    strSql.Append("CardNo=@CardNo,");
                    strSql.Append("AddTime=@AddTime,");
                    strSql.Append("NoticeContent=@NoticeContent,");
                    strSql.Append("IsRead=@IsRead,");
                    strSql.Append("NoticeTitle=@NoticeTitle");
                    strSql.Append(" where NoticeId=@NoticeId");
                    SqlParameter[] parameters = {
                            new SqlParameter("@CardNo", SqlDbType.NVarChar,20),
                            new SqlParameter("@AddTime",SqlDbType.DateTime),
                            new SqlParameter("@NoticeContent", SqlDbType.Text),
                            new SqlParameter("@IsRead", SqlDbType.Int,4),
                            new SqlParameter("@NoticeTitle",SqlDbType.NVarChar,200),
                            new SqlParameter("@NoticeId", SqlDbType.Int,4)};
                    parameters[0].Value = model.CardNo;
                    parameters[1].Value = model.AddTime;
                    parameters[2].Value = model.Note;
                    parameters[3].Value = (int)model.IsRead;
                    parameters[4].Value = ((int)model.Type).ToString();
                    parameters[5].Value = model.NoticeID;

                    int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
                    if (rows > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

        //        /// <summary>
        //        /// 删除一条数据
        //        /// </summary>
        //        public bool Delete(int NoteId)
        //        {

        //            StringBuilder strSql=new StringBuilder();
        //            strSql.Append("delete from T_SM_ReaderNotice ");
        //            strSql.Append(" where NoteId=@NoteId");
        //            SqlParameter[] parameters = {
        //                    new SqlParameter("@NoteId", SqlDbType.Int,4)
        //};
        //            parameters[0].Value = NoteId;

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
        //        /// 批量删除数据
        //        /// </summary>
        //        public bool DeleteList(string NoteIdlist )
        //        {
        //            StringBuilder strSql=new StringBuilder();
        //            strSql.Append("delete from T_SM_ReaderNotice ");
        //            strSql.Append(" where NoteId in ("+NoteIdlist + ")  ");
        //            int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
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
        //        public SeatManage.Model.T_SM_ReaderNotes GetModel(int NoteId)
        //        {

        //            StringBuilder strSql=new StringBuilder();
        //            strSql.Append("select  top 1 NoteId,CardNo,NoteContent],IsReader from T_SM_ReaderNotice ");
        //            strSql.Append(" where NoteId=@NoteId");
        //            SqlParameter[] parameters = {
        //                    new SqlParameter("@NoteId", SqlDbType.Int,4)
        //};
        //            parameters[0].Value = NoteId;

        //            SeatManage.Model.T_SM_ReaderNotes model=new SeatManage.Model.T_SM_ReaderNotes();
        //            DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
        //            if(ds.Tables[0].Rows.Count>0)
        //            {
        //                if(ds.Tables[0].Rows[0]["NoteId"]!=null && ds.Tables[0].Rows[0]["NoteId"].ToString()!="")
        //                {
        //                    model.NoteId=int.Parse(ds.Tables[0].Rows[0]["NoteId"].ToString());
        //                }
        //                if(ds.Tables[0].Rows[0]["CardNo"]!=null && ds.Tables[0].Rows[0]["CardNo"].ToString()!="")
        //                {
        //                    model.CardNo=ds.Tables[0].Rows[0]["CardNo"].ToString();
        //                }
        //                if(ds.Tables[0].Rows[0]["NoteContent]"]!=null && ds.Tables[0].Rows[0]["NoteContent]"].ToString()!="")
        //                {
        //                    model.NoteContent]=ds.Tables[0].Rows[0]["NoteContent]"].ToString();
        //                }
        //                if(ds.Tables[0].Rows[0]["IsReader"]!=null && ds.Tables[0].Rows[0]["IsReader"].ToString()!="")
        //                {
        //                    model.IsReader=int.Parse(ds.Tables[0].Rows[0]["IsReader"].ToString());
        //                }
        //                return model;
        //            }
        //            else
        //            {
        //                return null;
        //            }
        //        }



       
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(string cardNum, int pageIndex, int pageSize)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT NoticeId, CardNo,AddTime,NoticeTitle,NoticeContent,IsRead FROM");
            strSql.AppendFormat(" (SELECT ROW_NUMBER() OVER (ORDER BY AddTime DESC) AS SerialNumber,NoticeId,CardNo,AddTime,NoticeTitle,NoticeContent,IsRead FROM T_SM_ReaderNotice where cardNo='{0}' ) AS T", cardNum);
            strSql.AppendFormat(" WHERE T.SerialNumber >{0}  and T.SerialNumber <= {1}", pageIndex * pageSize, (pageIndex + 1) * pageSize);

            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  Method
    }
}

