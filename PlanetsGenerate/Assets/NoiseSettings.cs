using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoiseSettings 
{

    //强度
    public float strength = 1;
    //粗糙度
    public float roughness = 1;
    //中心
    public Vector3 centre;
    [Range(1,8)]
    public int numLayers = 1;
    //振幅衰减
    public float persistence = .5f;
    public float baseRoughness = 2;
    public float minValue;

}
