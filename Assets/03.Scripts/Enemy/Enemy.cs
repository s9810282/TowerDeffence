using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Monster;

public class Enemy : MonoBehaviour
{
    [SerializeField] Node targetNode;
    [SerializeField] List<Node> targetNodeList;

    [SerializeField] SpriteRenderer spriteRenderer;

    [SerializeField] EnemyInfo enemyInfo;

    int nodeCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        targetNodeList = PathManager.Instance.FinalNodeList;

        nodeCount = 0;
        transform.position = new Vector3(targetNodeList[nodeCount].x, targetNodeList[nodeCount].y, 0);

        nodeCount++;
    }

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetNodeList[nodeCount].x, targetNodeList[nodeCount].y, 0), Time.deltaTime * enemyInfo.speed);

        if (transform.position == new Vector3(targetNodeList[nodeCount].x, targetNodeList[nodeCount].y, 0))
        {
            if (nodeCount == targetNodeList.Count - 1)
                ObjectPool.Instance.ReturnObject(gameObject, enemyInfo.enemyType);

            nodeCount++;
        }
    }
}
