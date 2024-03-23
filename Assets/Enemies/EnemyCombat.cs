using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public delegate void AttackPlayer(float outDamage);
    public static AttackPlayer attackPlayer;

    public delegate void EnemyHit();
    public static EnemyHit enemyHit;

    [SerializeField] float maxAggro;
    [SerializeField] float maxHealth;
    [SerializeField] float damage;
    [SerializeField] float aggroOnHit;

    float currentHealth = 0f;
    float aggroRate = 0.5f;
    float currentAgro = 0f;
    bool isIdle = true;
    bool isAttacking = false;
    bool isBlocking = false;
    bool isHit = false;
    int phase = 1;
    int comboPos = 0;

    Animator animator;
    HUDManager hudManager;

    void Start() {
        PlayerCombat.attackEnemy += PlayerAttacksEnemy;

        animator = GetComponent<Animator>();
        hudManager = FindObjectOfType<HUDManager>();

        UpdateAnimator();
        HealEnemy(maxHealth - currentHealth);
    }

    void FixedUpdate() {
        if (!isAttacking && !isHit) {
            currentAgro += aggroRate;

            if (currentAgro >= maxAggro) { 
                currentAgro = 0f;
                isAttacking = true;
                comboPos++;
                // Debug.Log("Enemy attack");
                UpdateAnimator();
            }
        }
    }

    void HealEnemy(float healAmt) {
        currentHealth += healAmt;
        hudManager.UpdateEnemyHB(currentHealth / maxHealth);
        // Debug.Log("Enemy health is at " + currentHealth);
    }

    void PlayerAttacksEnemy(float playerDamage) {
        if (!isBlocking && !isAttacking) {
            isHit = true;
            currentHealth -= playerDamage;

            if (currentHealth <= maxHealth / 2) {
                phase++;
                Debug.Log("Phase 2");
            }

            hudManager.UpdateEnemyHB(currentHealth / maxHealth);
            animator.SetTrigger("Hit");
            currentAgro += aggroOnHit;
            currentAgro = Mathf.Clamp(currentAgro, 0f, maxAggro);
            // Debug.Log("Enemy health is at " + currentHealth);
            enemyHit.Invoke();
        }
    }

    void UpdateIdle() {
        isIdle = !isAttacking;
    }

    public void AttackEnded() {
        // Debug.Log("Enemy attack over");
        if (phase == 1) {
            comboPos = 0;
            isAttacking = false;
        } else {
            if (comboPos == 1) {
                Debug.Log("First done");
                comboPos++;
                isAttacking = true;
            } else if (comboPos == 2) {
                Debug.Log("Second done");
                comboPos = 0;
                isAttacking = false;
            }
        }
        
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
