using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArcadePUCCampinas;
using UnityEngine.UI;

public class SetNamePlayers : MonoBehaviour {

    public Dictionary<int, string> alphabet = new Dictionary<int, string>();
    public static string namePlayer1;
    public static string namePlayer2;
    private bool canChangeName1;
    private bool canChangeName2;
    public GameController gameController;
    public Text[] name1;
    public Text[] name2;
    private int currentText1;
    private int currentText2;
    private int currentLetra1;
    private int currentLetra2;

    private void Awake()
    {
        alphabet.Add(0, "A");
        alphabet.Add(1, "B");
        alphabet.Add(2, "C");
        alphabet.Add(3, "D");
        alphabet.Add(4, "E");
        alphabet.Add(5, "F");
        alphabet.Add(6, "G");
        alphabet.Add(7, "H");
        alphabet.Add(8, "I");
        alphabet.Add(9, "J");
        alphabet.Add(10, "K");
        alphabet.Add(11, "L");
        alphabet.Add(12, "M");
        alphabet.Add(13, "N");
        alphabet.Add(14, "O");
        alphabet.Add(15, "P");
        alphabet.Add(16, "Q");
        alphabet.Add(17, "R");
        alphabet.Add(18, "S");
        alphabet.Add(19, "T");
        alphabet.Add(20, "U");
        alphabet.Add(21, "V");
        alphabet.Add(22, "W");
        alphabet.Add(23, "X");
        alphabet.Add(24, "Y");
        alphabet.Add(25, "Z");
    }

    private void OnEnable()
    {
        canChangeName1 = true;
        canChangeName2 = true;
        currentText1 = 0;
        currentText2 = 0;
        currentLetra1 = 0;
        currentLetra2 = 0;

        for (int i = 1; i < 3; i++)
        {
            name1[i].color = Color.gray;
            name2[i].color = Color.gray;
            name1[i].text = "-";
            name2[i].text = "-";
        }

        name1[0].color = Color.white;
        name2[0].color = Color.white;

        name1[currentText1].text = alphabet[currentLetra1];
        name2[currentText2].text = alphabet[currentLetra2];
    }

    private void Update()
    {
        if (currentText1 == 2 && currentText2 == 2)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                this.gameObject.SetActive(false);
                gameController.StartGame();
            }
        }

        if (currentText1 == 2 && Input.GetKeyDown(KeyCode.Z))
        {
            for (int i = 0; i < 3; i++)
            {
                name1[i].color = Color.gray;
                if (i == 2)
                {
                    canChangeName1 = false;
                    namePlayer1 = name1[0].text + name1[1].text + name1[2].text;
                }
            }
        }
        if (currentText2 == 2 && Input.GetKeyDown(KeyCode.X))
        {
            for (int i = 0; i < 3; i++)
            {
                name2[i].color = Color.gray;
                if (i == 2)
                {
                    canChangeName2 = false;
                    namePlayer2 = name2[0].text + name2[1].text + name2[2].text;
                }
            }
        }

        if (InputArcade.Apertou(MenuNavigate.inputPlayer1, EControle.BAIXO) && canChangeName1)
        {
            if (currentLetra1 > 0 && currentLetra1 < alphabet.Count - 1)
            {
                currentLetra1++;
                name1[currentText1].text = alphabet[currentLetra1];
            }
            else if (currentLetra1 == 0)
            {
                name1[currentText1].text = alphabet[currentLetra1];
                currentLetra1 = 1;
            }
        }
        else if (InputArcade.Apertou(MenuNavigate.inputPlayer1, EControle.CIMA) && canChangeName1)
        {
            if (currentLetra1 < alphabet.Count && currentLetra1 > 0)
            {
                currentLetra1--;
                name1[currentText1].text = alphabet[currentLetra1];
            }
        }

        /// PLAYER 2

        else if (InputArcade.Apertou(MenuNavigate.inputPlayer2, EControle.BAIXO) && canChangeName2 || Input.GetKeyDown(KeyCode.M))
        {
            if (currentLetra2 > 0 && currentLetra2 < alphabet.Count - 1)
            {
                currentLetra2++;
                name2[currentText2].text = alphabet[currentLetra2];
            }
            if (currentLetra2 == 0)
            {
                name2[currentText2].text = alphabet[currentLetra2];
                currentLetra2 = 1;
            }
        }
        else if (InputArcade.Apertou(MenuNavigate.inputPlayer2, EControle.CIMA) && canChangeName2|| Input.GetKeyDown(KeyCode.N))
        {
            if (currentLetra2 < alphabet.Count && currentLetra2 > 0)
            {
                currentLetra2--;
                name2[currentText2].text = alphabet[currentLetra2];
            }
        }

        if (InputArcade.Apertado(MenuNavigate.inputPlayer1, EControle.AZUL) ||  Input.GetKeyDown(KeyCode.Z))
        {
            if (currentText1 < 2)
            {
                currentText1++;
                currentLetra1++;
                name1[currentText1].text = alphabet[currentLetra1];
                name1[currentText1].color = Color.white;
            }
        }
        if (InputArcade.Apertado(MenuNavigate.inputPlayer2, EControle.AZUL) || Input.GetKeyDown(KeyCode.X))
        {
            if (currentText2 < 2)
            {
                currentText2++;
                currentLetra2++;
                name2[currentText2].text = alphabet[currentLetra2];
                name2[currentText2].color = Color.white;
            }
        }
    }
}
