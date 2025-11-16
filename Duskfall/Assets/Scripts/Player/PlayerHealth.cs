using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [Header("Health")]
    public float maxHealth = 5;
    public float currentHealth;

    private bool damageable = true;

    private HealthUI healthUI;

    [Header("iFrames")]
    [SerializeField] private float iFrameDuration;
    [SerializeField] private float numOfFlashes;
    private SpriteRenderer spriteRend;
    private PlayerMovement playerMovement;

    private void Start()
    {
        if (healthUI == null)
        {
            healthUI = FindObjectOfType<HealthUI>();
        }

        spriteRend = GetComponent<SpriteRenderer>();
        playerMovement = GetComponent<PlayerMovement>();

        if (healthUI.savedHealth <= 0)
        {
            currentHealth = maxHealth;
            healthUI.savedHealth = maxHealth;
        }
        else
        {
            currentHealth = healthUI.savedHealth;
        }

        healthUI.SetMaxHearts(maxHealth);
        healthUI.UpdateHearts(currentHealth);
    }

    public void Damage(float damageAmount)
    {
        if (currentHealth > 0 && damageable)
        {
            currentHealth -= damageAmount;
            healthUI.UpdateHearts(currentHealth);
            StartCoroutine(Invunerability());
        }

        if (currentHealth <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }

    private IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(7, 8, true);
        damageable = false;
        for (int i = 0; i < numOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0.5f, 0.5f);
            yield return new WaitForSeconds(iFrameDuration / (numOfFlashes));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFrameDuration / (numOfFlashes));
        }
        Physics2D.IgnoreLayerCollision(7, 8, false);
        damageable = true;
    }

    public void Damage(float damageAmount, float KBForce, Vector2 KBAngle)
    {
        if (currentHealth > 0 && damageable)
        {
            playerMovement.AddExternalForce(KBAngle * KBForce);
            currentHealth -= damageAmount;
            healthUI.UpdateHearts(currentHealth);
            StartCoroutine(Invunerability());
        }

        if (currentHealth <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }
}
