
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Pool
{
    private GameObject ori;

    public Pool(GameObject ori)
    {
        this.ori = ori;
    }

    public List<GameObject> store = new List<GameObject>();
    public List<GameObject> uesd = new List<GameObject>();


    public GameObject GetObj()
    {
        GameObject temp;
        if (store.Count > 0)
        {
            temp = store[0];
            store.RemoveAt(0);
        }
        else
        {
            temp = GameObject.Instantiate(ori);
            var key = temp.AddComponent<PoolKey>();
            key.key = ori.name;
        }
        uesd.Add(temp); 
        return temp;
    }

    public void RestoreObj(GameObject temp)
    { 
        if (uesd.Contains(temp))
        {
            uesd.Remove(temp);
        }
        store.Add(temp);
    }


    public void DestoryObj(GameObject obj)
    {
        if (Application.isPlaying)
        {
            UnityEngine.GameObject.Destroy(obj);
        }
        else
        {
            UnityEngine.Object.DestroyImmediate(obj);
        }
    }


    public void Clear()
    {
        int length = store.Count;
        for (int i = length - 1; i >= 0; i--)
        {
            DestoryObj(store[i]);
        }
        length = uesd.Count;
        for (int i = length - 1; i >= 0; i--)
        {
            DestoryObj(uesd[i]);
        }
        store.Clear();
        uesd.Clear();

    }
}

public class PoolKey : MonoBehaviour
{
    public string key; 
    public int id { get => GetInstanceID(); }
}


public class ObjController : MonoBehaviour
{ 
    public static Dictionary<string, Pool> poolManager = new Dictionary<string, Pool>();

    //����һ����Ϸ����
    public static GameObject CreateObj(GameObject ori)
    {
        GameObject temp;
        if (poolManager.ContainsKey(ori.name) == false)
        {
            poolManager[ori.name] = new Pool(ori);
        }
        temp = poolManager[ori.name].GetObj();
        return temp;
    }

    //����һ����Ϸ����
    public static void Restore(GameObject obj)
    {
        var poolKey = obj.GetComponent<PoolKey>();
        if (poolKey && poolManager.ContainsKey(poolKey.key))
        {
            poolManager[poolKey.key].RestoreObj(obj);
        }
        else
        {
            Debug.Log($"<color=red>{obj.name} ��������޷������գ��Ʋ�1:poolKey==null({poolKey == null}),2:û���������</color>");
        }
    }

    //���ĳ����Ϸ����Ķ���أ�˳��ɾ�����иó�����Ķ���
    public static void ClearObjPool(GameObject obj)
    {
        if (poolManager.ContainsKey(obj.name))
        {
            poolManager[obj.name].Clear();
            poolManager.Remove(obj.name);
        }
        else
        {
            Debug.Log("<color=red>���ʧ��</color>");
        }
    }


    public static void ClearAll()
    {
        foreach (var item in poolManager)
        {
            item.Value.Clear();
        }
        poolManager.Clear();
    }

}
