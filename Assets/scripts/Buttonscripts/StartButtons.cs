using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtons : MonoBehaviour
{
    // Start button → goes to MainMenu
    public void OnStartPressed()
    {
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