using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.EnumType;
using System.Xml;
using System.IO;
using System.Diagnostics;

namespace SeatManage.ClassModel
{
    [Serializable]
    public class MyDirectory
    {
        string name = "";
        /// <summary>
        /// 文件夹名称
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string lastUpdate = "";

        public string LastUpdate
        {
            get { return lastUpdate; }
            set { lastUpdate = value; }
        }
        private string version = "";

        public string Version
        {
            get { return version; }
            set { version = value; }
        }
        List<MyDirectory> _Directories = new List<MyDirectory>();
        /// <summary>
        /// 子文件夹
        /// </summary>
        public List<MyDirectory> Directories
        {
            get { return _Directories; }
            set { _Directories = value; }
        }
        List<FileSimpleInfo> _Files = new List<FileSimpleInfo>();
        /// <summary>
        /// 文件信息
        /// </summary>
        public List<FileSimpleInfo> Files
        {
            get { return _Files; }
            set { _Files = value; }
        }
    }
    [Serializable]
    /// <summary>
    /// 要更新的文件信息
    /// </summary>
    public class FileUpdateInfo
    {

        MyDirectory _Files = new MyDirectory();// new List<MyDirectory>();
        /// <summary>
        /// 文件列表
        /// </summary>
        public MyDirectory Files
        {
            get { return _Files; }
            set { _Files = value; }
        }
        DateTime _ReleaseDate = DateTime.Parse("1900-1-1");
        /// <summary>
        /// 发布日期
        /// </summary>
        public DateTime ReleaseDate
        {
            get { return _ReleaseDate; }
            set { _ReleaseDate = value; }
        }

        private string _Version = "";
        /// <summary>
        /// 主版本号
        /// </summary>
        public string Version
        {
            get { return _Version; }
            set { _Version = value; }
        }
        private SeatManageSubsystem _SubsystemType = SeatManageSubsystem.None;
        /// <summary>
        /// 子系统类型
        /// </summary>
        public SeatManageSubsystem SubsystemType
        {
            get { return _SubsystemType; }
            set { _SubsystemType = value; }
        }
        private string startProgram;
        /// <summary>
        /// 开始启动程序
        /// </summary>
        public string StartProgram
        {
            get { return startProgram; }
            set { startProgram = value; }
        }
        private string _UpdateRemark = "";
        /// <summary>
        /// 更新说明
        /// </summary>
        public string UpdateLog
        {
            get { return _UpdateRemark; }
            set { _UpdateRemark = value; }
        }

        public override string ToString()
        {
            return Convert(this);
        }
        private static XmlDocument _XmlSystemInfo;

        /// <summary>
        /// 构造系统更新类的Xml
        /// </summary>
        /// <param name="path">系统所在文件夹</param>
        public FileUpdateInfo(string path)
        {
            this._Files = BuildUpdateConfigFile(path);
        }
        public FileUpdateInfo()
        { }

        List<string> _fileFullPaths = new List<string>();
        /// <summary>
        /// 构造要上传的文件路径
        /// </summary>
        /// <returns></returns>
        public   List<string> BuildUpdateFilePaths( )
        { 
            BuildUpdateFile("", this.Files);
            return _fileFullPaths;
        }



        /// <summary>
        /// 构造要上传的文件路径
        /// </summary>
        /// <returns></returns>
        private void BuildUpdateFile(string path, MyDirectory directory)
        {
            foreach (FileSimpleInfo file in directory.Files)
            {
                string filepath = "";
                if (string.IsNullOrEmpty(path))
                {
                    filepath = string.Format(@"\{0} ", file.Name);
                }
                else
                {
                    filepath = string.Format(@"{0}\{1} ", path, file.Name);
                }
                _fileFullPaths.Add(filepath.Trim());
            }
            foreach (MyDirectory dir in directory.Directories)
            {
                if (!string.IsNullOrEmpty(path))
                {
                    path = string.Format(@"{0}\{1}", path, dir.Name);
                }
                else
                {
                    path = string.Format(@"\{0}", dir.Name);
                }

                BuildUpdateFile(path, dir);
                if (!string.IsNullOrEmpty(path))
                {
                    path = path.Remove(path.LastIndexOf("\\"));
                }
            } 

        }
        /// <summary>
        /// 获取程序所有的简单文件信息
        ///  name属性为文件的相对路径
        /// </summary>
        /// <returns></returns>
        public List<FileSimpleInfo> BuildSystemFileSilmpleList()
        {
            BuildSystemFileSilmpleList("", this.Files);
            return fileSimples;
        }

        List<FileSimpleInfo> fileSimples = new List<FileSimpleInfo>();
        private void BuildSystemFileSilmpleList(string path, MyDirectory directory)
        {
            foreach (FileSimpleInfo file in directory.Files)
            {
                string filepath = "";
                if (string.IsNullOrEmpty(path))
                {
                    if (file.Name.IndexOf("\\") == -1)
                    {
                        filepath = string.Format(@"\{0} ", file.Name);
                    }
                    else
                    {
                        filepath = file.Name;
                    } 
                }
                else
                {
                    filepath = string.Format(@"{0}\{1} ", path, file.Name);
                }
                file.Name = filepath.Trim();
                fileSimples.Add(file);
            }
            foreach (MyDirectory dir in directory.Directories)
            {
                if (!string.IsNullOrEmpty(path))
                {
                    path = string.Format(@"{0}\{1}", path, dir.Name);
                }
                else
                {
                    path = string.Format(@"\{0}", dir.Name);
                }

                BuildSystemFileSilmpleList(path, dir);
                if (!string.IsNullOrEmpty(path))
                {
                    path = path.Remove(path.LastIndexOf("\\"));
                }
            } 
        }

        private static  MyDirectory  BuildUpdateConfigFile(string path)
        {
            MyDirectory dirFile = new MyDirectory();
            string pathName = path.Substring(path.LastIndexOf("\\") + 1);
            string lastUpdate = Directory.GetLastWriteTimeUtc(path).ToString();
            string version = "1.0.0";
            string[] files = Directory.GetFiles(path);
            dirFile.Name = pathName;
            dirFile.Version = version;
            dirFile.LastUpdate = Directory.GetLastWriteTimeUtc(path).ToString(); 
            for (int i = 0; i < files.Length; i++)
            {
                try
                {
                    FileSimpleInfo chileFile = new FileSimpleInfo();
                    FileInfo fileInfo = new FileInfo(files[i]);
                    chileFile.Name = fileInfo.Name;
                    chileFile.Version = FileVersionInfo.GetVersionInfo(fileInfo.FullName).FileVersion == null ? "1.0.0" : FileVersionInfo.GetVersionInfo(fileInfo.FullName).FileVersion;
                    chileFile.ModifyDateTime = fileInfo.LastWriteTimeUtc.ToLocalTime();
                    dirFile.Files.Add(chileFile);
                }
                catch (Exception ex)
                {
                    //System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
             string[] dirs = Directory.GetDirectories(path);
             for (int j = 0; j < dirs.Length; j++)
            {
                dirFile.Directories.Add(BuildUpdateConfigFile(dirs[j]));
            }
             return dirFile;
        }

        /// <summary>
        /// 构造更新配置信息
        /// </summary>
        /// <param name="path"></param>
        private static XmlElement BuildUpdateConfigFile(MyDirectory path)
        {
            string pathName = path.Name;  
            string lastUpdate = path.LastUpdate; 
            string version = path.Version;  
            XmlElement element = null;
            element = _XmlSystemInfo.CreateElement("dir");
            element.SetAttribute("name", pathName);
            element.SetAttribute("lastUpdate", lastUpdate);
            element.SetAttribute("version", version);
            for (int i = 0; i < path.Files.Count; i++)
            {
                try
                { 
                    XmlElement child = _XmlSystemInfo.CreateElement("file");
                    child.SetAttribute("lastUpdate", path.Files[i].ModifyDateTime.ToString());
                    child.SetAttribute("version", path.Files[i].Version);
                    child.SetAttribute("name", path.Files[i].Name);
                    element.AppendChild(child);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
            //  string[] dirs = Directory.GetDirectories(path);
            for (int j = 0; j < path.Directories.Count; j++)
            {
                element.AppendChild(BuildUpdateConfigFile(path.Directories[j]));
            }
            return element;
        }
        /// <summary>
        /// 重新初始化应用程序信息
        /// </summary>
        public void CreateApplicationRoot()
        {
            XmlNode node = _XmlSystemInfo.SelectSingleNode("//updater/application");
            node.Attributes["version"].Value = this._Version;
            node.Attributes["subsystem"].Value = ((int)this._SubsystemType).ToString();
            node.Attributes["lastUpdate"].Value = this._ReleaseDate.ToString();
            node.Attributes["systemName"].Value = this._SubsystemType.ToString();
            node.Attributes["startProgram"].Value = this.startProgram;
        }

        public static string Convert(FileUpdateInfo filesInfo)
        {
            _XmlSystemInfo = new XmlDocument();
            XmlDeclaration dec = _XmlSystemInfo.CreateXmlDeclaration("1.0", "utf-8", null);
            _XmlSystemInfo.AppendChild(dec);
            XmlElement root = _XmlSystemInfo.CreateElement("updater");//创建根节点
            XmlElement element = null;
            element = _XmlSystemInfo.CreateElement("application");
            element.SetAttribute("version", filesInfo._Version);
            element.SetAttribute("subsystem", ((int)filesInfo._SubsystemType).ToString());
            element.SetAttribute("lastUpdate", filesInfo._ReleaseDate.ToString());
            element.SetAttribute("systemName", filesInfo._SubsystemType.ToString());
            element.SetAttribute("startProgram", filesInfo.startProgram);
            _XmlSystemInfo.AppendChild(root);//根节点
            root.AppendChild(element);//
            element = _XmlSystemInfo.CreateElement("updateLog");
            element.InnerText = filesInfo.UpdateLog;//更新日志
            root.AppendChild(element);
            element = BuildUpdateConfigFile(filesInfo._Files);
            root.AppendChild(element);
            _XmlSystemInfo.AppendChild(root);
            return _XmlSystemInfo.OuterXml;
        }


        /// <summary>
        /// xml转换为更新类
        /// </summary>
        /// <param name="filesXml"></param>
        /// <returns></returns>
        public static FileUpdateInfo Convert(string filesXml)
        {
            FileUpdateInfo fileupdate = new FileUpdateInfo();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(filesXml);
            XmlNode node = doc.SelectSingleNode("//updater/application");
            fileupdate.Version = node.Attributes["version"].Value;
            fileupdate.SubsystemType = node.Attributes["subsystem"] != null ? (SeatManageSubsystem)int.Parse(node.Attributes["subsystem"].Value) : SeatManageSubsystem.None;
            fileupdate.ReleaseDate = node.Attributes["lastUpdate"] != null ? DateTime.Parse(node.Attributes["lastUpdate"].Value) : DateTime.Parse("1900-1-1");
            fileupdate.StartProgram = node.Attributes["startProgram"] != null ? node.Attributes["startProgram"].Value : "";
             
            node = doc.SelectSingleNode("//updater/updateLog");
            fileupdate.UpdateLog = node.InnerText;
            XmlNode nodelist = doc.SelectSingleNode("//updater/dir");
            fileupdate._Files = BuildUpdateConfigFile(nodelist); 
            return fileupdate;
        }

        static MyDirectory BuildUpdateConfigFile(XmlNode node)
        {
            MyDirectory dir = new MyDirectory();
            dir.LastUpdate = node.Attributes["lastUpdate"].Value;
            dir.Name = node.Attributes["name"].Value;
            dir.Version = node.Attributes["version"].Value;
            XmlNodeList nodes = node.ChildNodes;
            foreach (XmlNode n in node.ChildNodes)
            {
                if (n.Name == "file")
                {
                    FileSimpleInfo dir1 = new FileSimpleInfo();
                    dir1.ModifyDateTime = n.Attributes["lastUpdate"] == null ? DateTime.Parse("1900-1-1") : DateTime.Parse(n.Attributes["lastUpdate"].Value);
                    dir1.Name = n.Attributes["name"].Value;
                    dir1.Version = n.Attributes["version"].Value;
                    dir.Files.Add(dir1);
                }
                else if (n.Name == "dir")
                {
                    dir.Directories.Add(BuildUpdateConfigFile(n));
                }
            } 
            return dir;
           
        }

        /// <summary>
        /// 将更新信息保存到指定的文件
        /// </summary>
        /// <param name="filename">要将文档保存的其中的文件的位置</param>
        public void Save(string filename)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(this.ToString());
            doc.Save(filename);
        }
        /// <summary>
        /// 清空指定路径中的文件以及其子文件夹。
        /// </summary>
        /// <returns></returns>
        public static void DelDirectorys(string directoryPath)
        {
            if (Directory.Exists(directoryPath))
            {
                string[] filePaths = Directory.GetFiles(directoryPath);//获取文件夹中所有的文件
                for (int i = 0; i < filePaths.Length; i++)
                {
                    if (File.Exists(filePaths[i]))
                    {
                        File.Delete(filePaths[i]);
                    }
                }
                 string[] directorys = Directory.GetDirectories(directoryPath);//获取文件夹中所有的文件
                 for (int i = 0; i < directorys.Length; i++)
                 {
                     DelDirectorys(directorys[i]);
                 }
                 Directory.Delete(directoryPath);
            }
        }


    }
}
