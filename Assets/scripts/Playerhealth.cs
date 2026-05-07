using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image fillImage;
    public float minFallHeight = 10f;
    public int fireDamagePerSecond = 5;

    private float highestY;
    private bool isFalling;
    private bool onFire;
    private float fireTimer;

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
        fillImage.color = Color.green;
        highestY = transform.position.y;
    }

    void Update()
    {
        // Fall tracking
        if (transform.position.y >= highestY)
        {
            highestY = transform.position.y;
            isFalling = false;
        }
        else isFalling = true;

        // Fire damage
        if (onFire && (fireTimer -= Time.deltaTime) <= 0)
        {
            TakeDamage(fireDamagePerSecond);
            fireTimer = 1f;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (!isFalling) return;
        float fallDistance = highestY - transform.position.y;
        if (fallDistance >= minFallHeight)
            TakeDamage(Mathf.RoundToInt(fallDistance * 2));
        highestY = transform.position.y;
        isFalling = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("fire")) { onFire = true; fireTimer = 0f; }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("fire")) onFire = false;
    }

    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Max(currentHealth - damage, 0);
        healthSlider.value = currentHealth;
        fillImage.color = currentHealth >= 60 ? Color.green : currentHealth >= 30 ? Color.yellow : Color.red;
        if (currentHealth <= 0) SceneManager.LoadScene("Died");
    }
}