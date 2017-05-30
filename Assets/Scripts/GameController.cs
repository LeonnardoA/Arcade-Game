using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //HUD
    public GameObject winHUD;

    // LEVELS
    public Transform[] levels;
    public Transform[] player;
    private PlayerController player1;
    private PlayerController player2;
    private int totalLevels = 6;
    private int currentLevelPlayer1 = 0;
    private int currentLevelPlayer2 = 0;
    private int currentPositionPlayer1 = 30;
    private int currentPositionPlayer2 = 30;
    private int speed = 2;

    private void Start()
    {
        player1 = player[0].GetComponent<PlayerController>();
        player2 = player[1].GetComponent<PlayerController>();

        ChangeLevel("Player1");
        ChangeLevel("Player2");
    }

    private void Update()
    {
        levels[0].transform.localPosition = Vector3.Lerp(levels[0].transform.localPosition, new Vector3(levels[0].transform.localPosition.x,
            currentPositionPlayer1,
            levels[0].transform.localPosition.z), speed * Time.deltaTime);
        levels[1].transform.localPosition = Vector3.Lerp(levels[1].transform.localPosition, new Vector3(levels[1].transform.localPosition.x,
          currentPositionPlayer2,
          levels[1].transform.localPosition.z), speed * Time.deltaTime);

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
        currentLevelPlayer1 = 0;
        currentLevelPlayer2 = 0;
        currentPositionPlayer1 = 30;
        currentPositionPlayer2 = 30;
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
                    currentPositionPlayer1 -= 30;
                    Transform currentLevel1 = levels[0].Find("Map" + currentLevelPlayer1);
                    player1.currentMap = currentLevel1.gameObject;
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
                    currentPositionPlayer2 -= 30;
                    Transform currentLevel2 = levels[1].Find("Map" + currentLevelPlayer2);
                    player2.currentMap = currentLevel2.gameObject;
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

        for (int i = 1; i < totalLevels; i++)
        {
            levels[0].Find("Map" + i).GetComponent<MapController>().enabled = false;
            levels[1].Find("Map" + i).GetComponent<MapController>().enabled = false;
        }
        if (currentLevelPlayer1 <= 0)
            levels[0].Find("Map1").GetComponent<MapController>().enabled = true;
        else
            if (currentLevelPlayer2 < totalLevels)
            levels[0].Find("Map" + (currentLevelPlayer1)).GetComponent<MapController>().enabled = true;
        if (currentLevelPlayer2 <= 0)
            levels[1].Find("Map1").GetComponent<MapController>().enabled = true;
        else
            if (currentLevelPlayer2 < totalLevels)
            levels[1].Find("Map" + (currentLevelPlayer2)).GetComponent<MapController>().enabled = true;
    }
}