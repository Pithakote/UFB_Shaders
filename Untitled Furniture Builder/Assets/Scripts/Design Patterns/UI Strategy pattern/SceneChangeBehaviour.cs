using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChangeBehaviour : IButtonInteractable
{
    SceneAsset _nextSceneToLoad;

    public SceneChangeBehaviour(SceneAsset nextSceneToLoad)
    {
        _nextSceneToLoad = nextSceneToLoad;
    }
    public void ButtonBehaviour()
    {
        if (_nextSceneToLoad == null)
            return;

        LoadNextScene();
    }
   
    public void LoadNextScene()
    {
       // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene(_nextSceneToLoad.name);
    }
}
