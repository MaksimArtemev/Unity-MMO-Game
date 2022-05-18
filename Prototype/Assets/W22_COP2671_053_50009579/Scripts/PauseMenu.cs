using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject PauseMenuUI;
    [SerializeField] GameObject CollectibleUI;
    public static bool GameIsPause = false;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume() 
    {
        PauseMenuUI.SetActive(false);
        CollectibleUI.SetActive(true);
        Time.timeScale = 1f;
        GameIsPause = false;
    }

    public void Pause() 
    {
        PauseMenuUI.SetActive(true);
        CollectibleUI.SetActive(false);
        Time.timeScale = 0f;
        GameIsPause = true;
    }

    public void LoadMenu(int sceneID) 
    {   
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID);
        Debug.Log("Loading Menu...");
    }

    public void RestartGame() 
    {
        Debug.Log("Restarting game...");
    }

    public void QuitGame() 
    {
        Application.Quit();
        Debug.Log("Quitting game...");
    }

}
