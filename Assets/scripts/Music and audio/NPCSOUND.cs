using UnityEngine;

public class NPCSOUND : MonoBehaviour
{
    public AudioClip clip;
    public float hearRadius = 15f;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.volume = 0f;
        audioSource.Play();
    }

    void Update()
    {
        if (Camera.main == null) return;

        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        float vol = 1f - Mathf.Clamp01(distance / hearRadius);
        audioSource.volume = vol;
    }
}