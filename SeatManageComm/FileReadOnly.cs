using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SeatManage.SeatManageComm
{
    public class FileReadOnly
    {
        /// <summary>
        /// 去除只读
        /// </summary>
        /// <param name="dirPath"></param>
        public static void RemovingReadOnly(string dirPath)
        {
            string[] dirPathes = Directory.GetDirectories(dirPath, "*.*", SearchOption.AllDirectories);
            string[] filePathes = Directory.GetFiles(dirPath, "*.*", SearchOption.AllDirectories);
            foreach (var dp in dirPathes)
            {
                DirectoryInfo dir = new DirectoryInfo(dp);
                dir.Attributes = FileAttributes.Normal & FileAttributes.Directory;
            }
            foreach (var fp in filePathes)
            {
                File.SetAttributes(fp, FileAttributes.Normal);
            }
            DirectoryInfo mdir = new DirectoryInfo(dirPath);
            mdir.Attributes = FileAttributes.Normal & FileAttributes.Directory;
        }
        /// <summary>
        /// 添加只读
        /// </summary>
        /// <param name="dirPath"></param>
        public static void SetReadOnly(string dirPath)
        {
            string[] dirPathes = Directory.GetDirectories(dirPath, "*.*", SearchOption.AllDirectories);
            string[] filePathes = Directory.GetFiles(dirPath, "*.*", SearchOption.AllDirectories);
            foreach (var dp in dirPathes)
            {
                DirectoryInfo dir = new DirectoryInfo(dp);
                dir.Attributes = FileAttributes.ReadOnly & FileAttributes.Directory;
            }
            foreach (var fp in filePathes)
            {
                File.SetAttributes(fp, FileAttributes.ReadOnly);
            }
            DirectoryInfo mdir = new DirectoryInfo(dirPath);
            mdir.Attributes = FileAttributes.ReadOnly & FileAttributes.Directory;
        }
    }
}
