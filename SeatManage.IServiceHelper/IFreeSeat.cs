using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.ServiceHelper
{
    public interface IFreeSeat
    {
        string FreeSeat(string cardNo); 
    }
}
