using UnityEngine;

public class QuitGame : MonoBehaviour
{
   
    public void Quit()
    {
        // If the game is running in the Unity editor
#if UNITY_EDITOR
        // Stop playing the scene
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // For build
        Application.Quit();
#endif
    }
}
