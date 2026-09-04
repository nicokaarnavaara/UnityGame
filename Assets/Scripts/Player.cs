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

    [Header("Gravity")]
    [SerializeField] float gravityMultiplier = 2f;

    [Header("Dash")]
    [SerializeField] float dashForce = 10f;
    [SerializeField] float dashDuration = 0.2f;
    [SerializeField] float dashCoolDown = 1.0f;

    bool isDashing;
    bool canDash = true;
    int jumpCount;

    Vector2 moveInput;
    Rigidbody rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Move();
        ApplyGravity();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();

        if (moveInput.x > 0)
        {
            rigidBody.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (moveInput.x < 0)
        {
            rigidBody.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }

    void Move()
    {
        if (isDashing)
        {
            return;
        }

        rigidBody.linearVelocity = new Vector3(
            moveInput.x * moveSpeed,
            rigidBody.linearVelocity.y,
            rigidBody.linearVelocity.z
        );
    }

    void ApplyGravity()
    {
        if (gravityMultiplier == 1f)
        {
            return;
        }

        Vector3 extraGravity = Physics.gravity * (gravityMultiplier - 1f);
        rigidBody.AddForce(extraGravity, ForceMode.Acceleration);
    }

    void OnJump(InputValue value)
    {
        if (!value.isPressed)
        {
            return;
        }

        if (IsGrounded())
        {
            jumpCount = 0;
        }

        if (jumpCount >= maxJumps)
        {
            return;
        }

        rigidBody.linearVelocity = new Vector3(
            rigidBody.linearVelocity.x,
            jumpForce,
            rigidBody.linearVelocity.z
        );

        jumpCount++;
    }

    bool IsGrounded()
    {
        return Physics.Raycast(
            feetCollider.bounds.center,
            Vector3.down,
            feetCollider.bounds.extents.y + 0.3f,
            LayerMask.GetMask("Ground")
        );
    }

    void OnDash(InputValue value)
    {
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

        rigidBody.AddForce(
            transform.right * dashForce,
            ForceMode.Impulse
        );

        yield return new WaitForSeconds(dashDuration);

        isDashing = false;

        yield return new WaitForSeconds(dashCoolDown);

        canDash = true;
    }

}
