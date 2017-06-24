using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentMapUIControler : MonoBehaviour
{
    public GameObject[] player;
    public Image[] trace = new Image[2];
    public float[] traceTarget = new float[2];
    private bool changedPositionUI = false;
    private Transform currentMap1;
    private Transform currentMap2;
    private Transform startPosition1;
    private Transform startPosition2;
    private Vector2 firstPosition = new Vector2(470, 100);
    private Vector2 lastPosition = new Vector2(-52, 100);
    private float D;
    private float D2;
    private float offset;
    private float velocity;
    //public List<Transform> maps;

    private void Start()
    {
        Setup();
    }

    public void Setup()
    {
        traceTarget[0] = 0;
        traceTarget[1] = 0;
        trace[0].fillAmount = 0;
        trace[1].fillAmount = 0;

        player[0].transform.localPosition = new Vector2(-240f, -15f);
        player[1].transform.localPosition = new Vector2(-290f, -15f);

        offset = .1f;
        velocity = offset + 19.9162f;
    }

    private void Update()
    {
        if ((trace[0].fillAmount * 1000) > D || (trace[0].fillAmount * 1000) > 167 && (trace[0].fillAmount * 1050) > D || (trace[0].fillAmount * 1000) > 250 && (trace[0].fillAmount * 1100) > D)
            player[0].transform.RotateAround(this.transform.position, transform.forward, -velocity * Time.deltaTime);

        if ((trace[1].fillAmount * 1000) > D2 || (trace[1].fillAmount * 1000) > 167 && (trace[1].fillAmount * 1100) > D2 || (trace[1].fillAmount * 1000) > 250 && (trace[1].fillAmount * 1150) > D2)
            player[1].transform.RotateAround(this.transform.position, transform.forward, -velocity  * Time.deltaTime);
        
        if (trace[0].fillAmount < traceTarget[0])
            trace[0].fillAmount = Mathf.Lerp(trace[0].fillAmount, traceTarget[0], .5f * Time.deltaTime);
        if (trace[1].fillAmount < traceTarget[1])
            trace[1].fillAmount = Mathf.Lerp(trace[1].fillAmount, traceTarget[1], .5f * Time.deltaTime);
        
        Vector3 spokeToActual = player[0].transform.position - this.transform.position,
        spokeToCorrect = new Vector3(0, 0, 0) - this.transform.position;
        float angleFromCenter = Vector3.Angle(spokeToActual, spokeToCorrect);
        D = 2 * Mathf.PI * 190 * (angleFromCenter / 360);

        Vector3 spokeToActual2 = player[1].transform.position - this.transform.position,
       spokeToCorrect2 = new Vector3(0, 0, 0) - this.transform.position;
        float angleFromCenter2 = Vector3.Angle(spokeToActual2, spokeToCorrect2);
        D2 = 2 * Mathf.PI * 190 * (angleFromCenter2 / 360);
    }

    //void CalculatePosition(Vector2 center, float radius, float angle)
    //{
    //    Vector2 newPos = new Vector2((float)(center.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad)),
    //        (float)(center.y + radius * Mathf.Sin(angle * Mathf.Deg2Rad)));

    //    player[0].transform.position = newPos;
    //}

    public void ProgressBarController1()
    {
        traceTarget[0] += offset;

        Debug.Log(traceTarget[0]);
    }
    public void ProgressBarController2()
    {
        traceTarget[1] += offset;
    }
}


//Invoke("Setup", .1f);
//player[0].transform.position = new Vector2((currentMap1.position.x - 100), currentMap1.position.y);
//player[1].transform.position = new Vector2((currentMap2.position.x - 100), currentMap2.position.y);

//this.transform.position = new Vector2(470, 100);

//if (maps.Count == 0)
//{
//    for (int i = 0; i < GameController.totalLevels; i++)
//    {
//        maps.Add(transform.GetChild(i));
//    }
//}

//CancelInvoke("Contador");

//if (currentMap1 != (maps[GameController.currentLevelPlayer1]))
//{
//    currentMap1 = maps[GameController.currentLevelPlayer1];
//}
//if (currentMap2 != maps[GameController.currentLevelPlayer2])
//{
//    currentMap2 = maps[GameController.currentLevelPlayer2];
//}
//startPosition1 = player[0].transform;
//startPosition2 = player[1].transform;

//changedPositionUI = false;

//enum StatusRace
//{
//    Player1Front,
//    Player2Front,
//    Empty
//}
//enum Moviment
//{
//    Player1Stoped,
//    Player2Stoped,
//    Empty
//}

//StatusRace statusRace = StatusRace.Empty;
//Moviment moviment = Moviment.Empty;


//// StatusRace controller
//if (GameController.currentLevelPlayer1 > GameController.currentLevelPlayer2)
//    statusRace = StatusRace.Player1Front;
//else if (GameController.currentLevelPlayer2 > GameController.currentLevelPlayer1)
//    statusRace = StatusRace.Player2Front;
////

//if (startPosition1.position.x != (currentMap1.position.x - 100) && moviment != Moviment.Player2Stoped)
//{
//    if (player[0].transform.parent != this.transform)
//        player[0].transform.parent = this.transform;
//    if (player[0].transform.position.x < (currentMap1.position.x - 100))
//        player[0].transform.position = Vector2.Lerp(startPosition1.position, new Vector2((currentMap1.position.x - 100), currentMap1.position.y), 4f * Time.deltaTime);
//}

//if (startPosition2.position.x != (currentMap2.position.x - 100) && moviment != Moviment.Player1Stoped)
//{
//    if (player[1].transform.parent != this.transform)
//        player[1].transform.parent = this.transform;
//    if (player[1].transform.position.x < (currentMap2.position.x - 100))
//        player[1].transform.position = Vector2.Lerp(startPosition2.position, new Vector2((currentMap2.position.x - 100), currentMap2.position.y), 4f * Time.deltaTime);
//}

//if (GameController.currentLevelPlayer1 > 6f && GameController.currentLevelPlayer2 < 3f || GameController.currentLevelPlayer2 > 6f && GameController.currentLevelPlayer1 < 3f)
//{
//    this.transform.position = Vector2.Lerp(this.transform.position, lastPosition, Time.deltaTime);

//    changedPositionUI = true;

//switch (statusRace)
//{
//    case StatusRace.Player1Front:
//        player[1].transform.position = new Vector2(36f, 40f);
//        moviment = Moviment.Player1Stoped;
//        player[1].transform.parent = this.transform.parent;
//        if (GameController.currentLevelPlayer2 >= 3f)
//            moviment = Moviment.Empty;
//        break;
//    case StatusRace.Player2Front:
//        player[0].transform.position = new Vector2(36f, 40f);
//        moviment = Moviment.Player2Stoped;
//        player[0].transform.parent = this.transform.parent;
//        if (GameController.currentLevelPlayer1 >= 3f)
//            moviment = Moviment.Empty;
//        break;
//}
//}
//else
//    moviment = Moviment.Empty;

//if (GameController.currentLevelPlayer1 > 8f && !changedPositionUI || GameController.currentLevelPlayer2 > 8f && !changedPositionUI)
//{
//    this.transform.position = Vector2.Lerp(this.transform.position, lastPosition, Time.deltaTime);
//}