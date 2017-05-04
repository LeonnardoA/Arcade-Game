using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArcadePUCCampinas;

public class MapController : MonoBehaviour {
    
    public float speed;
    public Transform player;
    private Rigidbody playerRb;
    public List<Transform> dynamicsObjs = new List<Transform>();

    private int velocityPlayers = 4;

    private void Awake()
    {
        player.GetComponent<PlayerController>().currentMap = gameObject;
    }

    private void Start()
    {
        playerRb = player.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        RotateMap();

        if (InputArcade.Apertou(1, EControle.VERDE))
        {
            DoGravityChanges();
        }
    }

    private void RotateMap()
    {
        float moveHorizontal = InputArcade.Eixo(1, EEixo.HORIZONTAL);

        transform.Rotate(moveHorizontal * -transform.forward * speed * Time.deltaTime);
        if (transform.eulerAngles.z > 45 && transform.eulerAngles.z < 60)
            transform.rotation = Quaternion.Euler(0, 0, 45);
        else if (transform.eulerAngles.z < 315 && transform.eulerAngles.z > 200)
            transform.rotation = Quaternion.Euler(0, 0, -45);

        playerRb.velocity = new Vector3(Mathf.Clamp(playerRb.velocity.x, -velocityPlayers, velocityPlayers),
        Mathf.Clamp(playerRb.velocity.y, -velocityPlayers, velocityPlayers),
        Mathf.Clamp(playerRb.velocity.z, -velocityPlayers, velocityPlayers));
    }

    private void DoGravityChanges()
    {
        for (int i = 0; i < dynamicsObjs.Count; i++)
        {
            Gravity currentObj = dynamicsObjs[i].GetComponent<Gravity>();

            switch (currentObj.gravityDir)
            {
                case "DOWN":
                    currentObj.ChangeGravity("UP");
                    break;
                case "UP":
                    currentObj.ChangeGravity("DOWN");
                    break;
            }
        }
    }
}
