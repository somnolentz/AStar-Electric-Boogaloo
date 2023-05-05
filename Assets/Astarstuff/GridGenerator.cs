using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public Material openNodeCol;
    public Material currentNodeCol;
    public Material closedNodeCol;

    public GameObject normalAssCube;
    public Node[] nodesInGrid;
    public Vector2 gridsize;
    public Vector3 cellSize;

    Vector3 gridOrigin;
    float offsetX;
    float offsetY;

    void Awake()
    {
        // Create an array of nodes to hold all the nodes in the grid
        nodesInGrid = new Node[(int)(gridsize.x * gridsize.y)];
        for (int i = 0; i < gridsize.x * gridsize.y; i++)
        {
            // Create a new node object for each index in the array
            nodesInGrid[i] = new Node();
        }

        // Set the position of the grid's bottom-left node to the position of the GridGenerator object
        gridOrigin = gameObject.transform.position;

        // Generate the grid
        GenerateGrid();
    }

    void GenerateGrid()
    {
        // Loop through each row of nodes in the grid
        for (int y = 0; y < gridsize.y; y++)
        {
            // Calculate the vertical offset for this row
            offsetY = cellSize.z * y;

            // Loop through each node in this row
            for (int x = 0; x < gridsize.x; x++)
            {
                // Calculate the horizontal offset for this node
                offsetX = cellSize.x * x;

                // Get a reference to the current node object in the array
                Node currentNode = nodesInGrid[(int)(x + y * gridsize.x)]; // < --- converts it to 1D array

                // Set the current node's grid position (x,y)
                currentNode.nodeGridPosition = new Vector2Int(x, y);

                // Calculate the world position of the current node based on its grid position and the grid's origin
                currentNode.nodeworldPosition = new Vector3(gridOrigin.x + offsetX, gridOrigin.y, gridOrigin.z + offsetY);

                // Instantiate a cube at the current node's world position and set its scale to the size of a node
                currentNode.ngj = Instantiate(normalAssCube, currentNode.nodeworldPosition, Quaternion.identity);
                currentNode.ngj.transform.localScale = cellSize;
            }
        }
    }

    public List<Node> GetNeighbours(Node node)
    {
        // Create an empty list to store the neighbouring nodes
        List<Node> neighbours = new List<Node>();

        // Get the grid position of the input node
        Vector2Int gridPos = node.nodeGridPosition;

        // Check if there is a node to the left of the input node
        if (gridPos.x > 0)
        {
            // Get the node to the left of the input node and add it to the list of neighbours
            neighbours.Add(GetNodeFromGridPos(new Vector2Int(gridPos.x - 1, gridPos.y)));
        }

        // Check if there is a node to the right of the input node
        if (gridPos.x < gridsize.x - 1)
        {
            // Get the node to the right of the input node and add it to the list of neighbours
            neighbours.Add(GetNodeFromGridPos(new Vector2Int(gridPos.x + 1, gridPos.y)));
        }

        // Check if there is a node above the input node
        if (gridPos.y < gridsize.y - 1)
        {
            // Get the node above the input node and add it to the list of neighbours
            neighbours.Add(GetNodeFromGridPos(new Vector2Int(gridPos.x, gridPos.y + 1)));
        }

        // Check if there is a node below the input node
        if (gridPos.y > 0)
        {
            // Get the node below the input node and add it to the list of neighbours
            neighbours.Add(GetNodeFromGridPos(new Vector2Int(gridPos.x, gridPos.y - 1)));
        }

        // Return the list of neighbouring nodes
        return neighbours;
    }

    public Node GetNodeFromGridPos(Vector2Int position)
    {
        int index = (int)(position.x + position.y * gridsize.x);

        return nodesInGrid[index];
    }
}
