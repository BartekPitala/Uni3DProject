using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public AudioClip buttonOnClickSound;
    public Texture GameLogo;
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    public float buttonWidth = 500;
    public float buttonHeight = 1000;
    private float buttonMargin = 20;
    public GUISkin skin;
    public GUIStyle disabledButton;
    private GUIStyle mainStyle = new GUIStyle();
    public Font mainFont;


    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), GameLogo);



        GUI.BeginGroup(new Rect(Screen.width/2-buttonWidth/2, Screen.height/2.2f, buttonWidth, (buttonHeight + buttonMargin) * 3));
        if (GUI.Button(new Rect(0, 0, buttonWidth, buttonHeight), "NEW GAME", mainStyle))
        {
            GetComponent<AudioSource>().clip = buttonOnClickSound;
            GetComponent<AudioSource>().Play();
            Application.LoadLevel("scene1");
        }
        if (GUI.Button(new Rect(0, 0 + buttonHeight + buttonMargin, buttonWidth, buttonHeight), "ABOUT", mainStyle))
        {
            GetComponent<AudioSource>().clip = buttonOnClickSound;
            GetComponent<AudioSource>().Play();
            Application.LoadLevel("AboutScene");
        }
        if (GUI.Button(new Rect(0, 0 + (buttonHeight + buttonMargin) * 2, buttonWidth, buttonHeight), "EXIT", mainStyle))
        {
            GetComponent<AudioSource>().clip = buttonOnClickSound;
            GetComponent<AudioSource>().Play();
            Application.Quit();
        }
        GUI.EndGroup();
        GUI.skin = skin;

    }
    // Use this fo   r initialization

    void Start()
    {
        buttonWidth = (buttonWidth * Screen.width) / 1920;
        buttonHeight = (buttonHeight * Screen.height) / 1080;
        buttonMargin = (buttonMargin * Screen.height) / 300;
        Cursor.visible = true;
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        mainStyle.font = mainFont;
        mainStyle.fontSize = 20;
        Screen.lockCursor = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}






