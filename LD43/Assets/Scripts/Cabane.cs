using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cabane : MonoBehaviour
{
    public float Food;
    public float MaxFood;

    public float FoodLossPerSecond;
    public UIManager uimanager;

    public List<Food> foods;
    public List<String> texts;

    public Food currentFood()
    {
        return foods[count];
    }

    private int count = 0;

    void Start()
    {
        uimanager.ShowMessage(texts[count], 6);
        uimanager.EventSprite.sprite = foods[count].InventorySprite;
    }

    void Update ()
	{
	    Food -= FoodLossPerSecond * Time.deltaTime;
        if (Food <= 0) Lost();
	}

    public void Feed(Food foodObject)
    {
        /*float amount = foodObject.FoodAmount;
        if (foodObject.foodType == foodToMultiply) amount *= foodMultiplier;
        Food = Mathf.Clamp(Food + Mathf.Abs(amount), 0, MaxFood);*/
        Food = MaxFood;
        count++;
        if (count >= texts.Count)
        {
            Win();
            return;
        }
        uimanager.ShowMessage(texts[count], 6);
        uimanager.EventSprite.sprite = foods[count].InventorySprite;

    }

    public void Win()
    {
        uimanager.ShowMessage("You sacrificed everything for your children.", 10);
        Invoke("loadMenu", 5);
    }

    private bool lost = false;

    public void Lost()
    {
        if (lost) return;
        lost = true;
        uimanager.ShowMessage("You sacrificed your children.", 10);
        Invoke("loadMenu", 5);
    }

    public void loadMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
