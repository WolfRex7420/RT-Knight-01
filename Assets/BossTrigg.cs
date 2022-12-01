using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigg : MonoBehaviour
{
    public GameObject BossHealthBar;
    public void Update()
        {
         BossHealthBar.SetActive(true);
        }
}
