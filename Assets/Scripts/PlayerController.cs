﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Transform spawnPoint;
    public GameObject winHUD;

    private void Start()
    {
        winHUD.SetActive(false);
    }

    /*private void ResetMap()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.parent.rotation = Quaternion.identity;
        transform.localPosition = spawnPoint.position;
    }*/

    void ShowWinHUD()
    {
        winHUD.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        //if (other.gameObject.CompareTag("MarginMap"))
            //ResetMap();

        if (other.gameObject.name == "Portal")
            ShowWinHUD();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.CompareTag("Danger"))
           // ResetMap();
    }
}
