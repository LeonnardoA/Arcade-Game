using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    ///Gameplay
    [HideInInspector]
    public GameObject currentMap;
    public GameController gameController;
    private Transform spawnPoint;
    private MeshRenderer brilho;
    public string player;
    List<Rigidbody> playerRb = new List<Rigidbody>();

    private void Start()
    {
        if (currentMap)
            spawnPoint = currentMap.transform.FindChild("SpawnPoint");

        playerRb.Add(this.GetComponent<Rigidbody>());
        brilho = transform.Find("brilho").GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        brilho.material.mainTextureOffset = new Vector2(brilho.material.mainTextureOffset.x, brilho.material.mainTextureOffset.y + .2f * Time.deltaTime);
    }

    private void ResetMap()
    {
        if (currentMap)
        {
            Gravity.DoChangeGravity(player, "DOWN");
            playerRb[0].velocity = Vector3.zero;
            //playerRb[0].isKinematic = false;
            currentMap.transform.rotation = Quaternion.identity;
            transform.localRotation = Quaternion.identity;
            transform.localPosition = spawnPoint.localPosition;
            //Transform parent = transform.parent.parent.parent;
            // parent.FindChild("Canvas").FindChild("Text").GetComponent<Timer>().ResetTimer();
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
            transform.GetComponent<Rigidbody>().isKinematic = true;
            transform.Find("CuboInteiro").gameObject.SetActive(false);
            GameObject cuboQuebrado = transform.Find("CuboQuebrado").gameObject;
            cuboQuebrado.SetActive(true);
            cuboQuebrado.GetComponent<Animator>().SetTrigger("Die");

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

            StartCoroutine(Contador());
        }
    }

    IEnumerator Contador()
    {
        yield return new WaitForSeconds(.9f);
        transform.Find("CuboInteiro").gameObject.SetActive(true);
        transform.GetComponent<Rigidbody>().isKinematic = false;
        GameObject cuboQuebrado = transform.Find("CuboQuebrado").gameObject;
        cuboQuebrado.GetComponent<Animator>().SetTrigger("Revive");
        ResetMap();
    }
}
