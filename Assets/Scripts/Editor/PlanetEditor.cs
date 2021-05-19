using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Planet))]
public class PlanetEditor : Editor {

    Planet planet;
    //Var qui sauvegarde les éditeurs
    Editor shapeEditor;
    Editor colourEditor;

    /**
     * Permet de mettre à jour l'éditeur
     * 
     */
	public override void OnInspectorGUI()
	{
       
        using (var check = new EditorGUI.ChangeCheckScope())
        {
            base.OnInspectorGUI();
            if (check.changed)
            {
                planet.CreatePlanet();
            }
        }

        //Clic sur le bouton "Generate Planet" qui permet de générer une planète en fonction de ses paramètres
        if (GUILayout.Button("Create"))
        {
            planet.CreatePlanet();
        }

        //Affiche les paramètres de couleur et de shape dans l'inspector
        DrawSettingsEditor(planet.shapeSettings, planet.OnShapeSettingsUpdated, ref planet.shapeSettingsFoldout, ref shapeEditor);
        DrawSettingsEditor(planet.colorParameters, planet.OnColourSettingsUpdated, ref planet.colorParametersFoldout, ref colourEditor);
	}


    /**
     * Permet d'ajouter les champs des paramètres dans l'éditeur
     */
    void DrawSettingsEditor(Object settings, System.Action onSettingsUpdated, ref bool foldout, ref Editor editor)
    {
        if (settings != null)
        {
            //Ajoute les titres au dessus de la liste des paramètres
            foldout = EditorGUILayout.InspectorTitlebar(foldout, settings);
            //Permet de détecter si un paramètre a été changé sur l'éditeur afin de mettre à jour automatiquement l'apparence de la planète
            using (var check = new EditorGUI.ChangeCheckScope())
            {
                //Si on le bouton pour dérouler la liste des paramètres a été actionné
                if (foldout)
                {
                    //Créer une seule fois les éditeurs et les sauvegardes dans les var shapeEditor et colourEditor
                    CreateCachedEditor(settings, null, ref editor);
                    editor.OnInspectorGUI();

                    if (check.changed)
                    {
                        if (onSettingsUpdated != null)
                        {
                            onSettingsUpdated();
                        }
                    }
                }
            }
        }
    }

	private void OnEnable()
	{
        planet = (Planet)target;
	}
}
