using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Drawing;
using System.Windows;
using System.Windows.Media.Imaging;

namespace AMS.MediaPlayer.UI.Code
{
    public class GetImage
    {
        public static BitmapImage InitImage()
        {
            string Path = "";
            using (BinaryReader reader = new BinaryReader(File.Open(Path, FileMode.Open)))
            {
                BitmapImage bitmapImage;
                FileInfo fi = new FileInfo(Path);
                byte[] bytes = reader.ReadBytes((int)fi.Length);
                reader.Close();

                bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = new MemoryStream(bytes);
                bitmapImage.EndInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                return bitmapImage;
             }    
        }

        //SqlConnection conn;
        //SqlCommand cmd;
        //string ImagelocadPath = ConfigurationManager.ConnectionStrings["ImagelocadPath"].ConnectionString;
        //string StrConn = ConfigurationManager.ConnectionStrings["strConn"].ConnectionString;
        //public void imgToDB(string no)
        //{   //参数sql中要求保存的imge变量名称为@images
        //    //调用方法如：imgToDB("update UserPhoto set Photo=@images where UserNo='" + temp + "'");
        //    //Image image=Image.Parse(ds.Tables[0].Rows[0]["PrintAmount"].ToString())+1;
        //    string sql = "select [No],[CustomerImage] from [dbo].[SlipCustomer] where No='" + no + "'";
        //    FileStream fs = File.OpenRead(ImagelocadPath);
        //    byte[] imageb = new byte[fs.Length];
        //    fs.Read(imageb, 0, imageb.Length);
        //    fs.Close();
        //    conn = new SqlConnection(StrConn);
        //    cmd = new SqlCommand(sql,conn);
        //    cmd.Parameters.Add("@images", SqlDbType.Image).Value = imageb;
        //    if (cmd.Connection.State == ConnectionState.Closed)
        //    {
        //        cmd.Connection.Open();
        //    }
        //    try
        //    {
        //        cmd.ExecuteNonQuery();
        //    }
        //    catch
        //    { }
        //    finally
        //    { cmd.Connection.Close(); }
        //}
    }
}
