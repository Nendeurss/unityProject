using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoiseSettings {

    //Force du bruit
    public float strength = 1;
    [Range(1,8)]
    //Nombre de couche de bruit
    public int numLayers = 1;
    
    public float baseRoughness = 1;
    public float roughness = 2;
    public float persistence = .5f;
    public Vector3 centre;
    public float minValue;
}
