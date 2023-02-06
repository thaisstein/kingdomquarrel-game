using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WaveCounter : MonoBehaviour
{
    public Text waveCounter;
    void Start() {
        int aux = AutoSpawn.instance.currentWave - 1; // number of counting waves - 1 because it is a condition to leave the loop
        waveCounter.text = aux.ToString();
    }
 
}
