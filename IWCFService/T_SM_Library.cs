using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using SeatManage.EnumType;

namespace SeatManage.IWCFService
{
    public partial interface ISeatManageService : IExceptionService
    {
        /// <summary>
        /// 获取图书馆列表
        /// </summary>
        /// <param name="Schoolno"></param>
        /// <param name="no"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [OperationContract]
        List<SeatManage.ClassModel.LibraryInfo> GetLibraryList(string Schoolno, string no, string name);
        /// <summary>
        /// 新增图书馆
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddNewLibrary(SeatManage.ClassModel.LibraryInfo model);
        /// <summary>
        /// 新修改图书馆信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateLibrary(SeatManage.ClassModel.LibraryInfo model);
        /// <summary>
        /// 删除图书馆
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteLibrary(SeatManage.ClassModel.LibraryInfo model);
    }
}
