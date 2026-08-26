using UnityEngine;

public class BreakBlock : MonoBehaviour
{
    private bool isBreaking = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (isBreaking || !collision.gameObject.CompareTag("Player"))
            return;

        foreach (ContactPoint contact in collision.contacts)
        {
            // Player on top of the block
            if (contact.normal.y < -0.5f)
            {
                isBreaking = true;
                Invoke(nameof(DestroyBlock), 0.5f);
                break;
            }
        }
    }

    private void DestroyBlock()
    {
        Destroy(gameObject);
    }
}