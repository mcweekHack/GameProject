using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score_system : PersistentSingleton<Score_system>
{
    int Score;
    int CurrentScore;
    Coroutine Coro;
    Vector3 ScaleFl = new Vector3(1f, 1.2f, 1f);
    public void Score_Reset()
    {
        Score = 0;
        CurrentScore = 0;
        ScoreUI.UpdateScore(0);
    }
    public void AddScore(int Sc)
    {
        Score += Sc;
        if (Coro != null)
            StopCoroutine(Coro);
        Coro = StartCoroutine(ScoreGrowCoroutine());
    }
    public void SetScore(int Sc)
    {
        Score = Sc;
        CurrentScore = Sc;
        ScoreUI.UpdateScore(Score);
    }
    void OnEnable()
    {
        SetScore(0);
    }
    IEnumerator ScoreGrowCoroutine()
    {
        ScoreUI.ScoreTextScale(ScaleFl);
        while (CurrentScore< Score)
        {
            CurrentScore += 1;
            ScoreUI.UpdateScore(CurrentScore);
            yield return null;
        }
        ScoreUI.ScoreTextScale(Vector3.one);
    }

}
