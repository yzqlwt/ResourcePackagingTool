using System.Collections;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

class GenerateCode
{
    public static void ImageConfig(Dictionary<string, Dictionary<string, string>> TotalProperties)
    {
        var resTable = " ImageConfig = { \n";
        var keyvalue = "";
        foreach(var kv in TotalProperties)
        {
            var str = DictionaryToString(kv.Value);
            str = kv.Key + "=" + str + ",\n";
            keyvalue = keyvalue + str;
        }
        resTable = resTable + keyvalue + "\n}";
        Debug.Log(string.Format("resTable{0}", resTable));
    }
    public static string DictionaryToString(Dictionary<string, string> dict)
    {
        var keyvalue = "{";
        foreach (var kv in dict)
        {
            var str = string.Format(" {0}={1}, ", kv.Key, kv.Value);
            keyvalue += str;
        }
        return keyvalue + "}";
    }
}