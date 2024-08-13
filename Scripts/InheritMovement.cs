using UnityEngine;

public class InheritMovement : MonoBehaviour
{
    public GameObject[] platforms;
    private Vector3[] previousPlatformPositions;

    void Start()
    {
        platforms = new GameObject[4];
        platforms[0] = GameObject.Find("A");
        platforms[1] = GameObject.Find("B");
        platforms[2] = GameObject.Find("C");
        platforms[3] = GameObject.Find("D");

        previousPlatformPositions = new Vector3[platforms.Length];

        
        for (int i = 0; i < previousPlatformPositions.Length; i++)
        {
            previousPlatformPositions[i] = Vector3.zero;
        }

        for (int i = 0; i < platforms.Length; i++)
        {
            if (platforms[i] != null)
            {
                previousPlatformPositions[i] = platforms[i].transform.position;
            }
        }
    }

    public Vector3 GetPlatformMovement(int index)
    {
        if (index >= 0 && index < platforms.Length)
        {
            GameObject platform = platforms[index];
            if (platform != null)
            {
                Vector3 currentPlatformPosition = platform.transform.position;
                Vector3 platformMovement = currentPlatformPosition - previousPlatformPositions[index];
                previousPlatformPositions[index] = currentPlatformPosition;
                return platformMovement;
            }
            else
            {
                Debug.LogWarning($"platform{index + 1} is not initialized!");
                return Vector3.zero;
            }
        }
        else
        {
            Debug.LogWarning($"Invalid platform index: {index}. Must be between 0 and {platforms.Length - 1}.");
            return Vector3.zero;
        }
    }

    public Quaternion GetPlatformRotation(int index)
    {
        if (index >= 0 && index < platforms.Length)
        {
            GameObject platform = platforms[index];
            if (platform != null)
            {
                return platform.transform.rotation;
            }
            else
            {
                Debug.LogWarning($"platform{index + 1} is not initialized!");
                return Quaternion.identity;
            }
        }
        else
        {
            Debug.LogWarning($"Invalid platform index: {index}. Must be between 0 and {platforms.Length - 1}.");
            return Quaternion.identity;
        }
    }


    public Vector3 GetCurrentPosition(int index)
    {
        if (index >= 0 && index < platforms.Length)
        {
            GameObject platform = platforms[index];
            if (platform != null)
            {
                return platform.transform.position;
            }
            else
            {
                Debug.LogWarning($"platform{index + 1} is not initialized!");
                return Vector3.zero;
            }
        }
        else
        {
            Debug.LogWarning($"Invalid platform index: {index}. Must be between 0 and {platforms.Length - 1}.");
            return Vector3.zero;
        }
    }

    public Vector3 GetNextPosition(int index)
    {
        if (index >= 0 && index < platforms.Length)
        {
            GameObject platform = platforms[index];
            if (platform != null)
            {
                Vector3 currentPlatformPosition = platform.transform.position;
                Vector3 platformMovement = currentPlatformPosition - previousPlatformPositions[index];
                Vector3 nextPosition = currentPlatformPosition + platformMovement;
                return nextPosition;
            }
            else
            {
                Debug.LogWarning($"platform{index + 1} is not initialized!");
                return Vector3.zero;
            }
        }
        else
        {
            Debug.LogWarning($"Invalid platform index: {index}. Must be between 0 and {platforms.Length - 1}.");
            return Vector3.zero;
        }
    }
}