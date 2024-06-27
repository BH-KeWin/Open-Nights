using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightHitbox : MonoBehaviour
{
    [SerializeField]PlayerMovement player;
    bool inLight = false;
    int fillMeter;
    [SerializeField]AnimatronicAI affectedAnimatronic;

    void OnTriggerEnter(Collider col){
        if(col.gameObject.tag == "LightTag"){
            StopCoroutine("InLight");
            inLight = true;
            fillMeter = 0;
            StartCoroutine("InLight");
        }
    }

    IEnumerator InLight(){
        if(inLight){
            if(player.flashIsOn){
                fillMeter++;

                if(fillMeter >= 3 && inLight){
                    affectedAnimatronic.ResetAnimatronic();
                }
                yield return new WaitForSecondsRealtime(0.85f);
                StartCoroutine("InLight");
            }
        }else{
            fillMeter = 0;
            inLight = false;
            Debug.Log("lightOff");
            StopCoroutine("InLight");
        }
    }
}