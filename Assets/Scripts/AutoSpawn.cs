using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AutoSpawn : MonoBehaviour
{
    public static AutoSpawn instance;
    public GameObject[] enemies;
    public float spawnRate = 1f; // taux de spawn en secondes
    public int MaxSpawn; // nombre d'ennemis par vague
    private float nextSpawn;
    private int Nr; // compteur d'ennemis
    public int currentWave = 1; // numéro de vague actuelle
    private bool canSpawn = true;

    private bool pause = false;
    private bool endGame = false;
    
    [SerializeField] Text WavesTextField;
    private float fadeTime = 2.5f;
    private bool isFading;

    void Awake () {
        instance = this;
    }
    void Start()
    {
        ChangeWave(1);
        Debug.Log("Début du Jeu, vague 1");
        // MaxSpawn = 1; // vagues 1
    }

    void Update()
    {
        GameObject LicheEnnemy = GameObject.FindGameObjectWithTag("Liche");
        GameObject SoldierEnnemy = GameObject.FindGameObjectWithTag("Liche");

        if (canSpawn) // Spawn un ennnemi toutes les 2s
        {
            if (Time.time > nextSpawn && Nr < MaxSpawn) // Spawn un ennnemi toutes les 2s
            {
                nextSpawn = Time.time + spawnRate;
                Instantiate(enemies[UnityEngine.Random.Range(0,2)], transform.position, Quaternion.identity);
                Nr++;
            }

            if (Nr >= MaxSpawn) // si le nombre maximum d'ennemis a été atteint pour cette vague
            {
                canSpawn = false;
            }
        }
        else
        {
            if(!pause && !LicheEnnemy && !SoldierEnnemy )
            {
                pause = true;
                StartNextWave();
            }
            
        }

        if(!LicheEnnemy && !SoldierEnnemy && endGame == true) {
                Debug.Log("Jeu Terminé !");
                SceneManager.LoadScene(5);
        }

    }

    void StartNextWave()
    {
        currentWave = currentWave + 1; // passer à la vague suivante
        Nr = 0; // réinitialiser le décompte d'ennemis
        if (currentWave <= 3)
        {
            StartCoroutine("Pause");
            if(currentWave==2)
            {
                ChangeWave(2);
                MaxSpawn += 5 ; // vague 2
            }
            if(currentWave==3)
            {
                ChangeWave(3);
                MaxSpawn += 5; // vague 3
            }
        }
        else 
        {
            // Stop spawning enemies
            canSpawn = false;
            endGame = true;
        }
    }

    IEnumerator Pause() 
    {
        yield return new WaitForSeconds(5); // pause entre les vagues
        canSpawn = true;
        pause = false;
    } 

    void ChangeWave(int currentWave) {
            switch (currentWave)
        {
            case 1:
                WavesTextField.text = "Wave " + currentWave + " ! ";
                StartCoroutine(FadeInOut());
                break;
            case 2:
                WavesTextField.text = "Wave " + currentWave + " ! ";
                StartCoroutine(FadeInOut());
                break;
            case 3:
                WavesTextField.text = "Wave " + currentWave + " ! ";
                StartCoroutine(FadeInOut());
                break;
            default:
                break;
        }
    }

    IEnumerator FadeInOut() 
        {
        if (isFading)
            yield break;
        isFading = true;

        // Fade in
        float startAlpha = WavesTextField.color.a;
        float rate = 1.0f / fadeTime;
        float progress = 0.0f;

        while (progress < 1.0)
        {
            Color tempColor = WavesTextField.color;
            tempColor.a = Mathf.Lerp(startAlpha, 1, progress);
            WavesTextField.color = tempColor;
            progress += rate * Time.deltaTime;
            yield return null;
        }
        Color tempColor2 = WavesTextField.color;
        tempColor2.a = 1;
        WavesTextField.color = tempColor2;

        // Fade out
        startAlpha = WavesTextField.color.a;
        progress = 0;
        while (progress < 1.0)
        {
            Color tempColor = WavesTextField.color;
            tempColor.a = Mathf.Lerp(startAlpha, 0, progress);
            WavesTextField.color = tempColor;
            progress += rate * Time.deltaTime;
            yield return null;
        }
        Color tempColor3 = WavesTextField.color;
        tempColor3.a = 0;
        WavesTextField.color = tempColor3;
        isFading = false;
    }

}