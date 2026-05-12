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

    [Header("Earthquake Sound")]
    public AudioClip earthquakeClip;
    public float earthquakeVolume = 0.8f;
    public float soundDuration = 2f;
    public float fadeInDuration = 1f;  // how long to fade in
    public float fadeOutDuration = 1f; // how long to fade out

    private AudioSource audioSource;
    private float timer;
    private float shakeTimer;
    private Vector3 shakeOffset;
    private bool hasFaded = false;
    private bool isStopped = false;
    private bool hasShaken = false;

    void Start()
    {
        timer = shakeDelay;

        if (fadePanel != null)
            fadePanel.color = new Color(0, 0, 0, 0);

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = earthquakeClip;
        audioSource.loop = false;
        audioSource.volume = 0f;
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (isStopped) return;

        timer -= Time.deltaTime;

        if (timer <= 0 && !hasShaken)
        {
            hasShaken = true;
            shakeTimer = duration;
            audioSource.Play();
            StartCoroutine(FadeSound());
        }

        if (hasShaken && shakeTimer > 0)
        {
            shakeOffset = Random.insideUnitSphere * magnitude;
            shakeTimer -= Time.deltaTime;

            if (shakeTimer <= 0)
            {
                shakeOffset = Vector3.zero;

                if (!hasFaded)
                {
                    hasFaded = true;
                    StartCoroutine(FadeAndTeleport());
                }
            }
        }
        else if (!hasShaken)
        {
            shakeOffset = Vector3.zero;
        }
    }

    private IEnumerator FadeSound()
    {
        // Fade in
        float elapsed = 0f;
        while (elapsed < fadeInDuration)
        {
            elapsed += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0f, earthquakeVolume, elapsed / fadeInDuration);
            yield return null;
        }
        audioSource.volume = earthquakeVolume;

        // Hold for remaining sound duration
        float holdTime = soundDuration - fadeInDuration - fadeOutDuration;
        if (holdTime > 0)
            yield return new WaitForSeconds(holdTime);

        // Fade out
        elapsed = 0f;
        while (elapsed < fadeOutDuration)
        {
            elapsed += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(earthquakeVolume, 0f, elapsed / fadeOutDuration);
            yield return null;
        }
        audioSource.volume = 0f;
        audioSource.Stop();
    }

    void LateUpdate()
    {
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

        isStopped = true;
        shakeOffset = Vector3.zero;
        SceneManager.LoadScene(targetSceneName);
    }
}