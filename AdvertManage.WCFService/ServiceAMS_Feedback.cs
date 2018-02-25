using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdvertManage.DAL;
using AdvertManage.IWCFService;

namespace AdvertManage.WCFService
{

    public partial class AdvertManageService : IAdvertManageService
    {
        AMS_FeedbackDal dal = new AMS_FeedbackDal();
        /// <summary>
        /// 添加反馈意见
        /// </summary>
        /// <param name="model"> 反馈意见</param>
        /// <returns></returns> 
        public Model.Enum.HandleResult AddFeedback(Model.AMS_FeedbackModel model)
        {
            try
            {
                if (dal.AddFeedback(model) > 0)
                {
                    return Model.Enum.HandleResult.Successed;
                }
                else
                {
                    return Model.Enum.HandleResult.Failed;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 将抛出异常信息存入数据库
        /// </summary>
        /// <param name="error"></param>
        public void AddErrorMessage(string error)
        {
            try
            {
                dal.AddErrorMessage(error);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 网站访问量
        /// </summary>
        /// <param name="error"></param>
        public void SetPageView(string id)
        {
            try
            {
                dal.SetPageView(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
