using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    //"attachments": {
    //  "id": 4694,
    //  "name": "default.png",
    //  "uri": "/course-math/6a/1c/6acbcc67cfbf41a9477ed0d2e6f2c51c.png",
    //  "ext_name": "png"
    //}
public class Attachments
{
    public int id;
    public string name;
    public string uri;
    public string ext_name;
}

public class AttachmentsConfig
{
    public int id;
    public string name;
    public string itemId; //非服务器返回字段
    public Attachments attachments;
}

