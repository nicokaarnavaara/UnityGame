using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SpikeTrap : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("YOU DIED!");
            other.gameObject.SetActive(false);

            StartCoroutine(ReloadScene());
        }
    }

    private IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
