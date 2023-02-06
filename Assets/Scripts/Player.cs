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
       
    }

    private void ChooseCharacter(int selectedOption)
    {
        Character character = characterDB.GetCharacter(selectedOption);
        GameObject player = character.playerObject;
        player = Instantiate(player, PlayerSpawnPoint.position, Quaternion.identity);

        GameObject cameraObject = GameObject.Find("Main Camera");
        CameraFollow cameraScript = cameraObject.GetComponent<CameraFollow>();
        cameraScript.target = player.transform;
        Debug.Log("INVOCATION DE");
    }

    private void Load()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption");
    }
}
