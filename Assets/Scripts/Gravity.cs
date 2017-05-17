using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{

    [HideInInspector]
    public static string gravityDir1 = " ";
    [HideInInspector]
    public static string gravityDir2 = " ";
    public static float gravityForce1 = 20;
    public static float gravityForce2 = 20;
    private static List<Rigidbody> rb1 = new List<Rigidbody>();
    private static List<Rigidbody> rb2 = new List<Rigidbody>();

    private void Awake()
    {
        gravityDir1 = "DOWN";
        gravityDir2 = "DOWN";
    }

    private void Start()
    {
        SoundController.StartSounds();
    }
    
    public static void ConfigDynamicObjs(string player, List<Rigidbody> rb)
    {
        switch (player)
        {
            case "Player1":
                rb1 = rb;
                DoChangeGravity("Player1", gravityDir1);
                break;
            case "Player2":
                rb2 = rb;
                DoChangeGravity("Player2", gravityDir2);
                break;
        }
    }

    private void FixedUpdate()
    {
        if (rb1 != null)
        {
            if (gravityDir1 == "ZeroGravity")
                return;

            for (int i = 0; i < rb1.Count; i++)
            {
                if (rb1[i])
                    rb1[i].AddForce(new Vector3(0, gravityForce1, 0), ForceMode.Acceleration);
            }

        }
        if (rb2 != null)
        {
            if (gravityDir2 == "ZeroGravity")
                return;

            for (int i = 0; i < rb2.Count; i++)
            {
                if (rb2[i])
                    rb2[i].AddForce(new Vector3(0, gravityForce2, 0), ForceMode.Acceleration);
            }
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
        if (dir != "UP" && dir != "DOWN" && dir != "ZeroGravity")
            return;

        switch (dir)
        {
            case "DOWN":
                if (gravityForce1 > 0)
                {
                    rb1[0].velocity = new Vector3(rb1[0].velocity.x, 0, rb1[0].velocity.z);
                        gravityForce1 *= -1;
                }
                gravityDir1 = "DOWN";
                break;
            case "UP":
                if (gravityForce1 < 0)
                {
                    rb1[0].velocity = new Vector3(rb1[0].velocity.x, 0, rb1[0].velocity.z);
                    gravityForce1 *= -1;
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
        if (dir != "UP" && dir != "DOWN" && dir != "ZeroGravity")
            return;

        switch (dir)
        {
            case "DOWN":
                if (gravityForce2 > 0)
                {
                    rb2[0].velocity = new Vector3(rb2[0].velocity.x, 0, rb2[0].velocity.z);
                    gravityForce2 *= -1;
                }
                gravityDir2 = "DOWN";
                break;
            case "UP":
                if (gravityForce2 < 0)
                {
                    rb2[0].velocity = new Vector3(rb2[0].velocity.x, 0, rb2[0].velocity.z);
                    gravityForce2 = 20;
                }
                gravityDir2 = "UP";
                break;
            case "ZeroGravity":
                gravityDir2 = "ZeroGravity";
                gravityForce2 = 0;
                break;
        }
    }
}
