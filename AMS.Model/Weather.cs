using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMS.Model
{
    public class Weather
    {
        private int id;
        /// <summary>
        /// 自动编号
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string _WeatherName;
        /// <summary>
        /// 地区城市
        /// </summary>
        public string WeatherName
        {
            get { return _WeatherName; }
            set { _WeatherName = value; }
        }

        private string _WeatherNo;
        /// <summary>
        /// 地区编码
        /// </summary>
        public string WeatherNo
        {
            get { return _WeatherNo; }
            set { _WeatherNo = value; }
        }

    }
}
