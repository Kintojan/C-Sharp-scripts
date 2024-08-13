using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class Stopwatch : MonoBehaviour
{
    //References
    private PlayerMovement playerMovement;
    private PlayerController controller;
    private TextMeshProUGUI stopwatchText;
    //Stopwach variables
    private bool isRunning;
    private bool isFinished;
    private float startTime;
    private float timeRunning;   
    private float levelTime;  
    //Time data management
    public int currentSceneIndex;
    public const string SceneIndexKey = "SceneIndex";
    public const string Level1Key = "Level1";
    public const string Level2Key = "Level2";
    public const string Level1BestKey = "Level1Best";
    public const string Level2BestKey = "Level2Best";
    public const string LevelTimesKeyL1 = "LevelTimesL1";
    public const string LevelTimesKeyL2 = "LevelTimesL2";
    public const string LastLevelTimeKey = "LastLevelTime";
    public List<float> savedTimesL1 = new List<float>();
    public List<float> savedTimesL2 = new List<float>();


    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        playerMovement = GetComponent<PlayerMovement>();
        controller = GetComponent<PlayerController>();
        isRunning = false;
        LoadAllTimes();
        
    }

    void Update()
    {
        
        if (currentSceneIndex != 0 && currentSceneIndex != 1)
        {
            if (playerMovement.isCountdownFinished && !isRunning)
            {
                isRunning = true;
                startTime = Time.time;
            }
            SaveSceneIndex(currentSceneIndex);
            stopwatchText = GameObject.Find("StopwatchText").GetComponent<TextMeshProUGUI>();       
               
           
                if (isRunning)
                {
                    timeRunning = Time.time - startTime;
                    UpdateTimeText();
                }
            

            isFinished = playerMovement.IsPlayerFinished();
            if (isFinished)
            {
                isRunning = false;
                levelTime = timeRunning;

                if (currentSceneIndex == 2)
                {
                    SaveLevel1Time(levelTime);
                    SaveBestLevel1Time(levelTime);
                    SaveLevel1Times(levelTime);
                    SaveLastLevelTime(levelTime);
                }

                if (currentSceneIndex == 3)
                {
                    SaveLevel2Time(levelTime);
                    SaveBestLevel2Time(levelTime);
                    SaveLevel2Times(levelTime);
                    SaveLastLevelTime(levelTime);
                }
            }         
        }           
    }
 
    public void UpdateTimeText()
    {
            string timeText = $"Time: {timeRunning:F2}";
            stopwatchText.text = timeText;     
    }

    //Save data

    private void SaveSceneIndex(int sceneIndex)
    {
        PlayerPrefs.SetInt(SceneIndexKey, sceneIndex);
        PlayerPrefs.Save();
    }
    private void SaveLastLevelTime(float levelTime)
    {
        PlayerPrefs.SetFloat(LastLevelTimeKey, levelTime);
        PlayerPrefs.Save();
    }
    private void SaveLevel1Time(float levelTime)
    {
        PlayerPrefs.SetFloat(Level1Key, levelTime);
        PlayerPrefs.Save();
    }

    private void SaveLevel2Time(float levelTime)
    {
        PlayerPrefs.SetFloat(Level2Key, levelTime);
        PlayerPrefs.Save();
    }

    private void SaveBestLevel1Time(float levelTime)
    {
        float bestTime = PlayerPrefs.GetFloat(Level1BestKey, float.MaxValue);
        if (levelTime < bestTime)
        {
            bestTime = levelTime;
            PlayerPrefs.SetFloat(Level1BestKey, bestTime);
            PlayerPrefs.Save();
            Debug.Log($"New best time achieved for level 1: {bestTime} seconds.");
        }
    }

    private void SaveBestLevel2Time(float levelTime)
    {
        float bestTime = PlayerPrefs.GetFloat(Level2BestKey, float.MaxValue);
        if (levelTime < bestTime)
        {
            bestTime = levelTime;
            PlayerPrefs.SetFloat(Level2BestKey, bestTime);
            PlayerPrefs.Save();
        }
    }
    
    private void SaveLevel1Times(float time)
    {
        List<float> savedTimes = new List<float>();
        LoadSavedTimes(LevelTimesKeyL1, savedTimes);
        savedTimes.Add(time);
        PlayerPrefs.SetString(LevelTimesKeyL1, string.Join(",", savedTimes));
        PlayerPrefs.Save();
    }
    private void SaveLevel2Times(float time)
    {
        List<float> savedTimes = new List<float>();
        LoadSavedTimes(LevelTimesKeyL2, savedTimes);
        savedTimes.Add(time);
        PlayerPrefs.SetString(LevelTimesKeyL2, string.Join(",", savedTimes));
        PlayerPrefs.Save();
    }

    //Retrieve data 
    public float GetBestLevel1Time()
    {
        return PlayerPrefs.GetFloat(Level1BestKey, float.MaxValue);
    }

    public float GetBestLevel2Time()
    {
        return PlayerPrefs.GetFloat(Level2BestKey, float.MaxValue);
    }

    public float GetLastLevelTime()
    {
        return PlayerPrefs.GetFloat(LastLevelTimeKey);
    }

    public void LoadSavedTimes(string key, List<float> savedTimes)
    {
        string savedTimesString = PlayerPrefs.GetString(key, "");
        if (!string.IsNullOrEmpty(savedTimesString))
        {
            string[] savedTimesArray = savedTimesString.Split(',');
            foreach (string savedTimeString in savedTimesArray)
            {
                float savedTime;
                if (float.TryParse(savedTimeString, out savedTime))
                {
                    savedTimes.Add(savedTime);
                }
            }
        }
    }
    public void LoadLevel1Times()
    {
        LoadSavedTimes(LevelTimesKeyL1, savedTimesL1);
    }

    public void LoadLevel2Times()
    {
        LoadSavedTimes(LevelTimesKeyL2, savedTimesL2);
    }

    public void LoadAllTimes()
    {
        float bestLevel1Time = GetBestLevel1Time();
        float bestLevel2Time = GetBestLevel2Time();
        float lastLevelTime = GetLastLevelTime(); 
        LoadLevel1Times();
        LoadLevel2Times();  
        
       // Debug.Log("Last Level Time: " + lastLevelTime);
       // Debug.Log("Best Level 1 Time: " + bestLevel1Time);
       // Debug.Log("Best Level 2 Time: " + bestLevel2Time);

        foreach (float time in savedTimesL1)
        {
           // Debug.Log("Loaded Level 1 Time: " + time);
        }

        foreach (float time in savedTimesL2)
        {
           // Debug.Log("Loaded Level 2 Time: " + time);
        }
    }

    public int GetSavedSceneIndex()
    {
        return PlayerPrefs.GetInt(SceneIndexKey, 0);
    } 
     
}
