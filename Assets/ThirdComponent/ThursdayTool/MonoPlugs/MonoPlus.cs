using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.Networking;

public static class MonoPlus
{
    public static void SetActive(this Component com, bool flag)
    {
        if (com != null && com.gameObject.activeSelf != flag)
        {
            com.gameObject.SetActive(flag);
        }
    }


    public static bool IsNullOrEmpty(this string str)
    {
        return string.IsNullOrEmpty(str);
    }


    /// <summary>
    /// 异步重载
    /// </summary>
    /// <param name="asyncOp"></param>
    /// <returns></returns>
    public static TaskAwaiter GetAwaiter(this AsyncOperation asyncOp)
    {
        var tcs = new TaskCompletionSource<object>();
        asyncOp.completed += obj => { tcs.SetResult(null); };
        return ((Task)tcs.Task).GetAwaiter();
    }
}