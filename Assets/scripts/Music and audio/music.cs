using UnityEngine;
using System.Collections;

public class music : MonoBehaviour
{
    public AudioClip musicClip;
    public float volume = 0.5f;
    public float fadeIn = 1.5f;
    public float fadeOut = 1.5f;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = musicClip;
        audioSource.loop = true;
        audioSource.volume = 0f;
        audioSource.Play();
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float t = 0f;
        while (t < fadeIn)
        {
            t += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0f, volume, t / fadeIn);
            yield return null;
        }
    }

    public void StopMusic()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float t = 0f;
        while (t < fadeOut)
        {
            t += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(volume, 0f, t / fadeOut);
            yield return null;
        }
        audioSource.Stop();
    }
}