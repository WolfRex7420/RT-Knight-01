using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    public GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            Debug.Log("HIT");
            collision.transform.GetComponentInParent<PlayerHealth>().TakeDamage(15);
        }
    }
}
