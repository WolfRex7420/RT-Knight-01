using UnityEngine;
using UnityEngine.SceneManagement;

public class YouDied : MonoBehaviour
{
    public string Level0;

    public GameObject settingsWindow;

    public void MainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void CreditsScene()
    {
        SceneManager.LoadScene("Credits");
    }

    public void GiveUp()
    {
        Application.Quit();
    }
}
