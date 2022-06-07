using System.Collections;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;

public class PathCopyer : Editor
{
    //[MenuItem("GameObject/HLCTools/打印路径", false, 20)]
    [MenuItem("GameObject/打印路径", false, 20)]
    static void GetGObjPath()
    {
        Transform temTrans = Selection.transforms[0];
        if (temTrans == null)
        {
            Debug.LogError("Wrong Path");
        }
        string path = temTrans.name;
        path = GetPath(path, temTrans.parent);
        Debug.Log(path + "     注意 已经复制到剪贴板 ");
        GUIUtility.systemCopyBuffer = path;
    }


    [MenuItem("ThursdayTools/Copy Select Obj Path", false, 20)]
    static void GetGObjPathTool()
    {
        if (Selection.transforms.Length <= 0)
        {
            Debug.LogError("Wrong Path");
        }
        Transform temTrans = Selection.transforms[0];
        string path = temTrans.name;
        path = GetPath(path, temTrans.parent);
        Debug.Log(path + "     注意 已经复制到剪贴板 ");
        GUIUtility.systemCopyBuffer = path;
    }



    static string GetPath(string s, Transform trans)
    {
        //if (trans != null)  //带 root
        if (trans.parent != null)  //不带 root
        {
            s = trans.name + "/" + s;
            return GetPath(s, trans.parent);
        }
        return s;
    }





}
