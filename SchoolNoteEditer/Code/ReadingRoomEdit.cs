using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace SchoolNoteEditer.Code
{
    public class ReadingRoomEdit
    {
        /// <summary>
        /// 获取学校列表
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<ViewModel.ViewModel_School> GetSchools()
        {
            try
            {
                ObservableCollection<ViewModel.ViewModel_School> viewModelList = new ObservableCollection<ViewModel.ViewModel_School>();
                List<SeatManage.ClassModel.School> modelList = SeatManage.Bll.T_SM_School.GetSchoolInfoList(null, null);
                foreach (SeatManage.ClassModel.School model in modelList)
                {
                    ViewModel.ViewModel_School viewModel = new ViewModel.ViewModel_School();
                    viewModel.SchoolModel = model;
                    viewModelList.Add(viewModel);
                }
                return viewModelList;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 获取图书馆列表
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<ViewModel.ViewModel_Library> GetLibrarys()
        {
            try
            {
                ObservableCollection<ViewModel.ViewModel_Library> viewModelList = new ObservableCollection<ViewModel.ViewModel_Library>();
                List<SeatManage.ClassModel.LibraryInfo> modelList = SeatManage.Bll.T_SM_Library.GetLibraryInfoList(null, null, null);
                foreach (SeatManage.ClassModel.LibraryInfo model in modelList)
                {
                    ViewModel.ViewModel_Library viewModel = new ViewModel.ViewModel_Library();
                    viewModel.LibraryModel = model;
                    viewModelList.Add(viewModel);
                }
                return viewModelList;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 获取阅览室列表
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<ViewModel.ViewModel_ReadingRoom> GetReadingRooms()
        {
            try
            {
                ObservableCollection<ViewModel.ViewModel_ReadingRoom> viewModelList = new ObservableCollection<ViewModel.ViewModel_ReadingRoom>();
                List<SeatManage.ClassModel.ReadingRoomInfo> modelList = SeatManage.Bll.T_SM_ReadingRoom.GetReadingRooms(null,null,null);
                foreach (SeatManage.ClassModel.ReadingRoomInfo model in modelList)
                {
                    ViewModel.ViewModel_ReadingRoom viewModel = new ViewModel.ViewModel_ReadingRoom();
                    viewModel.ReadingRoomModel = model;
                    viewModelList.Add(viewModel);
                }
                return viewModelList;
            }
            catch
            {
                throw;
            }
        }
    }
}
