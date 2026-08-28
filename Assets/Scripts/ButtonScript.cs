using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1f; // Ensure the game is running at normal speed when the scene starts
    }
    public void startGamePlayGym()
    {
        // Load the gameplay scene for the gym
        SceneManager.LoadScene("MomentumScene");
    }

    public void startMainLevel()
    {
        SceneManager.LoadScene("Mainlevel");
    }
    public void exitGame()
    {
        Application.Quit();
    }
}
