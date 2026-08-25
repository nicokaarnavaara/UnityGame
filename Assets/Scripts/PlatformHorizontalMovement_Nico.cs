using UnityEngine;

public class PlatformHorizontalMovement_Nico : MonoBehaviour
{
    public float speed = 2f;
    public float width = 5f;

    private int direction = 1;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.Translate(Vector3.right * speed * direction * Time.deltaTime);

        if (transform.position.x > startPos.x + width)
        {
            direction = -1;
        }

        if (transform.position.x < startPos.x - width)
        {
            direction = 1;
        }
    }
}
