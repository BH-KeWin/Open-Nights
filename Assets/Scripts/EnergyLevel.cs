using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnergyLevel : MonoBehaviour
{
    [SerializeField]AudioSource powerOut;
    [SerializeField]GameObject pOnObject;
    [SerializeField]DoorHolder[] doorHolder;
    [SerializeField]GameObject[] turnOffObjects, turnOnObjects;
    [SerializeField]Slider usageSlider;
    [SerializeField]Image energyFill;
    public int energy = 100;

    public int usage = 1;
    float extraItem = 1;

    void Start(){
        UpdateUsageSlider();
        StartCoroutine("DecreaseEnergy");

        if(PlayerPrefs.GetString("Generator") == "active"){
            PlayerPrefs.SetString("Generator",  "not active");
            extraItem = 1.1f;
        }
    }

    public void UpdateUsageSlider(){
        usageSlider.value = usage;
    }

    IEnumerator DecreaseEnergy(){
        switch(usage){
            case 0:
            yield return new WaitForSecondsRealtime(13.8f * extraItem);
            break;
            
            case 1:
            yield return new WaitForSecondsRealtime(8.94f * extraItem);
            break;

            case 2:
            //yield return new WaitForSecondsRealtime(0.05f * extraItem);
            yield return new WaitForSecondsRealtime(7.6f * extraItem);
            break;

            case 3:
            yield return new WaitForSecondsRealtime(4.6f * extraItem);
            break;

            case 4:
            yield return new WaitForSecondsRealtime(3.6f * extraItem);
            break;

            case 5:
            yield return new WaitForSecondsRealtime(2.1f * extraItem);
            break;

            case 6:
            yield return new WaitForSecondsRealtime(0.65f * extraItem);
            break;
        }
        
        energy--;
        energyFill.fillAmount = (float)energy / 100f;

        if(energy > 0){
            StartCoroutine("DecreaseEnergy");
        }else{
            StartCoroutine("PowerOut");
        }    
    }

    IEnumerator PowerOut(){
        powerOut.Play();
        for(int x = 0; x < turnOffObjects.Length; x++){
            turnOffObjects[x].SetActive(false);
        }

        for(int y = 0; y < doorHolder.Length; y++){
            doorHolder[y].PowerOff();
        }

        for(int z = 0; z < turnOnObjects.Length; z++){
            turnOnObjects[z].SetActive(true);
        }
        
        yield return new WaitForSecondsRealtime(Random.Range(8, 12));
        pOnObject.SetActive(true);

    }
}
