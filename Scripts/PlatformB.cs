using UnityEngine;

public class PlatformB : PlatformMovementBase
{
    public float platformSpeed = 4f;
    private Vector3 startPoint; 
    private Vector3 endPoint; 
    private bool movingToEnd = true;

    protected override void Start()
    {
       
        base.Start();
        startPoint = transform.position;
        endPoint = startPoint - Vector3.forward * 6f;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        
    }

    public override void MovePlatform()
    {
        Vector3 targetPoint = (movingToEnd) ? endPoint : startPoint;
        Vector3 movementDirection = (targetPoint - transform.position).normalized;
        float movementDistance = platformSpeed * Time.fixedDeltaTime;
        transform.Translate(movementDirection * movementDistance);
        if (movingToEnd && Vector3.Distance(transform.position, endPoint) < 0.1f)
        {
            movingToEnd = false;
        }
        else if (!movingToEnd && Vector3.Distance(transform.position, startPoint) < 0.1f)
        {
            movingToEnd = true;
        }
    }
}
