using UnityEngine;

public class PlatformD : PlatformMovementBase
{
    public float rotationSpeed = 30f;
    public GameObject platformD;
    public Transform platformDTransform;
    public float platformSpeed = 5f;
    private Vector3 startPoint;
    private Vector3 endPoint;
    private bool movingToEnd = true;
   
    protected override void Start()
    {
        base.Start();
        startPoint = transform.position;
        endPoint = startPoint - Vector3.right * 15f;
        platformD = GameObject.Find("D");
        platformDTransform = platformD.transform;
        
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        
    }

    public override void MovePlatform()
    {
        platformDTransform.Rotate(0f, rotationSpeed * Time.fixedDeltaTime, 0f, Space.Self);
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
