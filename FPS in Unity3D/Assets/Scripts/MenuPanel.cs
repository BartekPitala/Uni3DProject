using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanel : MonoBehaviour {

	public GameObject menuPanel, menuButton;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Pause(){
		menuPanel.SetActive(true);
	}

	public void UnPause(){
		menuPanel.SetActive(false);
	}
}
