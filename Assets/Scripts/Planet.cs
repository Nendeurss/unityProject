using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {

    [SerializeField, HideInInspector]
    MeshFilter[] meshFilters;
    PlanetFace[] planetFaces;

    [Range(2,256)]
    public int resolution = 10;
 

    public ShapeSettings shapeSettings;
    public ColorParameters colorParameters;

    //Permet de sauvegarder l'état déroulé/enroulé des paramètres Shape et Colour
    [HideInInspector]
    public bool shapeSettingsFoldout;
    [HideInInspector]
    public bool colorParametersFoldout;

    ShapeGenerator shapeGenerator = new ShapeGenerator();
    ColorCreator colorCreator = new ColorCreator();

    /*void Awake()
    {
        CreatePlanet();
        
    }*/

	void Initialize()
    {
        shapeGenerator.UpdateSettings(shapeSettings);
        colorCreator.UpdateSettings(colorParameters);

        if (meshFilters == null || meshFilters.Length == 0)
        {
            meshFilters = new MeshFilter[6];
        }
        planetFaces = new PlanetFace[6];

        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

        for (int i = 0; i < 6; i++)
        {
            if (meshFilters[i] == null)
            {
                GameObject meshObj = new GameObject("face"+i);
                meshObj.transform.parent = transform;
                
                meshObj.AddComponent<MeshRenderer>();
                meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
            }
            meshFilters[i].GetComponent<MeshRenderer>().sharedMaterial = colorParameters.planetMaterial;

            planetFaces[i] = new PlanetFace( meshFilters[i].sharedMesh, resolution, directions[i], shapeGenerator);
        
        }
    }

    public void CreatePlanet()
    {
        Initialize();
        CreateMesh();
        GenerateColours();
    }

    void CreateMesh()
    {
        for (int i = 0; i < 6; i++)
        {
            if (meshFilters[i].gameObject.activeSelf)
            {
                planetFaces[i].ConstructMesh();
            }
        }

        colorCreator.UpdateElevation(shapeGenerator.elevationMinMax);
    }

    void GenerateColours()
    {
        colorCreator.UpdateColours();
    }

    public void OnShapeSettingsUpdated()
    {
        Initialize();
        CreateMesh();
        GenerateColours();
    }

    public void OnColourSettingsUpdated()
    {
        Initialize();
        GenerateColours();
    }
}

