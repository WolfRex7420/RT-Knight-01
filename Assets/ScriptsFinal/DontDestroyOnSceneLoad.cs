
using UnityEngine;

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

    }*/
}
