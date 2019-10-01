using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using B83.Win32;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
using QuickTools;
using QF.Master;

public class FilePathInfo
{
    public string FilePath;
    public string Extension;
    public string FileName;
    public string MD5;
}

public class FileDragAndDrop : MonoBehaviour
{
    // important to keep the instance alive while the hook is active.
    UnityDragAndDropHook hook;
    void OnEnable ()
    {
        // must be created on the main thread to get the right thread id.
        hook = new UnityDragAndDropHook();
        hook.InstallHook();
        hook.OnDroppedFiles += OnFilesAsync;
    }

    void OnDisable()
    {
        hook.UninstallHook();
    }

    void OnFilesAsync(List<string> aFiles, POINT aPos)
    {

        foreach(var path in aFiles)
        {
            MessageBoxV2.AddMessage(path);
            var extension = Path.GetExtension(path);
            var fileName = Path.GetFileName(path);
            var md5 = Utils.GetMD5HashFromFile(path);
            TypeEventSystem.Send(new FilePathInfo()
            {
                FilePath = path,
                Extension = extension,
                FileName = fileName,
                MD5 = md5
            });
        }
    }
}
