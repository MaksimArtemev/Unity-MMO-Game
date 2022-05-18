using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    private TextMeshProUGUI collectibleText;
    // Start is called before the first frame update
    void Start()
    {
        collectibleText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    public void UpdateDiamondText(PlayerInventory playerInventory)
    {
        collectibleText.text = playerInventory.NumberOfDiamonds.ToString();
    }
}
