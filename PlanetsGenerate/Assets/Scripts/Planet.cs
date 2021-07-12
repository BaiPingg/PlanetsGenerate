using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [Range(2,256)]
    public int resolution =10;
    public bool autoUpdated = true;

    public ShapeSettings shapSettings;
    public ColorSettings colorSettings;

    [HideInInspector]public bool shapSettingsFoldout;
    [HideInInspector]public bool colorSettingsFoldout;

    ShapeGenerator shapeGenerator;

    [SerializeField,HideInInspector]
    MeshFilter[] meshFilters;
    TerrainFace[] terrainFaces;

   
    void Initialize()
    {
        shapeGenerator = new ShapeGenerator(shapSettings);
        if (meshFilters==null || meshFilters.Length ==0)
        {
            meshFilters = new MeshFilter[6];
        }

        terrainFaces = new TerrainFace[6];
        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };
        for (int i = 0; i < meshFilters.Length; i++)
        {
            if (meshFilters[i] == null)
            {
                GameObject obj = new GameObject("mesh");
                obj.transform.parent = transform;

                obj.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
                meshFilters[i] = obj.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
            }
            terrainFaces[i] = new TerrainFace(shapeGenerator,meshFilters[i].sharedMesh, resolution, directions[i]);
        }
    }

    public void GeneratePlanet()
    {
        Initialize();
        GenerateMesh();
        Generateolor();
    }
    public void OnShapeSettingUpdated()
    {
        if (autoUpdated)
        {
            Initialize();
            GenerateMesh();
        }
     
    }
    public void OnColorSettingUpdated()
    {
        if (autoUpdated)
        {
            Initialize();
            Generateolor();
        }
    }
    void GenerateMesh()
    {
        foreach (var terrainFace in terrainFaces)
        {
            terrainFace.ConstructMesh();
        }
    }

    void Generateolor()
    {
        foreach (var m in meshFilters)
        {
            m.GetComponent<MeshRenderer>().sharedMaterial.color = colorSettings.plaetColor;

        }
    }
}
