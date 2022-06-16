using System.Collections;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;

public class TransHandle : Editor
{
    [MenuItem("GameObject/����λ��/��һ", false, 21)]
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

    [MenuItem("GameObject/����λ��/���", false, 21)]
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

    [MenuItem("GameObject/����λ��/��ո�����", false, 21)]
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
