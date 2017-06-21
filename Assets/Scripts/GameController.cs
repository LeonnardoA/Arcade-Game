using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MapsByDifficult
{
    public List<Transform> mapsN1 = new List<Transform>();
    public List<Transform> mapsN2 = new List<Transform>();
    public List<Transform> mapsN3 = new List<Transform>();
}

public class GameController : MonoBehaviour
{
    //HUD
    public GameObject winHUD;
    public Text bestTimeTxt;
    public CurrentMapUIControler _currentMapUIControler;
    public GameObject currentMapUIControler;
    public GameObject fade;
    public GameObject cameraMenu;
    public Camera[] camerasGameplay;
    public GameObject[] timer;

    // LEVELS
    public Transform[] levels;
    public Transform[] player;
    public List<Transform> maps = new List<Transform>();
    public Transform[] _camera;
    public MapsByDifficult mapsByDifficult;
    private int speed = 2;
    public static int totalLevels;
    public static int currentLevelPlayer1 = -1;
    public static int currentLevelPlayer2 = -1;
    private PlayerController player1;
    private PlayerController player2;

    public bool resetPlayerPrefs = false;

    private void Awake()
    {
        if(resetPlayerPrefs)
        PlayerPrefs.DeleteAll();

        if (PlayerPrefs.HasKey("BEST_TIME"))
            bestTimeTxt.text = "BEST TIME: " + PlayerPrefs.GetInt("BEST_TIME").ToString() + " s";
        else
            bestTimeTxt.text = "BEST TIME: 00 s";
    }

    private void Start()
    {
        //maps = null;
        for (int i = 0; i < (mapsByDifficult.mapsN1.Count + 3); i++)
        {
            int randomMap = Random.Range(0, mapsByDifficult.mapsN1.Count);
            maps.Insert(maps.Count, mapsByDifficult.mapsN1[randomMap]);
            mapsByDifficult.mapsN1.Remove(mapsByDifficult.mapsN1[randomMap]);
        }
        for (int x = 0; x < (mapsByDifficult.mapsN2.Count + 4); x++)
        {
            int randomMap = Random.Range(0, mapsByDifficult.mapsN2.Count);
            maps.Insert(maps.Count, mapsByDifficult.mapsN2[randomMap]);
            mapsByDifficult.mapsN2.Remove(mapsByDifficult.mapsN2[randomMap]);
        }
        for (int y = 0; y < (mapsByDifficult.mapsN3.Count + 3); y++)
        {
            int randomMap = Random.Range(0, mapsByDifficult.mapsN3.Count);
            maps.Insert(maps.Count, mapsByDifficult.mapsN3[randomMap]);
            mapsByDifficult.mapsN3.Remove(mapsByDifficult.mapsN3[randomMap]);
        }

        Setup();
    }

    private void Setup()
    {
        player1 = player[0].GetComponent<PlayerController>();
        player2 = player[1].GetComponent<PlayerController>();
        totalLevels = (maps.Count);

        for (int i = 0; i < totalLevels; i++)
        {
            if (i == 0)
            {
                Instantiate(maps[i], new Vector3(levels[0].position.x, 0, 0), Quaternion.identity, levels[0]);
                Instantiate(maps[i], new Vector3(levels[1].position.x, 0, 0), Quaternion.identity, levels[1]);
            }
            else
            {
                Instantiate(maps[i], new Vector3(levels[0].position.x, (levels[0].GetChild(i - 1).transform.position.y + 30), 0), Quaternion.identity, levels[0]);
                Instantiate(maps[i], new Vector3(levels[1].position.x, (levels[1].GetChild(i - 1).transform.position.y + 30), 0), Quaternion.identity, levels[1]);
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
                _camera[0].position = Vector3.Lerp(_camera[0].position, new Vector3(levels[0].GetChild(currentLevelPlayer1).position.x, levels[0].GetChild(currentLevelPlayer1).transform.position.y, _camera[0].position.z), speed * Time.deltaTime);
            if (currentLevelPlayer2 < levels[1].childCount)
                _camera[1].position = Vector3.Lerp(_camera[1].position, new Vector3(levels[1].GetChild(currentLevelPlayer2).position.x, levels[1].GetChild(currentLevelPlayer2).transform.position.y, _camera[1].position.z), speed * Time.deltaTime);
        }

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

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            PlayerPrefs.DeleteAll();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            currentMapUIControler.SetActive(true);
            camerasGameplay[0].enabled = true;
            camerasGameplay[1].enabled = true;
            cameraMenu.SetActive(false);
            fade.SetActive(true);
            timer[0].SetActive(true);
            timer[1].SetActive(true);
            MapController.inGame = true;
        }
    }

    public void ResetGame()
    {
        MapController.inGame = false;
        currentLevelPlayer1 = -1;
        currentLevelPlayer2 = -1;

        winHUD.SetActive(false);
        ChangeLevel("Player1");
        ChangeLevel("Player2");

        timer[0].GetComponentInChildren<Timer>().ResetTimer();
        timer[1].GetComponentInChildren<Timer>().ResetTimer();
        timer[0].SetActive(false);
        timer[1].SetActive(false);
        currentMapUIControler.SetActive(false);
        camerasGameplay[0].enabled = false;
        camerasGameplay[1].enabled = false;
        cameraMenu.SetActive(true);
        fade.SetActive(false);
        if (PlayerPrefs.HasKey("BEST_TIME"))
            bestTimeTxt.text = "BEST TIME: " + PlayerPrefs.GetInt("BEST_TIME").ToString() + " s";
        else
            bestTimeTxt.text = "BEST TIME: 00 s";
    }

    public void ChangeLevel(string currentPlayer)
    {
        switch (currentPlayer)
        {
            case "Player1":
                if (currentLevelPlayer1 < (totalLevels - 1))
                {
                    currentLevelPlayer1++;
                    Transform currentLevel1 = levels[0].GetChild(currentLevelPlayer1);
                    player1.currentMap = currentLevel1.gameObject;

                    player1.transform.SetParent(currentLevel1);
                    player1.ResetMap();
                }
                else
                {
                    winHUD.SetActive(true);
                    Text txt = winHUD.transform.Find("Text1").GetComponent<Text>();
                    txt.text = "1º Player 1";
                    Text txt2 = winHUD.transform.Find("Text2").GetComponent<Text>();
                    txt2.text = "2º Player 2";
                    timer[0].GetComponentInChildren<Timer>().StopTime();
                    timer[1].GetComponentInChildren<Timer>().StopTime();
                    MapController.inGame = false;
                }
                break;
            case "Player2":
                if (currentLevelPlayer2 < (totalLevels - 1))
                {
                    currentLevelPlayer2++;
                    Transform currentLevel2 = levels[1].GetChild(currentLevelPlayer2);
                    player2.currentMap = currentLevel2.gameObject;

                    player2.transform.SetParent(currentLevel2);
                    player2.ResetMap();
                }
                else
                {
                    winHUD.SetActive(true);
                    Text txt = winHUD.transform.Find("Text1").GetComponent<Text>();
                    txt.text = "1º Player 2";
                    Text txt2 = winHUD.transform.Find("Text2").GetComponent<Text>();
                    txt2.text = "2º Player 1";
                    timer[0].GetComponentInChildren<Timer>().StopTime();
                    timer[1].GetComponentInChildren<Timer>().StopTime();
                    MapController.inGame = false;
                }
                break;
        }

        if (levels[0].childCount != totalLevels || levels[1].childCount != totalLevels)
            return;
        for (int i = 0; i < totalLevels; i++)
        {
            levels[0].GetChild(i).GetComponent<MapController>().enabled = false;
            levels[1].GetChild(i).GetComponent<MapController>().enabled = false;
        }
        if (currentLevelPlayer1 >= 0 && currentLevelPlayer2 >= 0){// && currentLevelPlayer1 <= totalLevels && currentLevelPlayer2 <= totalLevels) {
            levels[0].GetChild(currentLevelPlayer1).GetComponent<MapController>().enabled = true;
            levels[1].GetChild(currentLevelPlayer2).GetComponent<MapController>().enabled = true;
            
            if (_currentMapUIControler)
                _currentMapUIControler.Setup();
        }
    }
}