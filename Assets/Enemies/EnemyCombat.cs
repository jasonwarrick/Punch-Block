using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public delegate void AttackPlayer(float outDamage);
    public static AttackPlayer attackPlayer;

    [SerializeField] float maxAggro;
    [SerializeField] float maxHealth;
    [SerializeField] float damage;

    float currentHealth = 0f;
    float aggroRate = 0.5f;
    float currentAgro = 0f;
    bool isIdle = true;
    bool isAttacking = false;
    bool isBlocking = false;
    bool isHit = false;

    Animator animator;

    void Start() {
        PlayerCombat.attackEnemy += PlayerAttacksEnemy;

        animator = GetComponent<Animator>();
        UpdateAnimator();
        HealEnemy(maxHealth - currentHealth);
    }

    void FixedUpdate() {
        if (!isAttacking && !isHit) {
            currentAgro += aggroRate;

            if (currentAgro >= maxAggro) { 
                currentAgro = 0f;
                isAttacking = true;
                // Debug.Log("Enemy attack");
                UpdateAnimator();
            }
        }
    }

    void HealEnemy(float healAmt) {
        currentHealth += healAmt;
        Debug.Log("Enemy health is at " + currentHealth);
    }

    void PlayerAttacksEnemy(float playerDamage) {
        if (!isBlocking) {
            isHit = true;
            currentHealth -= playerDamage;
            animator.SetTrigger("Hit");
            Debug.Log("Enemy health is at " + currentHealth);
        }
    }

    void UpdateIdle() {
        isIdle = !isAttacking;
    }

    public void AttackEnded() {
        // Debug.Log("Enemy attack over");
        isAttacking = false;
        UpdateAnimator();
    }

    public void EnemyHitEnded() {
        isHit = false;
    }

    void UpdateAnimator() {
        UpdateIdle();
        animator.SetBool("Idling", isIdle);
        animator.SetBool("Attacking", isAttacking);
    }

    public void TriggerAttackPlayer() {
        attackPlayer.Invoke(damage);
    }
}
