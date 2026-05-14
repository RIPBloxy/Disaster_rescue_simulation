using UnityEngine;

public class NPCSOUND : MonoBehaviour
{
    public AudioClip clip;
    public float hearRadius = 15f;
    private AudioSource audioSource;
    private bool stopped = false;

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
        if (stopped) return;
        if (Camera.main == null) return;

        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        audioSource.volume = 1f - Mathf.Clamp01(distance / hearRadius);
    }

    public void StopPermanently()
    {
        stopped = true;
        audioSource.Stop();
        audioSource.volume = 0f;
    }
}