using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] Slider playerHealthBar;
    [SerializeField] Slider enemyHealthBar;

    void Start() {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePlayerHB(float newHealthPercent) {
        playerHealthBar.value = 1f - newHealthPercent;
    }

    public void UpdateEnemyHB(float newHealthPercent) {
        enemyHealthBar.value = 1f - newHealthPercent;
    }
}
