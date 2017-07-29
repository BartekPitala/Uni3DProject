using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public Texture GameLogo;
    public float buttonWidth = 500;
    public float buttonHeight = 1000;
    private float buttonMargin = 20;
    public GUISkin skin;
    public GUIStyle disabledButton;


    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, 800, 300), GameLogo);



        GUI.BeginGroup(new Rect(Screen.width/10, Screen.height/1.6f, buttonWidth, (buttonHeight + buttonMargin) * 3));
        if (GUI.Button(new Rect(0, 0, buttonWidth, buttonHeight), "New Game"))
        {
            //SceneManager.LoadScene("scene1");
            Application.LoadLevel("scene1");
        }
        if (GUI.Button(new Rect(0, 0 + buttonHeight + buttonMargin, buttonWidth, buttonHeight), "Options"))//, disabledButton))
        {

        }
        if (GUI.Button(new Rect(0, 0 + (buttonHeight + buttonMargin) * 2, buttonWidth, buttonHeight), "Exit"))
        {
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
        buttonMargin = (buttonMargin * Screen.height) / 1080;
    }

    // Update is called once per frame
    void Update()
    {

    }
}






