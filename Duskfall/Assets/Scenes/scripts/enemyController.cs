using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController: MonoBehaviour
{
    //movement
    public float speed;
    private bool movingRight = true;
    public Transform groundDetection;
    ///////////
    //health
    public int health = 3;
    private int currentHealth;
    ///////////
    //damage
    private SpriteRenderer spriteRenderer;
    public Color damageColor = Color.red;
    private Color originalColor;
    public float damageflash = 0.1f;
    private float flashTimer = 0f;
    private bool flash = false;
    private void Start()
    {
        currentHealth = health;
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)// this part is temp remove later
        {
            originalColor = spriteRenderer.color;
        }
    }
    public void Update()
    {
        transform.Translate(Vector2.right * -speed * Time.deltaTime);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 2f);
        if (groundInfo.collider == false)
        {
            if (movingRight)
            {
                transform.eulerAngles = new Vector3(0, 100, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }

        if (flash && spriteRenderer != null)
        {
            flashTimer -= Time.deltaTime;
            if (flashTimer < 0f)
            {
                spriteRenderer.color = originalColor;
                flash = false;
            }
        }
        //if (Input.GetKeyDown(KeyCode.Space))// to test delete later
        //{
           // Damage(1);
        //}
    }
    public void Damage(int damage) 
    {
    currentHealth -= damage;
        FlashRed();
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }
    private void FlashRed() 
    {
        if (spriteRenderer != null)
        { 
        spriteRenderer.color = damageColor;
            flashTimer = damageflash;
            flash = true;
        }
    }


}
