using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class ColorCreator{

    ColorParameters settings;
    Texture2D texture;
    int associatedPlanetId;
    const int textureResolution = 50;

    public void UpdateSettings(ColorParameters settings,int associatedPlanetId)
    {
        this.associatedPlanetId = associatedPlanetId;
        this.settings = settings;
        settings.assign(associatedPlanetId);
        //Si notre objet de possède pas de texture dans nos assets on lui en fabrique une
        texture = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/texture"+this.associatedPlanetId+".asset", typeof(Texture2D));
        if (texture == null)
        {
            texture = new Texture2D(textureResolution, 1);
        }
    }

    //Met à jour l'emplacement de la couleur sur la planète
    public void UpdateElevation(MinMax elevationMinMax)
    {
        settings.planetMaterial.SetVector("_elevationMinMax", new Vector4(elevationMinMax.Min, elevationMinMax.Max));
    }

    //Met à jour la couleur
    public void UpdateColours()
    {
        //On parcours toutes les couleurs de notre Gradient et on l'applique sur une texture
        Color[] colors = new Color[textureResolution];
        for (int i = 0; i < textureResolution; i++)
        {
            colors[i] = settings.gradient.Evaluate(i / (textureResolution - 1f));
        }
        texture.SetPixels(colors);
        texture.Apply();
        
        //Une fois la texture faite, on l'applique sur notre planète et on sauvegarde dans nos assets.
        if (AssetDatabase.LoadAssetAtPath("Assets/texture"+associatedPlanetId+".asset",typeof(Texture2D)) != null)
        {
            settings.planetMaterial.SetTexture("_texture",(Texture)AssetDatabase.LoadAssetAtPath("Assets/texture" + associatedPlanetId + ".asset", typeof(Texture2D)));
            AssetDatabase.SaveAssets();
        } else
        {
            AssetDatabase.CreateAsset(texture, "Assets/texture"+associatedPlanetId+".asset");
            settings.planetMaterial.SetTexture("_texture", (Texture)AssetDatabase.LoadAssetAtPath("Assets/texture" + associatedPlanetId + ".asset", typeof(Texture2D)));
        }
        
    }
}
