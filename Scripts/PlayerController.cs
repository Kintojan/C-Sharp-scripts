using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputAction move;
    private InputAction jump;
    private InputAction menu;
    private Animator anim;
    public GameObject pauseMenu;
    public bool isPauseMenu = false;
    public bool isWalking = false;

    [NonSerialized] public float horizontalInput;
    [NonSerialized] public float verticalInput;

    private GameInput gameInput;

    PlayerMovement playerMovement;
   
    
    private void Awake()
    {
        gameInput = new GameInput();
        Initialize(gameInput); 
        playerMovement = GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
        pauseMenu = GameObject.Find("PauseMenu");
        pauseMenu.SetActive(false);
    }

    public void Initialize(GameInput gameInput)
    {
        move = gameInput.Player.Move;
        move.Enable();

        jump = gameInput.Player.Jump;
        jump.performed += DoJump;
        jump.Enable();

        menu = gameInput.Player.Menu;
        menu.performed += DoMenu;
        menu.Enable();
    }

    void Start()
    {
        Time.timeScale = 1f;
    }

    void Update()
    {
        
        
    }

    void FixedUpdate()
    {
        MovementInput();
        playerMovement.AllMovement();    
    }

    private void MovementInput()
    {
        horizontalInput = move.ReadValue<Vector2>().x;
        verticalInput = move.ReadValue<Vector2>().y;

        // Check if player is grounded and moving horizontally or vertically
        if (playerMovement.isCountdownFinished && playerMovement.IsGrounded() && (horizontalInput != 0f || verticalInput != 0f))
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }

        SetBool("Walk", isWalking);
    }

    public void DoMenu(InputAction.CallbackContext context)
    {
        if (pauseMenu != null) 
        {
            if (!IsPaused())
            {          
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                pauseMenu.SetActive(false);
                Time.timeScale = 1f;
            }
        }      
    }

    public void DoJump(InputAction.CallbackContext context)
    {
       
        playerMovement.Jump();           
    }
    

    public bool IsPaused()
    {
        if (pauseMenu != null)
        {
            return pauseMenu.activeSelf;          
        }
        else
        {       
            return false;
        }
    }

    private void SetBool(string paramName, bool value)
    {
        if (anim != null)
        {
            anim.SetBool(paramName, value);
        }
        else
        {
            Debug.LogWarning("Animator component is not assigned.");
        }
    }

}
