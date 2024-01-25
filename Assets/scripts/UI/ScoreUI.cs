using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreUI : MonoBehaviour
{
    static Text ScoreText;
    void Awake()
    {
        ScoreText = GetComponentInChildren<Text>();
        ScoreText.text = "0";
    }
    public static void UpdateScore(int Score)
    {
        ScoreText.text = Score.ToString();
        return;
    }
    public static void ScoreTextScale(Vector3 scl)
    {
        ScoreText.rectTransform.localScale = scl;
    }
}
