using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    [SerializeField] AudioSource punchWhoosh1;
    [SerializeField] AudioSource punchWhoosh2;
    [SerializeField] AudioSource punchImpact;

    void Start() {
        EnemyCombat.enemyHit += PlayPunchImpact;
    }

    public void PlayPunchWhoosh(int punch) {
        switch (punch) {
            case 1:
                punchWhoosh1.Play();
                break;

            case 2:
                punchWhoosh2.Play();
                break;
        }
    }

    public void PlayPunchImpact() {
        punchImpact.Play();
    }
}
