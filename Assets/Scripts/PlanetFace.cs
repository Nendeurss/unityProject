using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetFace {

    //Mesh auquel la face est associée
    Mesh mesh;
    //Niveau de détail de la face (doit être la même partout)
    int resolution;
    //Normal de la face
    Vector3 faceNormal;
    //Les axes X et Y de chaque faces
    Vector3 firstAxis;
    Vector3 secondAxis;

    ShapeGenerator shapeGenerator;

    //Constructeur
    public PlanetFace(Mesh mesh, int resolution, Vector3 faceNormal, ShapeGenerator shapeGenerator)
    {
        
        this.mesh = mesh;
        this.resolution = resolution;
        this.faceNormal = faceNormal;
        this.shapeGenerator = shapeGenerator;
        
        firstAxis = new Vector3(faceNormal.y, faceNormal.z, faceNormal.x);
        secondAxis = Vector3.Cross(faceNormal, firstAxis);
    }

    public void ConstructMesh()
    {
        Vector3[] vertices = new Vector3[resolution * resolution];
        int[] triangles = new int[(resolution - 1) * (resolution - 1) * 2 * 3];
        int triangleId = 0;

        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                int i = x + y * resolution;
                Vector2 ratio = new Vector2(x, y) / (resolution - 1);
                //On n'oublie pas de normaliser pour avoir la forme arrondie

                Vector3 oneVertice = (faceNormal + (ratio.x - .5f) * 2 * firstAxis + (ratio.y - .5f) * 2 * secondAxis).normalized;
                vertices[i] = shapeGenerator.ComputePoint(oneVertice);

                //On dessine la face
                if (x != resolution - 1 && y != resolution - 1)
                {
                    triangles[triangleId] = i;
                    triangles[triangleId + 1] = i + resolution + 1;
                    triangles[triangleId + 2] = i + resolution;

                    triangles[triangleId + 3] = i;
                    triangles[triangleId + 4] = i + 1;
                    triangles[triangleId + 5] = i + resolution + 1;
                    triangleId += 6;
                }
            }
        }

        //Tout effacer pour reconstruire le Mesh
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        
    }
}
