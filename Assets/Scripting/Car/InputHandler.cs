// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.InputSystem;
// using UnityEngine.SceneManagement;


// public class InputHandler : MonoBehaviour
// {
//     [SerializeField]
//     CarHandler carHandler;


//     void Update()
//     {
//     Vector2 input = Vector2.zero;
//     input.x = Input.GetAxis("Horizontal");
//     input.y = Input.GetAxis("Vertical");
//     carHandler.SetInput(input);
//     if (Input.GetKeyDown(KeyCode.R))
//         SceneManager.LoadScene(SceneManager.GetActiveScene().name);

//     float move = Input.GetAxis("Vertical");
//     if (move != 0) Debug.Log("Keyboard detected! Input value: " + move);
//     }
    
// }










using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public CarHandler car;

    void Update()
    {
        float move = Input.GetAxis("Vertical");   // W/S or Up/Down
        float steer = Input.GetAxis("Horizontal"); // A/D or Left/Right
        
        car.SetInput(move, steer);

        if (Input.GetKeyDown(KeyCode.R)) 
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}

public class SimpleCar
{
}