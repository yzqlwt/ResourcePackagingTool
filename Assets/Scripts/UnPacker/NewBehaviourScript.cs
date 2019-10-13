using NRatel.TextureUnpacker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        var plistFilePath = @"C:\Users\yzqlwt\Desktop\新建文件夹 (2)\default.plist";
        var pngFilePath = @"C:\Users\yzqlwt\Desktop\新建文件夹 (2)\default.png";
        var loader = NRatel.TextureUnpacker.Loader.LookingForLoader(plistFilePath);
        if (loader != null)
        {
            var plist = loader.LoadPlist(plistFilePath);
            var bigTexture = loader.LoadTexture(pngFilePath, plist.metadata);

            int total = plist.frames.Count;
            int count = 0;
            foreach (var frame in plist.frames)
            {
                try
                {
                    Core.Restore(bigTexture, frame);
                    count += 1;
                }
                catch
                {
                }
                yield return null;
            }
        }
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
