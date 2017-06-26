using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArcadePUCCampinas;

public class MapController : MonoBehaviour {
    [HideInInspector]
    public string currentPlayer; // Player1 - Player2
    public float speed;
    public Transform player;
    public List<Rigidbody> dynamicObjs = new List<Rigidbody>();

    private Rigidbody playerRb;
    public static bool inGame = false;
    
    public void OnEnable()
    {
        player = transform.Find("Player");
        currentPlayer = "Player1";
        if (player == null)
        {
            player = transform.Find("Player2");
            currentPlayer = "Player2";
        }

        if (player != null)
        {
            playerRb = player.GetComponent<Rigidbody>();

            Gravity.ConfigDynamicObjs(currentPlayer, dynamicObjs, playerRb);
        }
    }

    private void Update()
    {
        //System.Array values = System.Enum.GetValues(typeof(KeyCode));
        //foreach (KeyCode code in values)
        //{
        //    if (Input.GetKeyDown(code))
        //        print(System.Enum.GetName(typeof(KeyCode), code));
        //}
        if (inGame) {
            RotateMap();
            if (currentPlayer == "Player1")
            {
                if (InputArcade.Apertou(MenuNavigate.inputPlayer1, EControle.VERDE) || Input.GetKeyDown(KeyCode.Joystick1Button1))
                {
                    if (Gravity.gravityDir1 == "DOWN")
                    {
                        Gravity.DoChangeGravity(currentPlayer, "UP");
                        SoundController.PlaySound(0, "GravityUP");
                    }
                    else if (Gravity.gravityDir1 == "UP")
                    {
                        Gravity.DoChangeGravity(currentPlayer, "DOWN");
                        SoundController.PlaySound(0, "GravityDOWN");
                    }
                    else if (Gravity.gravityDir1 == "ZeroGravity")
                    {
                        Gravity.DoChangeGravity(currentPlayer, "DOWN");
                        SoundController.PlaySound(0, "GravityDOWN");
                    }
                }
            }
            else if (currentPlayer == "Player2")
            {
                if (InputArcade.Apertou(MenuNavigate.inputPlayer2, EControle.VERDE) || Input.GetKeyDown(KeyCode.Joystick2Button1))
                {
                    if (Gravity.gravityDir2 == "DOWN")
                    {
                        Gravity.DoChangeGravity(currentPlayer, "UP");
                        SoundController.PlaySound(1, "GravityUP");
                    }
                    else if (Gravity.gravityDir2 == "UP")
                    {
                        Gravity.DoChangeGravity(currentPlayer, "DOWN");
                        SoundController.PlaySound(1, "GravityDOWN");
                    }
                    else if (Gravity.gravityDir2 == "ZeroGravity")
                    {
                        Gravity.DoChangeGravity(currentPlayer, "DOWN");
                        SoundController.PlaySound(1, "GravityDOWN");
                    }
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
                moveHorizontal = InputArcade.Eixo(MenuNavigate.inputPlayer1, EEixo.HORIZONTAL);
            else
                moveHorizontal = InputArcade.Eixo(MenuNavigate.inputPlayer2, EEixo.HORIZONTAL);

            transform.Rotate(moveHorizontal * -transform.forward * speed * Time.deltaTime);
            if (transform.eulerAngles.z > 45 && transform.eulerAngles.z < 60)
                transform.rotation = Quaternion.Euler(0, 0, 45);
            else if (transform.eulerAngles.z < 315 && transform.eulerAngles.z > 200)
                transform.rotation = Quaternion.Euler(0, 0, -45);
        }

        /*playerRb.velocity = new Vector3(Mathf.Clamp(playerRb.velocity.x, -maxVelocty, maxVelocty),
        Mathf.Clamp(playerRb.velocity.y, -maxVelocty, maxVelocty),
        Mathf.Clamp(playerRb.velocity.z, -maxVelocty, maxVelocty));*/
    }
}
