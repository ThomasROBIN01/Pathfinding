using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using JetBrains.Annotations;

public class BreadthFirstSearch : MonoBehaviour
{
    // starting point

    [SerializeField] Node startNode;

    // target node / destination
    [SerializeField] Node targetNode;

    // Highlighted node with mouse
    [SerializeField] Node highlightedNode;

    // list of nodes that have been visited
    [SerializeField] List<Node> visitedNodes = new List<Node>();

    // List of nodes in queue
    [SerializeField] List<Node> nodeQueue = new List<Node>();

    // Lisat of nades that will define the path
    [SerializeField] List<Node> nodePath = new List<Node>();

    // create new instance of InputSystem_Actions
    // InputSystem_Actions can be found in the assets roots folder in Unity: we added a new one in Action Maps / UI / Submit 
    private InputSystem_Actions playerActions;

    [SerializeField] Material redMat, blueMat, greenMat, yellowMat;


    private void Awake()
    {
        // Initialize the InputSystem_Action
        playerActions = new InputSystem_Actions();
    }


    private void OnEnable()
    {
        // make sure that the playerActions 'Player' action map is enabled
        playerActions.UI.Enable();
    }

    private void Update()
    {
        // check if the 'Submit' input was pressed this frame
        if (playerActions.UI.Submit.WasPressedThisFrame())
        {
            BreadthFirstAlgorithm();
        }

        DetectNodeUnderCursor();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // BreadthFirstAlgorithm();
    }

    private void SetStartingNode (Node _startNode)
    {
        startNode = _startNode;

        // the start node in blue:
        MeshRenderer startNodeMeshRenderer = startNode.GetComponent<MeshRenderer>();
        startNodeMeshRenderer.material = blueMat;
    }

    private void SetTargetNode (Node _targetNode)
    {
        targetNode = _targetNode;

        // the target node in green:
        MeshRenderer targetNodeMeshRenderer = targetNode.GetComponent<MeshRenderer>();
        targetNodeMeshRenderer.material = greenMat;
    }

    private void DetectNodeUnderCursor ()   // to assign start and target nodes on mouse clicks
    {
        // convert the screen position of the cursor (mouse) into world space
        // draw a raycast from cursor to game environment
        Ray rayFromCursorToGameQWorld = Camera.main.ScreenPointToRay(playerActions.UI.Point.ReadValue<Vector2>());  // ray from the cursor heading to the game environment

        RaycastHit hit;

        if (Physics.Raycast(rayFromCursorToGameQWorld, out hit))
        {
            // detect any object with 'Node' attached it under cursor
            if (hit.collider.TryGetComponent(out Node _node))
            {
                // make that 'Node' the 'highlighted' node
                highlightedNode = _node;
            }
            else
            {
                highlightedNode = null;
            }
        }
        else
        {
            highlightedNode = null;     // if the raycast doesn't touch anything
        }


        // if highlighted node isn't null
        if (highlightedNode != null)
        {
            // if left click: assign highligted node as starting node
            if (playerActions.UI.SetStart.WasPressedThisFrame())
            {
                SetStartingNode(highlightedNode);
            }
            // if right click: assign highligted node as ending node
            else if (playerActions.UI.SetTarget.WasPressedThisFrame())
            {
                SetTargetNode(highlightedNode);
            }

        }

    }

    private void BreadthFirstAlgorithm()
    {
        // call the 'clear all parents' event
        EventManager.clearAllParentsEvent();

        // To visualisy represent the nodes affected, we will attribute some colours to the nodes:

        //// the start node in blue:
        //MeshRenderer startNodeMeshRenderer = startNode.GetComponent<MeshRenderer>();
        //startNodeMeshRenderer.material = blueMat;

        //// the target node in green:
        //MeshRenderer targetNodeMeshRenderer = targetNode.GetComponent<MeshRenderer>();
        //targetNodeMeshRenderer.material = greenMat;

        // Clearing previous data
        nodeQueue.Clear();
        visitedNodes.Clear();

        // add 'start node' to nodeQueue
        nodeQueue.Add(startNode);


        // enter while loop: 'have we found target node?'
        bool foundTarget = false;

        while (foundTarget == false)
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

                            if (activeNeighbour.GetParentNode() == null)
                            {
                                activeNeighbour.SetParentNode(activeNode);
                            }                            
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
                    if (activeNode != startNode)
                    {
                        // turn a visited node in red once we confirm it isn't the target
                        MeshRenderer visitedNodeRenderer = activeNode.GetComponent<MeshRenderer>();
                        visitedNodeRenderer.material = redMat;
                    }


                    // if no: move 'active node' out of queue and into 'visitedNodes', then continue
                    nodeQueue.Remove(activeNode);
                    visitedNodes.Add(activeNode);
                }

            }

        }

        GeneratePath();

    }

    private void GeneratePath()
    {
        // clear the list of any data that has been generated previously
        nodePath.Clear();

        // add the target node to the 'nodePath' list
        Node activeNode = targetNode;

        // enter loop: while path isn't complete
        bool pathComplete = false;

        while (!pathComplete)
        {
            // add the active node to the 'nodePath' list
            nodePath.Add(activeNode);

            // To colour the path in yellow:
            if (activeNode != startNode && activeNode != targetNode)
            {
                MeshRenderer pathNodeRenderer = activeNode.GetComponent<MeshRenderer>();
                pathNodeRenderer.material = yellowMat;
            }


            // check if active node is the starting node
            if (activeNode == startNode)
            {
                // yes: kill the loop, path is complete
                pathComplete = true;
                continue;
            }
            else
            {
                // no: make parent node = active node
                activeNode = activeNode.GetParentNode();
            }
        }       

    }

}
