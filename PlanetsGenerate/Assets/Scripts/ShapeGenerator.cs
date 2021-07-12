using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator 
{
    ShapeSettings settings;
    NoiseFilter[] noiseFilters;
    public ShapeGenerator(ShapeSettings settings)
    {
        this.settings = settings;
        noiseFilters = new NoiseFilter[settings.noiseLayers.Length];
        for (int i = 0; i < noiseFilters.Length; i++)
        {
            noiseFilters[i] = new NoiseFilter(settings.noiseLayers[i].noiseSettings);
        }
    }
    public Vector3 CalculatePointOnPlanet(Vector3 pointOnShpere)
    {
        float firstLayerValue = 0;
        float evelation = 0;

        if (noiseFilters.Length>0)
        {
            firstLayerValue = noiseFilters[0].Evalute(pointOnShpere);
            if (settings.noiseLayers[0].enabled)
            {
                evelation = firstLayerValue;

            }
        }
       
        for (int i = 1; i < noiseFilters.Length; i++)
        {
            if (settings.noiseLayers[i].enabled)
            {
                float mask = settings.noiseLayers[i].usedFirstLayerMask ? firstLayerValue : 0;
                evelation += noiseFilters[i].Evalute(pointOnShpere) * mask;
            }
           
        }
        return pointOnShpere * settings.planetRadius*(1+ evelation);
    }
}
