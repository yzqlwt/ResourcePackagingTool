using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.IO;
using QF.Master;
using QF.Res;

public class PropertiesScript : MonoBehaviour
{
    public Transform Item;
    public Transform Content;
    public Transform AddItemPanel;
    public Dictionary<string, string> Properties = new Dictionary<string, string>();
    public Dictionary<string, string> PropertiesTemplate;
    private ResLoader mResLoader;

    private void Start()
    {
        Item.gameObject.SetActive(false);
        TypeEventSystem.Register<SendProperties>(ReceiveProperties);
        mResLoader = ResLoader.Allocate();
    }

    public void SetPropertiesTemplate(Dictionary<string, string> dict)
    {
        PropertiesTemplate = new Dictionary<string, string>(dict);
    }

    public void ReceiveProperties(SendProperties pro)
    {
        SetProperties(pro.FuckProperties);
        transform.gameObject.SetActive(true);
    }

    public void SetProperties(Dictionary<string, string> properties)
    {
        ClearProperties();
        foreach (KeyValuePair<string, string> pro  in properties){
            var item = GetItem();
            SetItemKeyValue(item, pro);
        }
        Properties = new Dictionary<string, string>(properties);
    }
    public void ClearProperties()
    {
        foreach (Transform child in Content)
        {
            if (child.gameObject.name=="Property")
            {
                Destroy(child.gameObject);
            }
        }
        Properties.Clear();
    }


    public Transform GetItem()
    {
        var item = Instantiate(Item, Content);
        item.gameObject.SetActive(true);
        item.gameObject.name = "Property";
        var btn = item.Find("btn");
        btn.GetComponent<Button>().onClick.AddListener(() =>
        {
            ClickBtn(item);
        });


        return item;
    }

    public void AddItem()
    {
        var inputKey = AddItemPanel.Find("InputKey");
        var key = inputKey.GetComponent<InputField>().text;
        var inputValue = AddItemPanel.Find("InputValue");
        var value = inputValue.GetComponent<InputField>().text;
        var properties = new Dictionary<string, string>(Properties);
        properties.Add(key, value);
        if(key == "" || value == "")
        {
            return;
        }
        if (properties.ContainsKey(key))
        {
            properties[key] = value;
        }
        else
        {
            properties.Add(key, value);
        }
        SetProperties(properties);
        AddItemPanel.gameObject.SetActive(false);
    }
    public void CloseAddItemPanel()
    {
        AddItemPanel.gameObject.SetActive(false);
    }

    public void ClosePropertiesPanel()
    {
        gameObject.SetActive(false);
        OnValueChanged();
    }


    public void SetItemKeyValue(Transform item, KeyValuePair<string, string> pro)
    {
        item.Find("Key").GetComponent<Text>().text = pro.Key;
        item.Find("InputField").GetComponent<InputField>().text = pro.Value;
        var key = pro.Key;
        item.Find("btn").gameObject.SetActive(true);
        if (key == "Name")
        {
            item.Find("btn/Text").GetComponent<Text>().text = "+";
        }
        else if (key == "MD5" || key == "Extension")
        {
            item.Find("InputField").GetComponent<InputField>().readOnly = true;
            item.Find("btn/Text").GetComponent<Text>().text = "+";
        }
        else
        {
            item.Find("btn/Text").GetComponent<Text>().text = "-";
        }
    }

    public void ClickBtn(Transform item)
    {
        var key = item.Find("Key").GetComponent<Text>().text;
        if(key == "Name" || key == "MD5" || key == "Extension")
        {
            Debug.Log("添加Item");
            AddItemPanel.gameObject.SetActive(true);
        }
        else
        {
            Destroy(item.gameObject);
        }

    }
    public void OnValueChanged()
    {
        foreach (Transform child in Content)
        {
            if (child.gameObject.name == "Property")
            {
                var key = child.Find("Key").GetComponent<Text>().text;
                var value = child.Find("InputField").GetComponent<InputField>().text;
                if (Properties.ContainsKey(key))
                {
                    Properties[key] = value;
                }
                else
                {
                    Properties.Add(key, value);
                }
            }
        }
        TypeEventSystem.Send(new PropertiesChange
        {
            FuckProperties = Properties
        });
    }
    private void OnDestroy()
    {
        mResLoader.Recycle2Cache();
        mResLoader = null;
    }
}
