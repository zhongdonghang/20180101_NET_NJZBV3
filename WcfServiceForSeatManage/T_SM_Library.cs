using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.IWCFService;
using SeatManage.DAL;
using System.Data;
using SeatManage.ClassModel;
using SeatManage.EnumType;

namespace WcfServiceForSeatManage
{
    public partial class SeatManageDateService : ISeatManageService
    {
        SeatManage.DAL.T_SM_Library t_sm_library_dal = new T_SM_Library();
        /// <summary>
        /// 获取图书馆列表
        /// </summary>
        /// <param name="Schoolno"></param>
        /// <param name="no"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<SeatManage.ClassModel.LibraryInfo> GetLibraryList(string Schoolno, string no, string name)
        {
            StringBuilder strWhere = new StringBuilder();
            if (!string.IsNullOrEmpty(Schoolno))
            {
                strWhere.Append(" SchoolNo='" + Schoolno + "'");
            }
            if (!string.IsNullOrEmpty(no))
            {
                if (string.IsNullOrEmpty(strWhere.ToString()))
                {
                    strWhere.Append(" LibraryNo='" + no + "'");
                }
                else
                {
                    strWhere.Append(" and LibraryNo='" + no + "'");
                }
            }
            if (!string.IsNullOrEmpty(name))
            {
                if (string.IsNullOrEmpty(strWhere.ToString()))
                {
                    strWhere.Append(" LibraryName='" + name + "'");
                }
                else
                {
                    strWhere.Append(" and LibraryName='" + name + "'");
                }
            }
            try
            {
                DataSet ds = t_sm_library_dal.GetList(strWhere.ToString(), null);
                List<LibraryInfo> list = new List<LibraryInfo>();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        LibraryInfo lib = new LibraryInfo();
                        lib = DataRowToLibraryInfo(dr);
                        List<School> scl = GetSchoolList(lib.School.No, null);
                        if (scl.Count > 0)
                        {
                            lib.School = scl[0];
                        }
                        list.Add(lib);
                    }
                }
                return list;
            }
            catch
            {
                throw;
            }

        }
        /// <summary>
        /// 新增图书馆
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddNewLibrary(SeatManage.ClassModel.LibraryInfo model)
        {
            try
            {
                return t_sm_library_dal.Add(model);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 新修改图书馆信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateLibrary(SeatManage.ClassModel.LibraryInfo model)
        {
            try
            {
                return t_sm_library_dal.Update(model);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 删除图书馆
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool DeleteLibrary(SeatManage.ClassModel.LibraryInfo model)
        {
            try
            {
                return t_sm_library_dal.Delete(model);
            }
            catch
            {
                throw;
            }
        }

        private LibraryInfo DataRowToLibraryInfo(DataRow dr)
        {
            LibraryInfo library = new LibraryInfo();
            library.No = dr["LibraryNo"].ToString();
            library.Name = dr["LibraryName"].ToString();
            library.School.No = dr["SchoolNo"].ToString();
            library.AreaList = library.ToList(dr["AreaInfo"].ToString());
            return library;
        }
    }
}
