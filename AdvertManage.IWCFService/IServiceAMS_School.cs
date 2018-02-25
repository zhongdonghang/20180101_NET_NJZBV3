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
        /// 根据Id获取学校信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        Model.AMS_SchoolModel GetSchoolInfoByID(int id);
        /// <summary>
        /// 根据学校编号获取学校信息
        /// </summary>
        /// <param name="number">学校编号</param>
        /// <returns></returns>
        [OperationContract]
        Model.AMS_SchoolModel GetSchoolInfoByNum(string number);
        /// <summary>
        /// 获取所有的学校信息
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<Model.AMS_SchoolModel> GetAllSchoolInfo();

        /// <summary>
        /// 根据Id删除学校信息
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        Model.Enum.HandleResult DeleteSchoolInfo(int Id);
        /// <summary>
        /// 添加学校信息
        /// </summary>
        /// <param name="model"> 学校信息</param>
        /// <returns></returns>
        [OperationContract]
        Model.Enum.HandleResult AddSchoolInfo(Model.AMS_SchoolModel model);

        /// <summary>
        /// 更新学校信息
        /// </summary>
        /// <param name="model">学校 </param>
        /// <returns></returns>
        [OperationContract]
        Model.Enum.HandleResult UpdateSchoolInfo(Model.AMS_SchoolModel model);


    }
}
