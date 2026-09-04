using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int value = 1;
    public float rotationSpeed = 100f;

    private void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
            if (other.CompareTag("Player"))
        {
            Debug.Log("You got a diamond! " + value);

            CollectibleCounter.Instance.AddDiamond(value);

            Destroy(gameObject);
        }
    }
}
