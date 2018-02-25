using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using System.Web;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;

namespace SeatManageWebV2.Code
{
    public class DataToExcel:BasePage
    {

        #region 转成csv文件
        public void DataGridViewToExcel(Dictionary<string, DataTable> dataTables)
        {
            try
            {
                string csvStr = ConverDataSet2CSV(dataTables);
                if (csvStr == "") return;
                //DateTime.Now.ToString("yyyy-MM-dd HH-ss-mm");
                string fileName = DateTime.Now.ToString("yyyy-MM-dd HH-ss-mm")+".xls";
                string path = Server.MapPath("/FileDownLoad/") + fileName ;
                FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
                //将string转换成byte[]
                byte[] csvArray = System.Text.Encoding.Default.GetBytes(csvStr.ToCharArray(), 0, csvStr.Length - 1);
                fs.Write(csvArray, 0, csvStr.Length - 1);
                fs.Close();
                fs = null;      
                GC.Collect();         

                //以字符流的形式下载文件
                FileStream fs1 = new FileStream(path, FileMode.Open);
                byte[] bytes = new byte[(int)fs1.Length];
                fs1.Read(bytes, 0, bytes.Length);
                fs1.Close();
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;  filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
                HttpContext.Current.Response.BinaryWrite(bytes);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(ex.ToString());
               // throw;
            }
        }

        /// <summary>
        /// 将指定的数据集中指定的表转换成CSV字符串
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private string ConverDataSet2CSV(Dictionary<string, DataTable> dataTables)
        {
            string csvStr = "";


         
            foreach (var item in dataTables)
            {
                csvStr += item.Key;
                csvStr += "\n";
                foreach (DataColumn column in item.Value.Columns)
                {
                    csvStr +=  column.ColumnName + "\t" ;
                }
                //去掉最后一个","
               // csvStr = csvStr.Remove(csvStr.LastIndexOf(","), 1);
                csvStr += "\n";

                DataTable tb = item.Value;
                foreach (DataRow row in tb.Rows)
                {
                    foreach (DataColumn column in tb.Columns)
                    {
                        csvStr +=  row[column].ToString() + "\t" ;
                    }
                 //   csvStr = csvStr.Remove(csvStr.LastIndexOf(","), 1);
                    csvStr += "\n";
                }
            }

            //下面写出数据
           // DataTable tb = ds.Tables[tableName];
            //写表名
            //csvStr += tb.TableName + "\n";
            //第一步：写出列名
            //if (containColumName)
            //{
            //    foreach (DataColumn column in tb.Columns)
            //    {
            //        csvStr += "\"" + column.ColumnName + "\"" + ",";
            //    }
            //    //去掉最后一个","
            //    csvStr = csvStr.Remove(csvStr.LastIndexOf(","), 1);
            //    csvStr += "\n";
            //}
            //第二步：写出数据
            //foreach (DataRow row in tb.Rows)
            //{
            //    foreach (DataColumn column in tb.Columns)
            //    {
            //        csvStr += "\"" + row[column].ToString() + "\"" + ",";
            //    }
            //    csvStr = csvStr.Remove(csvStr.LastIndexOf(","), 1);
            //    csvStr += "\n";
            //}
            return csvStr;
        } 
        #endregion




        //public void DataGridViewToExcel(Dictionary<string, DataTable> dataTables)
        //{
        //    string path = "";
        //    Application objExcel = null;
        //    Workbook objWorkbook = null;
        //    Worksheet objsheet = null;
        //    object missing = System.Reflection.Missing.Value;
        //    try
        //    {
        //        objExcel = new Application();
        //        objWorkbook = objExcel.Workbooks.Add(missing);
        //        foreach (var item in dataTables)
        //        {
        //            objsheet = (Worksheet)objWorkbook.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
        //            objsheet.Name = item.Key;
        //            objExcel.Visible = false;
        //            int rowCount = item.Value.Rows.Count;
        //            int columnCount = item.Value.Columns.Count;
        //            string[,] datas = new string[rowCount + 1, columnCount];
        //            for (int i = 0; i < columnCount; i++)
        //            {
        //                datas[0, i] = item.Value.Columns[i].ColumnName.Trim();
        //            }
        //            for (int row = 0; row < rowCount; row++)
        //            {
        //                for (int col = 0; col < columnCount; col++)
        //                {

        //                    try
        //                    {
        //                        datas[row + 1, col] = item.Value.Rows[row][col].ToString().Trim();
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        SeatManage.SeatManageComm.WriteLog.Write(ex.Message);
        //                    }
        //                }
        //            }
        //            int exportRowCount = rowCount + 1;
        //            Char[] chr = "A".ToUpper().ToCharArray();
        //            chr[0] = Convert.ToChar(chr[0] + columnCount - 1);
        //            string c = new string(chr);
        //            Range range = objsheet.get_Range("A1", c + exportRowCount);
        //            range.Value2 = datas;
        //            objsheet.Columns.EntireColumn.AutoFit(); // Automatically change the column width              

        //        }


        //        string fileName1 = DateTime.Now.ToString("yyyy-MM-dd HH-ss-mm");
        //        path = Server.MapPath(".") + "\\" + fileName1 + ".xls";  //  这样写有问题           
        //        //objWorkbook.SaveCopyAs(path);
        //        objWorkbook.SaveAs(path, missing, missing, missing, missing, missing, XlSaveAsAccessMode.xlShared, missing, missing, missing, missing, missing);


        //        GC.Collect();
        //        string fileName = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".xls";//客户端保存的文件名             

        //        //以字符流的形式下载文件
        //        FileStream fs = new FileStream(path, FileMode.Open);
        //        byte[] bytes = new byte[(int)fs.Length];
        //        fs.Read(bytes, 0, bytes.Length);
        //        fs.Close();
        //        HttpContext.Current.Response.ContentType = "application/octet-stream";
        //        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;  filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
        //        HttpContext.Current.Response.BinaryWrite(bytes);
        //        HttpContext.Current.Response.Flush();
        //        HttpContext.Current.Response.End();

        //    }
        //    catch (Exception error)
        //    {
        //        SeatManage.SeatManageComm.WriteLog.Write(error.Message);
        //    }
        //    finally
        //    {

        //        if (objWorkbook != null) objWorkbook.Close(missing, missing, missing);
        //        if (objExcel.Workbooks != null) objExcel.Workbooks.Close();
        //        if (objExcel != null) objExcel.Quit();
        //        objsheet = null;
        //        objWorkbook = null;
        //        objExcel = null;

        //        if (File.Exists(path))
        //        {
        //            File.Delete(path);
        //        }

        //    }

        //}
        
    }
}