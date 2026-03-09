using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;    
    [SerializeField] float distance = 5.0f; 
    [SerializeField] float height = 2.0f;  
    [SerializeField] float smoothSpeed = 5.0f;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 wantedPosition = target.position - (target.forward * distance) + (Vector3.up * height);
        
        transform.position = Vector3.Lerp(transform.position, wantedPosition, smoothSpeed * Time.deltaTime);

        transform.LookAt(target.position + (Vector3.up * 1.5f));
    }
}