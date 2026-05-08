using UnityEngine;
using UnityEngine.SceneManagement;

public class Crossbutttons : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Destroy the persistent player so game resets properly
        PlayerPersists player = FindObjectOfType<PlayerPersists>();
        if (player != null)
            Destroy(player.gameObject);
    }

    // Update is called once per frame
    public void OnCrossPressed()
    {
        SceneManager.LoadScene("MainmenuScene");
   }
}
