using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Ce script gère les dégats infligés au Soldier
public class SoldierMgr : MonoBehaviour
{
    
    [SerializeField] private Collider swordCollider;
    private float lastUseTime;
    // Effets visuels
    public GameObject hitParticles;
    public GameObject diedParticles;
    
    // Points de vie
    public float life = 10f;
    
    // Composants
    [SerializeField]
    private Animator anim;
    
    // Dégats
    private void OnTriggerEnter(Collider other)
    {
        // Si l'épée du joueur Warrior touche l'ennemi [ Warrior - Attaque1 ]
        if(other.gameObject.name == "Sword")
        {
            // Effet de particules
            GameObject go = Instantiate(hitParticles, transform.position, Quaternion.identity);
            Destroy(go,2);
            GetHit(5); // Lancement de l'animation et perte de la vie
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

        // Si l'ennemi est touché par la boule de feu du Magician [Magician - Attaque1]
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
        // Si le joueur Warrior lance son action de tournoiement [ Warrior - Attaque3 ]
        if(other.gameObject.tag == "Spin")
        {
            if (Time.time >= lastUseTime + 0.5f) // On inflige un dégat toutes les 0.5s
            {
                // Animation de coup
                GameObject go = Instantiate(hitParticles, transform.position, Quaternion.identity);
                Destroy(go,2);
                // 1 dégat infligé
                GetHit(1);
                lastUseTime = Time.time;
            }
        }
    }

    // Perte de vie
    public void GetHit(float damage)
    {
        anim.SetTrigger("Hitted"); // lancement de l'animation
        life -= damage;
        UnityEngine.Debug.Log(life);
        if(life<=0) // Lorsqu'il n'y a plus de points de vie, il meurt
        {
            life = 0;
            // Animation 
            anim.SetTrigger("died");
            Destroy(this.gameObject, 3);
            
            //Effet de particules
            GameObject go2 = Instantiate(diedParticles, transform.position, Quaternion.identity);
            go2.transform.parent = transform;
            Destroy(go2,4);
            
            // Points d'expérience à rammasser
            GameObject loot = Instantiate(GameManager.Instance.lootEnemy[0], transform.position, Quaternion.identity);
            loot.transform.position += Vector3.up;
            loot.name = "Emerald";

        }
    }
}
