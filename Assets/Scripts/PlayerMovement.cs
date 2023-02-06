using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ce script gère le déplacement du joueur dans l'arène 
public class PlayerMovement : MonoBehaviour
{
    // Variable de déplacement
    [SerializeField]
    private float movementSpeed = 5.0f; // Vitesse de déplacement

    public bool isLookAtEnabled = true; // Peut-on utiliser LookAt

    // Référence vers le Transform du personnage
    private Transform playerTransform;

    // Référence vers le Rigidbody du personnage
    private Rigidbody playerRigidbody;

    // Référence vers la caméra
    private Camera mainCamera;

    //Les composants
    private Animator anim;
    void Start()
    {
        playerTransform = GetComponent<Transform>();
        playerRigidbody = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move() 
    {
        // Le personnage est orienté vers la position de la souris
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.transform.position.y));
        mousePosition.y = playerTransform.position.y;
        if (isLookAtEnabled)
        {
            playerTransform.LookAt(mousePosition);
        }

        anim.SetBool("Run", false);

            // Déplacement
        if (Input.GetKey(KeyCode.W))
        {
            anim.SetBool("Run", true);
            playerRigidbody.MovePosition(playerTransform.position + Vector3.forward * movementSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            anim.SetBool("Run", true);
            playerRigidbody.MovePosition(playerTransform.position + Vector3.back * movementSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            anim.SetBool("Run", true);
            playerRigidbody.MovePosition(playerTransform.position + Vector3.left * movementSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            anim.SetBool("Run", true);
            playerRigidbody.MovePosition(playerTransform.position + Vector3.right * movementSpeed * Time.deltaTime);
        }

        // Si le joueur tombe, il doit se relever
        if (transform.eulerAngles.z == 90) {
            Vector3 euler = transform.eulerAngles;
            euler.z = 0;
            transform.eulerAngles = euler;
        }
    }
    public void ToggleLookAt()
    {
        isLookAtEnabled = !isLookAtEnabled;
    }

    

    // Si on ramasse des points d'expérience
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.name == "Emerald")
        {
            GameManager.Instance.xp += 10;
            Destroy(other.gameObject);
        }
    }
}
