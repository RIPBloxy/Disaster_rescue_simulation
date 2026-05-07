using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // drag "timer" here
    public float timeLeft = 300f;    // 5 minutes

    void Update()
    {
        timeLeft -= Time.deltaTime;

        int minutes = Mathf.FloorToInt(timeLeft / 60f);
        int seconds = Mathf.FloorToInt(timeLeft % 60f);
        timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);

        if (timeLeft <= 0f)
            SceneManager.LoadScene("Gameover");
    }
}