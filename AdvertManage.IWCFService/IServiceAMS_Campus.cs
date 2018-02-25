using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace AdvertManage.IWCFService
{
    public partial interface IAdvertManageService
    {
        /// <summary>
        /// 根据Id获取校区信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        Model.AMS_CampusModel GetCampusInfoByID(int id);

        /// <summary>
        /// 根据编号获取校区信息
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        [OperationContract]
        Model.AMS_CampusModel GetCampusInfoByNum(string number);
        /// <summary>
        /// 根据学校编号获取校区信息
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
          [OperationContract]
        List<Model.AMS_CampusModel> GetCampusInfoListBySchoolNum(string number);
        /// <summary>
        /// 根据学校Id获取学校信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        List<Model.AMS_CampusModel> GetCampusInfoListBySchoolId(int id);

        /// <summary>
        /// 添加校区信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        Model.Enum.HandleResult AddCampus(Model.AMS_CampusModel model);
        /// <summary>
        /// 删除校区信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        Model.Enum.HandleResult DeleteCampus(int id);
        /// <summary>
        /// 更新校区信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        Model.Enum.HandleResult UpdateCampus(Model.AMS_CampusModel model);
    }
}
