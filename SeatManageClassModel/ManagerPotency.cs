/************************************************
 * 作者：王昊天
 * 创建时间：2013-6-4
 * 说明：用户授权类
 * 修改人：
 * 修改时间：
 * *********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.ClassModel
{
    [Serializable]
    public class ManagerPotency
    {
        private string _LoginID = "";
        /// <summary>
        /// 用户登录ID
        /// </summary>
        public string LoginID
        {
            get { return _LoginID; }
            set { _LoginID = value; }
        }

        private string _UserName = "";
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        private List<ReadingRoomInfo> _RightRoomList = new List<ReadingRoomInfo>();
        /// <summary>
        /// 用户可以管理的阅览室编号
        /// </summary>
        public List<ReadingRoomInfo> RightRoomList
        {
            get { return _RightRoomList; }
            set { _RightRoomList = value; }
        }
    }
}
