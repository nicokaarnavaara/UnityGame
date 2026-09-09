using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class WaterDeath : MonoBehaviour
{
    public GameoverRestart_Nico gameOver;

    private bool hasDied = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasDied)
        {
            hasDied = true;
            Die();
        }
    }

    private void Die()
    {

        if (gameOver != null)
        {
            gameOver.GameOver();
        }
        else
        {
            Debug.LogError("GAMEOVER REFERENCE IS NULL!");
        }
    }
}
