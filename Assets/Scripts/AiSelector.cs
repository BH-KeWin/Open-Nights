using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AiSelector : MonoBehaviour
{
    [SerializeField]String animatronicName, animatronicDescription;
    [SerializeField]TextMeshProUGUI aiLevelText, aiDescr;
    int aiLevel;
    bool holdingButton;

    public void Awake(){
        aiLevel =  PlayerPrefs.GetInt(animatronicName, 0);
        UpdateAiLevel();
    }

    public void OnHover(){
        aiDescr.SetText(animatronicDescription);
    }

    public void UnHover(){
        aiDescr.SetText("");
    }

    public void IncreaseValue(){
        if(aiLevel < 20){
            aiLevel++;
        }
        UpdateAiLevel();
        StartCoroutine("HolderTimer", "increase");
    }

    public void DecreaseValue(){
        if(aiLevel > 0){
            aiLevel--;
        }
        UpdateAiLevel();
        StartCoroutine("HolderTimer", "decrease");
    }

    IEnumerator HolderTimer(string dir){
        if(!holdingButton){
            yield return new WaitForSecondsRealtime(0.2f);
        }
        yield return new WaitForSecondsRealtime(0.05f);
        holdingButton = true;
        switch(dir){
            case "increase":
                if(aiLevel < 20){
                    aiLevel++;
                }
            break;

            case "decrease":
                if(aiLevel > 0){
                    aiLevel--;
                }
            break;
        }

        UpdateAiLevel();
        StartCoroutine("HolderTimer", dir);
    }

    public void UnHoldButton(){
        StopCoroutine("HolderTimer");
        holdingButton = false;
    }

    void UpdateAiLevel(){
        aiLevelText.SetText(aiLevel.ToString());
        PlayerPrefs.SetInt(animatronicName, aiLevel);
    }

    void UpdateAnimatronicDescription(){
        aiDescr.SetText(animatronicDescription);
    }
}
