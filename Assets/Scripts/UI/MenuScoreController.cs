using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScoreController : MonoBehaviour
{

    public List<Text> score;
    public List<Text> _name;
    public ArrayList _score = new ArrayList();
    public List<string> _name_ = new List<string>(10);

    private void OnEnable()
    {
        for (int i = 0; i < 10; i++)
        {
            score[i].text = "---";
            _name[i].text = "---";
        }

        _score.Clear();
        _name_.Clear();

        //for (int i = 0; i < 10; i++)
        //{
        //    if (PlayerPrefs.HasKey("ranking_score_" + i))
        //    {
        //        _score.Add(PlayerPrefs.GetInt("ranking_score_" + i));
        //    }
        //}

        for (int x = 0; x < PlayerPrefs.GetInt("ranking_count"); x++)
        {
            //if (x < _score.Count -1)
            //{
            //    if (_score[x] > _score[x + 1])
            //        _score.Reverse(x, (x + 1));
            //}
            if (PlayerPrefs.GetInt("ranking_score_" + x) > 0)
            {
                if (PlayerPrefs.HasKey("ranking_name_" + x) && PlayerPrefs.HasKey("ranking_score_" + x))
                {
                    int currentScore = 0;
                    if (PlayerPrefs.HasKey("ranking_score_min" + x))
                        currentScore = (60 * PlayerPrefs.GetInt("ranking_score_min" + x) + PlayerPrefs.GetInt("ranking_score_" + x));
                    else
                        currentScore = PlayerPrefs.GetInt("ranking_score_" + x);
                    _score.Add(currentScore);
                    _name_.Add(PlayerPrefs.GetString("ranking_name_" + x));


                }
                _score.Sort();
            }
        }

        for (int i = 0; i < _score.Count; i++)
        {
            if (_score[i].GetHashCode() < 60)
            {
                score[i].text = _score[i] + "s";
            }
            else if (_score[i].GetHashCode() >= 60)
            {
                int currentScore = Mathf.FloorToInt(_score[i].GetHashCode() - 60);
                score[i].text = "1:" + currentScore + "s";
            }
            else if (_score[i].GetHashCode() > 120)
            {
                int currentScore = Mathf.FloorToInt(_score[i].GetHashCode() - 120);
                score[i].text = "2:" + currentScore + "s";
            }
            else if (_score[i].GetHashCode() > 180)
            {
                int currentScore = Mathf.FloorToInt(_score[i].GetHashCode() - 180);
                score[i].text = "3:" + currentScore + "s";
            }
            else if (_score[i].GetHashCode() > 240)
            {
                int currentScore = Mathf.FloorToInt(_score[i].GetHashCode() - 240);
                score[i].text = "4:" + currentScore + "s";
            }
            else if (_score[i].GetHashCode() > 300)
            {
                int currentScore = Mathf.FloorToInt(_score[i].GetHashCode() - 300);
                score[i].text = "5:" + currentScore + "s";
            }
            else if (_score[i].GetHashCode() > 360)
            {
                int currentScore = Mathf.FloorToInt(_score[i].GetHashCode() - 360);
                score[i].text = "6:" + currentScore + "s";
            }
            else if (_score[i].GetHashCode() > 420)
            {
                int currentScore = Mathf.FloorToInt(_score[i].GetHashCode() - 420);
                score[i].text = "7:" + currentScore + "s";
            }
            else if (_score[i].GetHashCode() > 480)
            {
                int currentScore = Mathf.FloorToInt(_score[i].GetHashCode() - 480);
                score[i].text = "8:" + currentScore + "s";
            }
            else if (_score[i].GetHashCode() > 540)
            {
                int currentScore = Mathf.FloorToInt(_score[i].GetHashCode() - 540);
                score[i].text = "9:" + currentScore + "s";
            }
            else if (_score[i].GetHashCode() > 600)
            {
                int currentScore = Mathf.FloorToInt(_score[i].GetHashCode() - 600);
                score[i].text = "10:" + currentScore + "s";
            }
            else if (_score[i].GetHashCode() > 660)
            {
                score[i].text = "---";
            }

            _name[i].text = _name_[i];
        }
    }
}
