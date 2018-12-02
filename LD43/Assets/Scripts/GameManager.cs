using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManager : MonoBehaviour
{
    public float Timer = 0;
    public UIManager uiManager;
    public Cabane cabane;

    public float Endurance = 100;
    public float EnduranceLossPerSecond;
    public float EnduranceWonPerSecond;
    public FirstPersonController fpsctrl;

    public bool isGameOver = false;
	
	void Update ()
	{
	    Timer += Time.deltaTime;
	    uiManager.Timer = Timer;
	    if (!fpsctrl.m_IsWalking)
	    {
	        Endurance = Mathf.Clamp(Endurance - EnduranceLossPerSecond * Time.deltaTime, 0, 100);
	    }
	    else
	    {
	        Endurance = Mathf.Clamp(Endurance + EnduranceWonPerSecond * Time.deltaTime, 0, 100);
	    }
	    uiManager.Endurance = Endurance;

        if (cabane.Food <= 0 && !isGameOver)
	    {
	        isGameOver = true;
	        uiManager.ShowMessage("Your children starved...", 5);
	    }
	}
}
