using UnityEngine;

public class NPCSound : MonoBehaviour
{
    public enum NPCType { Survivor, Doctor }

    [Header("NPC Type")]
    public NPCType npcType;

    [Header("Sound")]
    public AudioClip survivorHelpClip;
    public AudioClip doctorTalkClip;

    [Header("Distance")]
    public float triggerRadius = 15f;
    public float minDistance = 2f;
    public float maxDistance = 15f;

    [Header("Volume")]
    [Range(0f, 1f)] public float maxVolume = 1f;

    private AudioSource audioSource;
    private Transform cam;
    private bool isPlaying = false;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.spatialBlend = 0f;
        audioSource.loop = true;
        audioSource.volume = 0f;
        audioSource.playOnAwake = false;

        if (npcType == NPCType.Survivor)
            audioSource.clip = survivorHelpClip;
        else
            audioSource.clip = doctorTalkClip;

        // Use main camera instead of player tag
        cam = Camera.main.transform;
    }

    void Update()
    {

        if (cam == null) return;

        float distance = Vector3.Distance(transform.position, cam.position);

        if (distance <= triggerRadius)
        {
            if (!isPlaying)
            {
                audioSource.Play();
                isPlaying = true;
            }

            // Volume increases as camera gets closer
            float t = 1f - Mathf.Clamp01((distance - minDistance) / (maxDistance - minDistance));
            audioSource.volume = Mathf.Lerp(0f, maxVolume, t);
        }
        else
        {
            if (isPlaying)
            {
                audioSource.Stop();
                audioSource.volume = 0f;
                isPlaying = false;
            }
        }
        Debug.Log("Distance to cam: " + distance);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, triggerRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, minDistance);
    }
}