using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.IO;

namespace SchoolNoteEditer.Code
{
    public class ToSQLScript
    {
        public static void ToSQLString(string savepath)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                ObservableCollection<ViewModel.ViewModel_School> School = Code.ReadingRoomEdit.GetSchools();
                foreach (ViewModel.ViewModel_School s in School)
                {
                    sb.AppendLine("INSERT INTO [T_SM_School]([SchoolNo],[SchoolName]) ");
                    sb.AppendFormat(" VALUES('{0}','{1}')", s.No, s.Name);
                    sb.AppendLine();
                }
                ObservableCollection<ViewModel.ViewModel_Library> Library = Code.ReadingRoomEdit.GetLibrarys();
                foreach (ViewModel.ViewModel_Library l in Library)
                {
                    sb.AppendLine("INSERT INTO [T_SM_Library]([LibraryNo],[LibraryName],[SchoolNo])");
                    sb.AppendFormat(" VALUES('{0}','{1}','{2}')", l.No, l.Name, l.LibraryModel.School.No);
                    sb.AppendLine();

                }
                ObservableCollection<ViewModel.ViewModel_ReadingRoom> Room = Code.ReadingRoomEdit.GetReadingRooms();
                foreach (ViewModel.ViewModel_ReadingRoom r in Room)
                {
                    sb.AppendLine("INSERT INTO [T_SM_ReadingRoom]([ReadingRoomNo],[ReadingRoomName],[LibraryNo],[ReadingSetting],[RoomSeat])");
                    sb.AppendFormat(" VALUES('{0}','{1}','{2}','{3}','{4}')", r.No, r.Name, r.ReadingRoomModel.Libaray.No, new SeatManage.ClassModel.ReadingRoomSetting().ToString(), r.ReadingRoomModel.SeatList.ToString());
                    sb.AppendLine();
                }
                if (!File.Exists(savepath))
                {
                    FileStream fs = File.Create(savepath);
                    fs.Close();
                }
                StreamWriter sw = new StreamWriter(savepath, false, Encoding.GetEncoding("GB2312"));
                sw.Write(sb.ToString());
                sw.Flush();
                sw.Close();
            }
            catch
            {
                throw;
            }
        }
    }
}
