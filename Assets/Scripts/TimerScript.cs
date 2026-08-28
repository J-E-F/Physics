using TMPro;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float elapsedTime;

    public TextMeshProUGUI resetTimer;
    public BallMovment ballMovmentScript;
    private void Start()
    {
        Time.timeScale = 1f; // Ensure the game is running at normal speed when the scene starts
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        resetTimer.text = "Restart:" + ballMovmentScript.restartTimer.ToString();
    }
}
