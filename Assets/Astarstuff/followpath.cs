using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followpath : MonoBehaviour
{
    public float speed = 5f;
    private int currentWaypoint = 0;
    private List<Node> path = new List<Node>();

    public Material mat;
    private Pathfinder pf;
    public Vector2Int startPos, endPos;
    public GridGenerator grid;
    public List<Node> fpath;
    private int currentPathIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        pf = new Pathfinder();
        pf.grid = grid;
        path = pf.FindPath(startPos, endPos);
        if (path != null)
        {
            foreach (var node in path)
            {
                node.ngj.GetComponent<MeshRenderer>().material = mat;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (path != null && currentPathIndex < path.Count) //if there is a path to be followed
        {
            Vector3 targetPosition = path[currentPathIndex].nodeworldPosition; //set position 
            float distance = Vector3.Distance(transform.position, targetPosition);
            if (distance < 0.1f)
            {
                currentPathIndex++;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            }
        }
    }
}
