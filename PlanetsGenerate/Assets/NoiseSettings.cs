using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoiseSettings 
{

    //ǿ��
    public float strength = 1;
    //�ֲڶ�
    public float roughness = 1;
    //����
    public Vector3 centre;
    [Range(1,8)]
    public int numLayers = 1;
    //���˥��
    public float persistence = .5f;
    public float baseRoughness = 2;
    public float minValue;

}
