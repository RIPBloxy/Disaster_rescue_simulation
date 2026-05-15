using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtons : MonoBehaviour
{
    [Header("Loading Screen")]
    public GameObject loadingScreenPanel;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (loadingScreenPanel != null)
            loadingScreenPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            OnStartPressed();
    }

    public void OnStartPressed()
    {
        StartCoroutine(LoadWithDelay());
    }

    private System.Collections.IEnumerator LoadWithDelay()
    {
        if (loadingScreenPanel != null)
            loadingScreenPanel.SetActive(true);

        yield return new WaitForSeconds(2f);

        DestroyPersistentPlayer();
        SceneManager.LoadScene("Mainscene");
    }

    private void DestroyPersistentPlayer()
    {
        PlayerPersists player = FindObjectOfType<PlayerPersists>();
        if (player != null)
            Destroy(player.gameObject);
    }
}