using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WeaponScript : MonoBehaviour
{
    private bool swing = false;
    int degree = 0;

    private float weaponX = -0.2f;
    private float weaponY = 0.1f;

    //public Sprite[] upgrades;
    //private int spriteIndex = 0;
    public int weaponPower;

    Vector3 pos;
    public GameObject player;

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            GetComponent<SpriteRenderer>().enabled = true;
            transform.GetChild(0).gameObject.SetActive(true);
            Attack();
        }
    }

    private void FixedUpdate()
    {
        if (swing)
        {
            degree -= 7;
            if (degree < -65)
            {
                degree = 0;
                swing = false;
                GetComponent<SpriteRenderer>().enabled = false;
                transform.GetChild(0).gameObject.SetActive(false);
            }
            transform.eulerAngles = Vector3.forward * degree;
        }
    }

    void Attack()
    {
        if (player.GetComponent<PlayerScript>().turnedLeft)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            transform.localScale = new Vector3(-0.069f, -0.038f, 0);
            weaponX = -0.0f;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
            transform.localScale = new Vector3(0.084f, -0.038f, 0);
            weaponX = 0.f;
        }
        pos = player.transform.position;
        pos.x += weaponX;
        pos.y += weaponY;
        transform.position = pos;
        swing = true;
    }

   /* public void UpgradeWeapon()
    {
        if (spriteIndex < upgrades.Length - 1)
        {
            spriteIndex++;
        }
        GetComponent<SpriteRenderer>().sprite = upgrades[spriteIndex];
        weaponPower++;
    }*/
}
