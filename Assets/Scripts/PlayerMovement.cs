using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]TimePass timePass;
    [SerializeField]CameraSystem camSys;
    [SerializeField]TextMeshProUGUI loaderText;
    [SerializeField]AudioSource jumpscareSound, squeakSound, flashOn, flashOff;
    [SerializeField]EnergyLevel energyLevel;
    [SerializeField]GameObject jumpscareObjects, playerUI, goObject, flashlight, hallwayBlockers;
    [SerializeField]Camera playerCam;
    [SerializeField]float speed = 13;
    [SerializeField]float maxRot, minRot;

    bool holdingEsc;
    public bool alive = true, flashIsOn;

    void FixedUpdate(){
        if(Input.mousePosition.x >= Screen.width - Screen.width / 4){
            if(transform.localEulerAngles.y < maxRot){
                transform.Rotate(Vector3.up, speed * Time.deltaTime);
            }
        }
        else if(Input.mousePosition.x <= Screen.width - (Screen.width / 4*3)){
            if(transform.localEulerAngles.y > minRot){
                transform.Rotate(Vector3.up, -speed * Time.deltaTime);
            }
        }

        if(Input.GetKey(KeyCode.Escape)){
            if(!holdingEsc){
                holdingEsc = true;
                loaderText.SetText("Main Menu");
                StartCoroutine("QuitToMenu");
            }
        }else{
            StopCoroutine("QuitToMenu");
            holdingEsc = false;
        }
    }

    void Update(){
        if(Input.GetMouseButtonDown(0)){
            RaycastHit rayHit;
            if(Physics.Raycast(playerCam.ScreenPointToRay(Input.mousePosition), out rayHit, 1000)){
                Interact(rayHit);
            }
        }

        if(Input.GetKeyDown(KeyCode.LeftControl)){
            if(energyLevel.energy > 0){
                flashIsOn = true;
                flashlight.SetActive(true);
                hallwayBlockers.SetActive(false);
                energyLevel.usage++;
                energyLevel.UpdateUsageSlider();
                flashOn.Play();
            }
        }

        if(Input.GetKeyUp(KeyCode.LeftControl)){
            if(flashIsOn){
                flashIsOn = false;
                flashlight.SetActive(false);

                if(energyLevel.energy > 0){
                    hallwayBlockers.SetActive(true);
                    energyLevel.usage--;
                    energyLevel.UpdateUsageSlider();
                    flashOff.Play();
                }
            }
        }
    }

    public void FlashOff(){
        if(flashlight.activeSelf){
            flashlight.SetActive(false);
            energyLevel.usage--;
            energyLevel.UpdateUsageSlider();
        }
    }

    void Interact(RaycastHit rayHitObject){
        switch(rayHitObject.transform.gameObject.name){
            case "Button":
                rayHitObject.transform.gameObject.GetComponent<DoorHolder>().ToggleDoor();
            break;

            case "PeterPlushie":
                rayHitObject.transform.gameObject.GetComponent<Animator>().Play("Squeak", 0, 0);
                squeakSound.Play();
            break;
        }
    }

    public void GameOver(GameObject jsModel){
        if(alive){
            timePass.enabled = false;
            alive = false;

            jsModel.SetActive(true);
            jumpscareObjects.SetActive(true);
            playerUI.SetActive(false);
            playerCam.enabled = false;
            GetComponent<AudioListener>().enabled = false;

            jumpscareSound.Play();
            StartCoroutine("GameOverScreen");
        }
    }

    IEnumerator QuitToMenu(){
        yield return new WaitForSecondsRealtime(3f);
        if(holdingEsc){
            FindFirstObjectByType(typeof(LoadingScreen)).GetComponent<LoadingScreen>().LoadScene("MainMenu");
        }
    }

    IEnumerator GameOverScreen(){
        yield return new WaitForSecondsRealtime(0.35f);
        goObject.SetActive(true);

        this.gameObject.SetActive(false);
    }
}
