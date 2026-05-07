using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    // ── Settings ──────────────────────────────────────────
    public int maxHealth = 100;
    public float minFallHeight = 5f;    // minimum fall distance to take damage
    public int fireDamagePerSecond = 5;

    // ── UI (auto found, no need to drag) ──────────────────
    private Slider healthSlider;
    private Image fillImage;

    // ── Private variables ─────────────────────────────────
    private int currentHealth;
    private float highestY;
    private bool isGrounded;
    private bool onFire;
    private float fireTimer;
    private CharacterController cc;

    void Start()
    {
        currentHealth = maxHealth;
        highestY = transform.position.y;
        cc = GetComponent<CharacterController>();
        RefreshUI();
    }

    void Update()
    {
        // Press T to test damage
        if (Input.GetKeyDown(KeyCode.T))
            TakeDamage(10);

        // Track highest Y while grounded
        bool currentlyGrounded = cc != null ? cc.isGrounded : false;

        if (currentlyGrounded)
        {
            // Just landed
            if (!isGrounded)
            {
                float fallDistance = highestY - transform.position.y;
                if (fallDistance >= minFallHeight)
                {
                    int damage = Mathf.RoundToInt(fallDistance * 2);
                    TakeDamage(damage);
                }
                highestY = transform.position.y;
            }
            // Reset highest point while on ground
            highestY = transform.position.y;
        }
        else
        {
            // Track highest point while in air
            if (transform.position.y > highestY)
                highestY = transform.position.y;
        }

        isGrounded = currentlyGrounded;

        // Fire damage every second
        if (onFire)
        {
            fireTimer -= Time.deltaTime;
            if (fireTimer <= 0f)
            {
                TakeDamage(fireDamagePerSecond);
                fireTimer = 1f;
            }
        }
    }

    // Fire trigger — make sure fire has a collider with Is Trigger ON and tag "fire"
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("fire"))
        {
            onFire = true;
            fireTimer = 0f;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("fire"))
            onFire = false;
    }

    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Max(currentHealth - damage, 0);

        if (healthSlider != null)
            healthSlider.value = currentHealth;

        UpdateBarColor();

        if (currentHealth <= 0)
            SceneManager.LoadScene("Death");
    }

    void UpdateBarColor()
    {
        if (fillImage == null) return;
        if (currentHealth >= 60)
            fillImage.color = Color.green;
        else if (currentHealth >= 30)
            fillImage.color = Color.yellow;
        else
            fillImage.color = Color.red;
    }

    public void RefreshUI()
    {
        healthSlider = null;
        fillImage = null;

        GameObject sliderObj = GameObject.Find("healthbar");
        if (sliderObj != null)
            healthSlider = sliderObj.GetComponent<Slider>();

        GameObject fillObj = GameObject.Find("fill");
        if (fillObj != null)
            fillImage = fillObj.GetComponent<Image>();

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }

        UpdateBarColor();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        RefreshUI();
    }
}