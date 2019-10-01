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
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.UI;
    
    
    public class UIAddActivityPanelData : QFramework.UIPanelData
    {
    }
    
    public partial class UIAddActivityPanel : QFramework.UIPanel
    {
        
        protected override void ProcessMsg(int eventId, QFramework.QMsg msg)
        {
            throw new System.NotImplementedException ();
        }
        
        protected override void OnInit(QFramework.IUIData uiData)
        {
            mData = uiData as UIAddActivityPanelData ?? new UIAddActivityPanelData();
            // please add init code here
            Check.onClick.AddListener(()=>
            {
                CheckClick();
            });
        }
        
        protected override void OnOpen(QFramework.IUIData uiData)
        {
        }
        
        protected override void OnShow()
        {
        }
        
        protected override void OnHide()
        {
        }
        
        protected override void OnClose()
        {
        }

        IEnumerator  SubmitClick()
        {
            if (CheckClick())
            {
                var config = InputFieldConfig.text;
                var desc = InputFieldDesc.text;
                WWWForm form = new WWWForm();
                form.AddField("config", config);
                form.AddField("desc", desc);
                WWW getData = new WWW("http://localhost/test", form);
                yield return getData;
                if (getData.error != null)
                {
                    Debug.Log(getData.text);
                }
                else
                {
                    MessageBoxV2.AddMessage("提交成功！", 3);
                }
                
            }
        }

        private bool CheckClick()
        {
            var config = InputFieldConfig.text;
            var desc = InputFieldDesc.text;
            var index = InputFieldActivityIndex.text;
            Debug.Log(config);
            Debug.Log(desc);
            Debug.Log(index);
            bool ret = true;
            if (config == "" || desc == "")
            {
                ret = false;
                MessageBoxV2.AddMessage("这么大的两个框，看不到吗？！ 回答我！！！", 5);
            }
            if(index == "")
            {
                MessageBoxV2.AddMessage("大哥，互动ID没写啊！", 5);
            }
            
            try
            {
                QF.SerializeHelper.FromJson<Dictionary<string, string>>(config);
            }
            catch(Newtonsoft.Json.JsonReaderException ex)
            {
                MessageBoxV2.AddMessage(ex.Message,3);
                ret = false;
            }
            return ret;
        }
    }
}
