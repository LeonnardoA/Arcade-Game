using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleRotate : MonoBehaviour {

    public float velocity;

    private void Update()
    {
        transform.Rotate(transform.forward * -velocity * Time.deltaTime);
    }
}
