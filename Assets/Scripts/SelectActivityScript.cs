using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QF.Master;
using QFramework;
using UnityEngine.Networking;
using System;
using System.Linq;
using QuickTools;

public class SetActivityIndex
{
    public string activity;
}

public class SelectActivityScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Dropdown ActivityOptions;
    public Transform DownLoad;
    public string Version = "0.0.0";
    public Text VersionText;
    void Start()
    {
        DownLoad.gameObject.SetActive(false);
        StartCoroutine(GetNeedUpdate());
        StartCoroutine(GetActivityList());
        VersionText.text = "version:"+Version;
    }

    public void OnClickStart()
    {
        var text = ActivityOptions.captionText.text;
        if (text.IndexOf("G") > -1)
        {
            TypeEventSystem.Send(new SetActivityIndex()
            {
                activity = text
            });
            Destroy(transform.gameObject);
        }

    }
    public void DownloadNewVersion()
    {
        Application.OpenURL("http://www.yzqlwt.com:8080/activity/download");
    }
    public void Guide()
    {
        var command = Application.streamingAssetsPath + "/bin/TexturePackerGUI.exe";
        var argu = "";
        Utils.processCommand(command, argu);
    }

    IEnumerator GetNeedUpdate()
    {
        var uri = "http://www.yzqlwt.com:8080/activity/version?version="+ Version;
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
                bool _b = Convert.ToBoolean(text);
                if (_b)
                {
                    DownLoad.gameObject.SetActive(true);
                }
            }
        }
    }
    IEnumerator GetActivityList()
    {
        var uri = "http://www.yzqlwt.com:8080/activity/list";
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

                var list = QF.SerializeHelper.FromJson<Dictionary<string, string>>(webRequest.downloadHandler.text);
                ActivityOptions.ClearOptions();
                ActivityOptions.AddOptions(new List<string>
                {
                    "ÇëÏÈÑ¡Ôñ¿Î³Ì"
                });

                var Activities = list.Select((kv) =>
                {
                    return kv.Key;
                }).ToList();
                ActivityOptions.AddOptions(Activities);
                Debug.Log(string.Format("Activity List:{0}", webRequest.downloadHandler.text));
            }
        }
    }

}
