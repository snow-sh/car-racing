// using UnityEngine;

// public class CarHandler : MonoBehaviour
// {
//     [SerializeField] Rigidbody rb;
//     public float speed = 2000f;
//     public float turnSpeed = 100f;

//     private float moveInput;
//     private float steerInput;

//     public void SetInput(float move, float steer)
//     {
//         moveInput = move;
//         steerInput = steer;
//     }

//     // void FixedUpdate()
//     // {

//     //     // If we are giving input but the car isn't moving, give it a tiny kick
//     // if (Mathf.Abs(moveInput) > 0.1f && rb.linearVelocity.magnitude < 0.1f)
//     // {
//     //     // This 'Mode.Impulse' helps break the initial friction
//     //     rb.AddForce(transform.forward * moveInput * speed * 0.5f, ForceMode.Impulse);
//     // }

//     // // Normal driving code
//     // Vector3 force = transform.forward * moveInput * speed;
//     // rb.AddForce(force, ForceMode.Acceleration);
//     //     // 1. Driving Force
//     //     // Vector3 force = transform.forward * moveInput * speed;
//     //     // rb.AddForce(force, ForceMode.Acceleration);

//     //     // 2. Steering (Only turn if moving)
//     //     if (rb.linearVelocity.magnitude > 0.1f)
//     //     {
//     //         float turn = steerInput * turnSpeed * Time.fixedDeltaTime;
//     //         // Reverse steering if driving backward
//     //         if (moveInput < 0) turn = -turn; 
            
//     //         rb.MoveRotation(rb.rotation * Quaternion.Euler(0, turn, 0));
//     //     }
//     // }

//     void FixedUpdate()
// {
//     // Change ForceMode to Acceleration - this ignores the Mass value!
//     Vector3 force = transform.forward * moveInput * speed;
//     rb.AddForce(force, ForceMode.Acceleration);

//     if (rb.linearVelocity.magnitude > 0.1f)
//     {
//         float turn = steerInput * turnSpeed * Time.fixedDeltaTime;
//         if (moveInput < 0) turn = -turn; 
//         rb.MoveRotation(rb.rotation * Quaternion.Euler(0, turn, 0));
//     }
// }

//     void Start()
// {
//     if (rb != null)
//     {
//         rb.WakeUp(); // Forces the physics engine to pay attention to this car
//     }
// }
// }













using UnityEngine;

public class CarHandler : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    
    // Adjusted values for ForceMode.Acceleration
    public float speed = 30f;       // Much lower! 2000 was too high.
    public float turnSpeed = 120f;
    public float maxVelocity = 50f; // This prevents the car from flying away

    private float moveInput;
    private float steerInput;

    public void SetInput(float move, float steer)
    {
        moveInput = move;
        steerInput = steer;
    }

    void Start()
    {
        if (rb != null) rb.WakeUp();
    }

void FixedUpdate()
{
    // 1. Calculate Local Velocity
    // This tells us how much we are moving Forward vs Sideways
    Vector3 localVelocity = transform.InverseTransformDirection(rb.linearVelocity);

    // 2. Kill Sideways Velocity (The "Grip" Secret)
    // This stops the 'ice skating' effect
    localVelocity.x *= 0.1f; // Adjust (0.1f to 0.9f) - lower is more grip
    rb.linearVelocity = transform.TransformDirection(localVelocity);

    // 3. Drive Force
    if (Mathf.Abs(moveInput) > 0.1f)
    {
        rb.AddForce(transform.forward * moveInput * speed, ForceMode.Acceleration);
    }

    // 4. Improved Steering (Only turn if moving)
    if (rb.linearVelocity.magnitude > 1f)
    {
        float turn = steerInput * turnSpeed * Time.fixedDeltaTime;
        // Check if we are reversing to flip steering
        if (localVelocity.z < 0) turn = -turn;

        rb.MoveRotation(rb.rotation * Quaternion.Euler(0, turn, 0));
    }
}

void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.CompareTag("Grass"))
    {
        Debug.Log("Get off the grass!");
        // Optional: Reset scene or reduce health
        UnityEngine.SceneManagement.SceneManager.LoadScene(0); 
    }
}
}