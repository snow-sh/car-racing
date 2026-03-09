using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public CarHandler car;

    void Update()
    {
        float move = Input.GetAxis("Vertical");   
        float steer = Input.GetAxis("Horizontal"); 
        
        car.SetInput(move, steer);

        if (Input.GetKeyDown(KeyCode.R)) 
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}

public class SimpleCar
{
}