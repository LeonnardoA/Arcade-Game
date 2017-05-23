using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private Vector3 positionTarget;
    private bool canDoAnim;
    public string player;
    public float speed;

    private void Start()
    {
        canDoAnim = false;

        transform.position = new Vector3(transform.position.x, 150, -50);
        switch (player) {

            case "Player1":
                positionTarget = new Vector3(0, 0, -15);
                break;
            case "Player2":
                positionTarget = new Vector3(50, 0, -15);
                break;
        }

        Invoke("Contador", .5f);
    }

    private void Update()
    {
        if (transform.position != positionTarget && canDoAnim)
        {
            transform.position = Vector3.MoveTowards(transform.position, positionTarget, speed * Time.deltaTime);
            Debug.Log("UPDATE IS RUNNING");
        }
        else if (transform.position == positionTarget && canDoAnim)
        {
            canDoAnim = false;
            MapController.inGame = true;
        }
    }
    void Contador()
    {
        canDoAnim = true;
    }
}
