using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    public GameObject player;
    public int weaponDamage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        weaponDamage = transform.parent.gameObject.GetComponent<Weapon>().weaponPower;
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyPatrol>().TakeDamage(weaponDamage);
        }
        /*if (collision.gameObject.CompareTag("Spawner"))
        {
            collision.gameObject.GetComponent<EnemyPatrol>().TakeDamage(weaponDamage);
        }*/
    }
}
