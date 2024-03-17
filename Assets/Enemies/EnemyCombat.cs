using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] float maxAggro;

    float aggroRate = 0.5f;
    float currentAgro = 0f;
    bool isIdle = true;
    bool isAttacking = false;

    Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
        UpdateAnimator();
    }

    void FixedUpdate() {
        if (!isAttacking) {
            currentAgro += aggroRate;

            if (currentAgro >= maxAggro) { 
                currentAgro = 0f;
                isAttacking = true;
                Debug.Log("Enemy attack");
                UpdateAnimator();
            }
        }
    }

    void UpdateIdle() {
        isIdle = !isAttacking;
    }

    public void AttackEnded() {
        Debug.Log("Enemy attack over");
        isAttacking = false;
        UpdateAnimator();
    }

    void UpdateAnimator() {
        UpdateIdle();
        animator.SetBool("Idling", isIdle);
        animator.SetBool("Attacking", isAttacking);
    }
}
