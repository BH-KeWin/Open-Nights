using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public void MouseOver(){
        int coins = PlayerPrefs.GetInt("CogCoins", 0);
        coins++;
        PlayerPrefs.SetInt("CogCoins",  coins);
        GetComponent<AudioSource>().Play();
        transform.localScale = new Vector3(0, 0, 0);
        Object.Destroy(this.gameObject, 0.1f);
    }
}
