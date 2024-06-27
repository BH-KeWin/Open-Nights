using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuButtonFiller : MonoBehaviour
{
    [SerializeField]Image RSlider, LSlider;

    void OnDisable(){
        RSlider.fillAmount = 0;
        LSlider.fillAmount = 0;
    }

    public void Hover(){
        StartCoroutine("HoverCor");
    }

    public void UnHover(){
        StartCoroutine("UnHoverCor");
    }

   IEnumerator HoverCor(){
        for(float x = 0; x < 1; x += 0.05f){
            yield return new WaitForSecondsRealtime(0.001f);
            RSlider.fillAmount = x;
            LSlider.fillAmount = x;
        }
        RSlider.fillAmount = 1;
        LSlider.fillAmount = 1;
    }

    IEnumerator UnHoverCor(){
        for(float x = 1; x > 0; x -= 0.05f){
            yield return new WaitForSecondsRealtime(0.001f);
            RSlider.fillAmount = x;
            LSlider.fillAmount = x;
        }
        RSlider.fillAmount = 0;
        LSlider.fillAmount = 0;
    }
}
