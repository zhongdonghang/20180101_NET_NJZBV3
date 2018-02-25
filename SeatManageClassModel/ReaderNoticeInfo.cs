using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.EnumType;

namespace SeatManage.ClassModel
{
    /// <summary>
    /// 通知读者的消息
    /// </summary>
      [Serializable]
    public class ReaderNoticeInfo
    {
          private int _NoticeID=-1;
          /// <summary>
          /// 消息ID
          /// </summary>
          public int NoticeID
          {
              get { return _NoticeID; }
              set { _NoticeID = value; }
          }
          private DateTime _AddTime = DateTime.Parse("1900-1-1");

          public DateTime AddTime
          {
              get { return _AddTime; }
              set { _AddTime = value; }
          }

          private string _CardNo="";
          /// <summary>
          /// 读者学号
          /// </summary>
          public string CardNo
          {
              get { return _CardNo; }
              set { _CardNo = value; }
          }

         
          private string _Note = "";
          /// <summary>
          /// 消息内容
          /// </summary>
          public string Note
          {
              get { return _Note; }
              set { _Note = value; }
          }

          NoticeType _Type = NoticeType.None;
          /// <summary>
          /// 消息类型
          /// </summary>
          public NoticeType Type
          {
              get { return _Type; }
              set { _Type = value; }
          }

          private LogStatus _IsRead = LogStatus.Valid;
          /// <summary>
          /// 标示是否已经阅读过
          /// </summary>
          public LogStatus IsRead
          {
              get { return _IsRead; }
              set { _IsRead = value; }
          }
    }
}
