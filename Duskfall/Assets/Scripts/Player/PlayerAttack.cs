using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public GameObject slashEffect;

    public Transform attackPos;
    public float attackRange;
    public LayerMask enemy;
    public int damage;

    private void Update()
    {

        if (timeBtwAttack <= 0)
        {
            if (InputManager.AttackWasPressed)
            {
                StartCoroutine(PlaySlashEffect());
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<enemyController>().Damage(damage);
                }

                timeBtwAttack = startTimeBtwAttack;
            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    private IEnumerator PlaySlashEffect()
    {
        slashEffect.SetActive(true);
        yield return new WaitForSeconds(startTimeBtwAttack);
        slashEffect.SetActive(false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
