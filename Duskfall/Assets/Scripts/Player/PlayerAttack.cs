using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    private Animator animator;
    private PlayerMovement PlayerMovement;

    public Transform attackPos;
    public float attackRange;
    public LayerMask damageableLayer;
    public float damage;
    public float KBForce;
    public Vector2 KBAngle;

    private bool hitboxActive = false;
    private HashSet<IDamageable> targetsHit = new HashSet<IDamageable>();

    private void Awake()
    {
        animator = GetComponent<Animator>();
        PlayerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {

        if (timeBtwAttack <= 0)
        {
            if (InputManager.AttackWasPressed)
            {
                animator.SetTrigger("attack");
                timeBtwAttack = startTimeBtwAttack;
            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }

        if (hitboxActive)
        {
            CheckHitbox();
        }
    }

    public void EnableHitbox()
    {
        hitboxActive = true;
        targetsHit.Clear();
    }

    public void DisableHitbox()
    {
        hitboxActive = false;
    }

    private void CheckHitbox()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPos.position, attackRange, damageableLayer);
        foreach (Collider2D c in colliders)
        {
            IDamageable damageable = c.GetComponentInParent<IDamageable>();

            if (damageable != null && !targetsHit.Contains(damageable))
            {
                targetsHit.Add(damageable);
                damageable.Damage(damage, KBForce, new Vector2(KBAngle.x * (PlayerMovement.isFacingRight ? 1 : -1), KBAngle.y));
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
