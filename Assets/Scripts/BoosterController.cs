using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterController : MonoBehaviour
{
    private Rigidbody playerRb;
    public bool canAddForce = false;

    private Transform currentMap;

    private void OnEnable()
    {
        playerRb = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "Player")
        {
            playerRb = other.GetComponent<Rigidbody>();
            Gravity.DoChangeGravity(playerRb.GetComponent<PlayerController>().player, "ZeroGravity");
            playerRb.velocity = Vector3.zero;
            other.transform.position = transform.position;
            other.transform.rotation = transform.rotation;

            StartCoroutine(DoBoost());
        }
    }

    private void FixedUpdate()
    {
        if (playerRb != null && canAddForce)
        {
            playerRb.AddForce(transform.right * 200, ForceMode.Acceleration);
        }
    }

    IEnumerator DoBoost()
    {
        yield return new WaitForSeconds(.5f);
        canAddForce = true;
        //playerRb.AddForce(transform.right * 1000, ForceMode.Acceleration);
        yield return new WaitForSeconds(.5f);
        canAddForce = false;
        StopCoroutine(DoBoost());
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.right * 10);
    }
}

