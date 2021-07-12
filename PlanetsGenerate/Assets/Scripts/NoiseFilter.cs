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
        ///Evaluate 原返回值为-1到1 转换为0到1
        float noiseVlaue = 0f;
      
        float frequency = settings.baseRoughness;
        float amplitude = 1;

        for (int i = 0; i < settings.numLayers; i++)
        {
            float v = noise.Evaluate(point * frequency + settings.centre);
            noiseVlaue += (v + 1) * .5f * amplitude;
            //frequency>1时，频率随每一层的增加而增加
            //amplitude<1，振幅随每次层的增加而减少
            frequency *= settings.roughness;
            amplitude *= settings.persistence;
        }
        noiseVlaue = Mathf.Max(0, noiseVlaue - settings.minValue);
        return noiseVlaue*settings.strength;

    }
}
