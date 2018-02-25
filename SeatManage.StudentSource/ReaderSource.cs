using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using SeatManage.ISystemTerminal.IStuLibSync;
using System.Data;
using System.Configuration;
using System.IO;

namespace SeatManage.StudentSource
{
    public class ReaderSource : IReaderSource
    {
        #region IReaderSource 成员
        private string strConn = "";
        ClassModel.StuLibSyncSetting _StuLibSyncSet = null;
        /// <summary>
        /// 源读者信息库设置
        /// </summary>
        public ClassModel.StuLibSyncSetting StuLibSyncSet
        {
            get
            {
                return _StuLibSyncSet;
            }
            set
            {
                _StuLibSyncSet = value;
                strConn = GetConnStr(_StuLibSyncSet);//解析连接字符串
            }
        }
        /// <summary>
        /// 获取源读者信息库中的读者信息并返回List
        /// </summary>
        /// <returns></returns>
        public List<ClassModel.ReaderInfo> GetSourceReaderInfo()
        {
            List<ClassModel.ReaderInfo> readerList = ReaderStudentInfo();

            return readerList;
        }
        /// <summary>
        /// 源读者信息库连接测试
        /// </summary>
        /// <returns></returns>
        public bool LinkDataSourceTest()
        {
            SqlConnection conn = new SqlConnection(strConn);
            try
            {
                conn.Open();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                conn.Close();
            }
        }


        #endregion

        #region 私有方法
        private List<ClassModel.ReaderInfo> ReaderStudentInfo()
        {
            try
            {
                List<ClassModel.ReaderInfo> list = new List<ClassModel.ReaderInfo>();
                DataSet ds = GetReaderInfoDs();
                //SeatManage.SeatManageComm.WriteLog.Write(string.Format("获取到{0}条读者信息", ds.Tables[0].Rows.Count));
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ClassModel.ReaderInfo reader = DataRowToReaderInfo(ds.Tables[0].Rows[i]);
                    list.Add(reader);
                }
                //  SeatManageComm.WriteLog.Write(string.Format("返回{0}条", list.Count));
                return list;
            }
            catch (Exception ex)
            {
                SeatManageComm.WriteLog.Write(string.Format("获取读者信息遇到错误：{0}", ex.Message));
                return new List<ClassModel.ReaderInfo>();
            }
        }

        private string GetConnStr(ClassModel.StuLibSyncSetting set)
        {
            string strConn = string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}", set.SouIP, set.SouDBName, set.SouUserName, set.SouPW);
            return strConn;
        }
        /// <summary>
        /// 获取读者信息
        /// </summary>
        /// <returns></returns>
        private DataSet GetReaderInfoDs()
        {
            SqlConnection conn = new SqlConnection(strConn);
            string cmdstr = "SELECT [CardId],[stucode],[Name],[Sex],[Type],[Dept],[Flag] FROM [dbo].[Reader]";
            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter adapt = new SqlDataAdapter(cmdstr, conn);
                adapt.Fill(ds);
                conn.Close();
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        private SeatManage.ClassModel.ReaderInfo DataRowToReaderInfo(DataRow dr)
        {
            SeatManage.ClassModel.ReaderInfo reader = new ClassModel.ReaderInfo();
            reader.CardID = dr["cardId"].ToString();
            reader.CardNo = dr["stucode"].ToString();
            reader.Name = dr["Name"].ToString();
            reader.ReaderType = ConvertDeptName_BSD(dr["Type"].ToString());
            reader.Sex = dr["Sex"].ToString();
            reader.Dept = dr["Dept"].ToString();
            return reader;
        }


        /// <summary>
        /// 新中新读者部门信息转换 （黑龙江大学）。
        /// </summary>
        /// <param name="typeCode"></param>
        /// <returns></returns>
        private string ConvertDeptName(string typeCode)
        {
            switch (typeCode)
            {
                case "01":
                    return "本科";
                case "02":
                    return "专科";
                case "03":
                    return "研究生";
                case "04":
                    return "博士生";
                case "05":
                    return "学生";
                case "06":
                    return "教师";
                case "07":
                    return "教工";
                case "08":
                    return "留学生";
                case "09":
                    return "在职硕士";
                case "10":
                    return "离退休教工";
                case "11":
                    return "聘任制";
                case "12":
                    return "工勤人员";
                case "13":
                    return "交换留学生";
                case "14":
                    return "自考生";
                case "15":
                    return "校外人员";
                case "16":
                    return "实业公司";
                case "17":
                    return "进修生";
            }
            return "";
        }
        /// <summary>
        /// 北师大的读者部门信息转换
        /// </summary>
        /// <returns></returns>
        private string ConvertDeptName_BSD(string typeCode)
        {
            switch (typeCode)
            {
                case "01":
                    return "本科生";
                case "02": return "博士";
                case "03": return "硕士";
                case "04": return "在职硕士";
                case "05": return "教工";
                case "09": return "水控管理员";
                case "10": return "各单位合同工";
                case "11": return "进修教师";
                case "13": return "校外人员";
                case "14": return "留学生";
                case "16": return "后勤集团职工";
                case "18": return "夜大生";
                case "19": return "集体工";
                case "20": return "备用1";
                case "21": return "备用2";
                case "22": return "进站博士后";
                case "23": return "校聘合同工";
                case "24": return "外籍教师";
                case "25": return "985关系未转";
                case "26": return "同等学力";
                case "27": return "奥运门禁";
                case "28": return "培训班";
                case "29": return "四合院上网充值卡";
                case "30": return "大科园职工";
                case "31": return "国外访问学者";
                default: return "";
            }
        }
        #endregion


        public ClassModel.ReaderInfo GetSourceReaderInfoByCardId(string cardId)
        {
            throw new NotImplementedException();
        }

        public ClassModel.ReaderInfo GetSourceReaderInfoByCardNo(string cardNo)
        {
            throw new NotImplementedException();
        }
    }
}
