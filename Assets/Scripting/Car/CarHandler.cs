using UnityEngine;

public class CarHandler : MonoBehaviour
{
    [Header("Setup - Drag these here!")]
    [SerializeField] Rigidbody rb;
    public Camera playerCam;        
    public AudioSource engineSound; 

    [Header("Movement Settings")]
    public float speed = 30f;
    public float turnSpeed = 120f;
    public float maxVelocity = 50f;
    
    [Header("Visual & Audio Juice")]
    public float minFOV = 60f;
    public float maxFOV = 80f;
    public float minPitch = 0.8f;
    public float maxPitch = 2.5f;

    private float moveInput;
    private float steerInput;

    public void SetInput(float move, float steer)
    {
        moveInput = move;
        steerInput = steer;
    }

    void Start()
    {
        if (rb != null) 
        {
            rb.WakeUp();
            rb.centerOfMass = new Vector3(0, -1f, 0); 
        }
    }

    void Update()
    {
        float speedFactor = rb.linearVelocity.magnitude / maxVelocity;

        if (playerCam != null)
        {
            playerCam.fieldOfView = Mathf.Lerp(minFOV, maxFOV, speedFactor);
        }

        if (engineSound != null)
        {
            engineSound.pitch = Mathf.Lerp(minPitch, maxPitch, speedFactor);
        }
    }

    void FixedUpdate()
    {

        Vector3 localVelocity = transform.InverseTransformDirection(rb.linearVelocity);
        localVelocity.x *= 0.1f; 
        rb.linearVelocity = transform.TransformDirection(localVelocity);

    
        if (Mathf.Abs(moveInput) > 0.1f)
        {
            rb.AddForce(transform.forward * moveInput * speed, ForceMode.Acceleration);
        }

        if (rb.linearVelocity.magnitude > 1f)
        {
            float turn = steerInput * turnSpeed * Time.fixedDeltaTime;
            if (localVelocity.z < 0) turn = -turn; 
            rb.MoveRotation(rb.rotation * Quaternion.Euler(0, turn, 0));
        }

        rb.AddForce(-transform.up * 10f * rb.linearVelocity.magnitude);
    }
}