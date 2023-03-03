using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime = 0.3f;
    [SerializeField] private float lerpTime = 0.1f;

    private void LateUpdate()
    {
        // Calculate the target position for the camera
        var fixedTargetPosition = new Vector3(0, target.position.y, -10);

        // Use lerp to smoothly move the camera towards the target position
        transform.position = Vector3.Lerp(transform.position, fixedTargetPosition, lerpTime);
    }
}
