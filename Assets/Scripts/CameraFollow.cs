using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime = 0.3f;
    private Vector3 _velocity = Vector3.zero;


    private void LateUpdate()
    {
        // Calculate the target position for the camera
        var fixedTargetPosition = new Vector3(0, target.position.y, -10);

        // Use lerp to smoothly move the camera towards the target position
        transform.position = Vector3.SmoothDamp(transform.position, fixedTargetPosition, ref _velocity, smoothTime);
    }
}
