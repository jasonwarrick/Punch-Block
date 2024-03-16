using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    int maxCombo = 2;

    bool isIdle = false;
    bool isBlocking = false;
    bool isAttacking = false;
    int comboPos = 0;

    Animator animator;

    void Start() {
        animator = GetComponentInChildren<Animator>();

        UpdateAnimator();
    }

    void Update() {
        if (Input.GetMouseButton(1) && !isAttacking && !isBlocking) {
            Debug.Log("Block");
            isBlocking = true;
            comboPos = 0;
            UpdateAnimator();
        } else if (Input.GetMouseButtonDown(0) && !isAttacking && !isBlocking) {
            isAttacking = true;

            if (comboPos == 0)
            {
                comboPos++;
                Debug.Log("first attack");
                UpdateAnimator();
            }

            SetIdle();
        }

        if (Input.GetMouseButtonUp(1) && isBlocking) {
            isBlocking = false;
            UpdateAnimator();
        }
    }

    void SetIdle() {
        isIdle = comboPos == 0 && !isBlocking;
    }

    public void AttackTriggered() {
        Debug.Log("attack");
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

    public void HitPlayer() {
        
    }

    void UpdateAnimator() {
        SetIdle();
        animator.SetBool("Idling", isIdle);
        animator.SetBool("Punching", comboPos == 1);
        animator.SetBool("FollowingUp", comboPos == 2);
        animator.SetBool("Blocking", isBlocking);
    }
}
