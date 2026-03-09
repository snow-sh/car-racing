using UnityEngine;
using UnityEngine.SceneManagement; 
using System.Collections;
using TMPro;

public class CarHandler : MonoBehaviour
{
    [Header("Setup - Drag components here!")]
    [SerializeField] Rigidbody rb;
    public Camera playerCam;        
    
    [Header("Audio Setup")]
    public AudioSource engineSound; 
    public AudioSource bgMusic;      
    public AudioSource crashSound;   

    [Header("UI Slots")]
    public GameObject menuPanel;    
    public GameObject gameOverText; 
    public GameObject gameHUD;    
    public TextMeshProUGUI scoreText; 

    [Header("Movement Settings")]
    public float speed = 30f;
    public float turnSpeed = 120f;
    public float maxVelocity = 50f;
    
    [Header("Visual Juice")]
    public float minFOV = 60f;
    public float maxFOV = 80f;

    private float moveInput;
    private float steerInput;
    private static bool isRestarting = false;
    private bool isGameOver = false;
    private float score = 0;

    public void SetInput(float move, float steer)
    {
        if (isGameOver) return; 
        moveInput = move;
        steerInput = steer;
    }

    void Awake()
    {
        if (isRestarting)
        {
            isRestarting = false;
            Time.timeScale = 1f;
        }
        else
        {
            Time.timeScale = 0f; 
        }

        if (bgMusic != null) bgMusic.ignoreListenerPause = true; 
    }

    void Start()
    {
        if (rb != null) rb.centerOfMass = new Vector3(0, -1f, 0); 

        bool showMenu = Time.timeScale == 0;
        if(menuPanel != null) menuPanel.SetActive(showMenu); 
        if(gameHUD != null) gameHUD.SetActive(!showMenu);
        if(gameOverText != null) gameOverText.SetActive(false); 

        if (bgMusic != null && !bgMusic.isPlaying) bgMusic.Play(); 
        if (!showMenu && engineSound != null) engineSound.Play();
    }

    public void StartGame()
    {
        if (isGameOver)
        {
            isRestarting = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            return;
        }

        isGameOver = false;
        Time.timeScale = 1f; 
        if(menuPanel != null) menuPanel.SetActive(false); 
        if(gameHUD != null) gameHUD.SetActive(true);
        if (engineSound != null) engineSound.Play(); 
    }

    void Update()
    {
        if (isGameOver || Time.timeScale == 0) return; 

        score += Time.deltaTime;
        if (scoreText != null) scoreText.text = "Score: " + Mathf.FloorToInt(score).ToString();

        float speedFactor = rb.linearVelocity.magnitude / maxVelocity;
        if (playerCam != null) playerCam.fieldOfView = Mathf.Lerp(minFOV, maxFOV, speedFactor); 
        if (engineSound != null) engineSound.pitch = Mathf.Lerp(0.8f, 2.5f, speedFactor); 
    }

    void FixedUpdate()
    {
        if (isGameOver || Time.timeScale == 0) return; 

        Vector3 localVelocity = transform.InverseTransformDirection(rb.linearVelocity);
        localVelocity.x *= 0.1f; 
        rb.linearVelocity = transform.TransformDirection(localVelocity);

        if (Mathf.Abs(moveInput) > 0.1f) rb.AddForce(transform.forward * moveInput * speed, ForceMode.Acceleration); 

        if (rb.linearVelocity.magnitude > 1f)
        {
            float turn = steerInput * turnSpeed * Time.fixedDeltaTime;
            if (localVelocity.z < 0) turn = -turn; 
            rb.MoveRotation(rb.rotation * Quaternion.Euler(0, turn, 0)); 
        }
        rb.AddForce(-transform.up * 10f * rb.linearVelocity.magnitude); 
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isGameOver)
        {
            StartCoroutine(CrashSequence()); 
        }
    }
    IEnumerator CrashSequence()
    {
        isGameOver = true;
        
        if (engineSound != null) engineSound.Stop(); 
        if (bgMusic != null) bgMusic.Stop(); 
        if (crashSound != null) crashSound.Play(); 

        if (gameHUD != null) gameHUD.SetActive(true); 
        if (gameOverText != null) gameOverText.SetActive(true); 

        Time.timeScale = 0.2f; 
        rb.linearVelocity = Vector3.zero; 

        yield return new WaitForSecondsRealtime(5.5f); 

        if (gameHUD != null) gameHUD.SetActive(false); 
        if (gameOverText != null) gameOverText.SetActive(false); 
        
        if (menuPanel != null) menuPanel.SetActive(true); 
        
        if (bgMusic != null) bgMusic.Play(); 
        Time.timeScale = 0f; 
    }
}