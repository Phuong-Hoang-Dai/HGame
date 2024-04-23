using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    CharacterController playerController;
    public Animator animator;
    public Transform cam;
    PlayerInput playerInput;

    Vector2 movementInput;
    Vector3 movement;
    
    bool isMovementPressed;
    bool isRunPressed;
    bool isAttacking;

    float targetAngle;
    float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    float speed = 6f;
    float accelerationRun = 2f;

    public GameObject currentWeapon;

    private void Awake()
    {
        playerInput = new PlayerInput();
        playerController = GetComponent<CharacterController>();
        playerInput.PlayerControls.Move.started += onMovenentInput;
        playerInput.PlayerControls.Move.canceled += onMovenentInput;
        playerInput.PlayerControls.Move.performed += onMovenentInput;

        playerInput.PlayerControls.Run.started += onRun;
        playerInput.PlayerControls.Run.canceled += onRun;
        playerInput.PlayerControls.Attack.started += onAttack;
    }

    private void onAttack(InputAction.CallbackContext context)
    {
        isAttacking = context.ReadValueAsButton();
    }

    private void onRun(InputAction.CallbackContext context)
    {
        isRunPressed = context.ReadValueAsButton();
    }

    void onMovenentInput(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        movement.x = movementInput.x;
        movement.z = movementInput.y;
        isMovementPressed = movementInput.x != 0 || movementInput.y !=0;
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;
        targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

        handleAnimation();
        if(isMovementPressed)
        {
            handleRotation();
            handleMovement();
        }
        handleGravity();
    }

   

    private void handleMovement()
    {
        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * new Vector3(0f, movement.y, 1);
        if(isRunPressed)
        {
            playerController.Move(moveDir.normalized * speed * accelerationRun * Time.deltaTime);
        }
        else
        {
            playerController.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }

    private void handleRotation()
    {
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle
                                        , ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    private void handleGravity()
    {
        if (playerController.isGrounded)
        {
            movement.y = -.05f;
        }
        else
        {
            movement.y = -9.8f;
        }
    }
    private void handleAnimation()
    {
        bool isWalking = animator.GetBool("isWalking");
        bool isRuning = animator.GetBool("isRuning");
        if (isWalking && !isMovementPressed)
        {
            animator.SetBool("isWalking", false);
        }
        else
        if (!isWalking && isMovementPressed)
        {
            animator.SetBool("isWalking", true);
        }
        if ((isMovementPressed && isRunPressed) && !isRuning)
        {
            animator.SetBool("isRuning", true);
        }
        else if ((isMovementPressed && !isRunPressed) && isRuning)
        {
            animator.SetBool("isRuning", false);
        }
        else if ((!isMovementPressed && !isRunPressed) && isRuning)
        {
            animator.SetBool("isRuning", false);
        }
        if (isAttacking)
        {
            animator.Play("Attack");
            isAttacking = false;
        }
    }

    public void Attack()
    {
        currentWeapon.GetComponentInChildren<DamageDealer>().Attack();
    }

    private void OnEnable()
    {
        playerInput.PlayerControls.Enable();
    }
    private void OnDisable()
    {
        playerInput.PlayerControls.Disable();
    }
}
