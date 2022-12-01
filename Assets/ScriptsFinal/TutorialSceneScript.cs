using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialSceneScript : MonoBehaviour
{
    public void LoadMMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
