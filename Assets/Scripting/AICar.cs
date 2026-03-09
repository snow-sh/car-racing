using UnityEngine;

public class AICar : MonoBehaviour
{
    private float speed;
    public float minSpeed = 30f;
    public float maxSpeed = 80f;

    void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<GameManager>().EndGame();
        }
    }
}