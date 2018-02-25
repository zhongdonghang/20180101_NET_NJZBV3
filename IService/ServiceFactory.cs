using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Reflection;
using System.Xml;
using System.IO;

namespace IService
{
    /// <summary>
    /// 服务工厂
    /// **如果需要实现多个服务，需要在app.config下添加多个程序集信息
    /// **结构为：<add key="ServiceAssembly" value="SeatService.Service.SeatWatch,SeatService.Service"/> 
    /// </summary>
    public class ServiceFactory
    {
       
        /// <summary>
        /// 构造服务对象
        /// </summary>
        /// <returns></returns>
        public static List<IService> CreateServiceAssemblys()
        {
            List<IService> serviceList = new List<global::IService.IService>();
            List<string> assmblysInfo = GetAssemblyInfo();
            foreach (string assemblyType in assmblysInfo)
            {
                if (!string.IsNullOrEmpty(assemblyType))
                {
                    try
                    {
                        //通过反射构造服务接口实现的程序集
                        IService obj = (IService)createInferface(assemblyType);
                        serviceList.Add(obj);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(string.Format("构建服务程序集{0}失败：{1}" ,assemblyType, ex.Message));
                    }
                }
                else
                {
                    throw new Exception("没有服务接口");
                }
            }
            return serviceList;
        }
        private static object createInferface(string assembly)
        {
            if (!string.IsNullOrEmpty(assembly))
            {
                try
                {
                    string[] type = assembly.Split(',');
                    if (type.Length == 0)
                    {
                        throw new Exception("接口实现配置错误");
                    }
                    return Assembly.Load(type[1]).CreateInstance(type[0]);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                throw new Exception("没有配置接口实现的相关信息");
            }
        }
        /// <summary>
        /// 获取程序配置信息
        /// </summary>
        /// <returns></returns>
        private static List<string> GetAssemblyInfo()
        {
            string configFileName = ConfigurationManager.AppSettings["configFileName"];
            List<string> assemblysInfo = new List<string>();
           XmlDocument doc  = new XmlDocument();
           string path = string.Format(@"{0}{1}", AppDomain.CurrentDomain.BaseDirectory, configFileName);
           if (File.Exists(path))
           {
               doc.Load(path);
           }
           else
           {
               return assemblysInfo;
           }
            XmlNodeList nodes = doc.SelectNodes("//configuration/appSettings/add");
            foreach (XmlNode node in nodes)
            {
                if (node.Attributes["key"] != null && node.Attributes["key"].Value == "ServiceAssembly")
                {
                    assemblysInfo.Add(node.Attributes["value"].Value);
                } 
            }
            return assemblysInfo;
        }
    }
}
