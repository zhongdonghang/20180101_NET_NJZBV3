using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using SeatManage.SeatManageComm;
using System.Diagnostics;

namespace InterfaceRegistryTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SeatManage.ClassModel.RegistryKey reg;
        /// <summary>
        /// 发布器注册激活码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (reg == null)
            {
                reg = new SeatManage.ClassModel.RegistryKey();
            } 
            reg.RegistryList["JuneberryReadingRoomInterfaceKey"] = textBox1.Text;
            reg.RegistryList["JuneberryAccessInterfaceKey"] = textBox2.Text;
            reg.RegistryList["JuneberryMediaReleaseKey"] = textBox3.Text;
            try
            {
                string schoolNum = SeatManage.Bll.Registry.GetSchoolNum();
                if (string.IsNullOrEmpty(schoolNum))
                {
                    lb_Error.Text = "注册失败！";
                    return;
                }
                if (!string.IsNullOrEmpty(reg.RegistryList["JuneberryReadingRoomInterfaceKey"]))
                {
                    string pass = SeatManage.SeatManageComm.MD5Algorithm.GetMD5Str32WithListKey(new List<string>() { schoolNum, "JuneberryReadingRoomInterfaceKey" });
                    if (reg.RegistryList["JuneberryReadingRoomInterfaceKey"] != pass.Substring(0, 8) + "-" + pass.Substring(8, 8) + "-" + pass.Substring(16, 8) + "-" + pass.Substring(24, 8))
                    {
                        lb_Error.Text = "注册码错误！";
                        return;
                    }
                }
                if (!string.IsNullOrEmpty(reg.RegistryList["JuneberryAccessInterfaceKey"]))
                {
                    string pass = SeatManage.SeatManageComm.MD5Algorithm.GetMD5Str32WithListKey(new List<string>() { schoolNum, "JuneberryAccessInterfaceKey" });
                    if (reg.RegistryList["JuneberryAccessInterfaceKey"] != pass.Substring(0, 8) + "-" + pass.Substring(8, 8) + "-" + pass.Substring(16, 8) + "-" + pass.Substring(24, 8))
                    {
                        lb_Error.Text = "注册码错误！";
                        return;
                    }
                }
                if (!string.IsNullOrEmpty(reg.RegistryList["JuneberryMediaReleaseKey"]))
                {
                    string pass = SeatManage.SeatManageComm.MD5Algorithm.GetMD5Str32WithListKey(new List<string>() { schoolNum, "JuneberryMediaReleaseKey" });
                    if (reg.RegistryList["JuneberryMediaReleaseKey"] != pass.Substring(0, 8) + "-" + pass.Substring(8, 8) + "-" + pass.Substring(16, 8) + "-" + pass.Substring(24, 8))
                    {
                        lb_Error.Text = "注册码错误！";
                        return;
                    }
                }
                if (SeatManage.Bll.Registry.SaveRegistryKey(reg))
                {
                    MessageBox.Show("保存成功！");
                    this.Close();
                }
                else
                {
                    lb_Error.Text = "保存失败！";
                }
            }
            catch (Exception ex)
            {
                lb_Error.Text = ex.Message;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                string commstring = GetConnSting();
                if (string.IsNullOrEmpty(commstring))
                {
                    MessageBox.Show("配置错误！");
                    this.Close();
                }
                else
                {
                    CreatConfig(commstring);
                }
                reg = SeatManage.Bll.Registry.GetRegistryKey();
                textBox1.Text = reg.RegistryList["JuneberryReadingRoomInterfaceKey"];
                textBox2.Text = reg.RegistryList["JuneberryAccessInterfaceKey"];
                textBox3.Text = reg.RegistryList["JuneberryMediaReleaseKey"];
            }
            catch (Exception ex)
            {
                MessageBox.Show("配置错误！");
                this.Close();
            }
        }

        private string GetConnSting()
        {
            string comm = "";
            XmlDocument doc = new XmlDocument();
            string fileDircetoryPath = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = string.Format("{0}ServiceHostTimerHost.exe.config", fileDircetoryPath);
            if (File.Exists(filePath))
            {
                doc.Load(filePath);
                XmlNodeList nodes = doc.SelectNodes("//configuration/connectionStrings/add");
                foreach (XmlNode node in nodes)
                {
                    if (node.Attributes["name"].Value == "EndpointAddress")
                    {
                        comm = AESAlgorithm.AESDecrypt(node.Attributes["connectionString"].Value);
                    }
                }
            }
            return comm;
        }

        private void CreatConfig(string commstring)
        {
            XmlDocument docx = new XmlDocument();
            XmlDeclaration dec = docx.CreateXmlDeclaration("1.0", "utf-8", null);
            docx.AppendChild(dec);
            XmlElement root = docx.CreateElement("configuration");//创建根节点
            XmlElement elementx = docx.CreateElement("connectionStrings");
            XmlElement addelement = docx.CreateElement("add");
            addelement.SetAttribute("name", "EndpointAddress");
            addelement.SetAttribute("connectionString", AESAlgorithm.AESEncrypt(commstring));
            elementx.AppendChild(addelement);
            root.AppendChild(elementx);
            XmlElement setelement = docx.CreateElement("startup");
            XmlElement supelement = docx.CreateElement("supportedRuntime");
            supelement.SetAttribute("version", "v4.0");
            supelement.SetAttribute("sku", ".NETFramework,Version=v4.0");
            setelement.AppendChild(supelement);
            root.AppendChild(setelement);
            docx.AppendChild(root);
            docx.Save(Process.GetCurrentProcess().MainModule.FileName + ".config");

        }
    }
}
