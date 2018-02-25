using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AuthorizeVerify;
using SeatManage.IWCFService;

namespace WcfServiceForSeatManage
{
    public partial class SeatManageDateService : ISeatManageService
    {

        /// <summary>
        /// 获取授权文件
        /// </summary>
        /// <returns></returns>
        public SeatManage.ClassModel.SystemAuthorization GetSystemAuthorization()
        {
            string filePath = string.Format(@"{0}SystemAuthorizedkeys", AppDomain.CurrentDomain.BaseDirectory);
            SeatManage.ClassModel.SystemAuthorization model = null;
            string encryptionContext = "";
            if (File.Exists(filePath))
            {
                encryptionContext = File.ReadAllText(filePath, Encoding.ASCII);
            }
            else
            {
                throw new Exception("未找到授权文件。");
            }
            try
            {
                model = SeatManage.SeatManageComm.JSONSerializer.Deserialize<SeatManage.ClassModel.SystemAuthorization>(SeatManage.SeatManageComm.AESAlgorithm.AESDecrypt(encryptionContext));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return model;
        }
        /// <summary>
        /// 保存授权文件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool SaveSystemAuthorization(SeatManage.ClassModel.SystemAuthorization model)
        {
            if (model == null)
            {
                return false;
            }
            string filePath = string.Format(@"{0}SystemAuthorizedkeys", AppDomain.CurrentDomain.BaseDirectory);
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
    }
}
