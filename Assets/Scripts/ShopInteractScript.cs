using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class ShopInteractScript : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI coinText;

    void Start(){
        UpdateCoinText();
    }

    void Update(){
        if(Input.GetMouseButtonDown(0)){
            RaycastHit rayHit;
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit, 1000)){
                Interact(rayHit);
            }
        }
    }

    void Interact(RaycastHit rayHitObject){
        switch(rayHitObject.transform.gameObject.name){

            //When you click on the drink, it checks if you have more than 30 coins, and if yeah, it removes 30 coins and deletes the item.
            case "DrinkHolder":
                if(PlayerPrefs.GetInt("CogCoins",  0) >= 30){

                    rayHitObject.transform.GetComponent<ShopHoverEffect>().OnMouseExit();
                    int coins = PlayerPrefs.GetInt("CogCoins", 0);
                    PlayerPrefs.SetInt("CogCoins",  coins -= 30);

                    UpdateCoinText();
                    Object.Destroy(rayHitObject.transform.gameObject);
                    PlayerPrefs.SetString("Drink",  "active");
                }
            break;

            //Same but for gen
            case "GenHolder":
                if(PlayerPrefs.GetInt("CogCoins",  0) >= 15){
                    int coins = PlayerPrefs.GetInt("CogCoins", 0);
                    PlayerPrefs.SetInt("CogCoins",  coins -= 15);

                    UpdateCoinText();
                    Object.Destroy(rayHitObject.transform.gameObject);
                    PlayerPrefs.SetString("Generator",  "active");
                }
            break;

            //Same but for ice
            case "IceHolder":
                if(PlayerPrefs.GetInt("CogCoins",  0) >= 45){
                    int coins = PlayerPrefs.GetInt("CogCoins", 0);
                    PlayerPrefs.SetInt("CogCoins",  coins -= 45);

                    UpdateCoinText();
                    Object.Destroy(rayHitObject.transform.gameObject);
                    PlayerPrefs.SetString("Ice",  "active");
                }
            break;
        }
    }

    void UpdateCoinText(){
        coinText.SetText(PlayerPrefs.GetInt("CogCoins",  0).ToString());
    }

}
