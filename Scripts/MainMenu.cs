using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private GameObject methodCaller;
    private SceneController sceneController;
    private Stopwatch stopwatch;  
    public TextMeshProUGUI L1BestText;
    public TextMeshProUGUI L2BestText;
    public float level1BestTime;
    public float level2BestTime;
    private string bestLevel1;
    private string bestLevel2;

    public void Awake()
    {
        methodCaller = GameObject.Find("MethodCaller");
        stopwatch = methodCaller.GetComponent<Stopwatch>();
        L1BestText = GameObject.Find("L1BestText").GetComponent<TextMeshProUGUI>();
        L2BestText = GameObject.Find("L2BestText").GetComponent<TextMeshProUGUI>();
        level1BestTime = stopwatch.GetBestLevel1Time();
        level2BestTime = stopwatch.GetBestLevel2Time();
        bestLevel1 = level1BestTime.ToString("F2");
        bestLevel2 = level2BestTime.ToString("F2");
        L1BestText.text = $"Best Time: {bestLevel1} s";              
        L2BestText.text = $"Best Time: {bestLevel2} s";       
    }
    
    public void OnLevel1ButtonClick()
    {
        sceneController.Load1Scene();

    }

    public void OnLevel2ButtonClick()
    {
        sceneController.Load2Scene();
    }
}
