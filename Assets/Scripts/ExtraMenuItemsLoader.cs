using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class ExtraMenuItemLoader : MonoBehaviour
{
    [SerializeField]GameObject continueButton, customNightButton, paycheckObj, trueEndingObj, menuObj;
    [SerializeField]TextMeshProUGUI loaderText;
    [SerializeField]GameObject[] stars;
    bool holdingDel = false;

    void FixedUpdate(){
        if(Input.GetKey(KeyCode.Delete)){
            if(!holdingDel){
                holdingDel = true;
                loaderText.SetText("Resetting game...");
                StartCoroutine("DeleteData");
            }
        }else{
            StopCoroutine("DeleteData");
            holdingDel = false;
        }
    }

    IEnumerator DeleteData(){
        yield return new WaitForSecondsRealtime(3f);
        if(holdingDel){
            //PlayerPrefs.DeleteAll();
            //FindFirstObjectByType(typeof(LoadingScreen)).GetComponent<LoadingScreen>().LoadScene("MainMenu");
        }
    }

    void Awake()
    {
        if(PlayerPrefs.GetInt("Night") > 1){
            continueButton.SetActive(true);
        }

        if(PlayerPrefs.GetString("beat5") == "true"){
            stars[0].SetActive(true);
        }

        if(PlayerPrefs.GetString("beat6") == "true"){
            if(PlayerPrefs.GetString("SeenPaycheck") == "false"){
                PlayerPrefs.SetString("SeenPaycheck", "true");
                paycheckObj.SetActive(true);
            }
            customNightButton.SetActive(true);
            stars[1].SetActive(true);
        }

        if(PlayerPrefs.GetString("beat7Ultimate") == "true"){
            stars[2].SetActive(true);
            if(PlayerPrefs.GetString("SeenTrueEnding") == "false"){
                menuObj.SetActive(false);
                PlayerPrefs.SetString("SeenTrueEnding", "true");
                trueEndingObj.SetActive(true);
                StartCoroutine("ReenableMenu");
            }
        }

        if(1 == 2){
            //Literally unachiveable star. 1 will never be 2 mwhahahaha!
            stars[3].SetActive(true);
        }
    }

    IEnumerator ReenableMenu(){
        yield return new WaitForSecondsRealtime(27);
        menuObj.SetActive(true);
        trueEndingObj.SetActive(false);
    }

}
