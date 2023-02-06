using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Warrior : MonoBehaviour
{
    [SerializeField] private Collider swordCollider;

    // vie du warrior
    [SerializeField]
    public int maxHealth = 50;
    public int currentHealth;
    public Image healthBar;

    [SerializeField]

    private float lastUseTime;
    private bool isSpinning = false;
    private bool isJumping = false;

    [SerializeField] private float jumpForce;

    [SerializeField]
    private float rotationSpeed = 180f; // vitesse de rotation de l'attaque N°3

    //Composants
    private Rigidbody playerRigidbody;
    private Animator anim;
    private AudioSource audioSource;

    //SFX
    public AudioClip[] swordSFX;

    //Effets visuels
    public GameObject SpinParticles; // Effet sur le tournoiement


    void  Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        swordCollider.enabled = false;
        currentHealth = maxHealth;

        GameObject canvasObject = GameObject.Find("Canvas");
        Image healthBarImage = canvasObject.GetComponentsInChildren<Image>()[1];
        healthBar = healthBarImage;
    }

    void Update()
    {
        Attack();
    }


    public void Attack(){

        // On lance la première attaque au clique gauche de la souris
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine("AttackSword");
        }

        // On lance la deuxième attaque au clique droit de la souris
        
        if (Input.GetMouseButton(1)&& !isJumping)
        { 
            isJumping = true;
            StartCoroutine("AttackJump");
        }

        // On lance la troisière attaque lorsqu'on appuie sur la barre espace
         if (Input.GetKeyDown(KeyCode.Space) && !isSpinning)
        {
            GetComponent<PlayerMovement>().ToggleLookAt(); // Desactivation de la rotation avec la position de la souris pour eviter les conflits de rotation
            isSpinning = true;
            StartCoroutine("AttackSpin");
            
        }
    }

    IEnumerator AttackSword()
    {
        swordCollider.enabled = true; // On active l'épée
        audioSource.PlayOneShot(swordSFX[Random.Range(0,3)]); // On fait jouer un son d'épée aléatoirement
        anim.SetTrigger("Attack1"); // On lance l'animation
        yield return new WaitForSeconds(1); // On laisse l'épée active la durée de l'animation
        swordCollider.enabled = false; // Puis on désactive l'épée
    }

    IEnumerator AttackJump()
    {
        anim.SetTrigger("startJump"); 
        playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // saut du joueur
        yield return new WaitForSeconds(5); // 5s de cooldown
        isJumping = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        // animation de fin de saut lorsqu'on touche soit le sol ou tout objet avec un collider
        if (isJumping)
        {
            Debug.Log("test");
            anim.SetTrigger("endJump");
        }

        if (other.gameObject.CompareTag("Trap"))
        {
            GetHit(5);
        }
    }


    IEnumerator AttackSpin()
    {
        float timeElapsed = 0f;
        
        GameObject go = Instantiate(SpinParticles, transform.position, Quaternion.identity); // Particules pour le tournoiement
        go.transform.position += Vector3.up;
        go.transform.rotation = Quaternion.Euler(99, 54, 23);
        Destroy(go,3); // Destruction après 3s
        
        swordCollider.enabled = false;
        while (timeElapsed < 3f)
        {
            timeElapsed += Time.deltaTime;
            //transform.rotation = Quaternion.RotateTowards(startingRotation, startingRotation * Quaternion.Euler(0f, rotationSpeed, 0f), rotationSpeed * Time.deltaTime);
            go.transform.parent = transform;
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
            yield return null;
        }
        swordCollider.enabled = true;

        yield return new WaitForSeconds(1);
        GetComponent<PlayerMovement>().ToggleLookAt();  // Réactivation du lookAt(mousePosition)
        yield return new WaitForSeconds(5);
        isSpinning = false;
    }
    
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "SoldierSword")
        {
            GetHit(5);
            anim.SetTrigger("hitted");
        }
    }
    private void OnTriggerStay(Collider other)
    { 
        if(other.gameObject.tag == "Speel")
        {
            // On inflige des dégats chaque seconde
            if (Time.time >= lastUseTime + 1f)
            {
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
        UnityEngine.Debug.Log("Warrior life = "+ currentHealth);

        // Si la vie de l'ennemi est inférieure ou égale à zéro, le détruire
        if (currentHealth <= 0)
        {
            anim.SetTrigger("died");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
