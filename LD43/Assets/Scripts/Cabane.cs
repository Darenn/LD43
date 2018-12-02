using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabane : MonoBehaviour
{
    public float Food;
    public float MaxFood;

    public float FoodLossPerSecond;

    public float foodMultiplier;
    public Food.FoodType foodToMultiply;
	
	void Update ()
	{
	    Food -= FoodLossPerSecond * Time.deltaTime;
	}

    public void Feed(Food foodObject)
    {
        float amount = foodObject.FoodAmount;
        if (foodObject.foodType == foodToMultiply) amount *= foodMultiplier;
        Food = Mathf.Clamp(Food + Mathf.Abs(amount), 0, MaxFood);
    }
}
