using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMenuMovement : MonoBehaviour {

    private void OnEnable()
    {
        transform.position = new Vector3(0, 0, 1500);
    }

    void Update()
    {
        transform.Translate(transform.right * 10 * Time.deltaTime);
    }
}
