using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour {

    private string gravityDir = " ";
    private float gravityForce = 10;
    private Rigidbody rb;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        gravityDir = "DOWN";
        ChangeGravity(gravityDir);
    }

    private void FixedUpdate()
    {
        rb.AddRelativeForce(transform.up * gravityForce, ForceMode.Acceleration);
    }

    public void ChangeGravity(string dir)
    {
        switch (dir)
        {
            case "DOWN":
                if (gravityForce > 0)
                {
                    rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                    gravityForce *= -1;
                }
                gravityDir = "DOWN";
                break;
            case "UP":
                if (gravityForce < 0)
                {
                    rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                    gravityForce *= -1;
                }
                gravityDir = "UP";
                break;
        }
    }
}
