using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject player;
    public Vector3 offset = new Vector3(0, 5, -9);

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
