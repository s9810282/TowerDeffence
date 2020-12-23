using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] Transform startPoint;
    [SerializeField] List<string> monsterList;

    [SerializeField] float spawnDelayTime;



    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartSpawnEnemys());
    }

    // Update is called once per frame
    void Update()
    {
           
    }

    IEnumerator StartSpawnEnemys()
    {
        WaitForSeconds waitTime = new WaitForSeconds(spawnDelayTime);
        
        while(monsterList.Count > 0)
        {
            GameObject monster = ObjectPool.Instance.GetObject();
            monsterList.RemoveAt(0);

            yield return waitTime;
        }
    }
}
