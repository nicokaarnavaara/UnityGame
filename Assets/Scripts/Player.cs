using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed = 10f;

    [Header("Jump")]
    [SerializeField] float jumpForce = 5f;
    [SerializeField] int maxJumps = 2;
    [SerializeField] BoxCollider feetCollider;

    public float gravityModifier;


    int jumpCount;

    [Header("Dash")]
    [SerializeField] float dashForce = 10f;
    [SerializeField] float dashDuration = 0.2f;
    [SerializeField] float dashCoolDown = 1.0f;

    bool isDashing;
    bool canDash = true;

    Vector2 moveInput;
    Rigidbody rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        Debug.Log("PLAYER STARTED");

        if (rigidBody == null)
        {
            Debug.LogError("ERROR: No Rigidbody found on Player!");
        }
        else
        {
            Debug.Log("Rigidbody found!");
        }

        Physics.gravity *= gravityModifier;
    }

    void FixedUpdate()
    {
        Move();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();

        Debug.Log("OnMove called! Input: " + moveInput);

        // Turn the player based on movement direction
        if (moveInput.x > 0)
        {
            rigidBody.rotation = Quaternion.Euler(0f, 0f, 0f);
            Debug.Log("Moving RIGHT");
        }
        else if (moveInput.x < 0)
        {
            rigidBody.rotation = Quaternion.Euler(0f, 180f, 0f);
            Debug.Log("Moving LEFT");
        }
    }

    void Move()
    {
        if (isDashing)
        {
            Debug.Log("Currently dashing - movement disabled");
            return;
        }

        rigidBody.linearVelocity = new Vector3(
            moveInput.x * moveSpeed,
            rigidBody.linearVelocity.y,
            rigidBody.linearVelocity.z
        );

        Debug.Log(
            "Move() | Input X: " + moveInput.x +
            " | Velocity: " + rigidBody.linearVelocity
        );
    }

    void OnJump(InputValue value)
    {
        Debug.Log("OnJump called!");

        if (!value.isPressed)
        {
            return;
        }

        if (IsGrounded())
        {
            jumpCount = 0;
            Debug.Log("Grounded - jump count reset");
        }

        if (jumpCount >= maxJumps)
        {
            Debug.Log("Maximum jumps reached");
            return;
        }

        rigidBody.linearVelocity = new Vector3(
            rigidBody.linearVelocity.x,
            jumpForce,
            rigidBody.linearVelocity.z
        );

        jumpCount++;

        Debug.Log("JUMP! Jump count: " + jumpCount);
    }

    bool IsGrounded()
    {
        bool grounded = Physics.Raycast(
            feetCollider.bounds.center,
            Vector3.down,
            feetCollider.bounds.extents.y + 0.3f,
            LayerMask.GetMask("Ground")
        );

        Debug.Log("Is Grounded: " + grounded);

        return grounded;
    }

    void OnDash(InputValue value)
    {
        Debug.Log("OnDash called!");

        if (!value.isPressed || isDashing || !canDash)
        {
            return;
        }

        StartCoroutine(Dash());
    }

    IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;

        Debug.Log("DASH STARTED");

        rigidBody.AddForce(
            transform.right * dashForce,
            ForceMode.Impulse
        );

        yield return new WaitForSeconds(dashDuration);

        isDashing = false;

        Debug.Log("DASH ENDED");

        yield return new WaitForSeconds(dashCoolDown);

        canDash = true;

        Debug.Log("DASH READY");
    }
}