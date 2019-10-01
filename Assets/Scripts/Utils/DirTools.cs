using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

class DirTools
{
    public static string ActivityIndex { get; set; }
    public static string Version { get; set; }
    public static string GetBasePathDir()
    {
        var desktoppath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory) + "/";
        var path = desktoppath + "/工具" + Version + "/" + ActivityIndex;
        if (ActivityIndex.IndexOf("G") < 0)
        {
            MessageBoxV2.AddMessage("ActivityIndex is null");
        }
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        return path;
    }
    public static string GetTmpResDir()
    {
        var baseDir = GetTmpDir();
        var resDir = baseDir + "/ResCache";
        if (!Directory.Exists(resDir))
            Directory.CreateDirectory(resDir);
        return resDir;
    }

    public static string GetTmpConfigDir()
    {
        var baseDir = GetTmpDir();
        var configDir = baseDir + "/ConfigCache";
        if (!Directory.Exists(configDir))
            Directory.CreateDirectory(configDir);
        return configDir;
    }
    public static void CopyDropFileToTmpResDir(FilePathInfo file)
    {
        string resDir = GetTmpResDir();
        string todir = Path.Combine(resDir, file.MD5 + file.Extension);
        if (File.Exists(file.FilePath))//必须判断要复制的文件是否存在
        {
            File.Copy(file.FilePath, todir, true);//三个参数分别是源文件路径，存储路径，若存储路径有相同文件是否替换
        }
    }

    public static string GetTmpDir()
    {
        var baseDir = GetBasePathDir();
        var tmpDir = baseDir + "/tmp";
        if (!Directory.Exists(tmpDir))
            Directory.CreateDirectory(tmpDir);
        return tmpDir;
    }

    public static void CleanUpDir()
    {
        var baseDir = GetBasePathDir();
        DeleteFolder(baseDir);
        if (!Directory.Exists(baseDir))
            Directory.CreateDirectory(baseDir);
    }

    public static string GetTmpOutPutDir()
    {
        var tmpDir = GetTmpDir();
        var outputDir = tmpDir + "/output";
        if (!Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);
        return outputDir;
    }

    public static string GetOutPutDir()
    {
        var baseDir = GetBasePathDir();
        var outputDir = baseDir + "/output";
        if (!Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);
        return outputDir;
    }
    public static void DeleteFolder(string directoryPath)
    {
        foreach (string d in Directory.GetFileSystemEntries(directoryPath))
        {
            if (File.Exists(d))
            {
                FileInfo fi = new FileInfo(d);
                if (fi.Attributes.ToString().IndexOf("ReadOnly") != -1)
                    fi.Attributes = FileAttributes.Normal;
                File.Delete(d);     //删除文件   
            }
            else
                DeleteFolder(d);    //删除文件夹
        }
        Directory.Delete(directoryPath);    //删除空文件夹
    }

}
