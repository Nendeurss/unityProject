using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator {

    ShapeSettings settings;
    NoiseFilter[] noiseFilters;
    public MinMax elevationMinMax;

    //On va chercher les paramètres de chaque niveau de bruit
    public void UpdateSettings(ShapeSettings settings)
    {
        this.settings = settings;
        noiseFilters = new NoiseFilter[settings.noiseLayers.Length];
        for (int i = 0; i < noiseFilters.Length; i++)
        {
            noiseFilters[i] = new NoiseFilter(settings.noiseLayers[i].noiseSettings);
        }
        elevationMinMax = new MinMax();
    }

    /**
     * Prend un point (vertice) sur la planète et calcul ses niveaux de bruits
     * 
     * 
     */
    public Vector3 ComputePoint(Vector3 vertice)
    {
        float firstLayerValue = 0;
        float elevation = 0;

        if (noiseFilters.Length >= 1)
        {
            //Permet de créer un niveau de bruit
            firstLayerValue = noiseFilters[0].Evaluate(vertice);
            if (settings.noiseLayers[0].enabled)
            {
                elevation = firstLayerValue;
            }
        }

        for (int i = 1; i < noiseFilters.Length; i++)
        {
            if (settings.noiseLayers[i].enabled) 
            {
                //Permet de garder la première couche de bruit comme masque, et donc de ne pas avoir de nouvelles zones de bruits.
                float mask = (settings.noiseLayers[i].useFirstLayerAsMask) ? firstLayerValue : 1;
                elevation += noiseFilters[i].Evaluate(vertice) * mask;
            }
        }
        elevation = settings.planetRadius * (1 + elevation);
        elevationMinMax.AddValue(elevation);
        return vertice * elevation;
    }
}
 