using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    ///Gameplay
   // [HideInInspector]
    public GameObject currentMap;
    public GameController gameController;
    private Transform spawnPoint;
    public string player;
    List<Rigidbody> playerRb = new List<Rigidbody>();

    private void Start()
    {
        if(currentMap)
        spawnPoint = currentMap.transform.FindChild("SpawnPoint");
        
        playerRb.Add(this.GetComponent<Rigidbody>());
    }

    private void ResetMap()
    {
        if (currentMap)
        {
            Gravity.DoChangeGravity(player, "DOWN");
            playerRb[0].velocity = Vector3.zero;
            //playerRb[0].isKinematic = false;
            currentMap.transform.rotation = Quaternion.identity;
            transform.localPosition = spawnPoint.localPosition;
            //Transform parent = transform.parent.parent.parent;
           // parent.FindChild("Canvas").FindChild("Text").GetComponent<Timer>().ResetTimer();

            switch (player)
            {
                case "Player1":
                    SoundController.PlaySound(0, "Respawn");
                    Gravity.gravityForce1 = -20;
                    break;
                case "Player2":
                    SoundController.PlaySound(0, "Respawn");
                    Gravity.gravityForce1 = -20;
                    break;
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("MarginMap"))
        {
            ResetMap();
        }
        if (other.gameObject.name == "Portal")
        {
            gameController.ChangeLevel(player);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Danger"))
        {
            ResetMap();
        }
    }
}
