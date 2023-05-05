using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UIElements;

public class MeshGen : MonoBehaviour
{
    public Texture2D heightMap;

    public int xscale = 20;
    public int zscale = 20;
    Mesh mesh;
    int[] triangles;
    Vector3[] vert; //each pt a coord

    

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh; //sets the meshfilter component to the mesh we just created.

        CreateTerrain();
        UpdateMesh();
        
    }

    void CreateTerrain()
    {
        //first part is to assign vertice positions. 2nd is for quads
        // Get the height map pixels
        Color[] heightMapPixels = heightMap.GetPixels(); //retturns array of color values.

        //for grid itself
        vert = new Vector3[(xscale + 1) * (zscale + 1)]; // calculates total number of vertices. + 1 due to how squares be dawg

        int index = 0;

        for (int z = 0; z <= zscale; z++) // <= because its zscale + 1
        {
            for (int x = 0; x <= xscale; x++)
            {
                // Get the height map value at this position
                float y = heightMapPixels[z * heightMap.width + x].grayscale * 100f; //It loops through each row and column of vertices in the terrain mesh.
                                                                                     //For each vertex, it calculates its y-coordinate using the grayscale
                                                                                     //the grayscale value of the corresponding pixel in the height map. The grayscale value is multiplied by 100 to scale the height.

                vert[index] = new Vector3(x, y, z); // set the y-coordinate using the height map value
                index++;
            }
        }

        //triangles = new int[6]; //sets the total number of points for one quad

        triangles = new int[xscale * zscale * 6]; //6 points for each quad. x + z dictates number of each quad.

        //for triangles L to R

        int tris = 0;

        int currentvert = 0;

        for (int z = 0; z < zscale; z++) //gens the quads on z axis
        {
            for (int x = 0; x < xscale; x++) //gen the quads on x axis
            {


                triangles[tris + 0] = currentvert + 0;
                triangles[tris + 1] = currentvert + xscale + 1;
                triangles[tris + 2] = currentvert + 1;

                triangles[tris + 3] = currentvert + 1;
                triangles[tris + 4] = currentvert + xscale + 1;
                triangles[tris + 5] = currentvert + xscale + 2;
                //structure goes as follows. triangle vert index [current tri were on used ass offset + vert index] = current vertice were looking at used as offset + placement of vertice to create triangle on x.

                currentvert++; // increments current vert were on
                tris += 6; //adds to the number of triangle verts generated.

                // gens quad without offset.
                //you want to generate this over and over but with an offset to create a plane.
            }

            currentvert++; //skips generating a triangle from one row to another and just moves on to next pt (bugfix)
        }

        //vert = new Vector3[]
        //{
        //    new Vector3 (0,0,0),
        //    new Vector3 (0,0,1),
        //    new Vector3 (1,0,0),
        //    new Vector3 (1, 0, 1)
        //};

        //triangles = new int[]
        //{
        //    0, 1, 2,
        //    1, 3, 2 //make sure its clockwise or normals will be flipped.
        //};
    }

 
    void UpdateMesh()
    {
        if (vert == null)
            return;

        mesh.Clear();
        mesh.vertices = vert;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
