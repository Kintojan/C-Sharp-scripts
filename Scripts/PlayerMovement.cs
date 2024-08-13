using System;
using TMPro;
using UnityEngine;
using System.Collections;


public class PlayerMovement : MonoBehaviour
{
    //References
    private PlayerController controller;
    private Rigidbody playerRigidbody;
    private InheritMovement inheritMovement;
    private SceneController sceneController;
    //Game objects
    private GameObject platformD;
    private GameObject finish;
    private GameObject player;
    private GameObject arrowPlatform;
    // Movement Variables
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed = 1.3f;
    [SerializeField] private float jumpForce = 5;
    [SerializeField] private float adjustedJumpForce = 5;
    // Platform Movement Vectors
    private Vector3 platformMovementA;
    private Vector3 platformMovementB;
    private Vector3 platformMovementC;
    private Vector3 platformMovementD;
    private Vector3 platformCurrentPositionD;
    private TextMeshProUGUI countdownText;
    //Bools
    public bool isCountdownFinished = false;
    private bool isOnArrowPlatform = false;



    private void Awake()
    {
        player = GameObject.Find("Player");
        finish = GameObject.Find("FinishLane");
        arrowPlatform = GameObject.Find("ArrowPlatform");
        platformD = GameObject.Find("D");
        sceneController = GetComponent<SceneController>();
        controller = GetComponent<PlayerController>();
        playerRigidbody = GetComponent<Rigidbody>();     
        inheritMovement = GetComponent<InheritMovement>();                
    }

    private void Start()
    {
       
        countdownText = GameObject.Find("CountdownText").GetComponent<TextMeshProUGUI>();
        StartCoroutine(StartCountdown());
    }
    private void FixedUpdate()
    {
        AllMovement();
        InheritPlatformMovement();    
    }

    private void Update()
    {
        IsOnArrowPlatform();
        SceneCaller();
    }

    private IEnumerator StartCountdown()
    {
        
        countdownText.text = "3!";
        yield return new WaitForSeconds(1f);
        countdownText.text = "2!";
        yield return new WaitForSeconds(1f);
        countdownText.text = "1!";
        yield return new WaitForSeconds(1f);
        countdownText.text = "Go!";
        countdownText.gameObject.SetActive(false);
        isCountdownFinished = true;
      
    }

    public void AllMovement()
    {
        if (isCountdownFinished)
        {
            Movement();
            Rotation();
        }
    }

    private void Movement()
    {     
        Vector3 moveDir = new Vector3(0f, 0f, controller.verticalInput);
        moveDir.Normalize();    
        Vector3 targetPos = transform.position + transform.forward * moveDir.z * movementSpeed * Time.fixedDeltaTime;
        playerRigidbody.MovePosition(targetPos);

    }

    private void Rotation()
    {     
       transform.Rotate(Vector3.up, controller.horizontalInput * rotationSpeed);
    }

    public void Jump()
    {   
        if (IsGrounded() && isCountdownFinished)
        {           
                playerRigidbody.AddForce(Vector3.up * adjustedJumpForce, ForceMode.Impulse);
        }
    }

    public bool IsGrounded()
    {
        if (this == null || transform == null)
        {
            return false;
        }
        return Physics.Raycast(transform.position, Vector3.down, 0.1f);
    }

    public bool IsInput()
    {
        return controller.verticalInput != 0;
    }
    public void InheritPlatformMovement()
    {
        platformMovementA = inheritMovement.GetPlatformMovement(0);
        platformMovementB = inheritMovement.GetPlatformMovement(1);
        platformMovementC = inheritMovement.GetPlatformMovement(2);
        platformMovementD = inheritMovement.GetPlatformMovement(3);
        platformCurrentPositionD = inheritMovement.GetCurrentPosition(3);
        // Handle player movement based on the current platform
        if (!IsInput() && IsGrounded())
        {
            if (IsPlayerOnPlatformA())
            {
                transform.position += platformMovementA;    
                adjustedJumpForce = jumpForce;
            }
            else if (IsPlayerOnPlatformB())
            {
                transform.position += platformMovementB;
                adjustedJumpForce = jumpForce;
            }
            else if (IsPlayerOnPlatformC())
            {
                if (platformMovementC.y > 0)
                {
                    // Platform is moving upwards
                    adjustedJumpForce = jumpForce * 1.25f;
                }
                else if (platformMovementC.y < 0)
                {
                    // Platform is moving downwards
                    adjustedJumpForce = jumpForce * 0.75f;
                }
                transform.position += platformMovementC;
            }
            else if (IsPlayerOnPlatformD())
            {
                float platformRotation = 30f * Time.deltaTime;              
                Vector3 currentPosition = platformCurrentPositionD;
                //rotatedDestination represents position where the player should be moved after rotation of the platform.
                Vector3 rotatedDestination = Quaternion.Euler(0, platformRotation, 0) * (transform.position - currentPosition) + currentPosition;              
                transform.position = rotatedDestination + platformMovementD;              
                transform.Rotate(Vector3.up, platformRotation);                
                adjustedJumpForce = jumpForce;
            }
            else
            {              
                adjustedJumpForce = jumpForce;
            }
        }
    }

    private bool IsPlayerOnPlatform(string platformName)
    {
        GameObject platform = GameObject.Find(platformName);
        if (platform == null)
        {
            Debug.LogError($"Platform {platformName} not found!");
            return false;
        }

        float raycastDistance = 0.1f;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance))
        {
            if (hit.collider.gameObject == platform)
            {
                return true;
            }
        }
        return false;
    }

    private bool IsPlayerOnPlatformA()
    {
        return IsPlayerOnPlatform("A");
    }

    private bool IsPlayerOnPlatformB()
    {
        return IsPlayerOnPlatform("B");
    }

    private bool IsPlayerOnPlatformC()
    {
        return IsPlayerOnPlatform("C");
    }
    private bool IsPlayerOnPlatformD()
    {
        return IsPlayerOnPlatform("D");
    }

    public bool IsPlayerFinished()
    {
        
        float raycastDistance = 2f;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance))
        {
            if (hit.collider.gameObject == finish)
            {
                return true;
            }
        }
        return false;
    }

    void IsOnArrowPlatform()
    {
        float raycastDistance = 2f;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance))
        {
            if (hit.collider.gameObject == arrowPlatform)
            {
                if (!isOnArrowPlatform)
                {
                    isOnArrowPlatform = true;
                   
                    BoostPlatform();
                }
            }
            else if (isOnArrowPlatform)
            {
                isOnArrowPlatform = false;
                ResetMovementSpeed();
            }
        }      
    }
    void BoostPlatform()
    {
        movementSpeed = 15f;
    }

    void ResetMovementSpeed()
    {
        movementSpeed = 7f;
    }

    public bool IsLost()
    {
        if (player != null && player.transform.position.y < -10f)
        {
            return true;          
        }
        return false;
    }

    private void SceneCaller()
    {
        if (IsLost())
        {
            sceneController.RestartScene();
        }

        if (IsPlayerFinished())
        {
            sceneController.FinishedMenuScene();
        }
    }
}
