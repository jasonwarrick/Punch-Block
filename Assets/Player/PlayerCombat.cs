using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    int maxCombo = 2;

    bool idle = false;
    bool attacking;
    int comboPos = 0;

    Animator animator;

    void Start() {
        animator = GetComponentInChildren<Animator>();

        UpdateAnimator();
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if (!attacking) {
                attacking = true;

                if (comboPos == 0) {
                    comboPos ++;
                    Debug.Log("first attack");
                    idle = comboPos == 0;
                    UpdateAnimator();
                }

                idle = comboPos == 0;
            }
        }

        
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

        idle = comboPos == 0;
        UpdateAnimator();
    }

    void UpdateAnimator() {
        animator.SetBool("Idling", idle);
        animator.SetBool("Punching", comboPos == 1);
        animator.SetBool("FollowingUp", comboPos == 2);
    }
}
