using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseFilter 
{
    Noise noise = new Noise();
    NoiseSettings settings;

    public NoiseFilter(NoiseSettings settings)
    {
        this.settings = settings;
    }

    public float Evalute(Vector3 point)
    {
        ///Evaluate ԭ����ֵΪ-1��1 ת��Ϊ0��1
        float noiseVlaue = 0f;
      
        float frequency = settings.baseRoughness;
        float amplitude = 1;

        for (int i = 0; i < settings.numLayers; i++)
        {
            float v = noise.Evaluate(point * frequency + settings.centre);
            noiseVlaue += (v + 1) * .5f * amplitude;
            //frequency>1ʱ��Ƶ����ÿһ������Ӷ�����
            //amplitude<1�������ÿ�β�����Ӷ�����
            frequency *= settings.roughness;
            amplitude *= settings.persistence;
        }
        noiseVlaue = Mathf.Max(0, noiseVlaue - settings.minValue);
        return noiseVlaue*settings.strength;

    }
}
