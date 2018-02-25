using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SeatManage.SeatClient.Config.Code
{
    public class batSQLFiles
    {
        private string _DBIP = "";
        private string _DBName = "";
        private string _DBUser = "";
        private string _DBPW = "";
        private string _SavePath = "";
        public batSQLFiles(string dbip, string dbname, string dbuser, string dbpw,string path)
        {
            _DBIP = dbip; 
            _DBName = dbname;
            _DBPW = dbpw;
            _DBUser = dbuser;
            _SavePath = path;
        }
        public string run()
        {
            try
            {
                string fileDircetoryPath = _SavePath;
                StringBuilder str_bat = new StringBuilder();
                str_bat.AppendFormat("osql -S{0} -U{1} -P{2}  -i {3} \n", _DBIP, _DBUser, _DBPW, fileDircetoryPath + "SQL批处理脚本\\进出记录备份.sql");
                str_bat.AppendFormat("osql -S{0} -U{1} -P{2}  -i {3} \n", _DBIP, _DBUser, _DBPW, fileDircetoryPath + "SQL批处理脚本\\收缩日志文件.sql");
                if (!Directory.Exists(fileDircetoryPath + "\\SQL批处理脚本\\"))
                {
                    Directory.CreateDirectory(fileDircetoryPath + "SQL批处理脚本\\");
                }
                string path = fileDircetoryPath + "SQL批处理脚本\\EveryDayBackUp.bat";
                if (!File.Exists(path))
                {
                    FileStream fs = File.Create(path);
                    fs.Close();
                }
                StreamWriter sw = new StreamWriter(path, false, Encoding.GetEncoding("GB2312"));
                sw.Write(str_bat);
                sw.Flush();
                sw.Close();
                //生成数据库脚本
                StringBuilder str_bak_sql = new StringBuilder();
                str_bak_sql.AppendFormat("--找到备份表中最大的id\n");
                str_bak_sql.AppendFormat("declare @id int\n");
                str_bak_sql.AppendFormat("set @id=0\n");
                str_bak_sql.AppendFormat("select @id=max([{0}].[dbo].T_SM_EnterOutLog_bak.EnterOutLogID)\n", _DBName);
                str_bak_sql.AppendFormat("from [{0}].[dbo].T_SM_EnterOutLog_bak\n", _DBName);
                str_bak_sql.AppendFormat("if @id is null\n");
                str_bak_sql.AppendFormat("begin\n");
                str_bak_sql.AppendFormat("set @id=0\n");
                str_bak_sql.AppendFormat("end\n");
                str_bak_sql.AppendFormat("select @id\n");
                str_bak_sql.AppendFormat("--根据最大的ID插入数据\n");
                str_bak_sql.AppendFormat("insert [{0}].[dbo].T_SM_EnterOutLog_bak\n", _DBName);
                str_bak_sql.AppendFormat("select * \n");
                str_bak_sql.AppendFormat("from [{0}].[dbo].T_SM_EnterOutLog\n", _DBName);
                str_bak_sql.AppendFormat("where [{0}].[dbo].T_SM_EnterOutLog.EnterOutLogID>@id\n", _DBName);
                str_bak_sql.AppendFormat("--删除30天前的记录\n");
                str_bak_sql.AppendFormat("delete [{0}].[dbo].T_SM_EnterOutLog \n", _DBName);
                str_bak_sql.AppendFormat("where datediff(day,  [{0}].[dbo].T_SM_EnterOutLog.EnterOutTime, GetDate())>30\n", _DBName);
                path = fileDircetoryPath + "SQL批处理脚本\\进出记录备份.sql";
                if (!File.Exists(path))
                {
                    FileStream fs = File.Create(path);
                    fs.Close();
                }
                sw = new StreamWriter(path, false, Encoding.GetEncoding("GB2312"));
                sw.Write(str_bak_sql);
                sw.Flush();
                sw.Close();

                //收缩数据库
                StringBuilder str_log_sql = new StringBuilder();
                str_log_sql.AppendFormat("USE {0};\n", _DBName);
                str_log_sql.AppendFormat("GO\n");
                str_log_sql.AppendFormat("ALTER DATABASE {0}\n", _DBName);
                str_log_sql.AppendFormat("SET RECOVERY SIMPLE;\n");
                str_log_sql.AppendFormat("GO\n");
                str_log_sql.AppendFormat("DBCC SHRINKFILE (N'{0}_log' , 1)\n", _DBName);
                str_log_sql.AppendFormat("GO\n");
                str_log_sql.AppendFormat("ALTER DATABASE {0}\n", _DBName);
                str_log_sql.AppendFormat("SET RECOVERY FULL;\n");
                str_log_sql.AppendFormat("GO\n");
                path = fileDircetoryPath + "SQL批处理脚本\\收缩日志文件.sql";
                if (!File.Exists(path))
                {
                    FileStream fs = File.Create(path);
                    fs.Close();
                }
                sw = new StreamWriter(path, false, Encoding.GetEncoding("GB2312"));
                sw.Write(str_log_sql);
                sw.Flush();
                sw.Close();
                return "创建成功！";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

    }
}
