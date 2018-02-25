using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace InterfaceRegistryTool
{
    public class RegistryClass
    {
        public void GetKey()
        {
            RegistryKey lm = Registry.LocalMachine;
            //对应HKEY_LOCAL_MACHINE基项分支
            RegistryKey software = lm.OpenSubKey("SOFTWARE", true);
            
            //打开Software项
            RegistryKey product;
            product = software.OpenSubKey("Juneberry", true);
            if (product == null)
            {
                product = software.CreateSubKey("Juneberry");
            }


        }
    }
}
