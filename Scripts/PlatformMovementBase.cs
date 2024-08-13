using System.Collections;
using UnityEngine;

public abstract class PlatformMovementBase : MonoBehaviour
{  
    private PlayerController controller;
    protected virtual void Start()
    {  
        controller = FindObjectOfType<PlayerController>();
    }

    protected virtual void FixedUpdate()
    {
        if (!controller.IsPaused())
        {
            MovePlatform();
        }
    }
    public abstract void MovePlatform();  
}
