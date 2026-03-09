using UnityEngine;
using Unity.Cinemachine; 

public sealed class CameraFovWarp : MonoBehaviour
{
    public CinemachineCamera virtualCamera;
    public Rigidbody carRigidbody;
    
    [Header("Settings")]
    public float minFOV = 60f;
    public float maxFOV = 80f;
    public float speedThreshold = 50f; 

    void Update()
    {
        if (virtualCamera != null && carRigidbody != null)
        {
            float currentSpeed = carRigidbody.linearVelocity.magnitude;
            
            float speedPercent = Mathf.Clamp01(currentSpeed / speedThreshold);
            
            virtualCamera.Lens.FieldOfView = Mathf.Lerp(minFOV, maxFOV, speedPercent);
        }
    }
}