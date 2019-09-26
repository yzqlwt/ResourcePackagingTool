using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Security.Cryptography;
using System.IO;
using System;
using QF.Master;

public class PropertiesChange
{
    public Dictionary<string, string> FuckProperties;
}

public class SendProperties
{
    public Dictionary<string, string> FuckProperties;
}

public class BlockImageScript : MonoBehaviour
{
    public Image image;
    public Text text;

    public Transform PropertiesPanel;

    private string md5;
    public string Md5
    {
        get
        {
            return md5;
        }
        set
        {
            md5 = value;
        }
    }
    public Dictionary<string, string> Properties { get; set; }
    private FileDrag file;
    private Dictionary<string, string> Template;
    void Start()
    {
        Md5 = "未设置";
    }

    public void SetImage(FileDrag file)
    {
        this.file = file;
        var toggle = transform.GetComponent<Toggle>();
        var toggleGroup = transform.parent.GetComponent<ToggleGroup>();
        toggle.group = toggleGroup;
        var property = new Dictionary<string, string>();
        foreach(var kv in Template)
        {
            property.Add(kv.Key, kv.Value);
        }
        property["Name"] = file.FileName.Replace(file.Extension, "");
        property["MD5"] = file.MD5;
        property["Extension"] = file.Extension;
        Properties = property;
        StartCoroutine(GetImage(file));
    }

    public void SetTemplate(Dictionary<string, string> template)
    {
        Template = template;
    }


    IEnumerator GetImage(FileDrag file)
    {
        var path = "";
        if(file.Extension == ".png" )
        {
            path = @"file://" + file.FilePath;
        }
        else if(file.Extension == ".swf")
        {
            path = @"file://" + Application.streamingAssetsPath+"/swf.png";
        }
        else
        {
            path = @"file://" + Application.streamingAssetsPath + "/unknown.png";
        }
        WWW www = new WWW(path);
        yield return www;
        if (string.IsNullOrEmpty(www.error))
        {
            Texture2D tex = www.texture;
            Sprite temp = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0, 0));
            Md5 = "MD5:" + file.MD5;
            image.sprite = temp;
        }
        text.text = file.FileName;
    }
    public void BlockPropertiesChange(PropertiesChange pro)
    {
        Properties = new Dictionary<string, string>(pro.FuckProperties);
        text.text = Properties["Name"]+file.Extension;
    }

    public void onValueChanged(bool open)
    {
        if (open)
        {
            TypeEventSystem.Send(new SendProperties
            {
                FuckProperties = Properties
            });
            TypeEventSystem.Register<PropertiesChange>(BlockPropertiesChange);

        }
        else
        {
            TypeEventSystem.UnRegister<PropertiesChange>(BlockPropertiesChange);
        }

    }

}
