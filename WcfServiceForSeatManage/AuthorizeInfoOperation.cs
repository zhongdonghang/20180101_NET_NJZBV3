using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.IWCFService;
using SeatManage.ClassModel;
using SeatManage.DAL;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace WcfServiceForSeatManage
{
    public partial class SeatManageDateService : ISeatManageService
    {

        /// <summary>
        /// 获取本地权限
        /// </summary>
        /// <returns></returns>
        public AuthorizeVerify.FunctionAuthorizeInfo GetFunctionAuthorize()
        {
            string filePath = string.Format(@"{0}sf_authorized_keys", AppDomain.CurrentDomain.BaseDirectory);
            AuthorizeVerify.FunctionAuthorizeInfo model = null;
            if (File.Exists(filePath))
            {
                try
                {
                    model = AuthorizeVerify.FunctionAuthorizeInfo.AnalyzeAuthorize(filePath);
                }
                catch
                {
                    throw;
                }
            }
            return model;
        }
        /// <summary>
        /// 获取指定位置权限文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public AuthorizeVerify.FunctionAuthorizeInfo GetFunctionAuthorizeFile(string filePath)
        {
            AuthorizeVerify.FunctionAuthorizeInfo model = new AuthorizeVerify.FunctionAuthorizeInfo();
            if (File.Exists(filePath))
            {
                try
                {
                    model = AuthorizeVerify.FunctionAuthorizeInfo.AnalyzeAuthorize(filePath);
                }
                catch
                {
                    throw;
                }

            }
            return model;
        }
        /// <summary>
        /// 保存授权文件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool SaveFunctionAuthorize(AuthorizeVerify.FunctionAuthorizeInfo model)
        {
            if (model == null)
            {
                return false;
            }
            string filePath = string.Format(@"{0}sf_authorized_keys", AppDomain.CurrentDomain.BaseDirectory);
            StreamWriter file = new StreamWriter(filePath, false, Encoding.ASCII);
            try
            {
                string strJson = SeatManage.SeatManageComm.JSONSerializer.Serialize(model);
                string ciphertext = SeatManage.SeatManageComm.AESAlgorithm.AESEncrypt(strJson);
                file.WriteLine(ciphertext);
                return true;
            }
            catch
            {
                throw;
            }
            finally
            {
                file.Flush();
            }
        }
        /// <summary>
        /// 判断功能是否被授权
        /// </summary>
        /// <param name="functionName"></param>
        /// <returns></returns>
        public bool IsAuthorize(string functionName)
        {
            try
            {
                AuthorizeVerify.FunctionAuthorizeInfo model = GetFunctionAuthorize();
                string schoolNum = GetSchoolNum();
                if (string.IsNullOrEmpty(schoolNum))
                {
                    return false;
                }
                if (schoolNum != model.SchoolNum)
                {
                    return false;
                }
                if (model.SystemFunction.Contains(functionName))
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
    }
}
