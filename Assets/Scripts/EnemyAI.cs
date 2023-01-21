using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.AI;

// Ce script gère les attaques des ennemis Soldier et Liche

public class EnemyAI : MonoBehaviour
{
    [Range(2,100)]
    public float detectDistance = 10; // Distance de détection du joueur

    public float attackDistance = 4.4f; // Distance d'attaque
    private Vector3 initialPos; 
    private Transform player; //Référence vers le personnage du joueur

    bool canAttack = true; // Possibilité d'attaquer
    
    // Les composants
    public NavMeshAgent agent;
    private Animator anim;

    // Nom de l'ennemi concerné
    private String name;

    // Scripts des dégats infligés
    public SoldierMgr soldierMgr;
    public LicheMgr licheMgr;

    // Effets de Particules
    [SerializeField] private GameObject speel;


    private void Start()
    {
        anim = GetComponent<Animator>();
        name = gameObject.tag; // Le nom de l'ennemi
        initialPos = transform.position; // position initial pour y retourner si le joueur réussi à s'échapper
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        // Choix du script pour gérer les points de vie
        if (name == "Soldier")
        {
            soldierMgr = GetComponent<SoldierMgr>();
        }
        if (name == "Liche")
        {
            licheMgr = GetComponent<LicheMgr>();
        }
    }

    private void Update()
    {
        // Si l'ennemi n'est pas mort
        if ((name == "Soldier" && soldierMgr.life > 0) || (name == "Liche" && licheMgr.life > 0))
        { 
            // Distance entre l'ennemi et le joueur
            float distance = Vector3.Distance(transform.position, player.position);

            if((distance < detectDistance)&& (distance > attackDistance)) // Si le joueur est à porté
            {
                // On le poursuit
                agent.destination = player.position;
            }
            if ((distance <= attackDistance) && canAttack) // Si le joueur est à porté et canAttack
            {
                // On peut attaquer le joueur
                canAttack = false;
                GetComponent<Animator>().SetTrigger("attack");
                StartCoroutine("AttackPlayer");
                
            }
            if (distance > detectDistance) // Si le joueur est trop loin
            {
                // On retourne à la position de départ
                agent.destination = initialPos;
            }

            // Animation de marche lorsque l'ennemi est en mouvement
            anim.SetFloat("walkSpeed", agent.velocity.magnitude); 
        }
    }

    IEnumerator AttackPlayer()
    {
        //Attaque du Soldier
        if(name == "Soldier")
        {
            yield return new WaitForSeconds(2); // 1s d'action puis 1s de cooldown
            canAttack = true; // On donne de nouveau la possibilité d'attaquer
        }
        //Attaque du Liche
        if (name == "Liche")
        {
            GameObject go = Instantiate(speel, transform.position, Quaternion.identity); // création d'une zone de magie
            go.transform.parent = transform;
            Destroy(go,5); // Destruction de la zone de magie
            yield return new WaitForSeconds(10); // 10s de cooldown
            canAttack = true; // On donne de nouveau la possibilité d'attaquer
        }
    }
    
    // Visualisation de la distance de detection
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectDistance);
    }
}
