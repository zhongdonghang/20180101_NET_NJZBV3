using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdvertManage.Model
{
    /// <summary>
    /// 学校Model
    /// </summary>
    [Serializable]
   public class AMS_SchoolModel
    {
       public int Id
       {
           get;
           set;
       }
       public string Number
       {
           get;
           set;
       }
       public string Name
       {
           get;
           set;
       }
       public string DTUip
       {
           get;
           set;

       }
       public string Describe
       {
           get;
           set;
       }
       public string ConnectionString
       {
           get;
           set;
       }
       public int Flag
       {
           get;
           set;
       }
    }
}
