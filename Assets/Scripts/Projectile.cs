using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private int nbre = 0; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (nbre >= 5)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        nbre += 1;
    }
}
