 
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SeatManage.ClassModel; 

namespace SeatClient.Test
{
    
    
    /// <summary>
    ///这是 DataTransferTest 的测试类，旨在
    ///包含所有 DataTransferTest 单元测试
    ///</summary>
    [TestClass()]
    public class DataTransferTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试特性
        // 
        //编写测试时，还可使用以下特性:
        //
        //使用 ClassInitialize 在运行类中的第一个测试前先运行代码
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //使用 ClassCleanup 在运行完类中的所有测试后再运行代码
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //使用 TestInitialize 在运行每个测试前先运行代码
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //使用 TestCleanup 在运行完每个测试后运行代码
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion




        [TestMethod()]
        public void DESEncodeTest()
        {
            string e = SeatManage.SeatManageComm.AESAlgorithm.DESEncode("net.tcp://192.168.1.100:10010");
            string d = SeatManage.SeatManageComm.AESAlgorithm.DESDecode(e);

        }

        [TestMethod()]
        public void FileUpdateInfoTest()
        {
           // FileUpdateInfo target = new FileUpdateInfo(@"E:\开发临时文件夹\x86\Debug"); // TODO: 初始化为适当的值
           // target.ReleaseDate = DateTime.Now;
           // target.StartProgram = "SeatClient.exe";
           // target.SubsystemType = SeatManage.EnumType.SeatManageSubsystem.SeatClient;
           // target.Version ="1.0.1";
           // target.UpdateLog += string.Format("{0}\r{1}\r\r",DateTime.Now,"无更新日志");
           //string s = target.ToString();

           //FileUpdateInfo target1 = FileUpdateInfo.Convert(s);
           //string convert = FileUpdateInfo.Convert(target1);
            //  Assert.Inconclusive("无法验证不返回值的方法。");
        }
         [TestMethod()]
        public void DelDirectorysTest()
        {
            //IService.IService service = IService.ServiceFactory.CreateServiceAssemblys();
            //    service.Start();

          //  FileUpdateInfo.DelDirectorys(@"C:\Users\BlackSnow\Desktop\新建文件夹");
        }
        [TestMethod()]
         public void CommandHandleTest()
         {
             //DataTransfer target = new DataTransfer();
             //target.CommandHandle();
         }
         
    }
}
