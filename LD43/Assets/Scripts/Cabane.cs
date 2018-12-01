using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabane : MonoBehaviour
{
    public float Food;
    public float MaxFood;

    public float FoodLossPerSecond;
	
	void Update ()
	{
	    Food -= FoodLossPerSecond * Time.deltaTime;
	}

    public void Feed(int amount)
    {
        Food = Mathf.Clamp(Food + Mathf.Abs(amount), 0, MaxFood);
    }
}
