using System.Collections;
using System.Collections.Generic;

public class CommonConfig
{
    public static string ServerUrl {
        get {
#if UNITY_EDITOR
            return "http://127.0.0.1:8080/";
#else
            return "http://www.yzqlwt.com:8080/";
#endif

        } }
}
