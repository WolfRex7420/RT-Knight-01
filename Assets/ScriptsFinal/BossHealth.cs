using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public float invincibilityTimeAfterHit = 3f;
    public float invincibilityFlashDelay = 0.2f;
    public bool isInvincible = false;

    public SpriteRenderer graphics;
    public HealthBar healthBar;

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
                Die();
                return;
            }

            isInvincible = true;
            StartCoroutine(InvincibilityFlash());
            StartCoroutine(HandleInvincibilityDelay());
        }
    }

    public void Die()
    {
        BossScript.instance.enabled = false;
        BossScript.instance.animator.SetTrigger("DeathBoss");
        BossScript.instance.rb.bodyType = RigidbodyType2D.Kinematic;
        BossScript.instance.rb.velocity = Vector3.zero;
        BossScript.instance.BossCollider.enabled = false;
        GameOverManager.instance.OnBossDeath();
        Debug.Log("Boss eliminated");
        
    }

    /* public void Respawn()
     {
         PlayerScript.instance.enabled = true;
         PlayerScript.instance.animator.SetTrigger("Respawn");
         PlayerScript.instance.rb.bodyType = RigidbodyType2D.Dynamic;
         PlayerScript.instance.playerCollider.enabled = true;
         currentHealth = maxHealth;
         healthBar.SetHealth(currentHealth);
     }*/

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
