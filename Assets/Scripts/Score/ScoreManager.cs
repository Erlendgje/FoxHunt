using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public static int score;

    public static int highscore;

    Text text;

    void Start()
    {
        text = GetComponent<Text>();

        score = 0;

        highscore = PlayerPrefs.GetInt("highscore", highscore);
    }

    void Update()
    {
        if (score > highscore);
        highscore = score;
        text.text = "" + score;
        PlayerPrefs.SetInt("highscore", highscore);
    }

}