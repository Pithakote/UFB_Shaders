using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public Animator animator;
  
   
    private bool AnimatorIsPlaying()
    {
        return animator.GetCurrentAnimatorStateInfo(0).length > animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
                Resume();
            else
                Pause();
        }

        
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        GameIsPaused = false;
        Cursor.visible = false;
        Time.timeScale = 1f;
        MouseLook.canMove = true;
    }

    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        GameIsPaused = true;
        MouseLook.canMove = false;
       //Time.timeScale = 0f;
        //StartCoroutine(setPaused());

    }

    IEnumerator setPaused()
    {
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 0f;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game..");
        Application.Quit();
    }

    public void loadMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }

    

   
}