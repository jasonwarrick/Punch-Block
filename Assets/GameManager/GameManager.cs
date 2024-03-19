using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject enemy;

    // Start is called before the first frame update
    void Start() {
        ToggleCombat(false);

        StartScreenManager.gameStarted += StartGame;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ToggleCombat(bool flag) {
        player.GetComponentInChildren<PlayerCombat>().enabled = flag;
        enemy.GetComponent<EnemyCombat>().enabled = flag;
    }

    void StartGame() {
        ToggleCombat(true);
    }
}
