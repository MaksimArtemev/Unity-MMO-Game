using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject PlayBtn;
    public void PlayGame(int sceneID) 
    {   
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID);
        Debug.Log("Play game...");
    }

}
