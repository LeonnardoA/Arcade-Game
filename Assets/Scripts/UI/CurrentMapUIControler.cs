using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentMapUIControler : MonoBehaviour
{
    public GameObject[] player;
    private bool changedPositionUI = false;
    private Transform currentMap1;
    private Transform currentMap2;
    private Transform startPosition1;
    private Transform startPosition2;
    private Vector2 firstPosition = new Vector2(470, 100);
    private Vector2 lastPosition = new Vector2(-52, 100);
    public List<Transform> maps;

    private void OnEnable()
    {
        //Invoke("Setup", .1f);
        player[0].transform.position = new Vector2((currentMap1.position.x - 100), currentMap1.position.y);
        player[1].transform.position = new Vector2((currentMap2.position.x - 100), currentMap2.position.y);

        this.transform.position = new Vector2(470, 100);
    }

    public void Setup()
    {
        if (maps.Count == 0)
        {
            for (int i = 0; i < GameController.totalLevels; i++)
            {
                maps.Add(transform.GetChild(i));
            }
        }

        CancelInvoke("Contador");

        if (currentMap1 != (maps[GameController.currentLevelPlayer1]))
        {
            currentMap1 = maps[GameController.currentLevelPlayer1];
        }
        if (currentMap2 != maps[GameController.currentLevelPlayer2])
        {
            currentMap2 = maps[GameController.currentLevelPlayer2];
        }
        startPosition1 = player[0].transform;
        startPosition2 = player[1].transform;

        changedPositionUI = false;
    }

    enum StatusRace
    {
        Player1Front,
        Player2Front,
        Empty
    }
    enum Moviment
    {
        Player1Stoped,
        Player2Stoped,
        Empty
    }

    StatusRace statusRace = StatusRace.Empty;
    Moviment moviment = Moviment.Empty;

    private void Update()
    {
        // StatusRace controller
        if (GameController.currentLevelPlayer1 > GameController.currentLevelPlayer2)
            statusRace = StatusRace.Player1Front;
        else if (GameController.currentLevelPlayer2 > GameController.currentLevelPlayer1)
            statusRace = StatusRace.Player2Front;
        //

        if (startPosition1.position.x != (currentMap1.position.x - 100) && moviment != Moviment.Player2Stoped)
        {
            if (player[0].transform.parent != this.transform)
                player[0].transform.parent = this.transform;
            if (player[0].transform.position.x < (currentMap1.position.x - 100))
                player[0].transform.position = Vector2.Lerp(startPosition1.position, new Vector2((currentMap1.position.x - 100), currentMap1.position.y), 4f * Time.deltaTime);
        }

        if (startPosition2.position.x != (currentMap2.position.x - 100) && moviment != Moviment.Player1Stoped)
        {
            if (player[1].transform.parent != this.transform)
                player[1].transform.parent = this.transform;
            if (player[1].transform.position.x < (currentMap2.position.x - 100))
                player[1].transform.position = Vector2.Lerp(startPosition2.position, new Vector2((currentMap2.position.x - 100), currentMap2.position.y), 4f * Time.deltaTime);
        }

        if (GameController.currentLevelPlayer1 > 6f && GameController.currentLevelPlayer2 < 3f || GameController.currentLevelPlayer2 > 6f && GameController.currentLevelPlayer1 < 3f)
        {
            this.transform.position = Vector2.Lerp(this.transform.position, lastPosition, Time.deltaTime);

            changedPositionUI = true;

            switch (statusRace)
            {
                case StatusRace.Player1Front:
                    player[1].transform.position = new Vector2(36f, 40f);
                    moviment = Moviment.Player1Stoped;
                    player[1].transform.parent = this.transform.parent;
                    if (GameController.currentLevelPlayer2 >= 3f)
                        moviment = Moviment.Empty;
                    break;
                case StatusRace.Player2Front:
                    player[0].transform.position = new Vector2(36f, 40f);
                    moviment = Moviment.Player2Stoped;
                    player[0].transform.parent = this.transform.parent;
                    if (GameController.currentLevelPlayer1 >= 3f)
                        moviment = Moviment.Empty;
                    break;
            }
        }
        else
            moviment = Moviment.Empty;

        if (GameController.currentLevelPlayer1 > 8f && !changedPositionUI || GameController.currentLevelPlayer2 > 8f && !changedPositionUI)
        {
            this.transform.position = Vector2.Lerp(this.transform.position, lastPosition, Time.deltaTime);
        }
    }
    void LateUpdate()
    {
        if (maps.Count >= GameController.totalLevels && GameController.currentLevelPlayer1 >= 0 && GameController.currentLevelPlayer2 >= 0)
        {
            if (moviment != Moviment.Player2Stoped)
            {
                player[0].transform.RotateAround(maps[GameController.currentLevelPlayer1].position, transform.forward, -20f * Time.deltaTime);
            }
            if (moviment != Moviment.Player1Stoped)
            {
                player[1].transform.RotateAround(maps[GameController.currentLevelPlayer2].position, transform.forward, - 10f * Time.deltaTime);
            }
        }
    }
}
