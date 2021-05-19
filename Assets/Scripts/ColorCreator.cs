using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCreator {

    ColorParameters settings;
    Texture2D texture;
    const int textureResolution = 50;

    public void UpdateSettings(ColorParameters settings)
    {
        this.settings = settings;
        //Si notre objet de possède pas de texture on lui en fabrique une
        if (texture == null)
        {
            texture = new Texture2D(textureResolution, 1);
        }
    }

    public void UpdateElevation(MinMax elevationMinMax)
    {
        settings.planetMaterial.SetVector("_elevationMinMax", new Vector4(elevationMinMax.Min, elevationMinMax.Max));
    }

    public void UpdateColours()
    {
        Color[] colors = new Color[textureResolution];
        for (int i = 0; i < textureResolution; i++)
        {
            colors[i] = settings.gradient.Evaluate(i / (textureResolution - 1f));
        }
        texture.SetPixels(colors);
        texture.Apply();
        settings.planetMaterial.SetTexture("_texture", texture);
    }
}
