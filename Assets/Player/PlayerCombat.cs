using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    int maxCombo = 2;

    bool isIdle = false;
    bool isBlocking = false;
    bool isAttacking = false;
    bool isKnocked = false;
    int comboPos = 0;

    Animator animator;

    void Start() {
        animator = GetComponentInChildren<Animator>();

        UpdateAnimator();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            HitPlayer();
        }

        if (!isAttacking && !isBlocking && !isKnocked) {
            if (Input.GetMouseButton(1)) {
                isBlocking = true;
                comboPos = 0;
                UpdateAnimator();
            } else if (Input.GetMouseButtonDown(0)) {
                isAttacking = true;

                if (comboPos == 0) {
                    comboPos++;
                    Debug.Log("first attack");
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
        isKnocked = true;
        isAttacking = false;
        isBlocking = false;
        comboPos = 0;
        animator.SetTrigger("IsHit");
    }

    public void EndHitPlayer() {
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
