
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnSceneLoad : MonoBehaviour
{
    public GameObject[] objects;

    void Awake()
    {
        foreach (var element in objects)
        {
            DontDestroyOnLoad(element);
        }
    }

    /*public RemoveFromDestroy()
    {
        //if retry button is pressed stop dont destroy on load
        //if main menu is accesed stop dont destroy on load
        
    }*/
}
