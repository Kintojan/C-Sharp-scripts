using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelFinishedMenu : MonoBehaviour
{
    private SceneController sceneController;
    private Stopwatch stopwatch;

    private TextMeshProUGUI l1BestText;
    private TextMeshProUGUI l2BestText; 
    private TextMeshProUGUI levelTimeText;

    private int levelIndex;
    private float levelTime;
    private float l1Best;
    private float l2Best;
    private float topTimes;


    private void Start()
    {
       
        sceneController = GetComponent<SceneController>();
        stopwatch = GetComponent<Stopwatch>();

        levelTimeText = GameObject.Find("LevelTimeText").GetComponent<TextMeshProUGUI>();
        l1BestText = GameObject.Find("L1BestText").GetComponent<TextMeshProUGUI>();
        l2BestText = GameObject.Find("L2BestText").GetComponent<TextMeshProUGUI>();
       
        levelTime = stopwatch.GetLastLevelTime();
        l1Best = stopwatch.GetBestLevel1Time();
        l2Best = stopwatch.GetBestLevel2Time();     

        string timeText = levelTime.ToString("F2");
        string bestLevel1 = l1Best.ToString("F2");
        string bestLevel2 = l2Best.ToString("F2");

        levelTimeText.text = $" Time: {timeText} s";
        l1BestText.text = $"Best Time: {bestLevel1} s";
        l2BestText.text = $"Best Time: {bestLevel2} s";
}

    public void OnLevel1ButtonClick()
    {
        sceneController.Load1Scene();
    }

    public void OnLevel2ButtonClick()
    {
        sceneController.Load2Scene();
    }

    public void OnNextLevelButtonClick()
    {
        sceneController.LoadNextScene();
    }

    public void OnRestartButtonClick()
    {
        levelIndex = stopwatch.GetSavedSceneIndex();       
        SceneManager.LoadScene(levelIndex);
    }
    public void OnMainMenuButtonClick()
    {
        sceneController.MainMenuScene();
    }

}
