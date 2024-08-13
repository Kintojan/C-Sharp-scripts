using UnityEngine;

public class UpDownPlatformMovement : PlatformMovementBase
{
    public float platformSpeed = 2f;
  
    private Vector3 startPoint; 
    private Vector3 endPoint; 
    private bool movingToEnd = true;
   

    protected override void Start()
    {
        base.Start(); 
        startPoint = transform.position;
        endPoint = startPoint + Vector3.up * 4f;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
       
    }

    public override void MovePlatform()
    {
      
        Vector3 movementDirection = (movingToEnd) ? Vector3.up : Vector3.down; 
        float movementDistance = platformSpeed * Time.fixedDeltaTime;    
        transform.Translate(movementDirection * movementDistance);   
        if (movingToEnd && transform.position.y >= endPoint.y)
        {
            movingToEnd = false;
           
        }
        else if (!movingToEnd && transform.position.y <= startPoint.y)
        {
            movingToEnd = true;
            
        }
    }  
}
