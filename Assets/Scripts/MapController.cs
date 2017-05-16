using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArcadePUCCampinas;

public class MapController : MonoBehaviour {

    public string currentPlayer; // Player1 - Player2
    public float speed;
    public Transform player;
    public List<Rigidbody> dynamicObjs = new List<Rigidbody>();

    private int maxVelocty = 4;
    private Rigidbody playerRb;
    private bool isEnabled = false;

    private void Awake()
    {
        player.GetComponent<PlayerController>().currentMap = gameObject;
    }

    public void OnEnable()
    {
        playerRb = player.GetComponent<Rigidbody>();

        if (currentPlayer == "Player1")
            Gravity.DoChangeGravity(currentPlayer, dynamicObjs, Gravity.gravityDir1);
        if (currentPlayer == "Player2")
            Gravity.DoChangeGravity(currentPlayer, dynamicObjs, Gravity.gravityDir2);

        isEnabled = true;
    }

    private void Update()
    {
        RotateMap();
        if (currentPlayer == "Player1")
        {
            if (InputArcade.Apertou(0, EControle.VERDE))
            {
                if (Gravity.gravityDir1 == "DOWN")
                {
                    Gravity.DoChangeGravity(currentPlayer, dynamicObjs, "UP");
                    SoundController.PlaySound(0, "GravityUP");
                }
                else
                {
                    Gravity.DoChangeGravity(currentPlayer, dynamicObjs, "DOWN");
                    SoundController.PlaySound(0, "GravityDOWN");
                }
            }
        }
        if (currentPlayer == "Player2")
        {
            if (InputArcade.Apertou(1, EControle.VERDE))
            {
                if (Gravity.gravityDir2 == "DOWN")
                {
                    Gravity.DoChangeGravity(currentPlayer, dynamicObjs, "UP");
                    SoundController.PlaySound(1, "GravityUP");
                }
                else
                {
                    Gravity.DoChangeGravity(currentPlayer, dynamicObjs, "DOWN");
                    SoundController.PlaySound(1, "GravityDOWN");
                }
            }
        }
    }


    private void RotateMap()
    {
        if (currentPlayer == "Player1" || currentPlayer == "Player2")
        {
            float moveHorizontal = 0;
            if (currentPlayer == "Player1")
                moveHorizontal = InputArcade.Eixo(0, EEixo.HORIZONTAL);
            else
                moveHorizontal = InputArcade.Eixo(1, EEixo.HORIZONTAL);

            transform.Rotate(moveHorizontal * -transform.forward * speed * Time.deltaTime);
            if (transform.eulerAngles.z > 45 && transform.eulerAngles.z < 60)
                transform.rotation = Quaternion.Euler(0, 0, 45);
            else if (transform.eulerAngles.z < 315 && transform.eulerAngles.z > 200)
                transform.rotation = Quaternion.Euler(0, 0, -45);
        }

        playerRb.velocity = new Vector3(Mathf.Clamp(playerRb.velocity.x, -maxVelocty, maxVelocty),
        Mathf.Clamp(playerRb.velocity.y, -maxVelocty, maxVelocty),
        Mathf.Clamp(playerRb.velocity.z, -maxVelocty, maxVelocty));
    }
}
