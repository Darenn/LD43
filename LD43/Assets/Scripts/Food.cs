using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{

    public int FoodAmount;

    public Sprite InventorySprite;

    public enum FoodType
    {
        notype,
        fruit1,
        fruit2,
        fruit3,
        meat,
        bigMeat
    };

    public FoodType foodType;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
