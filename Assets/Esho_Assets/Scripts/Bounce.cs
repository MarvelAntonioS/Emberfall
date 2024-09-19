using UnityEngine;

public class Bounce : MonoBehaviour
{
    public float amplitude = 0.1f;
    public float frequency = 3f;
    public float delay = 2f; 
    public float initialRise = 0.2f;
    private float startTime;
    private float bounceStartY; 
    private bool isBouncing = false; 
    private Rigidbody rb; 

    void Start()
    {
        startTime = Time.time;
        rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.isKinematic = false;
        }
    }

    void Update()
    {
        if (!isBouncing && Time.time >= startTime + delay)
        {
            Vector3 newPosition = transform.position;
            newPosition.y += initialRise;
            transform.position = newPosition;

            bounceStartY = transform.position.y;
            isBouncing = true;

            if (rb != null)
            {
                rb.isKinematic = true;
            }
        }

        if (isBouncing)
        {
            float offset = Mathf.Sin((Time.time - (startTime + delay)) * frequency) * amplitude;
            transform.position = new Vector3(transform.position.x, bounceStartY + offset, transform.position.z);
        }
    }
}
