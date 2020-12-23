using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private static ObjectPool instance;
    public static ObjectPool Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<ObjectPool>();

            return instance;
        }
    }


    [SerializeField] GameObject poolingObjectPrefab;
    [SerializeField] Transform parent;
    [SerializeField] int count;


    Queue<GameObject> poolingObjectQueue = new Queue<GameObject>();



    private void Awake()
    {
        Initialize(count);
    }

    private void Initialize(int initCount)
    {
        for (int i = 0; i < initCount; i++)
        {
            poolingObjectQueue.Enqueue(CreateNewObject());
        }
    }

    private GameObject CreateNewObject()
    {
        GameObject newObj = Instantiate(poolingObjectPrefab);
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(parent);
        return newObj;
    }



    public GameObject GetObject()
    {
        if (Instance.poolingObjectQueue.Count > 0)
        {
            var obj = Instance.poolingObjectQueue.Dequeue();
            obj.transform.SetParent(parent);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = Instance.CreateNewObject();
            newObj.gameObject.SetActive(true);
            newObj.transform.SetParent(parent);
            return newObj;
        }
    }



    public void ReturnObject(GameObject obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(parent);
        Instance.poolingObjectQueue.Enqueue(obj);
    }

}
