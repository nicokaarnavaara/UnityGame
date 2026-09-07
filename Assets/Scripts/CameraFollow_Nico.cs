using UnityEngine;

public class CameraFollow_Nico : MonoBehaviour
{


    [SerializeField] Vector3 offset;

    [SerializeField] private Transform target;

    [SerializeField] private float smoothTime;

    private Vector3 currentVelocity = Vector3.zero;

    private void Awake()
    {
    }


    void LateUpdate()
    {

        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
    }
}
