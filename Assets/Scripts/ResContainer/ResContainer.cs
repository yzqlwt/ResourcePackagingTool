using QF.Master;
using QFramework;
using QuickTools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ResContainer : MonoBehaviour
{
    public Transform BlockImagePrefab;
    public Transform Content;
    public Transform PropertiesPanel;
    public Text ActivityText;
    public string Version = "v0.1.1";
    public Dictionary<string, Dictionary<string, string>> TotalProperties = new Dictionary<string, Dictionary<string, string>>();
    public Dictionary<string, string> ResMap = new Dictionary<string, string>();
    public Dictionary<string, string> Template = new Dictionary<string, string>();
    public List<string> Activities;

    private void Awake()
    {
        TypeEventSystem.Register<SetActivityIndex>(ReceiveActivityIndex);
    }
    void Start()
    {
        PropertiesPanel.gameObject.SetActive(false);
        transform.GetComponent<FileDragAndDrop>().gameObject.SetActive(false);
    }

    void Test()
    {

        DragFile(new FileDrag
        {
            FilePath = @"C:\Users\yzqlwt\Pictures\互动2-1_slices\1@3x.png",
            FileName = "1@3x.png",
            Extension = ".png",
            MD5 = "1@3x111"
        });
        DragFile(new FileDrag
        {
            FilePath = @"C:\Users\yzqlwt\Pictures\互动2-1_slices\Group 2.png",
            FileName = "Group 2.png",
            Extension = ".png",
            MD5 = "1@3x11122"
        });
    }

    void ReceiveActivityIndex(SetActivityIndex activityIndex)
    {
        var activity = activityIndex.activity;
        transform.GetComponent<FileDragAndDrop>().gameObject.SetActive(true);
        StartCoroutine(GetConfigTemplate(activity));
    }

    IEnumerator GetConfigTemplate(string activity)
    {
        var uri = "http://www.yzqlwt.com:8080/activity/config?activity=" + activity;
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debug.Log(": Error: " + webRequest.error);
            }
            else
            {
                var text = webRequest.downloadHandler.text;
                Template = QF.SerializeHelper.FromJson<Dictionary<string, string>>(text);
                Debug.Log(text);
                TypeEventSystem.Register<FileDrag>(DragFile);
                CleanUpDir();
                ResMap.Clear();
#if UNITY_EDITOR
                Invoke("Test", 1.0f);
#endif
            }
        }
    }

    void SetProperties(Dictionary<string, string> properties)
    {
        PropertiesPanel.GetComponent<PropertiesScript>().SetProperties(properties);
    }

    void DragFile(FileDrag filedrag)
    {
        if (ResMap.ContainsKey(filedrag.MD5))
        {
            return;
        }else if (filedrag.Extension == ".zip")
        {
            MessageBoxV2.AddMessage("暂不支持拖入配置包，重现配置", 10);
            return;
        }
        var str = string.Format("Drag File：{0}", filedrag.FileName);
        MessageBoxV2.AddMessage(str,3);
        var BlockImage = GetBlockImage();
        BlockImage.GetComponent<BlockImageScript>().SetTemplate(Template);
        BlockImage.GetComponent<BlockImageScript>().SetImage(filedrag);
        CopyFile(filedrag);
        ResMap.Add(filedrag.MD5, filedrag.FileName);
    }
    Transform GetBlockImage()
    {
        var BlockImage = Instantiate(BlockImagePrefab, Content);
        return BlockImage;
    }
    
    string GetBasePathDir()
    {
        var desktoppath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory) + "/";
        var ActivityIndex = GetActivityIndex();
        var path = desktoppath + "/工具"+ Version+"/"+ ActivityIndex;
        if (ActivityIndex.IndexOf("G")<0)
        {
            MessageBoxV2.AddMessage("ActivityIndex is null");
        }
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        return path;
    }
    string GetTmpResDir()
    {
        var baseDir = GetTmpDir();
        var resDir = baseDir + "/ResCache";
        if (!Directory.Exists(resDir))
            Directory.CreateDirectory(resDir);
        return resDir;
    }

    string GetTmpConfigDir()
    {
        var baseDir = GetTmpDir();
        var configDir = baseDir + "/ConfigCache";
        if (!Directory.Exists(configDir))
            Directory.CreateDirectory(configDir);
        return configDir;
    }


    void CopyFile(FileDrag file)
    {
        string resDir = GetTmpResDir();
        string todir = Path.Combine(resDir, file.MD5+file.Extension);
        if (File.Exists(file.FilePath))//必须判断要复制的文件是否存在
        {
            File.Copy(file.FilePath, todir, true);//三个参数分别是源文件路径，存储路径，若存储路径有相同文件是否替换
        }
    }

    string GetActivityIndex()
    {
        var text = ActivityText.text;
        if (text.IndexOf("G") < 0)
        {
            return "";
        }
        else
        {
            return text;
        }
    }

    string GetTmpDir()
    {
        var baseDir = GetBasePathDir();
        var tmpDir = baseDir + "/tmp";
        if (!Directory.Exists(tmpDir))
            Directory.CreateDirectory(tmpDir);
        return tmpDir;
    }

    void CleanUpDir()
    {
        var baseDir = GetBasePathDir();
        Utils.DeleteFolder(baseDir);
        if (!Directory.Exists(baseDir))
            Directory.CreateDirectory(baseDir);
    }

    string GetTmpOutPutDir()
    {
        var tmpDir = GetTmpDir();
        var outputDir = tmpDir + "/output";
        if (!Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);
        return outputDir;
    }

    string GetOutPutDir()
    {
        var baseDir = GetBasePathDir();
        var outputDir = baseDir + "/output";
        if (!Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);
        return outputDir;
    }




    public void TexturePackage()
    {
        string name = "default";
        var command = Application.streamingAssetsPath + "/bin/TexturePacker.exe";
        var resDir = GetTmpResDir();
        var outputDir = GetTmpOutPutDir();
        var argu = string.Format(@"{0} --sheet {1}/{2}.png --data {1}/{2}.plist --allow-free-size --no-trim --max-size 2048 --format cocos2d", resDir, outputDir, name);
        Utils.processCommand(command, argu);

        string[] files = System.IO.Directory.GetFiles(resDir);

        // Copy the files and overwrite destination files if they already exist.
        foreach (string s in files)
        {
            // Use static Path methods to extract only the file name from the path.
            if (System.IO.Path.GetExtension(s) != ".png")
            {
                var fileName = System.IO.Path.GetFileName(s);
                var destFile = System.IO.Path.Combine(outputDir, fileName);
                System.IO.File.Copy(s, destFile, true);
            }

        }
    }

    public void Clear()
    {
        TotalProperties.Clear();
        ResMap.Clear();
        var lst = new List<Transform>();
        foreach (Transform child in Content)
        {
            lst.Add(child);
        }
        for (int i = 0; i < lst.Count; i++)
        {
            Destroy(lst[i].gameObject);

        }
        CleanUpDir();
    }


    public void Export()
    {
        var index = GetActivityIndex();
        if (index == "")
        {
            MessageBoxV2.AddMessage("请先选择Activity!!!");
            return;
        }
        var list = GetResTransform();
        TotalProperties.Clear();
        foreach (var tran in list)
        {
            var BlockImageScript = tran.GetComponent<BlockImageScript>();
            var properties = BlockImageScript.Properties;
            try
            {
                TotalProperties.Add(properties["Name"], properties);
            }
            catch (Exception ex)
            {
                Debug.Log(ex);
                MessageBoxV2.AddMessage("导出失败：重复的资源名称");
                return;
            }
        }
        var dataAsJson = QF.SerializeHelper.ToJson(TotalProperties);
        File.WriteAllText(GetTmpOutPutDir() + "/ResConfig.json", dataAsJson);
        TexturePackage();
        Compress();
        GenerateCode.ImageConfig(TotalProperties);
        System.Diagnostics.Process.Start(GetBasePathDir());

    }

    public void Compress()
    {
        ZipUtil.ZipDirectory(GetTmpOutPutDir(), GetOutPutDir() + "/ResConfig.zip", false);
    }

    public List<Transform> GetResTransform()
    {
        var list = new List<Transform>();
        foreach(Transform tran in Content)
        {
            list.Add(tran);
        }
        return list;
    }

}
