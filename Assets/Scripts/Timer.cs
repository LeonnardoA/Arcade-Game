using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    private Text textTimer;
    private int timer;
    private int min;
    private int[] rankingList = new int[10];

    private void Start()
    {
        textTimer = GetComponent<Text>();
        timer = -1;
        min = 0;
        textTimer.text = timer.ToString();
        InvokeRepeating("TimerControl", 0, 1);
    }

    public void TimerControl()
    {
        timer++;
        if (timer > 59)
        {
            timer = 0;
            min++;
        }
        if (min <= 0)
        {
            if (timer < 10)
                textTimer.text = "0" + timer;
            else
                textTimer.text = timer.ToString();
        }
        else
        {
            if (min < 10)
            {
                if (timer < 10)
                    textTimer.text = "0" + min + ":0" + timer.ToString();
                else
                    textTimer.text = "0" + min + ":" + timer.ToString();
            }
            else
            {
                if (timer < 10)
                    textTimer.text = min + ":0" + timer.ToString();
                else
                    textTimer.text = min + ":" + timer.ToString();
            }
        }
    }

    public void ResetTimer()
    {
        timer = -1;
        min = 0;
    }

    public void StopTime(string name)
    {
        CancelInvoke("TimerControl");

        //rankingList[rankingList.Length] = timer;

        PlayerPrefs.SetInt("ranking_count", rankingList.Length);

        //for (int i = 0; i < rankingList.Length; i++)
        //{
        //    if (i < (rankingList.Length-1))
        //        if (rankingList[i] > rankingList[i + 1])
        //            rankingList[i + 1] = rankingList[i];
        //}

        for (int x = 0; x < rankingList.Length; x++)
        {
            if (!PlayerPrefs.HasKey("ranking_name_" + x))
            {
                if (min > 0)
                    PlayerPrefs.SetInt("ranking_score_min" + x, min);

                PlayerPrefs.SetInt("ranking_score_" + x, timer);
                PlayerPrefs.SetString("ranking_name_" + x, name);
                x = rankingList.Length;
            }
        }
        //if (!PlayerPrefs.HasKey("BEST_TIME"))
        //    PlayerPrefs.SetInt("BEST_TIME", timer);
        //else
        //    if (timer < PlayerPrefs.GetInt("BEST_TIME"))
        //    PlayerPrefs.SetInt("BEST_TIME", timer);

        PlayerPrefs.Save();
    }
}
