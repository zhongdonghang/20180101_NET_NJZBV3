using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SeatManage.ClassModel;
namespace SeatManage
{
    public partial class Tip_IsBlacklist : UserControl
    {
        SeatClient.OperateResult.SystemObject clientobject = SeatClient.OperateResult.SystemObject.GetInstance(); 
        public Tip_IsBlacklist()
        {
            InitializeComponent();
        }

        private void Tip_IsBlacklist_Load(object sender, EventArgs e)
        {
            string roomNum = clientobject.EnterOutLogData.EnterOutlog.ReadingRoomNo;
            StringBuilder strMessage = new StringBuilder();
            //显示黑名单提示框，说明已经判断到黑名单中有记录，并且阅览室启用黑名单设置。
            foreach (BlackListInfo black in clientobject.EnterOutLogData.Student.BlacklistLog)
            {
                if (black.ReadingRoomID == roomNum)
                {
                    string blacklistOutTime = black.OutTime.ToShortDateString() + " " + black.OutTime.ToShortTimeString();
                    strMessage.Append(string.Format("    您在以下{0}存在黑名单记录,{1}失效。详情：{2}", black.ReadingRoomName, blacklistOutTime, black.ReMark));
                    break;
                }
            }
            if (string.IsNullOrEmpty(strMessage.ToString()))
            {
                strMessage.Append(string.Format("    您在黑名单中存在记录，被禁止选座。"));
            }

            lblMessage.Text = strMessage.ToString();
        }
    }
}
