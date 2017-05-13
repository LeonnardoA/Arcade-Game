using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //HUD
    public GameObject winHUD;

    ///Gameplay
    [HideInInspector]
    public GameObject currentMap;
    private Transform spawnPoint;
    public string player;

    private void Start()
    {
        winHUD.SetActive(false);
        if(currentMap)
        spawnPoint = currentMap.transform.FindChild("SpawnPoint");
    }

    private void ResetMap()
    {
        if (currentMap)
        {
            Gravity.DoChangeGravity(player, null, "DOWN");
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.parent.rotation = Quaternion.identity;
            transform.localPosition = spawnPoint.localPosition;
            Transform parent = transform.parent.parent;
            parent.FindChild("Canvas").FindChild("Text").GetComponent<Timer>().ResetTimer();
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("MarginMap"))
            ResetMap();

        if (other.gameObject.name == "Portal")
            ShowWinHUD();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Danger"))
            ResetMap();
    }

    //HUD
    void ShowWinHUD()
    {
        winHUD.SetActive(true);
    }
}
