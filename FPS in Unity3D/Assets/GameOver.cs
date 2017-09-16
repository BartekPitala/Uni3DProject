using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {


    public Texture GameOverTexture;
	// Use this for initialization

	void OnGUI(){
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), GameOverTexture);
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
