using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateControllerTEST : MonoBehaviour {

    public Transform target;

    private void Update()
    {
        transform.RotateAround(target.position, transform.forward, 90 * Time.deltaTime);
    }
}
