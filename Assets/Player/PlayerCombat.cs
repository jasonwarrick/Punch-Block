using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    int maxCombo = 2;

    bool idle = false;
    bool blocking = false;
    bool attacking = false;
    int comboPos = 0;

    Animator animator;

    void Start() {
        animator = GetComponentInChildren<Animator>();

        UpdateAnimator();
    }

    void Update() {
        if (Input.GetMouseButtonDown(1) && !attacking) {
            Debug.Log("Block");
            blocking = true;
            comboPos = 0;
            UpdateAnimator();
        } else if (Input.GetMouseButtonDown(0) && !attacking && !blocking) {
            attacking = true;

            if (comboPos == 0)
            {
                comboPos++;
                Debug.Log("first attack");
                UpdateAnimator();
            }

            SetIdle();
        }

        if (Input.GetMouseButtonUp(1) && blocking) {
            blocking = false;
            UpdateAnimator();
        }
    }

    void SetIdle() {
        idle = comboPos == 0 && !blocking;
    }

    public void AttackTriggered() {
        Debug.Log("attack");
        attacking = false;
    }

    public void AttackEnded() {
        Debug.Log("ended");
        if (attacking && comboPos > 0) {
            comboPos++;

            if (comboPos > maxCombo) {
                comboPos = 1;
            }

            attacking = true;
            Debug.Log("Attack queued");
        } else {
            attacking = false;
            comboPos = 0;
            
            Debug.Log("Attack not queued, idle = " + idle);
        }

        UpdateAnimator();
    }

    public void HitPlayer() {
        
    }

    void UpdateAnimator() {
        SetIdle();
        animator.SetBool("Idling", idle);
        animator.SetBool("Punching", comboPos == 1);
        animator.SetBool("FollowingUp", comboPos == 2);
        animator.SetBool("Blocking", blocking);
    }
}
