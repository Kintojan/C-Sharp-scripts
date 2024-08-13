using UnityEngine;

public class PlatformA : PlatformMovementBase
{
    public float platformSpeed = 4f;
    private Vector3 startPoint; 
    private Vector3 endPoint; 
    private bool movingToEnd = true;

    protected override void Start()
    {
        base.Start();
        startPoint = transform.position;
        endPoint = startPoint - Vector3.right * 4f;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate(); 
        
        
    }

    public override void MovePlatform()
    {   
        Vector3 movementDirection = (movingToEnd) ? Vector3.left : Vector3.right;
        float movementDistance = platformSpeed * Time.fixedDeltaTime;
        transform.Translate(movementDirection * movementDistance);
        if (movingToEnd && transform.position.x <= endPoint.x)
        {
            movingToEnd = false;
        }
        else if (!movingToEnd && transform.position.x >= startPoint.x)
        {
            movingToEnd = true;
        }
    }
}
