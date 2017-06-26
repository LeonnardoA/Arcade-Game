using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ArcadePUCCampinas;

public class MenuNavigate : MonoBehaviour {
    
    public static int inputPlayer1 = 0;
    public static int inputPlayer2 = 1;
    public Text[] elementsUI;
    public GameController gameController;
    private GameObject selectPlayer;
    private GameObject score;
    private GameObject credits;
    public static bool canNavigate = true;
    private int selected;

    private void Awake()
    {
        score = this.transform.Find("Menu Score").gameObject;
        credits = this.transform.Find("Menu Credits").gameObject;
        selectPlayer = this.transform.Find("Menu SelectName").gameObject;
        score.SetActive(false);
        credits.SetActive(false);
        selectPlayer.SetActive(false);
        canNavigate = true;
        selected = 0;
    }

    private void Start()
    {
        inputPlayer1 = 0;
        inputPlayer2 = 1;

        for (int i = 0; i < elementsUI.Length; i++)
        {
            if (i == 0)
                elementsUI[i].color = Color.white;
            else
                elementsUI[i].color = Color.gray;
        }
    }

    private void Update()
    {
        if (InputArcade.Apertou(0, EControle.AMARELO) || Input.GetKeyDown(KeyCode.E))
        {
            if (inputPlayer1 == 0)
            {
                inputPlayer1 = 1;
                inputPlayer2 = 0;
            }
            else
            {
                inputPlayer1 = 0;
                inputPlayer2 = 1;
            }
        }

        if (InputArcade.Apertado(inputPlayer1, EControle.AZUL) || Input.GetKeyDown(KeyCode.Z))
        {
            switch (selected)
            {
                case 0:
                    //gameController.StartGame();
                    selectPlayer.SetActive(true);
                    canNavigate = false;
                    break;
                case 1:
                    if (canNavigate)
                    {
                        score.SetActive(true);
                        canNavigate = false;
                    }
                    else
                    {
                        score.SetActive(false);
                        canNavigate = true;
                    }
                    break;
                case 2:
                    if (canNavigate)
                    {
                        credits.SetActive(true);
                        canNavigate = false;
                    }
                    else
                    {
                        credits.SetActive(false);
                        canNavigate = true;
                    }
                    break;
            }
        }


        if (InputArcade.Apertou(inputPlayer1, EControle.BAIXO) && canNavigate || Input.GetKeyDown(KeyCode.DownArrow) && canNavigate)
        {
            if (selected < (elementsUI.Length - 1))
            {
                selected++;
                elementsUI[selected - 1].color = Color.gray;
                elementsUI[selected].color = Color.white;
            }
        }
        else if (InputArcade.Apertou(inputPlayer1, EControle.CIMA) && canNavigate || Input.GetKeyDown(KeyCode.UpArrow) && canNavigate)
        {
            if (selected > 0)
            {
                selected--;
                elementsUI[selected].color = Color.white;
                elementsUI[selected + 1].color = Color.gray;
            }
        }
    }
}
