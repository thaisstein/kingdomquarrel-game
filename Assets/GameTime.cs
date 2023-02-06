using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameTime : MonoBehaviour
{
    public Text gameTime;
    void Start() {
        float aux = TimerRolling.instance.t;
        string minutes = ((int) aux/ 60).ToString();
        string seconds = (aux % 60).ToString("f2");

        gameTime.text = minutes + ":" + seconds;
    }
 
}
