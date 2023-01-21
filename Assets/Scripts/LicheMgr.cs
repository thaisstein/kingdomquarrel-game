using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Ce script gère les dégats infligés au Liche

public class LicheMgr : MonoBehaviour
{
    private float lastUseTime;
    // Effets visuels
    public GameObject hitParticles;
    public GameObject diedParticles;
    
    // Points de vie
    public float life = 15f;
    
    // Composants
    [SerializeField]
    private Animator anim;

    // Dégats
    private void OnTriggerEnter(Collider other)
    {
        // Si l'épée du Warrior touche l'ennemi [ Warrior - Attaque1 ]
        if(other.gameObject.tag == "Sword")
        {
            // Effet de particules
            GameObject go = Instantiate(hitParticles, transform.position, Quaternion.identity);
            Destroy(go,2);
            GetHit(5); // Perd de la vie
        }
        // Si le joueur Warrior saute sur l'ennemi [ Warrior - Attaque2 ]
        if(other.gameObject.tag == "jumpAttack")
        {
            // L'ennemi est repoussé dans la direction opposée
            float thrust = 7f; // distance de propultion
            Vector3 moveDirection = -transform.forward * thrust;
            GetComponent<NavMeshAgent>().velocity = moveDirection;

            // Animation de coup
            GameObject go = Instantiate(hitParticles, transform.position, Quaternion.identity);
            Destroy(go,2);
            // 2 dégats infligés
            GetHit(2);
        }
        // Si l'ennemi est touché par la boule de feu du Magician [ Magician - Attaque1 ]
        if(other.gameObject.tag == "FireBall")
        {
            // Effet de particules
            GameObject go = Instantiate(hitParticles, transform.position, Quaternion.identity);
            Destroy(go,2);
            // L'ennemi est repoussé
            float thrust = 10f; // distance de propultion
            Vector3 moveDirection = -transform.forward * thrust;
            GetComponent<NavMeshAgent>().velocity = moveDirection;

            GetHit(5); // Perd de la vie
        }

        // Si l'ennemi est touché par la boule de feu du Magician [Magician - Attaque2]
        if(other.gameObject.tag == "Projectile")
        {
            // Effet de particules
            GameObject go = Instantiate(hitParticles, transform.position, Quaternion.identity);
            Destroy(go,2);
            GetHit(2); // Perd de la vie
        }
    }

    private void OnTriggerStay(Collider other)
    { 
        //Si il est touché par la toupie [ Warrior - Attaque2 ]
        if(other.gameObject.tag == "Spin")
        {
            if (Time.time >= lastUseTime + 0.5f) // On inflige un dégat toutes les 0.5s
            {
                // Effet de particules pour simuler l'impact
                GameObject go = Instantiate(hitParticles, transform.position, Quaternion.identity);
                Destroy(go,2);
                GetHit(1);
                lastUseTime = Time.time;
            }
        }
    }

    // Perte de vie
    public void GetHit(float damage)
    {
        anim.SetTrigger("Hitted"); // On joue l'animation
        life -= damage;
        if(life<=0) // Lorsqu'il n'y a plus de points de vie, il meurt
        {
            life = 0;
            // Animation de mort 
            anim.SetTrigger("died");
            Destroy(this.gameObject, 2);
            
            //Effet de particules pour simuler l'impact
            GameObject go2 = Instantiate(diedParticles, transform.position, Quaternion.identity);
            go2.transform.parent = transform;
            Destroy(go2,3);
            
            // Points d'expérience à ramasser
            GameObject loot = Instantiate(GameManager.Instance.lootEnemy[0], transform.position, Quaternion.identity);
            loot.transform.position += Vector3.up;
            loot.name = "Emerald";
        }
    }

}
