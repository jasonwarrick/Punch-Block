using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public delegate void AttackEnemy(float damage);
    public static AttackEnemy attackEnemy;

    [SerializeField] float maxHealth;
    [SerializeField] float damage;
    int maxCombo = 2;

    bool isIdle = false;
    bool isBlocking = false;
    bool isAttacking = false;
    bool isKnocked = false;
    int comboPos = 0;

    float currentHealth = 0f;

    Animator animator;
    HUDManager hudManager;

    void Start() {
        EnemyCombat.attackPlayer += EnemyAttacksPlayer;

        animator = GetComponentInChildren<Animator>();
        hudManager = FindObjectOfType<HUDManager>();

        UpdateAnimator();
        HealPlayer(maxHealth - currentHealth);
    }

    void Update() {
        if (!isAttacking && !isBlocking && !isKnocked) {
            if (Input.GetMouseButton(1)) {
                isBlocking = true;
                comboPos = 0;
                UpdateAnimator();
            } else if (Input.GetMouseButtonDown(0)) {
                isAttacking = true;

                if (comboPos == 0) {
                    comboPos++;
                    UpdateAnimator();
                }

                SetIdle();
            }
        } 

        if (Input.GetMouseButtonUp(1) && isBlocking) {
            isBlocking = false;
            UpdateAnimator();
        }
    }

    void SetIdle() {
        isIdle = comboPos == 0 && !isBlocking && !isKnocked;
    }

    void HealPlayer(float healAmt) {
        currentHealth += healAmt;
        hudManager.UpdatePlayerHB(currentHealth / maxHealth);
        Debug.Log("Enemy health is at " + currentHealth);
    }

    public void AttackTriggered() {
        Debug.Log("attack");
        attackEnemy.Invoke(damage);
    }

    public void ReadyForNextAttack() {
        isAttacking = false;
    }

    public void AttackEnded() {
        Debug.Log("ended");
        if (isAttacking && comboPos > 0) {
            comboPos++;

            if (comboPos > maxCombo) {
                comboPos = 1;
            }

            isAttacking = true;
            Debug.Log("Attack queued");
        } else {
            isAttacking = false;
            comboPos = 0;
            
            Debug.Log("Attack not queued, idle = " + isIdle);
        }

        UpdateAnimator();
    }

    public void EnemyAttacksPlayer(float enemyDamage) {
        if(!isBlocking) {
            isKnocked = true;
            isAttacking = false;
            comboPos = 0;
            currentHealth -= enemyDamage;
            hudManager.UpdatePlayerHB(currentHealth / maxHealth);
            animator.SetTrigger("IsHit");
            Debug.Log("Player health is at " + currentHealth);
        }
    }

    public void EndPlayerHit() {
        isKnocked = false;
        UpdateAnimator();
    }

    void UpdateAnimator() {
        SetIdle();
        animator.SetBool("Idling", isIdle);
        animator.SetBool("Punching", comboPos == 1);
        animator.SetBool("FollowingUp", comboPos == 2);
        animator.SetBool("Blocking", isBlocking);
    }
}
