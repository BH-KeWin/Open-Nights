using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI nightText;
    [SerializeField]GameObject loadingScreen;
    
    public void LoadScene(String sceneName){
        StartCoroutine(LoadSceneAsync(sceneName));

        if(sceneName != "ShopScene" && sceneName != "MainMenu" && PlayerPrefs.GetString("CustomNight") != "active"){
            nightText.SetText("Night " + PlayerPrefs.GetInt("Night").ToString());
        }
    }

    IEnumerator LoadSceneAsync(String sceneName){

        loadingScreen.SetActive(true);
        yield return new WaitForSecondsRealtime(1);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone){
            yield return null;
        }
    }
}
