using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LoadingSceneManager : MonoBehaviour
{
    public static string nextScene;
    public Image progressBar;

    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    IEnumerator LoadScene()
    {
        AsyncOperation LoadingOperation = SceneManager.LoadSceneAsync(nextScene);
        LoadingOperation.allowSceneActivation = false;
        float timer = 0.0f;
        while (!LoadingOperation.isDone)
        {
            yield return null;
            timer += Time.deltaTime;

            progressBar.fillAmount = timer / 10f;
                
                
            if(timer > 10)
            {
                LoadingOperation.allowSceneActivation = true;
            }
        }
        yield return null;
    }
}

