using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //HUD
    public GameObject winHUD;
    public CurrentMapUIControler currentMapUIControler;

    // LEVELS
    public Transform[] levels;
    public Transform[] player;
    public Transform[] maps;
    public Transform[] _camera;
    private PlayerController player1;
    private PlayerController player2;
    public static int totalLevels;
    public static int currentLevelPlayer1 = -1;
    public static int currentLevelPlayer2 = -1;
    private int speed = 2;

    private void Start()
    {
        currentMapUIControler = FindObjectOfType<CurrentMapUIControler>();

        player1 = player[0].GetComponent<PlayerController>();
        player2 = player[1].GetComponent<PlayerController>();
        totalLevels = (maps.Length - 1);
        for (int i = 0; i < maps.Length; i++)
        {
            if (i == 0)
            {
                Instantiate(maps[i], new Vector3(levels[0].position.x, 0, 0), Quaternion.identity, levels[0]);
                Instantiate(maps[i], new Vector3(levels[1].position.x, 0, 0), Quaternion.identity, levels[1]);
            }
            else
            {
                Instantiate(maps[i], new Vector3(levels[0].position.x, (maps[i - 1].position.y + 30), 0), Quaternion.identity, levels[0]);
                Instantiate(maps[i], new Vector3(levels[1].position.x, (maps[i - 1].position.y + 30), 0), Quaternion.identity, levels[1]);
            }
        }

        ChangeLevel("Player1");
        ChangeLevel("Player2");
    }

    private void Update()
    {
        if (levels[0].childCount >= totalLevels && levels[1].childCount >= totalLevels)
        {
            if (currentLevelPlayer1 < levels[0].childCount)
                _camera[0].position = Vector3.Lerp(_camera[0].position, new Vector3(levels[0].GetChild(currentLevelPlayer1).position.x, maps[currentLevelPlayer1].position.y, _camera[0].position.z), speed * Time.deltaTime);
            if (currentLevelPlayer2 < levels[1].childCount)
                _camera[1].position = Vector3.Lerp(_camera[1].position, new Vector3(levels[1].GetChild(currentLevelPlayer2).position.x, maps[currentLevelPlayer2].position.y, _camera[1].position.z), speed * Time.deltaTime);
        }//levels[0].transform.localPosition = Vector3.Lerp(levels[0].transform.localPosition, new Vector3(levels[0].transform.localPosition.x,
        //    currentPositionPlayer1,
        //    levels[0].transform.localPosition.z), speed * Time.deltaTime);
        //levels[1].transform.localPosition = Vector3.Lerp(levels[1].transform.localPosition, new Vector3(levels[1].transform.localPosition.x,
        //  currentPositionPlayer2,
        //  levels[1].transform.localPosition.z), speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ChangeLevel("Player1");
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            ChangeLevel("Player2");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResetGame();
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            Application.Quit();   
        }
    }

    public void ResetGame()
    {
        MapController.inGame = true;
        currentLevelPlayer1 = -1;
        currentLevelPlayer2 = -1;
        //currentPositionPlayer1 = 30;
        //currentPositionPlayer2 = 30;
        ChangeLevel("Player1");
        ChangeLevel("Player2");
        winHUD.SetActive(false);
    }

    public void ChangeLevel(string currentPlayer)
    {
        switch (currentPlayer)
        {
            case "Player1":
                if (currentLevelPlayer1 < totalLevels)
                {
                    currentLevelPlayer1++;
                    Transform currentLevel1 = levels[0].GetChild(currentLevelPlayer1);
                    player1.currentMap = currentLevel1.gameObject;
                    //currentPositionPlayer1 -= 30;
                    player1.transform.SetParent(currentLevel1);
                    player1.ResetMap();
                }
                else
                {
                    winHUD.SetActive(true);
                    Text txt = winHUD.transform.Find("Text").GetComponent<Text>();
                    txt.text = "Jogador 1 venceu!";
                    MapController.inGame = false;
                }
                break;
            case "Player2":
                if (currentLevelPlayer2 < totalLevels)
                {
                    currentLevelPlayer2++;
                    Transform currentLevel2 = levels[1].GetChild(currentLevelPlayer2);
                    player2.currentMap = currentLevel2.gameObject;
                    //currentPositionPlayer2 -= 30;
                    player2.transform.SetParent(currentLevel2);
                    player2.ResetMap();
                }
                else
                {
                    winHUD.SetActive(true);
                    Text txt = winHUD.transform.Find("Text").GetComponent<Text>();
                    txt.text = "Jogador 2 venceu!";
                    MapController.inGame = false;
                }
                break;
        }

        for (int i = 0; i < totalLevels; i++)
        {
            levels[0].GetChild(i).GetComponent<MapController>().enabled = false;
            levels[1].GetChild(i).GetComponent<MapController>().enabled = false;
        }
        if (currentLevelPlayer1 >= 0 && currentLevelPlayer2 >= 0) {
            levels[0].GetChild(currentLevelPlayer1).GetComponent<MapController>().enabled = true;
            levels[1].GetChild(currentLevelPlayer2).GetComponent<MapController>().enabled = true;
            currentMapUIControler.Setup();
        }
        //if (currentLevelPlayer1 <= 0)
        //    levels[0].GetChild(currentLevelPlayer1).GetComponent<MapController>().enabled = true;
        //else
        //    if (currentLevelPlayer2 < totalLevels)
        //    levels[0].GetChild(currentLevelPlayer1).GetComponent<MapController>().enabled = true;
        //if (currentLevelPlayer2 <= 0)
        //    levels[1].GetChild(currentLevelPlayer2).GetComponent<MapController>().enabled = true;
        //else
        //    if (currentLevelPlayer2 < totalLevels)
        //    levels[1].GetChild(currentLevelPlayer2).GetComponent<MapController>().enabled = true;
    }
}