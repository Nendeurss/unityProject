using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ShapeSettings : ScriptableObject {

    //Rayon de la planète
    public float planetRadius = 1;
    public NoiseLayer[] noiseLayers;

    [System.Serializable]

    public class NoiseLayer
    {
        //Variable d'activation du bruit
        public bool enabled = true;
        //Masque du bruit
        public bool useFirstLayerAsMask;
        //Paramètre de bruit
        public NoiseSettings noiseSettings;
    }
}
