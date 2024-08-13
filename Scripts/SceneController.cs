using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    int currentSceneIndex;
    public void MainMenuScene()
    {
        SceneManager.LoadScene(0);
    }

    public void FinishedMenuScene()
    {
        SceneManager.LoadScene(1);
    }

    public void Load1Scene()
    {
        SceneManager.LoadScene(2);     
    }

    public void Load2Scene()
    {
        SceneManager.LoadScene(3);
    }
    public void LoadNextScene()
    {
        
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

       
        if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {     
                SceneManager.LoadScene(currentSceneIndex + 2);      
        }
        else
        {
            Debug.LogWarning("No next scene available. Cannot load next scene.");
        }
    }

    public void RestartScene()
    {
       currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentSceneIndex);
    }
}
