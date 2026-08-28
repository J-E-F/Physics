using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScript : MonoBehaviour
{
    public bool activateWinCondition = false;
    public GivePoints points;
    public int pointscore;
    public TextMeshProUGUI pointText;
    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if(activateWinCondition)
        {
            pointText.text = "Points: " + pointscore.ToString();
        }
    }
    public void restartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f; // Resume time scale when restarting
    }
    public void mainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
