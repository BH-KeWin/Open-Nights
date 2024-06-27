using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JumpscareRandomly : MonoBehaviour
{
    [SerializeField]PlayerMovement player;
    [SerializeField]GameObject jsModel;
    void OnEnable(){
        StartCoroutine("JSRandom");
    }


    IEnumerator JSRandom(){
        yield return new WaitForSecondsRealtime(Random.Range(8, 15));
        GetComponent<AudioSource>().Stop();
        player.GameOver(jsModel);
    }
}
