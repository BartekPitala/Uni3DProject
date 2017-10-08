using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class About : MonoBehaviour {

    public Texture GameLogo;
    public AudioClip buttonOnClickSound;
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    private GUIStyle mainStyle = new GUIStyle();
    public Font mainFont;
    public Vector2 hotSpot = Vector2.zero;
    public float buttonWidth = 500;
    public float buttonHeight = 1000;
    private float buttonMargin = 20;
    public GUISkin skin;
    public Text gameDescription;


	int calc_font_size()
	{
		int font_size = Screen.width / 120;
		return font_size;
	}

    void OnGUI()
    {
		StreamReader reader = new StreamReader("Assets/about_text.txt"); 
		string text = reader.ReadToEnd ();

    	GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), GameLogo);
        GUI.BeginGroup(new Rect(Screen.width/2-buttonWidth/2, Screen.height/3f, buttonWidth, (buttonHeight + buttonMargin) * 2));
        if (GUI.Button(new Rect(0, 0, buttonWidth*1.6f, buttonHeight), "BACK TO MAIN MENU", mainStyle))
        {
            GetComponent<AudioSource>().clip = buttonOnClickSound;
            GetComponent<AudioSource>().Play();
            Application.LoadLevel("Menu");
        }
		if (GUI.Button(new Rect(0, Screen.height/25, (Screen.width - 200), (Screen.height - 200)), text, mainStyle))
        {
        }
      
        GUI.EndGroup();
        GUI.skin = skin;

    }

	// Use this for initialization
	void Start () {
		buttonWidth = (buttonWidth * Screen.width) / 1920;
        buttonHeight = (buttonHeight * Screen.height) / 1080;
        buttonMargin = (buttonMargin * Screen.height) / 300;
        Cursor.visible = true;
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        mainStyle.font = mainFont;
		mainStyle.fontSize = calc_font_size ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
