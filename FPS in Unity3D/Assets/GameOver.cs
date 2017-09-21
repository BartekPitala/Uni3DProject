using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {

	public AudioClip buttonOnClickSound;
    public Texture GameOverTexture;
	public float buttonWidth = 50;
    public float buttonHeight = 100;
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    private GUIStyle mainStyle = new GUIStyle();
    public Font mainFont;

    void OnGUI(){
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), GameOverTexture);
        
        if (GUI.Button(new Rect(Screen.width*0.4f-buttonWidth / 2.0f, Screen.height*0.6f, buttonWidth, buttonHeight), "PLAY AGAIN", mainStyle))
        {
        	GetComponent<AudioSource>().clip = buttonOnClickSound;
            GetComponent<AudioSource>().Play();
            Application.LoadLevel("scene1");
        }
        if (GUI.Button(new Rect(Screen.width * 0.6f-buttonWidth / 2.0f, Screen.height * 0.6f, buttonWidth, buttonHeight), "MAIN MENU", mainStyle))
        {
        	GetComponent<AudioSource>().clip = buttonOnClickSound;
            GetComponent<AudioSource>().Play();
            Application.LoadLevel("Menu");
        }


    }

	 void Start()
    {
        buttonWidth = Screen.width/7.3f;
        buttonHeight = Screen.height/7.3f;
        Cursor.visible = true;
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        mainStyle.font = mainFont;
        mainStyle.fontSize = 20;
        Screen.lockCursor = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
