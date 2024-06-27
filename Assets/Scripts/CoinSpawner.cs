using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField]GameObject[] canvases;
    [SerializeField]GameObject coinPref;


    void Start(){
        StartCoroutine("SpawnCoins");
    }

    IEnumerator SpawnCoins(){
        //yield return new WaitForSecondsRealtime(0.01f);
        yield return new WaitForSecondsRealtime(Random.Range(5,10));
        Vector3 randomPos = new Vector3(Random.Range(-640.32f, 384.33f), Random.Range(-193.45f, 395.98f), 0);
        Transform temp = Instantiate(coinPref, transform.position, Quaternion.identity).transform;
        temp.SetParent(canvases[Random.Range(0, canvases.Length)].transform);
        temp.localPosition = randomPos;
        StartCoroutine("SpawnCoins");
    }
}
