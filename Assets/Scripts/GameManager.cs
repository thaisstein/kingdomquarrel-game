using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ce script gère l'évolution et les points d'expérience
public class GameManager : MonoBehaviour
{
    // Pattern Singleton
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }

    
    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Caractéristiques du joueur
    public int xp = 0;

    public void ShowXP()
    {
        print("Expérience = " + xp);
    }

    // Loot
    public GameObject[] lootEnemy;
}
