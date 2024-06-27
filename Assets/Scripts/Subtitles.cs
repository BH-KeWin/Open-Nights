using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Subtitles : MonoBehaviour
{
    int textPhase = 0;
    [SerializeField]String[] text;
    [SerializeField]float[] textTime;
    [SerializeField]TextMeshProUGUI subtitleText;
    public IEnumerator BeginText(){
        if(textPhase < text.Length){
            subtitleText.SetText(text[textPhase]);
            yield return new WaitForSecondsRealtime(textTime[textPhase]);

            textPhase++;
            StartCoroutine("BeginText");
        }
        else{
            subtitleText.SetText("");
        }
    }
}
