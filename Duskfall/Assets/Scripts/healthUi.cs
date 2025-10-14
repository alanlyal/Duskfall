using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public Image[] hearts;

    private PlayerHealth playerHealth;

    void Update()
    {
        // Automatically find the player if not assigned
        if (playerHealth == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerHealth = player.GetComponent<PlayerHealth>();
                if (playerHealth == null)
                {
                    playerHealth = player.GetComponentInChildren<PlayerHealth>();
                    if (playerHealth == null)
                        return; // PlayerHealth not found yet
                }
            }
            else
            {
                return; // Player not spawned yet
            }
            health = playerHealth.health;
            maxHealth = playerHealth.maxHealth;
            for (int i = 0; i < hearts.Length; i++)
            {
                if (hearts[i] == null) continue; // Skip missing heart images
                if (fullHeart == null || emptyHeart == null) continue; // Skip if sprites are not assigned
                if (i < health)
                {
                    hearts[i].sprite = fullHeart;
                }
                else
                {
                    hearts[i].sprite = emptyHeart;
                }
                if (i < maxHealth)
                {
                    hearts[i].enabled = true;
                }
                else
                {
                    hearts[i].enabled = false;
                }
            }

        }
    }
}
