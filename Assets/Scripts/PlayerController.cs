using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    ///Gameplay
    [HideInInspector]
    public GameObject currentMap;
    private GameObject cuboQuebrado;
    public GameController gameController;
    private bool playerIsAlive = true;
    public Animator fade;
    private Transform spawnPoint;
    private MeshRenderer brilho;
    public string player;
    List<Rigidbody> playerRb = new List<Rigidbody>();

    private void Start()
    {
        playerIsAlive = true;
        if (currentMap)
            spawnPoint = currentMap.transform.FindChild("SpawnPoint");

        playerRb.Add(this.GetComponent<Rigidbody>());
        brilho = transform.Find("brilho").GetComponent<MeshRenderer>();
        cuboQuebrado = transform.Find("CuboQuebrado").gameObject;
    }

    private void Update()
    {
        brilho.material.mainTextureOffset = new Vector2(brilho.material.mainTextureOffset.x, brilho.material.mainTextureOffset.y + .2f * Time.deltaTime);
    }

    private void ResetMap()
    {
        if (currentMap && playerIsAlive)
        {
            playerIsAlive = false;
            StartCoroutine(DoReset());
        }
    }

    IEnumerator DoReset()
    {
        yield return new WaitForSeconds(.5f);
        fade.SetTrigger("DoFade");
        yield return new WaitForSeconds(.5f);
        Gravity.DoChangeGravity(player, "DOWN");
        playerRb[0].velocity = Vector3.zero;
        //playerRb[0].isKinematic = false;
        currentMap.transform.rotation = Quaternion.identity;
        transform.localRotation = Quaternion.identity;
        transform.localPosition = spawnPoint.localPosition;
        //Transform parent = transform.parent.parent.parent;
        // parent.FindChild("Canvas").FindChild("Text").GetComponent<Timer>().ResetTimer();
        playerIsAlive = true;
        cuboQuebrado.GetComponent<Animator>().SetTrigger("Revive");
        Invoke("waitAnimRevive", .5f);       
    }

    void waitAnimRevive()
    {
        transform.Find("CuboInteiro").gameObject.SetActive(true);
        cuboQuebrado.SetActive(false);
        transform.GetComponent<Rigidbody>().isKinematic = false;
        StopCoroutine(DoReset());
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
            cuboQuebrado.SetActive(true);
            cuboQuebrado.GetComponent<Animator>().SetTrigger("Die");
            ResetMap();

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
}
