//using Newtonsoft.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using UnityEngine;
using System.IO.Compression;
using Debug = UnityEngine.Debug;

namespace QuickTools
{
    class Utils
    {
        /// <summary>
        /// 获取文件MD5值
        /// </summary>
        /// <param name="fileName">文件绝对路径</param>
        /// <returns>MD5值</returns>
        public static string GetMD5HashFromFile(string fileName)
        {
            try
            {
                FileStream file = new FileStream(fileName, FileMode.Open);
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
            }
        }
        public static void processCommand(string command, string argument)
        {
            using (Process process = new Process())
            {
                process.StartInfo.FileName = command;

                process.StartInfo.Arguments = argument;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.Start();

                StreamReader reader = process.StandardOutput;
                string output = reader.ReadToEnd();
                UnityEngine.Debug.Log(output);
                MessageBoxV2.AddMessage(output, 10);
                process.WaitForExit();
            }
        }
        public static void DeleteFolder(string directoryPath)
        {
            //foreach (string d in Directory.GetFileSystemEntries(directoryPath))
            //{
            //    if (File.Exists(d))
            //    {
            //        FileInfo fi = new FileInfo(d);
            //        if (fi.Attributes.ToString().IndexOf("ReadOnly") != -1)
            //            fi.Attributes = FileAttributes.Normal;
            //        File.Delete(d);     //删除文件   
            //    }
            //    else
            //        DeleteFolder(d);    //删除文件夹
            //}
            Directory.Delete(directoryPath,true);    //删除空文件夹
        }
        public static string GetVarName(System.Linq.Expressions.Expression<Func<string, string>> exp)
        {
            return ((System.Linq.Expressions.MemberExpression)exp.Body).Member.Name;
        }

        public static string GetPcInfo()
        {
            //设备的模型

            Debug.Log("设备模型"+ SystemInfo.deviceModel);

            //设备的名称

            Debug.Log("设备名称"+SystemInfo.deviceName);

            //设备的类型

            Debug.Log("设备类型（PC电脑，掌上型）" + SystemInfo.deviceType.ToString());

            //系统内存大小

            Debug.Log("系统内存大小MB" + SystemInfo.systemMemorySize.ToString());

            //操作系统

            Debug.Log("操作系统" + SystemInfo.operatingSystem);

            //设备的唯一标识符

            Debug.Log("设备唯一标识符" + SystemInfo.deviceUniqueIdentifier);

            //显卡设备标识ID

            Debug.Log("显卡ID" + SystemInfo.graphicsDeviceID.ToString());

            //显卡名称

            Debug.Log("显卡名称" + SystemInfo.graphicsDeviceName);

            //显卡类型

            Debug.Log("显卡类型" + SystemInfo.graphicsDeviceType.ToString());

            //显卡供应商

            Debug.Log("显卡供应商" + SystemInfo.graphicsDeviceVendor);

            //显卡供应唯一ID

            Debug.Log("显卡供应唯一ID" + SystemInfo.graphicsDeviceVendorID.ToString());

            //显卡版本号

            Debug.Log("显卡版本号" + SystemInfo.graphicsDeviceVersion);

            //显卡内存大小

            Debug.Log("显存大小MB" + SystemInfo.graphicsMemorySize.ToString());

            //显卡是否支持多线程渲染

            Debug.Log("显卡是否支持多线程渲染" + SystemInfo.graphicsMultiThreaded.ToString());

            //支持的渲染目标数量

            Debug.Log("支持的渲染目标数量" + SystemInfo.supportedRenderTargetCount.ToString());
            return SystemInfo.deviceUniqueIdentifier;
        }
    }
}

