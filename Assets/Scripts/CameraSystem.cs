using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    [SerializeField]PlayerMovement player;
    [SerializeField]Animator camObjAnim;
    [SerializeField]AudioSource camSwitchSound;
    [SerializeField]EnergyLevel energyLevel;
    [SerializeField]GameObject[] cameras;
    [SerializeField]GameObject camUI, playerCam, whiteStatic, PFXObj, globalStatic, blockerObj;
    int prevCam;
    public bool camUp = false;

    public void CamPlayAnim(){
        if(!camUp){
            camObjAnim.Play("CamOpenAnim", 0, 0);
        }
        StartCoroutine("WaitForAnim");
    }
    public void CameraSwitch(){
        whiteStatic.SetActive(true);
        PFXObj.SetActive(true);
        blockerObj.SetActive(true);
        if(!camUp){
            camSwitchSound.Play();
            camUp = true;
            camUI.SetActive(true);
            playerCam.GetComponent<PlayerMovement>().FlashOff();
            playerCam.SetActive(false);
            cameras[prevCam].SetActive(true);
            
            energyLevel.usage++;
            energyLevel.UpdateUsageSlider();

            globalStatic.SetActive(true);

            player.flashIsOn = false;
        }else{
            PFXObj.SetActive(false);
            camUp = false;
            camUI.SetActive(false);
            playerCam.SetActive(true);
            cameras[prevCam].SetActive(false);

            energyLevel.usage--;
            energyLevel.UpdateUsageSlider();

            camObjAnim.Play("CamCloseAnim", 0, 0);
            globalStatic.SetActive(false);
            blockerObj.SetActive(true);
        }
        StartCoroutine("ResetAnim");
    }

    public void CameraChange(int camNum){
        camSwitchSound.Play();
        whiteStatic.SetActive(true);
        cameras[prevCam].SetActive(false);
        cameras[camNum].SetActive(true);
        prevCam = camNum;
        StartCoroutine("ResetAnim");
    }

    IEnumerator WaitForAnim(){
        yield return new WaitForSecondsRealtime(0.2f);
        CameraSwitch();
    }

    IEnumerator ResetAnim(){
        yield return new WaitForSecondsRealtime(0.3f);
        whiteStatic.SetActive(false);
    }
}