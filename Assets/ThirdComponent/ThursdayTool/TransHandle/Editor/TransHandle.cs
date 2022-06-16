using System.Collections;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;

public class TransHandle : Editor
{
    [MenuItem("GameObject/设置位置/第一", false, 21)]
    static void ChildFirstSibling()
    {
        Transform trans = Selection.transforms[0];
        if (trans == null)
        {

            Debug.Log("Wrong Path");
            return;
        }
        trans.SetAsFirstSibling();
    }

    [MenuItem("GameObject/设置位置/最后", false, 21)]
    static void ChildLastSibling()
    {
        Transform trans = Selection.transforms[0];
        if (trans == null)
        {

            Debug.Log("Wrong Path");
            return;
        }
        trans.SetAsLastSibling();
    }

    [MenuItem("GameObject/设置位置/清空父物体", false, 21)]
    static void ChildGFree()
    {
        Transform trans = Selection.transforms[0];
        if (trans == null)
        {

            Debug.Log("Wrong Path");
            return;
        }
        trans.parent = null;
        trans.SetAsLastSibling();
    }




}
