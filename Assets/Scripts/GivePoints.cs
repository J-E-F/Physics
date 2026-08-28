using UnityEngine;

public class GivePoints : MonoBehaviour
{
    public int pointscore;

    public int pointAmount;

    public TimerScript timerScript;

    public GameObject winScreen;
    public WinScript winScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Add points to the player's score
            pointscore += pointAmount;

            // Optionally, you can also access the timerScript if needed
            // For example, you could log the elapsed time when points are given
            Debug.Log("Points given! Current score: " + pointscore + ". Elapsed time: " + timerScript.elapsedTime);
            int timerValue = Mathf.FloorToInt(timerScript.elapsedTime);
            pointAmount-= timerValue;
            Invoke("WinGame", 0.5f);
        }
    }
    public void WinGame()
    {
        winScreen.SetActive(true);
        Time.timeScale = 0f; // Pause the game
        winScript.activateWinCondition = true;
        winScript.pointscore = pointAmount;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
