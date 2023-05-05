using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bungoid : MonoBehaviour
{
    public Material mat;
    private Pathfinder pf;
    public Vector2Int startPos, endPos;
    public GridGenerator grid;
    // Start is called before the first frame update
    void Start()
    {
        pf = new Pathfinder();
        pf.grid = grid;
        List<Node> path = pf.FindPath(startPos, endPos);

        foreach (var node in pf.FindPath(startPos, endPos))
        {
            node.ngj.GetComponent<MeshRenderer>().material = mat;
        }
    }

   
}
