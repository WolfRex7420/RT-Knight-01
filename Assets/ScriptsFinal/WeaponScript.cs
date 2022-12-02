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
        if (collision.gameObject.CompareTag("Enemy1"))
        {
            collision.gameObject.GetComponent<Mob1>().TakeDamage1(weaponDamage);
        }

        if (collision.gameObject.CompareTag("Enemy2"))
        {
            collision.gameObject.GetComponent<Mob2>().TakeDamage2(weaponDamage);
        }

        if (collision.gameObject.CompareTag("Enemy3"))
        {
            collision.gameObject.GetComponent<Mob3>().TakeDamage3(weaponDamage);
        }

        if (collision.gameObject.CompareTag("Enemy4"))
        {
            collision.gameObject.GetComponent<Mob4>().TakeDamage4(weaponDamage);
        }

        if (collision.gameObject.CompareTag("Enemy5"))
        {
            collision.gameObject.GetComponent<Mob5>().TakeDamage5(weaponDamage);
        }

        if (collision.gameObject.CompareTag("Enemy6"))
        {
            collision.gameObject.GetComponent<Mob6>().TakeDamage6(weaponDamage);
        }
    }
}
