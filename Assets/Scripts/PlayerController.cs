using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Transform spawnPoint;
    public GameObject winHUD;

    private void Start()
    {
        winHUD.SetActive(false);
    }

    private void ResetMap()
    {
        Physics.gravity = new Vector3(0, -9.81f, 0);
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.parent.rotation = Quaternion.identity;
        transform.localPosition = spawnPoint.position;
    }

    void DoWinGame()
    {
        winHUD.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("MarginMap"))
            ResetMap();

        else if (other.gameObject.name == "Portal")
            DoWinGame();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Danger"))
            ResetMap();
    }
}
