using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{ //aha
    public BoxCollider2D collider;
    public int weaponDamage;

    public void start()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<BossHealth>().TakeDamage(weaponDamage);
        }

        if (collision.gameObject.CompareTag("EnemyS"))
        {
            collision.gameObject.GetComponent<Mob1>().TakeDamage(weaponDamage);
        }
    }
}
