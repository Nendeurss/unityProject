using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {


    [HideInInspector]
    MeshFilter[] meshFilters;
    MeshCollider[] meshColliders;
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

    //Id de la planète
    public int planetId;

	void Initialize()
    {
        //Initialisation des paramètres de forme et de couleur des planètes
        shapeGenerator.UpdateSettings(shapeSettings);
        colorCreator.UpdateSettings(colorParameters,planetId);

        //Si la planète n'a pas encore été construite, on construit ses mesh
        if (meshFilters == null || meshFilters.Length == 0)
        {
            meshFilters = new MeshFilter[6];
        }

        //On créer les 6 faces de la planète
        planetFaces = new PlanetFace[6];

        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

        for (int i = 0; i < 6; i++)
        {
            //On créer les mesh
            if (meshFilters[i] == null)
            {
                GameObject meshObj = new GameObject("face"+i);
                meshObj.transform.parent = transform;
                
                meshObj.AddComponent<MeshRenderer>();
                meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();

                //On créer les mesh collider pour pouvoir marcher dessus    
                meshColliders[i] = meshObj.AddComponent<MeshCollider>();
                meshColliders[i] = new MeshCollider();
            }
            meshFilters[i].GetComponent<MeshRenderer>().sharedMaterial = colorParameters.planetMaterial;

            planetFaces[i] = new PlanetFace( meshFilters[i].sharedMesh, resolution, directions[i], shapeGenerator);
        
        }


    }


    //Fonction lié au bouton Create
    //Créer la planète
    public void CreatePlanet()
    {
        Initialize();
        CreateMesh();
        GenerateColours();
    }

    //Créer les mesh de la planète
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

    //Génère la couleur de la planète
    void GenerateColours()
    {
        colorCreator.UpdateColours();
    }

    //Mis à jour de la forme de la planète à la modification d'un de ses paramètres
    public void OnShapeSettingsUpdated()
    {
        Initialize();
        CreateMesh();
    }

    //Mis à jour de la couleur de la planète à la modification d'un de ses paramètres
    public void OnColourSettingsUpdated()
    {
        Initialize();
        GenerateColours();
    }
}

