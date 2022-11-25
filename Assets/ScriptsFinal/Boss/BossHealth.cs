using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public GameObject EndLevelDoor;
    public int maxHealth = 500;
    public int currentHealth;

    public float invincibilityTimeAfterHit = 2f;
    public float invincibilityFlashDelay = 0.2f;
    public bool isInvincible = false;

    public SpriteRenderer graphics;
    public BossHealthBar healthBar;

    //public AudioClip hitSound;

    public static BossHealth instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de BossHealth dans la scène");
            return;
        }

        instance = this;
    }

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
   
    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            //AudioManager.instance.PlayClipAt(hitSound, transform.position);
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);

            if (currentHealth <= 0)
            {
                BossDie();
                return;
            }

            isInvincible = true;
            StartCoroutine(InvincibilityFlash());
            StartCoroutine(HandleInvincibilityDelay());
        }
    }

    public void BossDie()
    {
        BossScript.instance.enabled = false;
        BossScript.instance.animator.SetTrigger("BDeath");
        BossScript.instance.rb.bodyType = RigidbodyType2D.Kinematic;
        BossScript.instance.rb.velocity = Vector3.zero;
        BossScript.instance.BossCollider.enabled = false;
        WinManager.instance.OnBossDeath();
        Debug.Log("Boss eliminated");
        EndLevelDoor.SetActive(true);

    }

    public IEnumerator InvincibilityFlash()
    {
        while (isInvincible)
        {
            graphics.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(invincibilityFlashDelay);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(invincibilityFlashDelay);
        }
    }

    public IEnumerator HandleInvincibilityDelay()
    {
        yield return new WaitForSeconds(invincibilityTimeAfterHit);
        isInvincible = false;
    }
}
