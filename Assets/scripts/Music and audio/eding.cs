using UnityEngine;

public class eding : MonoBehaviour
{
    public AudioClip clip;
    [Range(0f, 1f)] public float volume = 0.5f;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.volume = volume;
        audioSource.Play();
    }

    void Update()
    {
        audioSource.volume = volume;
    }
}