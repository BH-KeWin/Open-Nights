using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class TimePass : MonoBehaviour
{
    [SerializeField]PlayerMovement player;
    [SerializeField]AudioSource nightAudioPlayer, nightEndPlayer, ambiencePlayer, ambienceIntensePlayer, ambienceIntensePlayer2;
    [SerializeField]Subtitles[] subtitles;
    [SerializeField]AudioClip[] nightCalls;
    [SerializeField]AiLevels[] aiLevels;
    [SerializeField]GameObject winUI, nightCallMuteButton, helperText;
    [SerializeField]TextMeshProUGUI timeText, nightText, loaderText;
    public int nightNum;
    int gameTime = 0;
    int extraItem = 0;
    void Awake(){
        nightNum = PlayerPrefs.GetInt("Night", 1);

        if(PlayerPrefs.GetInt("Night") == 1){
            helperText.SetActive(true);
        }

        if(PlayerPrefs.GetString("CustomNight") == "active"){
            nightNum = 7;
        }

        //nightNum = 1;
        nightText.SetText("Night " + nightNum.ToString());
        StartCoroutine("Count");
        StartCoroutine("StartNightCall");

        if(PlayerPrefs.GetString("Drink") == "active"){
            PlayerPrefs.SetString("Drink",  "not active");
            extraItem = 6;
        }
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Numlock)){
            //Haha. During testing we had to have a night skip function.
            //StartCoroutine("EndNight");
        }
    }

    public int GetAILevel(int aiNum){
        return aiLevels[nightNum-1].aiNum[aiNum];
    }

    IEnumerator StartNightCall(){
        yield return new WaitForSecondsRealtime(3f);

        if(PlayerPrefs.GetString("CustomNight") == "not active" || !PlayerPrefs.HasKey("CustomNight")){
            nightAudioPlayer.clip = nightCalls[nightNum-1];
            subtitles[nightNum-1].StartCoroutine("BeginText");

            nightAudioPlayer.Play();
            nightCallMuteButton.SetActive(true);
        }

        yield return new WaitForSecondsRealtime(15f);
        nightCallMuteButton.SetActive(false);
    }

    IEnumerator Count(){
        yield return new WaitForSecondsRealtime(76 - extraItem);
        gameTime++;
        timeText.SetText(gameTime + " AM");

        if(gameTime < 6){
            StartCoroutine("Count");
        }else{
            StartCoroutine("EndNight");
        }
    }

    IEnumerator FadeMusic(AudioSource audio, string fadeMode, float desiredVolume){
        switch(fadeMode){
            case "in":
                for (float i = 0; i < desiredVolume; i+=0.02f){
                    yield return new WaitForSecondsRealtime(0.1f);
                    audio.volume = i;
                }

            break;

            case "out":
                for (float i = audio.volume; i > 0; i-=0.02f){
                    yield return new WaitForSecondsRealtime(0.1f);
                    audio.volume = i;
                }
            break;
        }
    }

    IEnumerator EndNight(){
        if(player.alive){
            player.alive = false;


            StartCoroutine(FadeMusic(ambiencePlayer, "out", 0));
            StartCoroutine(FadeMusic(ambienceIntensePlayer, "out", 0));
            StartCoroutine(FadeMusic(ambienceIntensePlayer2, "out", 0));
            nightEndPlayer.volume = 0;
            nightEndPlayer.Play();
            StartCoroutine(FadeMusic(nightEndPlayer, "in", 0.5f));


            yield return new WaitForSecondsRealtime(4.6f);

            winUI.SetActive(true);

            switch(nightNum){
                case 5:
                    PlayerPrefs.SetString("beat5", "true");
                break;

                case 6:
                    PlayerPrefs.SetString("beat6", "true");
                    PlayerPrefs.SetString("SeenPaycheck", "false");

                break;

                case 7:
                    if(PlayerPrefs.GetInt("Peter") == 20 && PlayerPrefs.GetInt("Ben") == 20 && PlayerPrefs.GetInt("Toby") == 20 && PlayerPrefs.GetInt("Bib") == 20 && PlayerPrefs.GetInt("Old Peter") == 20){
                        PlayerPrefs.SetString("beat7Ultimate", "true");
                        PlayerPrefs.SetString("SeenTrueEnding", "false");
                    }
                break;
            }

            if(nightNum == 5 || nightNum == 6 || nightNum == 7){
                if(nightNum == 5 || nightNum == 7){
                    PlayerPrefs.SetInt("Night", 6);
                }

                yield return new WaitForSecondsRealtime(6);
                PlayerPrefs.SetString("CustomNight", "not active");
                loaderText.SetText("Main Menu");
                FindFirstObjectByType(typeof(LoadingScreen)).GetComponent<LoadingScreen>().LoadScene("MainMenu");
                yield return null;
            }

            yield return new WaitForSecondsRealtime(6);
            PlayerPrefs.SetString("CustomNight", "not active");
            LoadShop();
        }
    }

    private void LoadShop(){
        loaderText.SetText("Shop");
        FindFirstObjectByType(typeof(LoadingScreen)).GetComponent<LoadingScreen>().LoadScene("ShopScene");
    }
}
