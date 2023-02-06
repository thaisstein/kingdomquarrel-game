using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TimerRolling : MonoBehaviour
{
    public static TimerRolling instance;
    public Text timerText;
    public float t;
    private float startTime;

    private void Awake() {
        instance = this;
    }
    void Start()
    {
        startTime = Time.time;
        
    }

    void Update()
    {
        t = Time.time - startTime;
        string minutes = ((int) t / 60).ToString();
        string seconds = (t % 60).ToString("f2");
        timerText.text = minutes + ":" + seconds;
    }
}
