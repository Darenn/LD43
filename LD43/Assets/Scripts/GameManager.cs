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
    public bool NoEnduranceLoss = false;
    public FirstPersonController fpsctrl;


    public bool isGameOver = false;

    public void StartGame()
    {
        fpsctrl.enabled = true;
        cabane.enabled = true;
    }
	
	void Update ()
	{
	    Timer += Time.deltaTime;
	    uiManager.Timer = Timer;
	    if (!fpsctrl.m_IsWalking && !NoEnduranceLoss)
	    {
	        Endurance = Mathf.Clamp(Endurance - EnduranceLossPerSecond * Time.deltaTime, 0, 100);
	    }
	    else
	    {
	        Endurance = Mathf.Clamp(Endurance + EnduranceWonPerSecond * Time.deltaTime, 0, 100);
	    }
	    uiManager.Endurance = Endurance;
	    if (Endurance <= 0)
	    {
	        fpsctrl.blockRunning = true;
	    }

	    else if(Endurance >= EnduranceLossPerSecond)
	    {
	        fpsctrl.blockRunning = false;
	    }

        if (cabane.Food <= 0 && !isGameOver)
	    {
	        isGameOver = true;
	        uiManager.ShowMessage("Your children starved...", 5);
	    }
	}
}
