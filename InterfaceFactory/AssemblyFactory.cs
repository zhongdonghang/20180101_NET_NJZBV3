using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Configuration;

namespace SeatManage.InterfaceFactory
{
    /// <summary>
    /// 通过反射构建程序集
    /// </summary>
    public class AssemblyFactory
    {
        /// <summary>
        /// 构造程序集
        /// </summary>
        /// <param name="key">配置信息</param>
        /// <returns></returns>
        public static object CreateAssembly(string key)
        {
            string assemblyType = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrEmpty(assemblyType))
            {
                throw new Exception(string.Format("没有找到接口“{0}”配置的信息", key));
            }
            try
            {
                Object obj =  createInferface(assemblyType);
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("构建接口{0}失败：{1}" ,key, ex.Message));
            }
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
    }
}
