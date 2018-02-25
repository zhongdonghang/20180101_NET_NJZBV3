using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace UpdateOperate
{
    class Program
    {

        [DllImport("kernel32.dll")]
        public static extern uint WinExec(string lpCmdLine, uint uCmdShow);

        static void Main(string[] args)
        {
            Console.Write("按回车进行更新！");
            Console.ReadLine();
            Console.WriteLine("正在更新请稍等！");
            Code.AddBookKey abk = new Code.AddBookKey();
            Console.WriteLine(abk.GetCommstr());
            string re = abk.BookKeyon();
            Console.WriteLine(re);
            Console.ReadLine();
            if (re == "处理完成！")
            {
                string vBatFile = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + "\\dl.bat";
                using (StreamWriter vStreamWriter = new StreamWriter(vBatFile, false, Encoding.Default))
                {

                    vStreamWriter.Write(string.Format(
                    ":del\r\n" +
                    " del \"{0}\"\r\n" +
                    "if exist \"{0}\" goto del\r\n" + //此处已修改
                    "del %0\r\n", AppDomain.CurrentDomain.BaseDirectory + "\\UpdateOperate.exe"));
                }
                WinExec(vBatFile, 0);
            }
        }
    }
}
