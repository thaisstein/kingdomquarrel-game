using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Transform PlayerSpawnPoint;
    public CharacterDatabase characterDB;
    public SpriteRenderer artworkSprite;
    private int selectedOption;
    // Start is called before the first frame update
    void Start()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption");
        PlayerSpawnPoint = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
        ChooseCharacter(selectedOption);
        // if (!PlayerPrefs.HasKey("selectedOption"))
        // {
        //     selectedOption = 0;
        // }
        // else
        // {
        //     Load();
        // }
        // UpdateCharacter(selectedOption);
    }

    private void UpdateCharacter(int selectedOption)
    {
        // Character character = characterDB.GetCharacter(selectedOption);
        // artworkSprite.sprite = character.characterSprite;
    }
    private void ChooseCharacter(int selectedOption)
    {
        Character character = characterDB.GetCharacter(selectedOption);
        GameObject player = character.playerObject;
        player = Instantiate(player, PlayerSpawnPoint.position, Quaternion.identity);

        GameObject cameraObject = GameObject.Find("Main Camera");
        CameraFollow cameraScript = cameraObject.GetComponent<CameraFollow>();
        cameraScript.target = player.transform;

        if(selectedOption==1) 
        {
            Debug.Log("Perso 1 choisi");
        }
        if(selectedOption==2) 
        {
            Debug.Log("Perso 2 choisi");
        }
    }

    private void Load()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption");
    }
}
