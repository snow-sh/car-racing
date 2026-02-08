using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb;

    float accelerationMultiplier = 3;
    float breaksMultiplier = 15;
    Vector2 input = Vector2.zero;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (input.y > 0)
            Accelerate();
        else
            rb.drag = 0.2f;

        if (input.y < 0)
            Break();


    }

    void Accelerate()
    {
        rb.drag = 0;
        rb.AddForce(rb.transform.forward * accelerationMultiplier * input.y);
    }

    void Break()
    {
       if (rb.velocity.z <= 0)
            return;

        rb.AddForce(rb.transform.forward * breaksMultiplier * input.y);
    }
}
