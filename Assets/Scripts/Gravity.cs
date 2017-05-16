using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{

    [HideInInspector]
    public static string gravityDir1 = " ";
    [HideInInspector]
    public static string gravityDir2 = " ";
    private static float gravityForce1 = 10;
    private static float gravityForce2 = 10;
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

    private void FixedUpdate()
    {
        if (rb1 != null)
        {
            for (int i = 0; i < rb1.Count; i++)
            {
                if (rb1[i])
                    rb1[i].AddForce(new Vector3(0, gravityForce1, 0), ForceMode.Acceleration);
            }

        }
        if (rb2 != null)
        {
            for (int i = 0; i < rb2.Count; i++)
            {
                if (rb2[i])
                    rb2[i].AddForce(new Vector3(0, gravityForce2, 0), ForceMode.Acceleration);
            }
        }
    }

    public static void DoChangeGravity(string player, List<Rigidbody> dynamicObjs, string dir)
    {
        switch (player)
        {
            case "Player1":
                if (dynamicObjs == null)
                {
                    ChangeGravity1(dir);
                    return;
                }
                rb1 = dynamicObjs;
                ChangeGravity1(dir);
                break;
            case "Player2":
                if (dynamicObjs == null)
                {
                    ChangeGravity2(dir);
                    return;
                }
                rb2 = dynamicObjs;
                ChangeGravity2(dir);
                break;
        }
    }

    private static void ChangeGravity1(string dir)
    {
        if (dir != "UP" && dir != "DOWN")
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
        }
    }

    private static void ChangeGravity2(string dir)
    {
        if (dir != "UP" && dir != "DOWN")
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
                    gravityForce2 *= -1;
                }
                gravityDir2 = "UP";
                break;
        }
    }
}
