using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace AdvertManage.DataTransfer
{
    public class PrintTemplate
    {
        /// <summary>
        /// 获取和更新打印模板
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool GetPrintTemplate(int id)
        {
            Model.AMS_PrintTemplateModel printTemplate = BLL.AMS_PrintTemplateBLL.GetPrintTemplateById(id);
            if (printTemplate != null)
            {
                SeatManage.ClassModel.AMS_PrintTemplateModel model = new SeatManage.ClassModel.AMS_PrintTemplateModel();
                model.Describe = printTemplate.Describe;
                model.EndDate = printTemplate.EndDate;
                model.EffectDate = printTemplate.EffectDate;
                model.Template = printTemplate.Template;
                List<string> imagesName = GetImagesName(model.Template);
                //下载打印模版中的图片
                AdvertManage.BLL.FileOperate fileOperate = new BLL.FileOperate();  
                for (int i = 0; i < imagesName.Count; i++)
                {
                    string fileFullName = string.Format(@"{0}{1}", ServiceSet.TempFilePath, imagesName[i]);
                    if (!File.Exists(fileFullName))//文件不存在，则下载。
                    {
                        if (!fileOperate.FileDownLoad(fileFullName, imagesName[i], SeatManage.EnumType.SeatManageSubsystem.SeatSlip))
                        {
                            return false;//下载失败，返回false；
                        } 
                    } 
                }
                //下载完成，执行上传操作。
                SeatManage.Bll.FileOperate seatFileOperate = new SeatManage.Bll.FileOperate();
                for (int i = 0; i < imagesName.Count; i++)
                {
                    string fileFullName = string.Format(@"{0}{1}", ServiceSet.TempFilePath, imagesName[i]);
                    if ( File.Exists(fileFullName))//文件不存在，则下载。
                    {
                        if (!seatFileOperate.UpdateFile(fileFullName, imagesName[i], SeatManage.EnumType.SeatManageSubsystem.SeatSlip))
                        {
                            return false;//下载失败，返回false；
                        } 
                    }
                }

                if (SeatManage.Bll.T_SM_PrintTemplate.AddPrintTemplate(model) == SeatManage.EnumType.HandleResult.Successed)
                {
                    //打印模板添加成功，上传文件
                    for (int i = 0; i < imagesName.Count; i++)
                    {
                        string fileFullName = string.Format(@"{0}{1}", ServiceSet.TempFilePath, imagesName[i]);
                        if (File.Exists(fileFullName))//文件存在则删除。
                        {
                            File.Delete(fileFullName);
                        }
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                //如果获取为空的，也作完成操作
                return true;
            }
        }
        /// <summary>
        /// 获取打印模板里的图片名称
        /// </summary>
        /// <param name="slipTemplate"></param>
        /// <returns></returns>
        static List<string> GetImagesName(string slipTemplate)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(slipTemplate);
            XmlNodeList nodes = xmlDoc.SelectNodes("//Print/Pic");
            List<string> imagesName = new List<string>();
            foreach (XmlNode node in nodes)
            {
                imagesName.Add(node.InnerText);
            }
            return imagesName;
        }
    }
}
