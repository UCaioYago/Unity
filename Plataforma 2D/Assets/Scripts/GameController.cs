using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController gc;
    public Text coinsText;
    public int coins;

    public Text lifeText;
    public int lives = 3;
    void Awake()
    {
        if(gc == null)
        {
            gc = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (gc != this)
        {
            Destroy(gameObject);
        }
        RefreshScreen();
    }
    public void SetLives(int life)
    {
        lives *= life;
        RefreshScreen();
    }
    public void RefreshScreen()
    {
        coinsText.text = coins.ToString();
        lifeText.text = lives.ToString();
    }
}
