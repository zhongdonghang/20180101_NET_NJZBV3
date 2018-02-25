using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeatManageWebV2.Code
{
    public class BespeakEnumKey_Value
    {
        private string _BespeakState;

        public string BespeakState
        {
            get { return _BespeakState; }
            set { _BespeakState = value; }
        }
        private string _Value;

        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }
    }
}