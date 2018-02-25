using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBUtility;

namespace AMS.DAL
{
    public  class WeiXin_Admin
    {
        public bool AdminLogin(string UID, string PWD, int type)
        {
            string sql = string.Format("select * from WeiXin_Admin where LoginID='{0}',PWD='{1}',Type={2}", UID, PWD, type);
            if (DbHelperSQL.Exists("sql") != null)
            {
                return true;
            }
            return false;
        }
        public bool AddAdmin(string UID,string PWD,int Type)
        {
            string sql = string.Format("insert into WeiXin_Admin (LoginID,PWD,Type) values('{0}','{1}',{3})", UID, PWD, Type);
            if (DbHelperSQL.ExecuteSql(sql) > 0)
            {
                return true;
            }
            return false;
        }
        public bool DeleteAdmin(string UID)
        {
            string sql = string.Format("delete from WeiXin_Admin where LoginID='{0}'", UID);
            if (DbHelperSQL.ExecuteSql(sql) > 0)
            {
                return true;
            }
            return false;
        }
        public bool UpdateAdmin(string UID, string PWD, int type)
        {
            string sql = string.Format("update WeiXin_Admin set PWD='{0}',type={1} where LoginID='{2}'", PWD, type, UID);
            if (DbHelperSQL.ExecuteSql(sql) > 0)
            {
                return true;
            }
            return false;
        }
    }
}
