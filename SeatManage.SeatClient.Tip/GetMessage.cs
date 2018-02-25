using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.EnumType;
using SeatClient.OperateResult;
using System.Windows.Forms;
using SeatManage.MyUserControl;
namespace SeatManage.SeatClient.Tip
{
    public class GetMessage
    {
        /// <summary>
        /// 选座频繁提示
        /// </summary>
        public static void SelectSeatFrequent(Form form)
        {
            Tip_SelectSeatFrequent uc = new Tip_SelectSeatFrequent();
            form.Controls.Add(uc);
        }
        /// <summary>
        /// 选座操作结果提示
        /// </summary>
        /// <param name="form"></param>
        /// <param name="result"></param>
        public static void SelectSeatResult(Form form, HandleResult result,TipType type)
        {
            switch (result)
            {
                case HandleResult.Failed:
                    Tip_Failed FaliedUc = new Tip_Failed(type);
                    break;
                case HandleResult.Successed:
                    Tip_SelectSeatResult uc = new Tip_SelectSeatResult(type);
                    form.Controls.Add(uc);
                    break;
            }

        }
        /// <summary>
        /// 黑名单限制提示
        /// </summary>
        /// <param name="form"></param>
        public static void IsBlacklist(Form form)
        {
            Tip_IsBlacklist uc = new Tip_IsBlacklist();
            form.Controls.Add(uc);
        }
        /// <summary>
        /// 暂离提示
        /// </summary>
        /// <param name="form"></param>
        public static void ShortLeavtTip(Form form)
        {
            Tip_ShortLeave uc = new Tip_ShortLeave();
            form.Controls.Add(uc);
        }
        /// <summary>
        /// 离开提示
        /// </summary>
        /// <param name="form"></param>
        public static void LeaveTip(Form form)
        {
            Tip_Leave uc = new Tip_Leave();
            form.Controls.Add(uc);
        }


        /// <summary>
        /// 读者暂离回来
        /// </summary>
        /// <param name="form"></param>
        public static void ComeToBack(Form form)
        {
            Tip_ComeBack uc = new Tip_ComeBack();
            form.Controls.Add(uc);
        }
        /// <summary>
        /// 座位锁定中
        /// </summary>
        /// <param name="form"></param>
        public static void SeatLocking(Form form)
        {
            Tip_SeatIsLocking uc = new Tip_SeatIsLocking();
            form.Controls.Add(uc);
        }
        /// <summary>
        /// 座位不存在
        /// </summary>
        /// <param name="form"></param>
        public static void SeatNotExists(Form form)
        {
            Tip_SeatNotExists uc = new Tip_SeatNotExists();
            form.Controls.Add(uc);
        }
        /// <summary>
        /// 座位正在被使用的提示
        /// </summary>
        /// <param name="form"></param>
        public static void SeatUsing(Form form,TipType type)
        {
            Tip_Failed uc = new Tip_Failed(type);
            form.Controls.Add(uc);
        }
        /// <summary>
        /// 阅览室当前状态为关闭
        /// </summary>
        /// <param name="form"></param>
        public static void RoomClosing(Form form)
        {
            Tip_ReadingRoomClosing uc = new Tip_ReadingRoomClosing();
            form.Controls.Add(uc);
        }
        /// <summary>
        /// 等待成功提示
        /// </summary>
        /// <param name="form"></param>
        public static void WaitSeatSuccess(Form form)
        {
            Tip_WaitSeat uc = new Tip_WaitSeat();
            form.Controls.Add(uc);
        }
        /// <summary>
        /// 等待座位操作频繁提示。
        /// </summary>
        /// <param name="form"></param>
        /// <param name="type"></param>
        public static void WaitSeatFrequent(Form form, TipType type)
        {
            Tip_SimpleTip uc = new Tip_SimpleTip(type);
            form.Controls.Add(uc); 
        }
    }
}
