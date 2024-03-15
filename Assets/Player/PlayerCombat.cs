using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    Animator animator;

    bool idle = false;
    bool punch = false;
    bool followUp = false;

    // Start is called before the first frame update
    void Start() {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update() {
        idle = !(punch || followUp);

        if (Input.GetMouseButtonDown(0)) {
            if (idle) {
                punch = true;
                animator.SetBool("Punching", punch);
            } else {
                punch = false;
                followUp = true;
            }
        }

        // Debug.Log("idle: " + idle + "; punch: " + punch + "; follow up: " + followUp);
    }

    public void AttackEnded() {
        if (!followUp) {
            punch = false;
            idle = true;
            animator.SetBool("Idling", idle);
            animator.SetBool("Punching", punch);
        }
    }
}
