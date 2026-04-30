using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeDelay = 30f;
    public float duration = 2f;
    public float magnitude = 0.3f;

    private float timer;
    private float shakeTimer;
    private Vector3 shakeOffset;

    void Start()
    {
        timer = shakeDelay;
    }

    void Update()
    {
        Debug.Log(timer);
        timer -= Time.deltaTime;

        // Start earthquake
        if (timer <= 0 && shakeTimer <= 0)
        {
            shakeTimer = duration;
        }

        // Generate shake
        if (shakeTimer > 0)
        {
            shakeOffset = Random.insideUnitSphere * magnitude;
            shakeTimer -= Time.deltaTime;
        }
        else
        {
            shakeOffset = Vector3.zero;
        }
    }

    void LateUpdate()
    {
        // Apply shake AFTER other scripts
        transform.position += shakeOffset;
    }
}