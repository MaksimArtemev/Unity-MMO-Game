using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamonds : MonoBehaviour
{
    //character and diamon collision detection
    private void OnTriggerEnter(Collider other) 
    {
        PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();

        if(playerInventory != null)
        {
            playerInventory.DiamondsCollected();
            gameObject.SetActive(false);
        }
    }
}
