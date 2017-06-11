using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentMapUIControler : MonoBehaviour
{

    public GameObject[] player;
    private Transform currentMap1;
    private Transform currentMap2;
    private bool player1CanRotate = false;
    private bool player2CanRotate = false;
    private Transform startPosition1;
    private Transform startPosition2;
    public List<Transform> maps;
    //float currentDistancePlayer1 = 10;
    //float currentDistancePlayer2 = 40;

    private void OnEnable()
    {
        Invoke("Setup", .1f);
        player[0].transform.position = currentMap1.position;
        player[1].transform.position = currentMap2.position;
    }

    public void Setup()
    {
        if (maps.Count == 0)
        {
            for (int i = 0; i <= GameController.totalLevels; i++)
            {
                maps.Add(transform.GetChild(i));
            }
        }

        CancelInvoke("Contador");

        if (currentMap1 != (maps[GameController.currentLevelPlayer1]))
        {
            currentMap1 = maps[GameController.currentLevelPlayer1];
            player1CanRotate = false;
        }
        if (currentMap2 != maps[GameController.currentLevelPlayer2])
        {
            currentMap2 = maps[GameController.currentLevelPlayer2];
            player2CanRotate = false;
        }
        startPosition1 = player[0].transform;
        startPosition2 = player[1].transform;

        InvokeRepeating("Contador", 0, .01f);
    }

    private void Update()
    {
        if (startPosition1.position.x != currentMap1.position.x)// && !player1CanRotate)
            player[0].transform.position = Vector2.Lerp(startPosition1.position, currentMap1.position, 2 * Time.deltaTime);
        else
            player1CanRotate = true;

        if (startPosition2.position.x != currentMap2.position.x)// && !player2CanRotate)
        {
            player[1].transform.position = Vector2.Lerp(startPosition2.position, currentMap2.position, 2 * Time.deltaTime);
        }
        else
            player2CanRotate = true;
    }

    void Contador()
    {
        if (maps.Count >= GameController.totalLevels && GameController.currentLevelPlayer1 >= 0 && GameController.currentLevelPlayer2 >= 0)
        {
            //if (player1CanRotate)
                player[0].transform.RotateAround(maps[GameController.currentLevelPlayer1].position, transform.forward, -20 * Time.deltaTime);
            //if (player2CanRotate)
                player[1].transform.RotateAround(maps[GameController.currentLevelPlayer2].position, transform.forward, -10 * Time.deltaTime);
        }
    }
}
