using UnityEngine;
using UnityEngine.SceneManagement;

public class WaterDeath : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player hit water and died!");

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
