using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Node targetNode;
    [SerializeField] List<Node> targetNodeList;

    [SerializeField] float speed;

    int nodeCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        targetNodeList = GameManager.Instance.FinalNodeList;

        nodeCount = 0;
        transform.position = new Vector3(targetNodeList[nodeCount].x, targetNodeList[nodeCount].y, 0);

        nodeCount++;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetNodeList[nodeCount].x, targetNodeList[nodeCount].y, 0), Time.deltaTime * speed);

        if(transform.position == new Vector3(targetNodeList[nodeCount].x, targetNodeList[nodeCount].y, 0))
        {
            if (nodeCount == targetNodeList.Count - 1)
                return;

            nodeCount++;
        }
    }
}
