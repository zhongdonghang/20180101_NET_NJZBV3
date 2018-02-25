using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.ISystemTerminal.ILoginValidate
{
    public interface ILoginValidate
    {
        string CheckUser(string cardNo, string password);
    }
}
