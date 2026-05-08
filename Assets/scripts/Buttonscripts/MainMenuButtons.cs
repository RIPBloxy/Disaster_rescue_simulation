using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    // Play button → goes to MainScene
    public void OnPlayPressed()
    {
        SceneManager.LoadScene("Lobby");
    }

    // Setting button → shows Coming Soon
    public void OnSettingPressed()
    {
        SceneManager.LoadScene("Comming Soon");
    }

    // Exit button → closes the game
    public void OnExitPressed()
    {
        Application.Quit();
    }
}