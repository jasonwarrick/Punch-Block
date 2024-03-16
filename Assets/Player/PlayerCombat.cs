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

    // Start is called before the first frame update
    void Start() {
        animator = GetComponentInChildren<Animator>();

        UpdateAnimator();
        idle = comboPos == 0;
    }

    // Update is called once per frame
    void Update() {
        Debug.Log(comboPos == 0);
        idle = comboPos == 0;

        if (Input.GetMouseButtonDown(0)) {
            attacking = true;

            if (comboPos == 0) {
                comboPos ++;
                Debug.Log("first attack");
                UpdateAnimator();
            }

            if (comboPos == maxCombo) {
                comboPos = 1;
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
            Debug.Log("Attack queued");
        } else {
            attacking = false;
            comboPos = 0;
            Debug.Log("Attack not queued");
        }

        UpdateAnimator();
    }

    void UpdateAnimator() {
        animator.SetBool("Idling", idle);
        animator.SetBool("Punching", comboPos == 1);
        animator.SetBool("FollowingUp", comboPos == 2);
    }
}
