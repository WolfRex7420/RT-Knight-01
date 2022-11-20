
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour
{
    public GameObject WinManagerUI;

    public static WinManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de WinManager dans la scène");
            return;
        }

        instance = this;
    }

    public void OnBossDeath()
    {
        WinManagerUI.SetActive(true);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadCreditsScene()
    {
        SceneManager.LoadScene("Credits");
    }
}
