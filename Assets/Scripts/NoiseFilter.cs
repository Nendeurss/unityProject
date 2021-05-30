using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseFilter {

    //Paramètre du bruit
    NoiseSettings settings;
    //Nouveau bruit
    Noise noise = new Noise();

    public NoiseFilter(NoiseSettings settings)
    {
        this.settings = settings;
    }

    //On prend un point en paramètre et on calcule son niveau de bruit
    public float Evaluate(Vector3 point)
    {
        float noiseValue = 0;
        float frequency = settings.baseRoughness;
        float amplitude = 1;

        //En fonction du nombre de couche de bruit, on recalcul le niveau de bruit du point
        for (int i = 0; i < settings.numLayers; i++)
        {
            float v = noise.Evaluate(point * frequency + settings.centre);
            noiseValue += (v + 1) * .5f * amplitude;
            frequency *= settings.roughness;
            amplitude *= settings.persistence;
        }

        noiseValue = Mathf.Max(0, noiseValue - settings.minValue);
        return noiseValue * settings.strength;
    }
}
