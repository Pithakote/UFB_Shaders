using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private void Awake()
    { 
        
        DontDestroyOnLoad(gameObject);
        
    }
    private void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Level 1 (with room)")
            Destroy(gameObject);
    }
}
