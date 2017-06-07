using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public static string gravityDir1 = " ";
    public static string gravityDir2 = " ";
    public static float gravityForce1 = 20;
    public static float gravityForce2 = 20;
    public static bool canChangeGravity1 = true;
    public static bool canChangeGravity2 = true;
    private static List<Rigidbody> rb1 = new List<Rigidbody>();
    private static List<Rigidbody> rb2 = new List<Rigidbody>();
    private static Rigidbody playerRb1;
    private static Rigidbody playerRb2;

    private void Awake()
    {
        gravityDir1 = "DOWN";
        gravityDir2 = "DOWN";
    }

    private void Start()
    {
        SoundController.StartSounds();
    }

    public static void ConfigDynamicObjs(string player, List<Rigidbody> rb, Rigidbody playerRb)
    {
        switch (player)
        {
            case "Player1":
                rb1 = rb;
                playerRb1 = playerRb;
                DoChangeGravity("Player1", gravityDir1);
                break;
            case "Player2":
                rb2 = rb;
                playerRb2 = playerRb;
                DoChangeGravity("Player2", gravityDir2);
                break;
        }
    }

    private void FixedUpdate()
    {
        if (rb1.Count > 0 || playerRb1)
        {
            if (gravityDir1 == "ZeroGravity")
                return;

            if (rb1.Count > 0)
                for (int i = 0; i < rb1.Count; i++)
                    if (rb1[i])
                        rb1[i].AddForce(new Vector3(0, (gravityForce1 / 2), 0), ForceMode.Acceleration);
            if (playerRb1)
                playerRb1.AddForce(new Vector3(0, gravityForce1, 0), ForceMode.Acceleration);
        }
        if (rb2.Count > 0 || playerRb2)
        {
            if (gravityDir2 == "ZeroGravity")
                return;

            if (rb2.Count > 0)
                for (int i = 0; i < rb2.Count; i++)
                    if (rb2[i])
                        rb2[i].AddForce(new Vector3(0, (gravityForce2 / 2), 0), ForceMode.Acceleration);
            if (playerRb2)
                playerRb2.AddForce(new Vector3(0, gravityForce2, 0), ForceMode.Acceleration);
        }
    }

    public static void DoChangeGravity(string player, string dir)
    {
        switch (player)
        {
            case "Player1":
               // rb1 = dynamicObjs;
                ChangeGravity1(dir);
                break;
            case "Player2":
                //rb2 = dynamicObjs;
                ChangeGravity2(dir);
                break;
        }
    }

    private static void ChangeGravity1(string dir)
    {

        if (dir != "UP" && dir != "DOWN" && dir != "ZeroGravity" || !canChangeGravity1)
            return;

        switch (dir)
        {
            case "DOWN":
                if (gravityForce1 > 0)
                {
                    if (playerRb1)
                    {
                        gravityForce1 *= -1;
                        playerRb1.velocity = new Vector3(playerRb1.velocity.x, 0, playerRb1.velocity.z);
                        if (playerRb1.useGravity)
                            playerRb1.useGravity = false;
                    }

                    if (rb1.Count > 0)
                        for (int i = 0; i < rb1.Count; i++)
                            rb1[i].velocity = new Vector3(rb1[i].velocity.x, 0, rb1[i].velocity.z);
                }
                gravityDir1 = "DOWN";
                break;
            case "UP":
                if (gravityForce1 < 0)
                {
                    if (playerRb1)
                    {
                        gravityForce1 *= -1;
                        playerRb1.velocity = new Vector3(playerRb1.velocity.x, 0, playerRb1.velocity.z);
                        if (playerRb1.useGravity)
                            playerRb1.useGravity = false;
                    }

                        if (rb1.Count > 0)
                        for (int i = 0; i < rb1.Count; i++)
                            rb1[i].velocity = new Vector3(rb1[i].velocity.x, 0, rb1[i].velocity.z);
                }
                gravityDir1 = "UP";
                break;
            case "ZeroGravity":
                gravityDir1 = "ZeroGravity";
                gravityForce1 = 20;
                break;
        }
    }

    private static void ChangeGravity2(string dir)
    {
        if (dir != "UP" && dir != "DOWN" && dir != "ZeroGravity" || !canChangeGravity2)
            return;

        switch (dir)
        {
            case "DOWN":
                if (gravityForce2 > 0)
                {
                    if (playerRb2)
                    {
                        gravityForce2 *= -1;
                        playerRb2.velocity = new Vector3(playerRb2.velocity.x, 0, playerRb2.velocity.z);
                        if (playerRb2.useGravity)
                            playerRb2.useGravity = false;
                    }

                    if (rb2.Count > 0)
                        for (int i = 0; i < rb2.Count; i++)
                            rb2[i].velocity = new Vector3(rb2[i].velocity.x, 0, rb2[i].velocity.z);
                }
                gravityDir2 = "DOWN";
                break;
            case "UP":
                if (gravityForce2 < 0)
                {
                    if (playerRb2)
                    {
                        gravityForce2 *= -1;
                        playerRb2.velocity = new Vector3(playerRb2.velocity.x, 0, playerRb2.velocity.z);
                        if (playerRb2.useGravity)
                            playerRb2.useGravity = false;
                    }

                    if (rb2.Count > 0)
                        for (int i = 0; i < rb2.Count; i++)
                            rb2[i].velocity = new Vector3(rb2[i].velocity.x, 0, rb2[i].velocity.z);
                }
                gravityDir2 = "UP";
                break;
            case "ZeroGravity":
                gravityDir2 = "ZeroGravity";
                gravityForce2 = 20;
                break;
        }
    }
}
