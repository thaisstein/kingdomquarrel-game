using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script de rotation des emeralds( = points d'expérience ) 
public class Rotate : MonoBehaviour
{
    
    [SerializeField]
    private Vector3 rot;

    void Update()
    {
        transform.Rotate(rot * Time.deltaTime); // rotation de l'objet
    }
}
