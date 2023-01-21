using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ce script gère le comportement du projectile FireBall à l'attaque 1 du Magician

public class FireBall : MonoBehaviour
{
    public Transform FirePoint;
    public GameObject test;
    public GameObject explosionParticles;
    void Start()
    {
        FirePoint = GameObject.FindGameObjectWithTag("FirePoint").transform;
    }

    // Update is called once per frame
    void Update()
    {
       // GetComponent<Rigidbody>().velocity = 10 * FirePoint.forward;
       GetComponent<Rigidbody>().AddRelativeForce(new Vector3 
                                                (0, 70f,0));
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(explosionParticles, transform.position, transform.rotation);
        Destroy(gameObject);
        Destroy(explosionParticles);
    }
}
