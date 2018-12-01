using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Image FoodBar;
    public Cabane Cabane;

    public Image[] InventorySlots;
    public Image[] InventorySelectors;
    [SerializeField] private Inventory inventory;
	
	// Update is called once per frame
	void Update ()
	{
	    FoodBar.fillAmount = Cabane.Food / Cabane.MaxFood;

	    for (int i = 0; i < inventory.Objects.Length; i++)
	    {
	        GameObject obj = inventory.Objects[i];
	        if (obj != null)
	        {
	            InventorySlots[i].sprite = obj.GetComponent<Food>().InventorySprite;
	        }
	        else
	        {
	            InventorySlots[i].sprite = null;
	        }
	    }

        for (int i = 0; i < InventorySelectors.Length; i++)
	    {
	        InventorySelectors[i].enabled = false;
	    }
	    InventorySelectors[inventory.CurrentSlot].enabled = true;
	}
}
