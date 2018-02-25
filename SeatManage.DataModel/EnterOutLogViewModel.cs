using System;

namespace SeatManage.DataModel
{
      [Serializable]
   public class EnterOutLogViewModel:EnterOutLogInfo
   {
        ReadingRoomInfo _ReadingRoom = new ReadingRoomInfo();

        public ReadingRoomInfo ReadingRoom
        {
            get { return _ReadingRoom; }
            set { _ReadingRoom = value; }
        }

        ReaderInfo _Reader = new ReaderInfo();

        public ReaderInfo Reader
        {
            get { return _Reader; }
            set { _Reader = value; }
        }

    }
}
