using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    private Text textTimer;
    private int timer;

    private void Start()
    {
        textTimer = GetComponent<Text>();
        timer = -1;
        textTimer.text = timer.ToString();
        InvokeRepeating("TimerControl", 0, 1);
    }

    private void TimerControl()
    {
        timer++;
        textTimer.text = timer.ToString();
    }

    public void ResetTimer()
    {
        CancelInvoke("TimerControl");
        timer = -1;
        InvokeRepeating("TimerControl", 0, 1);
    }

    public void StopTime()
    {
        CancelInvoke("TimerControl");

        if (!PlayerPrefs.HasKey("BEST_TIME"))
            PlayerPrefs.SetInt("BEST_TIME", timer);
        else
            if (timer < PlayerPrefs.GetInt("BEST_TIME"))
            PlayerPrefs.SetInt("BEST_TIME", timer);

        PlayerPrefs.Save();
    }
}
