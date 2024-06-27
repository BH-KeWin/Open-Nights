using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHolder : MonoBehaviour
{
    [SerializeField]AudioSource openSound, closeSound;
    [SerializeField]Material buttonActiveMat, buttonInactiveMat;
    [SerializeField]EnergyLevel energyLevel;
    public Animator doorAnim;
    public bool closed = false;

    public void ToggleDoor(){
        if(doorAnim.GetCurrentAnimatorStateInfo(0).IsName("DoorIdle")){
            if(closed){
                doorAnim.SetTrigger("OpenDoor");
                doorAnim.ResetTrigger("CloseDoor");
                closed = false;
                energyLevel.usage--;
                energyLevel.UpdateUsageSlider();

                Material[] materials = GetComponent<MeshRenderer>().materials;
                materials[2] = buttonInactiveMat;
                GetComponent<MeshRenderer>().materials = materials;
                openSound.Play();
            }else{
                if(energyLevel.energy > 0){
                    doorAnim.SetTrigger("CloseDoor");
                    doorAnim.ResetTrigger("OpenDoor");
                    closed = true;
                    energyLevel.usage++;
                    energyLevel.UpdateUsageSlider();

                    Material[] materials = GetComponent<MeshRenderer>().materials;
                    materials[2] = buttonActiveMat;
                    GetComponent<MeshRenderer>().materials = materials;
                    closeSound.Play();
                }
            }
        }
    }

    public void PowerOff(){
        if(closed){
            doorAnim.SetTrigger("OpenDoor");
            doorAnim.ResetTrigger("CloseDoor");
        }
    }
}
