using UnityEngine;

public class completed : MonoBehaviour
{
    public AudioClip musicClip;
    public float volume = 0.5f;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = musicClip;
        audioSource.loop = false;
        audioSource.volume = volume;
        audioSource.Play();
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }
}