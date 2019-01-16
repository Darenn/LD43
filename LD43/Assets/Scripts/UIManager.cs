using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Image FoodBar;
    public Cabane Cabane;

    public Image[] InventorySlots;
    public Image[] InventorySelectors;
    [SerializeField] private Inventory inventory;

    public Text TimerText;

    public Image EnduranceBar;

    public float Timer; // modified by GameManager
    public float Endurance;

    public Text EventText;
    public EventManager em;
    public Image EventSprite;
    public Image EventTimer;

    void Awake()
    {
        EventText.text = "";
    }
	
	// Update is called once per frame
	void Update ()
	{
	    FoodBar.fillAmount = Cabane.Food / Cabane.MaxFood;
	    EnduranceBar.fillAmount = Endurance / 100;

        for (int i = 0; i < inventory.Objects.Length; i++)
	    {
	        GameObject obj = inventory.Objects[i];
	        if (obj != null)
	        {
	            InventorySlots[i].sprite = obj.GetComponent<GrabbableObject>().InventorySprite;
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

	    string min = (int) (Timer / 60f) + "";
	    if ((Timer / 60f) < 10) min = "0" + min;
	    string sec = (int) Timer % 60f + "";
	    if ((Timer % 60f) < 10) sec = "0" + sec;
        TimerText.text =  min + ":" + sec;

	    //EventSprite.sprite = em.currentSprite;
	    EventTimer.fillAmount = em.timer / em.maxTimer;
	}

    public void ShowMessage(string message, float time)
    {
        StartCoroutine(StartShowMessage(message, time));
    }

    IEnumerator StartShowMessage(string message, float time)
    {
        EventText.text = message;
        yield return new WaitForSeconds(time);
        if (EventText.text == message)
            EventText.text = "";
    }
}
