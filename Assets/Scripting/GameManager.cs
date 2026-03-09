using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel; 

    void Start()
    {
        Time.timeScale = 1; 
    }

    public void EndGame()
    {
        gameOverPanel.SetActive(true); 
        Time.timeScale = 0;           
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}