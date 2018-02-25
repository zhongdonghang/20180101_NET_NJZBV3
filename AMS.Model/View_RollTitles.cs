using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMS.Model
{
    [Serializable]
    public class View_RollTitles
    {
        public View_RollTitles() { }

        private string _Name;
        /// <summary>
        /// 滚动名称
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private DateTime _EffectDate;
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime EffectDate
        {
            get { return _EffectDate; }
            set { _EffectDate = value; }
        }
        private DateTime _EndDate;
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }
        private string _Type;
        /// <summary>
        /// 内容
        /// </summary>
        public string Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        private int _CustomerId;
        /// <summary>
        /// 客户ID
        /// </summary>
        public int CustomerId
        {
            get { return _CustomerId; }
            set { _CustomerId = value; }
        }
        private string _CompanyName;
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CompanyName
        {
            get { return _CompanyName; }
            set { _CompanyName = value; }
        }
        private string _CustomerNo;
        /// <summary>
        /// 
        /// </summary>
        public string CustomerNo
        {
            get { return _CustomerNo; }
            set { _CustomerNo = value; }
        }
        private string _LinkWay;
        /// <summary>
        /// 
        /// </summary>
        public string LinkWay
        {
            get { return _LinkWay; }
            set { _LinkWay = value; }
        }
        private string _Describe;
        /// <summary>
        /// 客户备注
        /// </summary>
        public string Describe
        {
            get { return _Describe; }
            set { _Describe = value; }
        }
        private string _LoginId;
        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginId
        {
            get { return _LoginId; }
            set { _LoginId = value; }
        }
        private string _UserPwd;
        /// <summary>
        /// 密码
        /// </summary>
        public string UserPwd
        {
            get { return _UserPwd; }
            set { _UserPwd = value; }
        }
        private string _BranchName;
        /// <summary>
        /// 部门
        /// </summary>
        public string BranchName
        {
            get { return _BranchName; }
            set { _BranchName = value; }
        }
        private string _UserName;
        /// <summary>
        /// 姓名
        /// </summary>
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        private string _Remark;
        /// <summary>
        /// 编辑人备注
        /// </summary>
        public string Remark
        {
            get { return _Remark; }
            set { _Remark = value; }
        }

        private int _Oprator;
        /// <summary>
        /// 编辑人ID
        /// </summary>
        public int Oprator
        {
            get { return _Oprator; }
            set { _Oprator = value; }
        }
        private int _ID;
        /// <summary>
        /// 滚动ID
        /// </summary>
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

    }
}
