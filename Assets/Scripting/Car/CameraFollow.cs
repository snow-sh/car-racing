using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;      // Drag your Car here
    [SerializeField] float distance = 5.0f; // How far behind
    [SerializeField] float height = 2.0f;   // How high up
    [SerializeField] float smoothSpeed = 5.0f;

    void LateUpdate()
    {
        if (target == null) return;

        // Calculate the position behind the car
        Vector3 wantedPosition = target.position - (target.forward * distance) + (Vector3.up * height);
        
        // Smoothly move the camera to that position
        transform.position = Vector3.Lerp(transform.position, wantedPosition, smoothSpeed * Time.deltaTime);

        // Always look at the car
        transform.LookAt(target.position + (Vector3.up * 1.5f));
    }
}