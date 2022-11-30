using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossExitDoor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Boss.instance.enabled = false;
            Boss.instance.Banimator.SetTrigger("BDeath");
            Boss.instance.Brb.bodyType = RigidbodyType2D.Kinematic;
            Boss.instance.Brb.velocity = Vector3.zero;
            Boss.instance.BossCollider.enabled = false;
            WinManager.instance.OnBossDeath();
        }
    }
}
