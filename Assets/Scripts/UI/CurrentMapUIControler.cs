using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentMapUIControler : MonoBehaviour {
    
    public GameObject[] player;
    public List<Transform> maps;
    //float currentDistancePlayer1 = 10;
    //float currentDistancePlayer2 = 40;

    private void OnEnable()
    {
        Invoke("Setup", .1f);
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

        player[0].transform.position = (maps[GameController.currentLevelPlayer1].position);
        player[1].transform.position = (maps[GameController.currentLevelPlayer2].position);

        InvokeRepeating("Contador", 0, .1f);
    }

    void Contador()
    {
        if (maps.Count >= GameController.totalLevels && GameController.currentLevelPlayer1 >= 0 && GameController.currentLevelPlayer2 >= 0)
        {
            player[0].transform.RotateAround(maps[GameController.currentLevelPlayer1].position, transform.forward, -80 * Time.deltaTime);
            player[1].transform.RotateAround(maps[GameController.currentLevelPlayer2].position, transform.forward, -50 * Time.deltaTime);
        }
    }
}
