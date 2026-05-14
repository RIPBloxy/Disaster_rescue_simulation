using System.Collections;
using UnityEngine;

public class Musicwithwalking : MonoBehaviour
{
    public static Musicwithwalking Instance;

    [Header("Clips")]
    public AudioClip walkClip;
    public AudioClip runClip;
    public AudioClip jumpClip;
    public AudioClip lobbyMusic;

    [Header("Volumes")]
    [Range(0f, 1f)] public float footstepVolume = 0.8f;
    [Range(0f, 1f)] public float musicVolume    = 0.4f;
    [Range(0f, 1f)] public float duckVolume     = 0.1f; // volume during jump

    [Header("Step Intervals")]
    public float walkInterval = 0.50f;
    public float runInterval  = 0.28f;

    private AudioSource _foot;
    private AudioSource _music;

    private float _stepTimer;
    private bool  _walking;
    private bool  _running;

    // ----------------------------------------------------------------
    void Awake()
    {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else { Destroy(gameObject); return; }

        _foot  = gameObject.AddComponent<AudioSource>();
        _music = gameObject.AddComponent<AudioSource>();

        _music.clip       = lobbyMusic;
        _music.loop       = true;
        _music.volume     = musicVolume;
        _music.playOnAwake = false;
        _music.Play(); // lobby music plays from the start, always
    }

    void Update()
    {
        if (!_walking && !_running) return;

        _stepTimer -= Time.deltaTime;
        if (_stepTimer <= 0f)
        {
            _foot.PlayOneShot(_running ? runClip : walkClip, footstepVolume);
            _stepTimer = _running ? runInterval : walkInterval;
        }
    }

    // ----------------------------------------------------------------
    // Call these from your PlayerController every frame

    public void SetWalking(bool on) { _walking = on; if (!on) _stepTimer = 0f; }
    public void SetRunning(bool on) { _running = on; if (!on) _stepTimer = 0f; }

    // Call this once when the player jumps
    public void PlayJump()
    {
        _foot.PlayOneShot(jumpClip);
        StopAllCoroutines();
        StartCoroutine(DuckAndRestore());
    }

    // ----------------------------------------------------------------
    // Ducks footstep + music on jump, then fades back up
    private IEnumerator DuckAndRestore()
    {
        _foot.volume  = duckVolume;
        _music.volume = duckVolume;

        yield return new WaitForSeconds(0.4f);

        float t = 0f;
        while (t < 0.3f)
        {
            t += Time.deltaTime;
            float ratio   = t / 0.3f;
            _foot.volume  = Mathf.Lerp(duckVolume, footstepVolume, ratio);
            _music.volume = Mathf.Lerp(duckVolume, musicVolume,    ratio);
            yield return null;
        }

        _foot.volume  = footstepVolume;
        _music.volume = musicVolume;
    }
}