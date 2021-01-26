# UntitledFurnitureBuilder

# UntitledFurnitureBuilder

## Template Scene
A template scene has been made available and can be found in Scenes->TemplateSceneOrShaderShowScene->ShaderShowScene(Folder)->ShaderShowScene(Scene).

## Shaders and Materials
* To get started, please use the template Materials available from Materials->ToonMaterials->Templates. 
* Each material that uses ToonURPShader or ToonURPShaderTransparent will automatically have outlines assigned to them. 
      * If the outlines can't be seen, please refer to the material's properties and change the OutlineThickness and OutlineColor to see the changes.
* If you are creating a mesh to be used for the toon shader, please make sure to center the pivot. As the outline derives from the pivot, not the surface normals.
  ### Simple Reasons:
      * Using Normals for outlines created bugs.
      * Using pivot made more sense as a bug free solution. 
* **Do not change the Player prefab**. Especially the main camera and the UI camera. Two cameras have used as a **STACK**, one for rendering just the UI and the other for everything else besides the UI.
  ### Reason:
      * Using single camera created weird depth UI behaviour. If anyone can find a solution, please do let us know. 
* **Do not change the project settings that has anything to do with Render Pipelines**

