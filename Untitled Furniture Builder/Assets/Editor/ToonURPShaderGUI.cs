using UnityEngine;
using UnityEditor;
using System;

public class ToonURPShaderGUI : ShaderGUI//is being included in ToonURPShader.shader
{
    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {
        // render the default gui
        base.OnGUI(materialEditor, properties);
        GUILayout.Space(12);
        GUILayout.Label("Main Properties", EditorStyles.boldLabel);

        // foreach (MaterialProperty property in properties)
        //  materialEditor.ShaderProperty(property, property.displayName);
        /*
        Material targetMat = materialEditor.target as Material;

        // see if redify is set, and show a checkbox
        bool specular = Array.IndexOf(targetMat.shaderKeywords, "_SPECULAR_COLOR") != -1;
        EditorGUI.BeginChangeCheck();
        specular = EditorGUILayout.Toggle("Specular material", specular);
        if (EditorGUI.EndChangeCheck())
        {
            // enable or disable the keyword based on checkbox
            if (specular)
                targetMat.EnableKeyword("_SPECULAR_COLOR");
            else
                targetMat.DisableKeyword("_SPECULAR_COLOR");
        }
        */

        MaterialProperty _whichGloss = ShaderGUI.FindProperty("_Gloss", properties);

        MaterialProperty _baseMap = ShaderGUI.FindProperty("_BaseMap", properties);
      
        MaterialProperty _textureColor = ShaderGUI.FindProperty("_TextureColor", properties);

        MaterialProperty _brightness = ShaderGUI.FindProperty("_Brightness", properties);
        MaterialProperty _strength = ShaderGUI.FindProperty("_Strength", properties);
        //  MaterialProperty _diffuse = ShaderGUI.FindProperty("_Diffuse", properties);


       
       

        MaterialProperty _smoothness = ShaderGUI.FindProperty("_Smoothness", properties);

        MaterialProperty _occlusion = ShaderGUI.FindProperty("_Occlusion", properties);

        MaterialProperty _emission = ShaderGUI.FindProperty("_Emission", properties);

        MaterialProperty _scaleAndNumberOfRings = ShaderGUI.FindProperty("_ScaleAndNumberOfRings", properties);


        materialEditor.ShaderProperty(_whichGloss, _whichGloss.displayName);
        materialEditor.ShaderProperty(_baseMap, _baseMap.displayName);
        materialEditor.ShaderProperty(_textureColor, _textureColor.displayName);

        materialEditor.ShaderProperty(_brightness, _brightness.displayName);
        materialEditor.ShaderProperty(_strength, _strength.displayName);

      
     

        materialEditor.ShaderProperty(_smoothness, _smoothness.displayName);

        materialEditor.ShaderProperty(_emission, _emission.displayName);

        materialEditor.ShaderProperty(_scaleAndNumberOfRings, _scaleAndNumberOfRings.displayName);

        if (_whichGloss.floatValue == 0.0f)//for specular
        {
            MaterialProperty _specular = ShaderGUI.FindProperty("_Specular", properties);
            materialEditor.ShaderProperty(_specular, _specular.displayName);
        }
        else if (_whichGloss.floatValue == 1.0f)//for metalic
        {
            MaterialProperty _metallic = ShaderGUI.FindProperty("_Metallic", properties);
            materialEditor.ShaderProperty(_metallic, _metallic.displayName);
        }


      //  materialEditor.ShaderProperty(_diffuse, _diffuse.displayName);
    }
}
