using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pOnObject : MonoBehaviour
{
    [SerializeField]PlayerMovement player;
    [SerializeField]GameObject jsModel, lightObj;

    void Start(){
        StartCoroutine("beginAnim");
    }

    IEnumerator beginAnim(){
        yield return new WaitForSecondsRealtime(Random.Range(8, 12));
        lightObj.SetActive(false);
        yield return new WaitForSecondsRealtime(Random.Range(4, 6));
        player.GameOver(jsModel);
    }
}
