using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

	[SerializeField] private Animator m_animator;

	void Start () {
		
	}
	
	void Update () {
		
	}

	void PressPlay() {
		m_animator.SetTrigger("PressPlay");
	}
}
