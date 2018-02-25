using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdvertManage.Model;
using AdvertManage.IWCFService;
using AdvertManage.DAL;
using AdvertManage.Model.Enum;

namespace AdvertManage.WCFService
{
    public partial class AdvertManageService : IAdvertManageService
    {
        AMS_EnterOutLogDal enterOutLogDal = new AMS_EnterOutLogDal();
        /// <summary>
        /// 添加进出记录列表
        /// </summary>
        /// <param name="logList"></param>
        /// <returns></returns>
        public HandleResult AddEnterOutLogList(List<AMS_EnterOutLog> logList)
        {
            try
            {
                Model.AMS_SchoolModel schoolModel = null;
                foreach (AMS_EnterOutLog model in logList)
                {
                    if (schoolModel == null)
                    {
                      schoolModel =  GetSchoolInfoByNum(model.SchoolNum);
                    }
                    model.SchoolId = schoolModel.Id;
                    enterOutLogDal.Add(model);
                }
                return HandleResult.Successed;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

       
    }
}
