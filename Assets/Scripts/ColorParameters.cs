using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu()]
public class ColorParameters : ScriptableObject {

    public Gradient gradient;
    public Material planetMaterialModel;

    [HideInInspector]
    public Material planetMaterial;

    public void assign(int associatedPlanetId)
    {
        
        planetMaterial = (Material) AssetDatabase.LoadAssetAtPath("Assets/Graphics/Planet Mat" + associatedPlanetId + ".mat", typeof(Material));
        if (planetMaterial == null)
        {
            planetMaterial = new Material(planetMaterialModel);
            AssetDatabase.CreateAsset(planetMaterial, "Assets/Graphics/Planet Mat" + associatedPlanetId + ".mat");
        }
            
       
    }
} 
