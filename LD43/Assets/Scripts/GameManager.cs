using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float Timer = 0;
    public UIManager uiManager;
    public Cabane cabane;
	
	void Update ()
	{
	    Timer += Time.deltaTime;
	    uiManager.Timer = Timer;

	    if (cabane.Food <= 0)
	    {
	        uiManager.ShowMessage("Your children starved...", 5);
	    }
	}
}
