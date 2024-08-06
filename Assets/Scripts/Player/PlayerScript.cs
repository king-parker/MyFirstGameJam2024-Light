using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public float jumpForce = 500.0f;
    public float jumpStrength = 50.0f;
    public float moveSpeed = 10.0f;
    public Vector2 groundBoxcastSize;
    public float groundBoxcastDistance;
    public LayerMask groundLayer;

    private Rigidbody2D playerBody;
    private PlayerControls controls;
    //public BoxCollider2D groundBoxcast;
    private float moveInput;
    private bool hasDoubleJump = true;

    public bool isOnGround()
    {
        if (Physics2D.BoxCast(transform.position, groundBoxcastSize, 0, -transform.up, groundBoxcastDistance, groundLayer))
        {
            return true;
        }
        else {
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - (transform.up * groundBoxcastDistance), groundBoxcastSize);
    }

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.Jump.performed += ctx => Jump();

        controls.Gameplay.Move.performed += ctx => moveInput = ctx.ReadValue<float>();
        controls.Gameplay.Move.canceled += ctx => moveInput = 0.0f;
    }

    private void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();

        //groundBoxcast = this.gameObject.transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (isOnGround())
        {
            hasDoubleJump = true;
        }
    }

    private void FixedUpdate()
    {
        playerBody.velocity = new Vector2(moveInput * moveSpeed, playerBody.velocity.y);
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Jump()
    {
        var onGround = isOnGround();
        if (onGround || hasDoubleJump)
        {
            hasDoubleJump = onGround ? true : false;
            //playerBody.AddForce(Vector2.up * jumpForce);
            playerBody.velocity = Vector2.up * jumpStrength;
        }
        else if (hasDoubleJump)
        {
            hasDoubleJump = false;
        }
    }    
}
