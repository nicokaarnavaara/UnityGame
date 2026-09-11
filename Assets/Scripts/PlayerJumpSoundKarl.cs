using UnityEngine;
using UnityEngine.InputSystem;
using FMODUnity;

public class PlayerJumpSoundKarl : MonoBehaviour
{
    [Header("FMOD")]
    [SerializeField] private EventReference jumpEvent;

    private void OnJump(InputValue value)
    {
        if (!value.isPressed)
        {
            return;
        }

        RuntimeManager.PlayOneShot(
            jumpEvent,
            transform.position
        );
    }
}