using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class RestartButton : MonoBehaviour
{
    public Button restartButton;
    // Start is called before the first frame update
    void Start()
    {
        Button button = restartButton.GetComponent<Button>();
        button.onClick.AddListener(RestartScene);
    }

    void RestartScene()
    {
        SceneManager.LoadScene("Level 1 (with room)", LoadSceneMode.Single);
    }
}
