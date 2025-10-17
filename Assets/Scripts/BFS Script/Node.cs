using UnityEngine;
using System.Collections.Generic;

public class Node : MonoBehaviour
{
    public List<Node> neighbours = new List<Node>();

    private Node parentNode;

    private void Start()
    {
        FindNeighbours();

        EventManager.clearAllParentsEvent += ClearParent;   // Subscribe
    }

    private void OnDestroy()    // Unsubscribe
    {
        EventManager.clearAllParentsEvent -= ClearParent;
    }

    private void FindNeighbours()
    {
        // Find out which object are adjacent / nearby this one
        Collider[] objectsNearMe = Physics.OverlapBox(transform.position, transform.localScale / 1.8f);

        // assign the north neighbour
        foreach (Collider obj in objectsNearMe)
        {
            if (obj.TryGetComponent(out Node node))
            {
                if (node.transform.position.z == transform.position.z + 1 && node.transform.position.x == transform.position.x)
                {
                    neighbours.Add(node);
                }
            }
        }

        // assign the east neighbour
        foreach (Collider obj in objectsNearMe)
        {
            if (obj.TryGetComponent(out Node node))
            {
                if (node.transform.position.x == transform.position.x + 1 && node.transform.position.z == transform.position.z)
                {
                    neighbours.Add(node);
                }
            }
        }

        // assign the south neighbour
        foreach (Collider obj in objectsNearMe)
        {
            if (obj.TryGetComponent(out Node node))
            {
                if (node.transform.position.z == transform.position.z - 1 && node.transform.position.x == transform.position.x)
                {
                    neighbours.Add(node);
                }
            }
        }

        // assign the west neighbour
        foreach (Collider obj in objectsNearMe)
        {
            if (obj.TryGetComponent(out Node node))
            {
                if (node.transform.position.x == transform.position.x - 1 && node.transform.position.z == transform.position.z)
                {
                    neighbours.Add(node);
                }
            }
        }
    }

    // set parent   // The parents will be used to define the path
    public void SetParentNode (Node _parent)
    {
        parentNode = _parent;
    }

    // get parent
    public Node GetParentNode () 
    { 
        return parentNode;
    }

    // clear parent
    private void ClearParent ()
    {
        parentNode = null;
    }
}
