using UnityEngine;
using System.Collections;

public class AudioManger : MonoBehaviour
{
    [Header("Music")]
    public AudioClip musicClip;
    public float volume = 0.5f;
    public float fadeIn = 1.5f;
    public float fadeOut = 1.5f;

    [Header("Footsteps")]
    public AudioClip walkClip;
    public AudioClip runClip;
    [Range(0f, 1f)] public float walkVolume = 0.8f;
    [Range(0f, 1f)] public float runVolume = 1f;

    [Header("Jump")]
    public AudioClip jumpClip;
    [Range(0f, 1f)] public float jumpVolume = 1f;

    private AudioSource musicSource;
    private AudioSource footstepSource;
    private AudioSource jumpSource;
    private Transform player;

    void Start()
    {
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.clip = musicClip;
        musicSource.loop = true;
        musicSource.volume = 0f;
        musicSource.Play();
        StartCoroutine(FadeIn());

        footstepSource = gameObject.AddComponent<AudioSource>();
        footstepSource.loop = true;
        footstepSource.playOnAwake = false;

        jumpSource = gameObject.AddComponent<AudioSource>();
        jumpSource.loop = false;
        jumpSource.playOnAwake = false;
        jumpSource.volume = jumpVolume;

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        bool isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A)
                     || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
        bool isRunning = isMoving && Input.GetKey(KeyCode.LeftShift);

        if (isRunning)
        {
            if (footstepSource.clip != runClip || !footstepSource.isPlaying)
            {
                footstepSource.clip = runClip;
                footstepSource.volume = runVolume;
                footstepSource.Play();
            }
        }
        else if (isMoving)
        {
            if (footstepSource.clip != walkClip || !footstepSource.isPlaying)
            {
                footstepSource.clip = walkClip;
                footstepSource.volume = walkVolume;
                footstepSource.Play();
            }
        }
        else
        {
            footstepSource.Stop();
        }

        // Jump sound
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpSource.volume = jumpVolume;
            jumpSource.PlayOneShot(jumpClip);
        }
    }

    IEnumerator FadeIn()
    {
        float t = 0f;
        while (t < fadeIn)
        {
            t += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(0f, volume, t / fadeIn);
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
            musicSource.volume = Mathf.Lerp(volume, 0f, t / fadeOut);
            yield return null;
        }
        musicSource.Stop();
    }
}