using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigg : MonoBehaviour
{
    public GameObject BossHealthBar;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            BossHealthBar.SetActive(true);
        }
    }
}
