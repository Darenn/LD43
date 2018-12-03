using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MenuManager : MonoBehaviour {

	[SerializeField] private Animator m_animator;
    public GameObject Player;
    public Transform PlayerSpawn;
    public GameManager gm;

    public CanvasGroup HUD;
    public CanvasGroup Menu;

    public float InGameAnimationTime;
    public float MenuFadeInTime;
    public float TimeToDisplayHUD;
    public float HUDFadeOutTime;

    void Start () {
		
	}
	
	void Update () {
		
	}



	public void PressPlay()
	{
	    Player.transform.DOMove(PlayerSpawn.position, InGameAnimationTime);
	    Player.transform.DORotateQuaternion(PlayerSpawn.rotation, InGameAnimationTime);
	    DOTween.To(() => Menu.alpha, x => Menu.alpha = x, 0, MenuFadeInTime);
	    Menu.interactable = false;
        Invoke("StartGame", InGameAnimationTime);
	    Invoke("ShowHUD", TimeToDisplayHUD);
	}

    public void StartGame()
    {
        gm.StartGame();        
    }

    public void ShowHUD()
    {
        DOTween.To(() => HUD.alpha, x => HUD.alpha = x, 1, HUDFadeOutTime);
    }
}
