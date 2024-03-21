using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] GameObject hud;
    [SerializeField] GameObject startScreen;

    // Start is called before the first frame update
    void Start() {
        hud.GetComponent<Canvas>().enabled = false;
        TriggerStartScreen();

        StartScreenManager.gameStarted += ActivateHUD;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TriggerStartScreen() {
        Instantiate(startScreen);
    }

    void ActivateHUD() {
        hud.GetComponent<Canvas>().enabled = true;
    }
}
