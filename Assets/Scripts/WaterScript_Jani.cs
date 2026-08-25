using UnityEngine;
using UnityEngine.SceneManagement;

public class WaterScript_Jani : MonoBehaviour
{

    private bool isDead = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water") && !isDead)
        {
            isDead = true;
            Die();
        }
    }

    void Die()
    {
        Debug.Log("You fell into the water and died!");
        
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        Time.timeScale = 0f;
    }
}
