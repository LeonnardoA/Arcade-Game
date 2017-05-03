using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArcadePUCCampinas;

public class MapController : MonoBehaviour {

    public float speed;
    public Rigidbody player1;

    private int velocityPlayers = 4;

    private void Update()
    {
        float moveHorizontal = InputArcade.Eixo(1, EEixo.HORIZONTAL);
        
        transform.Rotate(moveHorizontal * -transform.forward * speed * Time.deltaTime);
        if (transform.eulerAngles.z > 45 && transform.eulerAngles.z < 60)
            transform.rotation = Quaternion.Euler(0, 0, 45);
        else if (transform.eulerAngles.z < 315 && transform.eulerAngles.z > 200)
            transform.rotation = Quaternion.Euler(0, 0, -45);

        if (InputArcade.Apertou(1, EControle.VERDE))
        {
            Physics.gravity *= -1;
        }
        player1.velocity = new Vector3(Mathf.Clamp(player1.velocity.x, -velocityPlayers, velocityPlayers),
        Mathf.Clamp(player1.velocity.y, -velocityPlayers, velocityPlayers),
        Mathf.Clamp(player1.velocity.z, -velocityPlayers, velocityPlayers));
        
        Debug.Log(transform.eulerAngles.z);
    }
}
