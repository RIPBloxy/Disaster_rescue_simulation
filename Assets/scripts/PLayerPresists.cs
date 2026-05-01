using UnityEngine;

public class PlayerPersists : MonoBehaviour
{
    public static PlayerPersists Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // entire PLAYER + all children persist
        }
        else
        {
            Destroy(gameObject);
        }
    }
}