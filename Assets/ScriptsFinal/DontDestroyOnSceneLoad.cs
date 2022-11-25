
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnSceneLoad : MonoBehaviour
{
    public GameObject[] objects;
    public static DontDestroyOnSceneLoad Instance;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de DontDestroyOnSceneLoad dans la scène");
            return;
        }

        Instance = this;
        foreach (var element in objects)
        {
            DontDestroyOnLoad(element);
        }
    }

    /*public void RemoveFromDestroy()
    {
        //if retry button is pressed stop dont destroy on load
        //if main menu is accesed stop dont destroy on load
        if (Input.GetKeyDown(KeyCode.A))
        {
            DestroyImmediate(Instance);
        }
    }*/
}
