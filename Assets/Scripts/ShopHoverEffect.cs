using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;
using System;

public class ShopHoverEffect : MonoBehaviour
{
    [SerializeField]String itemNameStr, itemDescStr, itemCostStr;
    [SerializeField]TextMeshProUGUI itemName, itemDescription, itemCost;
    [SerializeField]Animator animator;
    [SerializeField]GameObject costBG;
    bool anim = false;

    void OnMouseOver(){
        if(!anim){
            animator.Play("Hover", 0, 0);
            anim = true;
        }

        costBG.SetActive(true);
        itemCost.SetText(itemCostStr);
        itemName.SetText(itemNameStr);
        itemDescription.SetText(itemDescStr);
    }

    public void OnMouseExit(){
        anim = false;
        animator.Play("UnHover", 0, 0);

        costBG.SetActive(false);
        itemCost.SetText("");
        itemName.SetText("");
        itemDescription.SetText("");
    }
}
