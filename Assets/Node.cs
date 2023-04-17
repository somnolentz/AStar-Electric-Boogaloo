using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Node
{
    public float hCost; // cost to move from the start node to next node
    public float gCost = Mathf.Infinity; // cost to move from this node to the end node
    public float fCost { get { return gCost + hCost; } } // total cost of the node

    public Vector3 nodeworldPosition;
    public Vector2Int nodeGridPosition;

    public Node parent;

    public GameObject ngj;


}

