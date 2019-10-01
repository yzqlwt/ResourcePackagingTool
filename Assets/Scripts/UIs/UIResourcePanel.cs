//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QFramework.Example
{
    using QF.Master;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.Networking;
    using UnityEngine.UI;
    
    
    public class UIResourcePanelData : QFramework.UIPanelData
    {
        public string activityIndex;
    }
    
    public partial class UIResourcePanel : QFramework.UIPanel
    {

        public string Version = "v0.0.1";
        public Dictionary<string, Dictionary<string, string>> TotalProperties = new Dictionary<string, Dictionary<string, string>>();
        public Dictionary<string, Transform> ResMap = new Dictionary<string, Transform>();

        public Transform ResBlockPrefab;
        public Transform Content;

        protected override void ProcessMsg(int eventId, QFramework.QMsg msg)
        {
            throw new System.NotImplementedException ();
        }

        public void Test()
        {
            TypeEventSystem.Send(new FilePathInfo()
            {
                FilePath = @"C:\Users\yzqlwt\Dropbox\flash\�Ӽ���\brick.png",
                Extension = ".png",
                FileName = "brick.png",
                MD5 = "dddddddddddddddddddddd"
            });
            TypeEventSystem.Send(new FilePathInfo()
            {
                FilePath = @"C:\Users\yzqlwt\Dropbox\flash\�Ӽ���\Ԫ�� 2 ����.png",
                Extension = ".png",
                FileName = "Ԫ�� 2 ����.png",
                MD5 = "dddddddddddddddddddddd111"
            });
            TypeEventSystem.Send(new FilePathInfo()
            {
                FilePath = @"C:\Users\yzqlwt\Dropbox\flash\�Ӽ���\brick.png",
                Extension = ".png",
                FileName = "brick.png",
                MD5 = "dddddddddddddddddddddd222"
            });
        }

        protected override void OnInit(QFramework.IUIData uiData)
        {
            mData = uiData as UIResourcePanelData ?? new UIResourcePanelData();
            // please add init code here
            var activity = mData.activityIndex;
            TextLabel.text = "ResArea-" + activity;
            Debug.Log(activity);
            gameObject.AddComponent<FileDragAndDrop>();
            DirTools.ActivityIndex = activity;
            DirTools.Version = Version;
            StartCoroutine(GetConfigTemplate(activity));
            TypeEventSystem.Register<ResBlockNameChanged>(NameChanged);
        }

        public void NameChanged(ResBlockNameChanged nameChanged)
        {
            Transform tran;
            ResMap.TryGetValue(nameChanged.MD5, out tran);
            if (tran)
            {
                tran.GetComponent<ResBlockScript>().text.text = nameChanged.FileName;
            }
        }

        protected override void OnClose()
        {
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
                    var template = QF.SerializeHelper.FromJson<Dictionary<string, string>>(text);
                    Debug.Log(text);
                    TypeEventSystem.Register<FilePathInfo>((file)=>
                    {
                        if (ResMap.ContainsKey(file.MD5))
                        {
                            return;
                        }
                        else if (file.Extension == ".zip")
                        {
                            MessageBoxV2.AddMessage("����������δ���", 3);
                        }
                        else
                        {
                            var str = string.Format("Drag File��{0}", file.FileName);
                            MessageBoxV2.AddMessage(str, 3);
                            var BlockImage = Instantiate(ResBlockPrefab, Content);
                            BlockImage.GetComponent<ResBlockScript>().SetTemplate(template);
                            BlockImage.GetComponent<ResBlockScript>().SetImage(file);
                            DirTools.CopyDropFileToTmpResDir(file);
                            ResMap.Add(file.MD5, BlockImage);
                        }
                    });
                    DirTools.CleanUpDir();
                    ResMap.Clear();
#if UNITY_EDITOR
                    Invoke("Test", 3.0f);
#endif
                }
            }
        }


    }


}