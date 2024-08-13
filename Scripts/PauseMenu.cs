using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private SceneController sceneController;
    private PlayerController controller;

    public void Awake()
    {
        sceneController = GetComponent<SceneController>();
        controller = GetComponent<PlayerController>();
    }
    public void OnResumeButtonClick()
    {
        controller.pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OnRestartButtonClick()
    {
        sceneController.RestartScene();
        Time.timeScale = 1f;
    }

   
    public void OnMainMenuButtonClick()
    {      
        sceneController.MainMenuScene();
    }
}
