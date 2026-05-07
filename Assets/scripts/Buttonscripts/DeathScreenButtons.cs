using UnityEngine;
using UnityEngine.SceneManagement;

public class DiedScreenButtons : MonoBehaviour
{
    void Start()
    {
        // Unlock and show cursor so player can click buttons
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Destroy the persistent player so game resets properly
        PlayerPersists player = FindObjectOfType<PlayerPersists>();
        if (player != null)
            Destroy(player.gameObject);
    }

    public void OnRetryPressed()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void OnMainMenuPressed()
    {
        SceneManager.LoadScene("MainmenuScene");
    }
}