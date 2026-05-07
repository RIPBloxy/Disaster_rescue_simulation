using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public float shakeDelay = 30f;
    public float duration = 2f;
    public float magnitude = 0.3f;

    public string targetSceneName = "NextScene";
    public Image fadePanel;
    public float fadeDuration = 1.5f;

    private float timer;
    private float shakeTimer;
    private Vector3 shakeOffset;
    private bool hasFaded = false;

    // --- ADD ---
    private bool isStopped = false;

    void Start()
    {
        timer = shakeDelay;

        if (fadePanel != null)
            fadePanel.color = new Color(0, 0, 0, 0);
    }

    void Update()
    {
        // --- ADD: stop everything if done ---
        if (isStopped) return;

        Debug.Log(timer);
        timer -= Time.deltaTime;

        if (timer <= 0 && shakeTimer <= 0)
        {
            shakeTimer = duration;
        }

        if (shakeTimer > 0)
        {
            shakeOffset = Random.insideUnitSphere * magnitude;
            shakeTimer -= Time.deltaTime;

            if (shakeTimer <= 0 && !hasFaded)
            {
                hasFaded = true;
                StartCoroutine(FadeAndTeleport());
            }
        }
        else
        {
            shakeOffset = Vector3.zero;
        }
    }

    void LateUpdate()
    {
        // --- ADD: stop applying shake if done ---
        if (isStopped) return;

        transform.position += shakeOffset;
    }

    private IEnumerator FadeAndTeleport()
    {
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsed / fadeDuration);

            if (fadePanel != null)
                fadePanel.color = new Color(0, 0, 0, alpha);

            yield return null;
        }

        // --- ADD: stop shake before loading ---
        isStopped = true;
        shakeOffset = Vector3.zero;

        SceneManager.LoadScene(targetSceneName);
    }
}