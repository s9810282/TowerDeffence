using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterType
{
    TypeA = 1,
    TypeB = 2,
    TypeC = 3,
}

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


    [SerializeField] GameObject monster_A_PoolingPrefab;
    [SerializeField] GameObject monster_B_PoolingPrefab;
    [SerializeField] GameObject monster_C_PoolingPrefab;


    [SerializeField] Transform parent;
    [SerializeField] int count;


    Queue<GameObject> monster_A_PoolingQueue = new Queue<GameObject>();
    Queue<GameObject> monster_B_PoolingQueue = new Queue<GameObject>();
    Queue<GameObject> monster_C_PoolingQueue = new Queue<GameObject>();


    private void Awake()
    {
        Initialize(count);
    }

    private void Initialize(int initCount)
    {
        for (int i = 0; i < initCount; i++)
        {
            monster_A_PoolingQueue.Enqueue(CreateNewObject(monster_A_PoolingPrefab));
            monster_B_PoolingQueue.Enqueue(CreateNewObject(monster_B_PoolingPrefab));
            monster_C_PoolingQueue.Enqueue(CreateNewObject(monster_C_PoolingPrefab));
        }
    }

    private GameObject CreateNewObject(GameObject poolingPrefab = null, MonsterType monsterType = MonsterType.TypeA)
    {
        GameObject createMonster = null;

        if (poolingPrefab != null)
            createMonster = poolingPrefab;
        else
        {
            switch(monsterType)
            {
                case MonsterType.TypeA:
                    createMonster = monster_A_PoolingPrefab;
                    break;

                case MonsterType.TypeB:
                    createMonster = monster_B_PoolingPrefab;
                    break;

                case MonsterType.TypeC:
                    createMonster = monster_C_PoolingPrefab;
                    break;
            }
        }

        GameObject newObj = Instantiate(createMonster);
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(parent);
        return newObj;
    }

    public GameObject GetObject(MonsterType monsterType)
    {

        Queue<GameObject> monsterPoolingQueue = new Queue<GameObject>();

        switch (monsterType)
        {
            case MonsterType.TypeA:
                monsterPoolingQueue = monster_A_PoolingQueue;
                break;

            case MonsterType.TypeB:
                monsterPoolingQueue = monster_B_PoolingQueue;
                break;

            case MonsterType.TypeC:
                monsterPoolingQueue = monster_C_PoolingQueue;
                break;
        }

        if (monsterPoolingQueue.Count > 0)
        {
            var obj = monsterPoolingQueue.Dequeue();
            obj.transform.SetParent(parent);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = Instance.CreateNewObject(null, monsterType);
            newObj.gameObject.SetActive(true);
            newObj.transform.SetParent(parent);
            return newObj;
        }
    }

    public void ReturnObject(GameObject obj, MonsterType monsterType)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(parent);

        switch (monsterType)
        {
            case MonsterType.TypeA:
                monster_A_PoolingQueue.Enqueue(obj);
                break;

            case MonsterType.TypeB:
                monster_B_PoolingQueue.Enqueue(obj);
                break;

            case MonsterType.TypeC:
                monster_C_PoolingQueue.Enqueue(obj);
                break;
        }     
    }

}
