using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuButtons : MonoBehaviour
{
    [SerializeField]AudioSource menuMusic, nightCall;
    [SerializeField]TextMeshProUGUI nightText, loaderText;
    [SerializeField]GameObject mainMenu, extrasMenu, newsPaperObj, nightCallMuteButton, nightCallSubtitle;
    
    void Start(){
        Application.targetFrameRate = 60;
    }
    public void RetryButton(){
        loaderText.SetText("Night " + PlayerPrefs.GetInt("Night").ToString());
        FindFirstObjectByType(typeof(LoadingScreen)).GetComponent<LoadingScreen>().LoadScene("SampleScene");
    }

    public void MuteNightCall(){
        Object.Destroy(nightCall);
        nightCallMuteButton.SetActive(false);
        nightCallSubtitle.SetActive(false);
    }

    public void BackToMenuButton(){
        SceneManager.LoadScene("MainMenu");
    }

    public void NewGameButton(){
        newsPaperObj.SetActive(true);
        PlayerPrefs.SetString("CustomNight", "not active");
        PlayerPrefs.SetInt("Night", 1);
        loaderText.SetText("Night 1");
        StartCoroutine("NewspaperBegin");
    }
    IEnumerator NewspaperBegin(){
        yield return new WaitForSecondsRealtime(11);
        menuMusic.enabled = false;
        PlayerPrefs.SetInt("Night", 1);
        FindFirstObjectByType(typeof(LoadingScreen)).GetComponent<LoadingScreen>().LoadScene("SampleScene");
    }

    public void ContinueButton(){
        loaderText.SetText("Night " + PlayerPrefs.GetInt("Night").ToString());
        PlayerPrefs.SetString("CustomNight", "not active");
        menuMusic.enabled = false;
        FindFirstObjectByType(typeof(LoadingScreen)).GetComponent<LoadingScreen>().LoadScene("SampleScene");
        
    }

    public void BeginNightButton(){
        PlayerPrefs.SetInt("Night", PlayerPrefs.GetInt("Night") + 1);

        loaderText.SetText("Night " + PlayerPrefs.GetInt("Night").ToString());
        PlayerPrefs.SetString("CustomNight", "not active");
        menuMusic.enabled = false;
        FindFirstObjectByType(typeof(LoadingScreen)).GetComponent<LoadingScreen>().LoadScene("SampleScene");
    }

    public void ContinueButtonHover(){
        nightText.SetText("Night " + PlayerPrefs.GetInt("Night").ToString());
        nightText.gameObject.SetActive(true);
    }
    public void ContinueButtonUnHover(){
        nightText.gameObject.SetActive(false);
    }

    public void CustomNightButton(){
        PlayerPrefs.SetString("CustomNight", "active");
        loaderText.SetText("Night 7");
        menuMusic.enabled = false;
        FindFirstObjectByType(typeof(LoadingScreen)).GetComponent<LoadingScreen>().LoadScene("SampleScene");
    }

    public void ExtraButton(){
        extrasMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void BackButton(){
        extrasMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void QuitButton(){
        Application.Quit();
    }

}
