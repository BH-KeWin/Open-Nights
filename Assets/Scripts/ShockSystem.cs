using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockSystem : MonoBehaviour
{
    [SerializeField]AudioSource shockSound;
    [SerializeField]GameObject shockButton, shockUI;
    [SerializeField]EnergyLevel energyLevel;
    [SerializeField]AnimatronicAI affectedAnimatronic;
    public void Shocked(){
        shockButton.SetActive(false);
        StartCoroutine("ShockAnim");
    }

    IEnumerator ShockAnim(){
        if(affectedAnimatronic.stage <= 3){
            affectedAnimatronic.ResetAnimatronic();
        }

        shockSound.Play();
        shockUI.SetActive(true);
        energyLevel.usage++;
        energyLevel.energy -= 4;
        energyLevel.UpdateUsageSlider();

        yield return new WaitForSecondsRealtime(0.2f);
        shockUI.SetActive(false);
        yield return new WaitForSecondsRealtime(1.8f);

        energyLevel.usage--;
        energyLevel.UpdateUsageSlider();
        shockButton.SetActive(true);
    }
}
