using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;
using UnityEngine.UI;

public class StartScreenManager : MonoBehaviour
{
    public delegate void GameStarted();
    public static GameStarted gameStarted;

    [SerializeField] Sprite[] sprites = new Sprite[4];

    float timer = 0f;
    int arrPos = 0;

    [SerializeField] Image countdownImage;

    // Start is called before the first frame update
    void Start() {
        countdownImage.sprite = sprites[arrPos];
    }

    // Update is called once per frame
    void Update() {
        timer += Time.deltaTime;

        if (timer >= 1f) {
            timer = 0f;
            arrPos++;

            if (arrPos + 1 > sprites.Length) {
                gameStarted.Invoke();
                Destroy(gameObject);
            } else {
                countdownImage.sprite = sprites[arrPos];
            }
        }
    }
}
