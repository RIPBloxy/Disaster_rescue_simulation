using UnityEngine;
using UnityEngine.SceneManagement;

public class CompletedButtons : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        // Destroy the persistent player when completed screen loads
        // so it starts fresh when we restart
        PlayerPersists player = FindObjectOfType<PlayerPersists>();
        if (player != null)
            Destroy(player.gameObject);
    }

    // Restart button → restarts the game
    public void OnRestartPressed()
    {
        SceneManager.LoadScene("MainScene");
    }

    // Mainmenu button → goes to main menu
    public void OnMainMenuPressed()
    {
        SceneManager.LoadScene("MainmenuScene");
    }
}