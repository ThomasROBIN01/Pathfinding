using UnityEngine;
using System.Collections.Generic;

public class BreadthFirstSearch : MonoBehaviour
{
    // starting point

    [SerializeField] Node startNode;

    // target node / destination
    [SerializeField] Node targetNode;

    // list of nodes that have been visited
    [SerializeField] List<Node> visitedNodes = new List<Node>();

    // List of nodes in queue
    [SerializeField] List<Node> nodeQueue = new List<Node>();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BreadthFirstAlgorithm();
    }


    private void BreadthFirstAlgorithm()
    {
        // add 'start node' to nodeQueue

        nodeQueue.Add(startNode);

        bool foundTarget = false;

        
        // enter while loop: 'have we found target node?'
        while(foundTarget == false)
        {            
            // 'pop' first item in nodeQueue if nodeQueue is not empty            
            if (nodeQueue.Count != 0)
            {
                Node activeNode = nodeQueue[0];

                // pop = check if each neighbour of first item in queue (active node) has been added to nodeQueue
                for (int i = 0; i < activeNode.neighbours.Count; i++) // for every neighbours
                {
                    Node activeNeighbour = activeNode.neighbours[i];

                    // if not in queue and not in the visited queue: add neighbour to queue
                    if(!nodeQueue.Contains(activeNeighbour))     // if not in queue
                    {
                        if(!visitedNodes.Contains(activeNeighbour)) // if not in the visited queue
                        {
                            nodeQueue.Add(activeNeighbour);  // add neighbour to queue
                        }
                    }

                }

                // then: check if 'active node' is target
                if (activeNode == targetNode)
                {
                    // if yes: destination found, kill while loop
                    foundTarget = true;
                    continue;
                }
                else
                {
                    // if no: move 'active node' out of queue and into 'visitedNodes', then continue
                    nodeQueue.Remove(activeNode);
                    visitedNodes.Add(activeNode);
                }

            }

        }

    }

}
