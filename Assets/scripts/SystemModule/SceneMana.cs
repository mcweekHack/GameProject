using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneMana : PersistentSingleton<SceneMana>
{
    const string GameScene = "Gameplay";
    const string StartScene = "StartMenu";

    [SerializeField] Color image_color;
    [SerializeField] Image emission_image;
    [SerializeField] float fadeTime;
    Coroutine LoadCoro;
    void LoadScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
    public void LoadGameScene()
    {
        if (LoadCoro != null)
            StopCoroutine(LoadCoro);
        LoadCoro = StartCoroutine(LoadCoroutine(GameScene));
    }
    public void BackToStartMenu()
    {
        if (LoadCoro != null)
            StopCoroutine(LoadCoro);
        LoadCoro = StartCoroutine(LoadCoroutine(StartScene));
    }
    IEnumerator LoadCoroutine(string SceneName)
    {
        var loading = SceneManager.LoadSceneAsync(SceneName);
        loading.allowSceneActivation = false;
        emission_image.gameObject.SetActive(true);
        while (image_color.a < 1f)
        {
            image_color.a = Mathf.Clamp01(image_color.a + Time.unscaledDeltaTime / fadeTime);
            emission_image.color = image_color;
            yield return null;
        }
        loading.allowSceneActivation = true;
        while (image_color.a > 0f)
        {
            image_color.a = Mathf.Clamp01(image_color.a - Time.unscaledDeltaTime / fadeTime);
            emission_image.color = image_color;
            yield return null;
        }
        emission_image.gameObject.SetActive(false);
        TimeMana.instance.ContinueGame();
    }
}
