using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SocketLib
{
    public class RequestHandler
    {
        private string temp = string.Empty;
        public string[] GetActualString(string input)
        {
            return GetActualString(input, null);
        }
        private string[] GetActualString(string input, List<string> outputList)
        {
            if (outputList == null)
                outputList = new List<string>();
            if (!String.IsNullOrEmpty(temp))
                input = temp + input;
            string output = "";
            string pattern = @"(?<=^\[length=)(\d+)(?=\])";
            int length;
            if (Regex.IsMatch(input, pattern))
            {
                Match m = Regex.Match(input, pattern);
                //获取消息字符串实际应有的长度
                length = Convert.ToInt32(m.Groups[0].Value);
                // 获取需要进行截取的位置
                int startIndex = input.IndexOf(']') + 1;

                // 获取从此位置开始后所有字符的长度
                output = input.Substring(startIndex);
                if (output.Length == length)
                {
                    // 如果output的长度与消息字符串的应有长度相等
                    // 说明刚好是完整的一条信息
                    outputList.Add(output);
                    temp = "";
                }
                else if (output.Length < length)
                {
                    // 如果之后的长度小于应有的长度，
                    // 说明没有发完整，则应将整条信息，包括元数据，全部缓存
                    // 与下一条数据合并起来再进行处理
                    temp = input;
                    // 此时程序应该退出，因为需要等待下一条数据到来才能继续处理
                }
                else if (output.Length > length)
                {
                    // 如果之后的长度大于应有的长度，
                    // 说明消息发完整了，但是有多余的数据
                    // 多余的数据可能是截断消息，也可能是多条完整消息
                    output = output.Substring(0, length);
                    outputList.Add(output);
                    temp = "";

                    // 缩短input的长度
                    input = input.Substring(startIndex + length);
                    GetActualString(input, outputList);
                }
            }
            else
            { // 说明“[”，“]”就不完整
                temp = input;
            }
            return outputList.ToArray();
        }

        private int lackLength = 0;//缺少长度
        private byte[] tempBytes;//消息临时数组.完整的消息长度应该为tempBytes.Length+lackLength；
        public List<byte[]> GetActualObject(byte[] bytes)
        {
            return GetActualObject(bytes, null);
        }

        public List<byte[]> GetActualObject(byte[] bytes, List<byte[]> outputList)
        {
            if (outputList == null)
                outputList = new List<byte[]>(); 
            byte[] srcBytes = null;
            if (tempBytes == null)
            {
                //如果tempBytes长度为null，说明第一次处理数据，则取前四位，转换为int作为
                byte[] byteLength = new byte[4];
                Buffer.BlockCopy(bytes, 0, byteLength, 0, 4);
                lackLength = BitConverter.ToInt32(byteLength, 0);
                srcBytes = new byte[bytes.Length - 4]; 
                Buffer.BlockCopy(bytes, 4, srcBytes, 0, srcBytes.Length);
            }
            else
            {
                srcBytes = bytes;
            }

            if (lackLength == srcBytes.Length)//说明数组长度刚刚好
            {
                if (tempBytes == null)
                {
                    outputList.Add(srcBytes);
                    tempBytes = null;
                    lackLength = 0;
                }
                else
                {
                    Buffer.BlockCopy(srcBytes, 0, tempBytes, tempBytes.Length - lackLength, srcBytes.Length);
                    outputList.Add(tempBytes);//消息添加到list中 
                    tempBytes = null;
                    lackLength = 0;
                }
            }
            else if (srcBytes.Length < lackLength)
            {
                //如果字节长度小于消息完整长度，把数据放到缓冲区
                // 从字节开始位置取数据，缓冲区从tempBytes.Length - lackLength位置开始存放，把bytes中的内容全部存放到tempbytes中  
                if (tempBytes == null)
                {
                    tempBytes = new byte[lackLength];
                }
                Buffer.BlockCopy(srcBytes, 0, tempBytes, tempBytes.Length - lackLength, srcBytes.Length);
                lackLength = lackLength - srcBytes.Length;//重新设置完整消息剩余长度
            }
            else if (srcBytes.Length > lackLength)
            {
                if (tempBytes == null)
                {
                    tempBytes = new byte[lackLength];
                }

                //如果消息长度大于 完整消息剩余字节数，则剩余字节数长度内容，存放到tempBytes中。
                Buffer.BlockCopy(srcBytes, 0, tempBytes, tempBytes.Length - lackLength, lackLength);
                outputList.Add(tempBytes);//消息添加到list中 

                int l = srcBytes.Length - lackLength;//计算剩余的消息内容还需要多大的缓冲区存放。总长度-消息剩余长度-偏移位置 

                byte[] temp = new byte[l];//构建一个缓冲区
                Buffer.BlockCopy(srcBytes, lackLength, temp, 0, srcBytes.Length - lackLength);//取剩余内容放到临时缓冲区中
                tempBytes = null;
                lackLength = 0;
                GetActualObject(temp, outputList);

            }
            return outputList;

        }
    }
}
