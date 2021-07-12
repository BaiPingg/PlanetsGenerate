using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainFace
{
    ShapeGenerator shapeGenerator;
    Mesh mesh;
    int resolution;
    Vector3 localUp;
    Vector3 axisA;
    Vector3 axisB;

    public TerrainFace(ShapeGenerator shapeGenerator, Mesh mesh, int resolution, Vector3 localUp)
    {
        this.shapeGenerator = shapeGenerator;
        this.mesh = mesh;
        this.resolution = resolution;
        this.localUp = localUp;

        axisA = new Vector3(localUp.y, localUp.z, localUp.x);
        axisB = Vector3.Cross(localUp, axisA);

    }
    public void ConstructMesh()
    {
        Vector3[] vertices = new Vector3[resolution * resolution];
        int[] triangles = new int[(resolution - 1) * (resolution - 1) * 2 * 3];
        int triIdnex = 0;
        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                int i = y * resolution + x;
                Vector2 percent = new Vector2(x, y) / (resolution - 1);
                Vector3 pointOnUnitCube = localUp + (percent.x - .5f) * 2 * axisA + (percent.y - .5f) * 2 * axisB;
                Vector3 pointOnUnitShpere = pointOnUnitCube.normalized;
                vertices[i] = shapeGenerator.CalculatePointOnPlanet(pointOnUnitShpere);

                if (x != resolution - 1 && y != resolution - 1)
                {
                    triangles[triIdnex] = i;
                    triangles[triIdnex + 1] = i + resolution + 1;
                    triangles[triIdnex + 2] = i + resolution;

                    triangles[triIdnex + 3] = i;
                    triangles[triIdnex + 4] = i + 1;
                    triangles[triIdnex + 5] = i + resolution + 1;

                    triIdnex += 6;
                }
            }
        }
        mesh.Clear();
        mesh.vertices = vertices;
        //mesh.normals = SphereNormals(vertices);
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

    }
    Vector3[] SphereNormals(Vector3[] vertices)
    {
        Vector3[] normals = new Vector3[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            normals[i] = vertices[i].normalized;


        }
        return normals;
    }

}