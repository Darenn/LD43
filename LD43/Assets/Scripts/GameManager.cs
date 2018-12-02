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
	        Endurance -= EnduranceLossPerSecond * Time.deltaTime;
	    }
	    else
	    {
	        Endurance += EnduranceWonPerSecond * Time.deltaTime;
        }
	    uiManager.Endurance = Endurance;

        if (cabane.Food <= 0 && !isGameOver)
	    {
	        isGameOver = true;
	        uiManager.ShowMessage("Your children starved...", 5);
	    }
	}
}
