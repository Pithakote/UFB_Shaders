using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChangeBehaviour : IButtonInteractable
{
    //SceneAsset _nextSceneToLoad;
   // Scene _nextScene;
    string _nextScene;
   
    public SceneChangeBehaviour(string nextScene)
    {
        _nextScene = nextScene;
    }
    public void ButtonBehaviour()
    {
        if (_nextScene == null)
            return;

        LoadNextScene();
    }
   
    public void LoadNextScene()
    {
       // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene(_nextScene);
    }
}
