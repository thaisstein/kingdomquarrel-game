using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Ce script gère les attaques du joueur Magician
public class Magician : MonoBehaviour
{
    public GameObject FireBall; // Boule de feu
    public GameObject Wall; // Mur de glace
    public GameObject WallParticles; // Mur de glace
    public Transform FirePoint; // position de départ pour la création de la boule de feu
    public GameObject Projectile; // référence au projectile qui va orbiter autour du personnage
    

    private bool isWall = false;
    private bool isProjectile = false;

    [SerializeField]
    private float lastUseTime;

    //Les composants
    private Animator anim;

    // Points de vie
     [SerializeField] int maxHealth = 40;
    public int currentHealth;
     [SerializeField] Image healthBar;

    void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
    }
    void Update()
    {
        Attack();
    }

    public void Attack(){

        // On lance la première attaque au clique gauche de la souris
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine("AttackFireBall");
        }

        // On lance la deuxième attaque au clique droit de la souris
        
        if (Input.GetMouseButton(1) & !isProjectile)
        { 
            isProjectile = true;
            StartCoroutine("AttackProjectile");
    
        }

        // On lance la troisière attaque lorsqu'on appuie sur la barre espace
         if (Input.GetKeyDown(KeyCode.Space) & !isWall)
        {
            isWall = true;
            StartCoroutine("AttackWall");
        }
    }
    IEnumerator AttackFireBall()
    {
        anim.SetTrigger("attackFire");
        yield return new WaitForSeconds(1);
        Quaternion rot = FirePoint.rotation;
        Vector3 pos = FirePoint.position;
        Instantiate(FireBall, pos, rot);
        yield return new WaitForSeconds(2); //2s de cooldown 
    }

    IEnumerator AttackProjectile()
    {
        GameObject projectile = Instantiate(Projectile, transform.position + transform.forward * 3  + new Vector3(0, 2f, 0), Quaternion.identity);
        projectile.transform.parent = transform;
        float orbitSpeed = 350f; // vitesse d'orbite
        float startTime = Time.time;
        while (Time.time - startTime <= 5)
        {
            projectile.transform.RotateAround(transform.position, Vector3.up, orbitSpeed * Time.deltaTime);
            yield return null;
        }
        // détruit le projectile après la durée d'orbite
        Destroy(projectile);
        yield return new WaitForSeconds(5); //2s de cooldown
        isProjectile = false;
    }

    IEnumerator AttackWall()
    {
        anim.SetTrigger("attackWall");
        // Création du mur
        GameObject go1 = Instantiate(Wall, transform.position + (transform.forward * 3), transform.rotation*Quaternion.AngleAxis(90f, Vector3.up));
        GameObject go2 = Instantiate(WallParticles, transform.position + (transform.forward * 3), transform.rotation*Quaternion.AngleAxis(90f, Vector3.up));
        //Disparition du mur après 5s
        Destroy(go1,5);
        Destroy(go2,5);
        yield return new WaitForSeconds(10); // 10s de cooldown
        isWall = false;
    }


    // Dégats
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "SoldierSword")
        {
            //UnityEngine.Debug.Log("touché");
            GetHit(5);
            anim.SetTrigger("hitted");
        }

        if (other.gameObject.CompareTag("Trap"))
        {
            GetHit(5);
        }
    }
    
    private void OnTriggerStay(Collider other)
    { 
        if(other.gameObject.tag == "Speel")
        {
            // On inflige des dégats chaque seconde
            if (Time.time >= lastUseTime + 1f)
            {
                //UnityEngine.Debug.Log("touché");
                GetHit(1);
                anim.SetTrigger("hitted");
                lastUseTime = Time.time;
            }
        }
    }

    public void GetHit(int damage)
    {
        // Enlever les dégâts reçus à la vie de l'ennemi
        currentHealth -= damage;
        healthBar.fillAmount = (float)currentHealth / maxHealth;
        UnityEngine.Debug.Log("Magician life = " + currentHealth);

        // Si la vie de l'ennemi est inférieure ou égale à zéro, le détruire
        if (currentHealth <= 0)
        {
            anim.SetTrigger("died");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
