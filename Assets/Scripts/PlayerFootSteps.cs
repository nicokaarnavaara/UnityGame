using UnityEngine;
using FMODUnity;

public class PlayerFootsteps : MonoBehaviour
{
    [Header("FMOD")]
    [SerializeField] private EventReference gravelFootstep;

    [Header("Ground Check")]
    [SerializeField] private BoxCollider feetCollider;
    [SerializeField] private LayerMask groundLayer;

    [Header("Timing")]
    [SerializeField] private float stepInterval = 0.45f;
    [SerializeField] private float minimumSpeed = 0.2f;

    private Rigidbody rigidBody;
    private float stepTimer;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        bool isMoving =
            Mathf.Abs(rigidBody.linearVelocity.x) > minimumSpeed;

        if (isMoving && IsGrounded())
        {
            stepTimer -= Time.deltaTime;

            if (stepTimer <= 0f)
            {
                PlayFootstep();
                stepTimer = stepInterval;
            }
        }
        else
        {
            // Seuraava askel soi heti, kun pelaaja lähtee liikkeelle.
            stepTimer = 0f;
        }
    }

    private bool IsGrounded()
    {
        if (feetCollider == null)
        {
            return false;
        }

        return Physics.Raycast(
            feetCollider.bounds.center,
            Vector3.down,
            feetCollider.bounds.extents.y + 0.3f,
            groundLayer,
            QueryTriggerInteraction.Ignore
        );
    }

    private void PlayFootstep()
    {
        RuntimeManager.PlayOneShot(
            gravelFootstep,
            transform.position
        );
    }
}