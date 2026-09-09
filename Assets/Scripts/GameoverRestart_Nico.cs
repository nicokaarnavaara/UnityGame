using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameoverRestart_Nico : MonoBehaviour
{
    public GameObject gameOverPanel;
    public TMP_Text rageMessageText;

    private static int deathCount = 0;

    private string[] rageMessages =
    {
        "Oops... that was embarrassing.",
        "You died AGAIN?",
        "BRO... HOW DID YOU DIE THERE?",
        "Let someone else play"
    };

    private void Start()
    {
        Debug.Log("GameoverRestart_Nico STARTED");

        gameOverPanel.SetActive(false);
    }

    public void GameOver()
    {
        Debug.Log("===== GAME OVER CALLED =====");

        deathCount++;

        int messageIndex = Mathf.Min(deathCount - 1, rageMessages.Length - 1);

        rageMessageText.text = rageMessages[messageIndex];

        Debug.Log("Activating panel: " + gameOverPanel.name);

        gameOverPanel.SetActive(true);

        Debug.Log("Panel active state: " + gameOverPanel.activeSelf);

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}