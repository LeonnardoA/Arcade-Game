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
        if (other.transform.name == "Player" || other.transform.name == "Player2")
        {
            playerRb = other.GetComponent<Rigidbody>();
            Gravity.DoChangeGravity(playerRb.GetComponent<PlayerController>().player, "ZeroGravity");
            playerRb.velocity = Vector3.zero;
            other.transform.position = transform.position;
            other.transform.rotation = transform.rotation;
            if (playerRb.transform.name == "Player")
                Gravity.canChangeGravity1 = false;
            if (playerRb.transform.name == "Player2")
                Gravity.canChangeGravity2 = false;
            StartCoroutine(DoBoost());
        }
    }

    private void FixedUpdate()
    {
        if (playerRb != null && canAddForce)
        {
            StopCoroutine(DoBoost());
            canAddForce = false;
            playerRb.AddForce(transform.right * 1000, ForceMode.Impulse);
        }
    }

    IEnumerator DoBoost()
    {
        yield return new WaitForSeconds(.5f);
        canAddForce = true;
        SoundController.PlaySound(0, "Boost");
        ////playerRb.AddForce(transform.right * 1000, ForceMode.Acceleration);
        //yield return new WaitForSeconds(.5f);
        //canAddForce = false;
        if (playerRb.transform.name == "Player")
            Gravity.canChangeGravity1 = true;
        if (playerRb.transform.name == "Player2")
            Gravity.canChangeGravity2 = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.right * 10);
    }
}

