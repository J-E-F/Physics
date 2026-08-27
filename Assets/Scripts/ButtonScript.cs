using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
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
