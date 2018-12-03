using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Cameras;

public class Food : MonoBehaviour
{

    public int FoodAmount;
    public float TimeToRott;

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

    private bool dropped = false;
    public bool isGrabbed = false;
    private Coroutine c;

    public void droppedOnFloor()
    {
        isGrabbed = false;
        dropped = true;
        c = StartCoroutine(rott());
    }

    public void grabbed()
    {
        isGrabbed = true;
        dropped = false;
        if (c != null)
            StopCoroutine(c);
    }

    IEnumerator rott()
    {
        yield return new WaitForSeconds(TimeToRott);
        if (dropped)
        {
            Destroy(gameObject);
        }
    }
}
