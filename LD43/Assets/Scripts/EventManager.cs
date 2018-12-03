using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class EventManager : MonoBehaviour
{

    public UIManager uimanager;
    public float TimeBetweenEvents;

    private List<Action> events;
    [SerializeField] private Food[] food;

    public float timer;
    public float maxTimer;
    public Sprite currentSprite;

    public bool launchingEventing = false;

    public bool boostSprint = false;
    public bool noSprint = false;

    public FirstPersonController fpsc;
    public Sprite sprintBonusSprite;
    public Sprite noSprintSprite;

    public Cabane cabane;
    public GameManager gm;

    void Awake () {
        events = new List<Action>();
        events.Add(eventNeedFoodx2);
        events.Add(eventNeedFoodx2);
        events.Add(eventRejectFoodDivBy2);
        //events.Add(cantSprint);
        events.Add(infiniteSprint);
    }
	
	void Update ()
	{
	    timer = Mathf.Clamp(timer - Time.deltaTime, 0, 100);
	    if (timer <= 0 && !launchingEventing)
        {
            resetToDefault();
	        StartCoroutine(WaitAndThrowEvent());
        }
	}

    IEnumerator WaitAndThrowEvent()
    {
        launchingEventing = true;
        yield return new WaitForSeconds(TimeBetweenEvents);
        ThrowRandomEvent();
    }

    void resetToDefault()
    {
        setTimer(0);
        launchingEventing = false;
        currentSprite = null;
    }

    void ThrowRandomEvent()
    {       
        events[UnityEngine.Random.Range(0, events.Count)]();
    }

    void eventNeedFoodx2()
    {
        float t = 30;
        setTimer(t);
        Food f = food[UnityEngine.Random.Range(0, food.Length)];
        currentSprite = f.InventorySprite;
        cabane.foodMultiplier = 20;
        cabane.foodToMultiply = f.foodType;
        StartCoroutine(coNeedFood(t));
    }

    IEnumerator coNeedFood(float time)
    {
        yield return new WaitForSeconds(time);
        resetToDefault();
        cabane.foodMultiplier = 1;
        cabane.foodToMultiply = Food.FoodType.notype;
    }

    void eventRejectFoodDivBy2()
    {
        float t = 30;
        setTimer(t);
        Food f = food[UnityEngine.Random.Range(0, food.Length)];
        currentSprite = f.InventorySprite;
        cabane.foodMultiplier = 0.5f;
        cabane.foodToMultiply = f.foodType;
        StartCoroutine(coRejectFood(t));
    }

    IEnumerator coRejectFood(float time)
    {
        yield return new WaitForSeconds(time);
        resetToDefault();
        cabane.foodMultiplier = 1;
        cabane.foodToMultiply = Food.FoodType.notype;
    }

    void cantSprint()
    {
        float t = 5;
        setTimer(t);
        noSprint = true;
        fpsc.blockRunning = true;
        StartCoroutine(coCantSprint(t));
    }

    IEnumerator coCantSprint(float time)
    {
        yield return new WaitForSeconds(time);
        resetToDefault();
        noSprint = false;
        fpsc.blockRunning = false;
    }

    void infiniteSprint()
    {
        float t = 5;
        setTimer(t);
        boostSprint = true;
        gm.NoEnduranceLoss = true;
        StartCoroutine(coInfiniteSprint(t));
    }

    IEnumerator coInfiniteSprint(float time)
    {
        yield return new WaitForSeconds(time);
        resetToDefault();
        boostSprint = false;
        gm.NoEnduranceLoss = false;
    }

    void fog()
    {
        
    }

    void setTimer(float time)
    {
        timer = maxTimer = time;
    }
}
