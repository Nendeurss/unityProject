using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ColorParameters : ScriptableObject {

    public Gradient gradient;
    public Material planetMaterialModel;

    [HideInInspector]
    public Material planetMaterial;

    public void assign()
    {
        planetMaterial = new Material(planetMaterialModel);
    }
} 
