/*************************************
 * 作者：王昊天
 * 创建日期：2013-6-3
 * 说明：功能菜单类
 * 修改人：
 * 修改时间：
 * ************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.ClassModel
{
    [Serializable]
    public class SysMenuInfo
    {
        private int _MenuID = 0;
        /// <summary>
        /// 菜单编号
        /// </summary>
        public int MenuID
        {
            get { return _MenuID; }
            set { _MenuID = value; }
        }

        private string _MainNum = "";
        /// <summary>
        /// 一级菜单编号
        /// </summary>
        public string MainNum
        {
            get { return _MainNum; }
            set { _MainNum = value; }
        }
        private string _FuncPageNum = "";
        /// <summary>
        /// 功能页面编号
        /// </summary>
        public string FuncPageNum
        {
            get { return _FuncPageNum; }
            set { _FuncPageNum = value; }
        }
        private List<SysMenuInfo> _ChildMenu = new List<SysMenuInfo>();
        /// <summary>
        /// 子菜单，二级菜单没有子菜单
        /// </summary>
        public List<SysMenuInfo> ChildMenu
        {
            get { return _ChildMenu; }
            set { _ChildMenu = value; }
        }

        private int _Index = 0;
        /// <summary>
        /// 排列序号
        /// </summary>
        public int Index
        {
            get { return _Index; }
            set { _Index = value; }
        }

        private string _MenuName;
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName
        {
            get { return _MenuName; }
            set { _MenuName = value; }
        }

        private string _ImageUrl="";
        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImageUrl
        {
            get { return _ImageUrl; }
            set { _ImageUrl = value; }
        }

        private int _MenuLv = 0;
        /// <summary>
        /// 菜单等级一级或二级菜单
        /// </summary>
        public int MenuLv
        {
            get { return _MenuLv; }
            set { _MenuLv = value; }
        }
        private string _MenuLink;
        /// <summary>
        /// 功能页面连接地址
        /// </summary>
        public string MenuLink
        {
            get { return _MenuLink; }
            set { _MenuLink = value; }
        }

    }
}
