using System;
namespace SeatManage.ClassModel
{
    /// <summary>
    /// T_SM_StudyRoom:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class StudyRoomInfo
    {
        public StudyRoomInfo()
        { }
        #region Model
        private string _studyroomno;
        private string _studyroomname;
        private StudyRoomSetting _setting;
        private string _renark;
        private string _RoomImage;
        /// <summary>
        /// 研习间图片
        /// </summary>
        public string RoomImage
        {
            get { return _RoomImage; }
            set { _RoomImage = value; }
        }
        /// <summary>
        /// 研习间编号
        /// </summary>
        public string StudyRoomNo
        {
            set { _studyroomno = value; }
            get { return _studyroomno; }
        }
        /// <summary>
        /// 研习间名称
        /// </summary>
        public string StudyRoomName
        {
            set { _studyroomname = value; }
            get { return _studyroomname; }
        }
        /// <summary>
        /// 研习间设置
        /// </summary>
        public StudyRoomSetting Setting
        {
            set { _setting = value; }
            get { return _setting; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _renark = value; }
            get { return _renark; }
        }

        #endregion Model

    }
}

