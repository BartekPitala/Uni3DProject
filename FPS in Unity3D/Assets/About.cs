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


    void OnGUI()
    {
       // StreamReader reader = new StreamReader("Assets/GameDescription.txt"); 

    	GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), GameLogo);
        GUI.BeginGroup(new Rect(Screen.width/2-buttonWidth/2, Screen.height/3f, buttonWidth, (buttonHeight + buttonMargin) * 2));
        if (GUI.Button(new Rect(0, 0, buttonWidth*1.6f, buttonHeight), "BACK TO MAIN MENU", mainStyle))
        {
            GetComponent<AudioSource>().clip = buttonOnClickSound;
            GetComponent<AudioSource>().Play();
            Application.LoadLevel("Menu");
        }
		if (GUI.Button(new Rect(0, Screen.height/25, 1000, 1000), "WELCOME TO MMP SHOOTER!\nAT EASE, SOLDIER. THIS IS A SHORT INTRODUCITON TO GAME PRINCIPLES AND MECHANICS\nDUE TO AN EXPERIMENT THAT WENT OUT OF CONTROL, THE CITY OF NEW YORK HAS BEEN INFECTED\nWITH A DEADLY DISEASE. WHAT'S WORSE SOME OF THE DECEASED RISE AGAIN AS BLOODTHIRSTY" +
			"\nZOMBIES! WHOEVER COULD, EVACUTAED FROM THE CITY. A QUARANTINE HAS BEEN APPLIED TO\nPREVENT FURTHER SPREAD OF THE DISEASE. THE NEW YORK CITY IS NOW A GHOST TOWN..." +
			"\nBUT THE TIME HAS COME TO RECLAIM THE CROWN JEWEL OF HUMAN CIVILIZATION!\nYOU ARE A MEMBER OR AN ELITE STRIKE FORCE-YOUR JOB IS TO ENTER THE CITY AND EXTERMINATE" +
			"\nTHE UNWELCOME GUESTS. DUE TO AN EXTREME LEVEL OF CONTAMINATION, YOU ARE ONLY ALLOWED" +
			"\nTO STAY IN THE CITYFOR A SHORT PERIOD OF TIME, LIMITED BY YOUR GAS MASK CANISTER LIFETIME." +
			"\nTHIS VALUE IS REPRESENTED ON YOUR VISOR DISPLAY AS \"CANISTER LIFETIME\". WHEN IT REACHES" +
			"\nZERO, YOU ARE DEAD,BOY! YOU MUST KILL EM ALL BEFORE YOU CAN THINK ABOUT LEAVING THE ZONE." +
			"\nTHEREFORE, I STRONGLY ADVISE YOU NOT TO WASTE TIME ON SIGHTSEEING, AND FOCUS ON KILLING" +
			"\nTHESE FILTHY BEASTS! BEWARE THEIR CLAWS,THOUGH. THEY CAN EASILY PUNCTURE YOUR ARMOUR" +
			"\nIF YOU RIN LOW ON AMMO, SEARCH FOR CANISTERS LEFT OVER BY PREVIOUS SQUADS. YOU MAY" +
			"\nALSO FIND SOME WEAPONS. WHEN YOU GET HIT, SEARCH FOR A FIRST AID KIT. TRY TO KILL THE" +
			"\nZOMBIES AS FAST AS YOU CAN. WE WILL RECORD YOUR TIME. CAN YOU BEAT IT IN THE NEXT RUN?" +
			"\nNOW, GET OUT OF HERE AND KILL SOME ZOMBIES GOOD LUCK!!!", mainStyle))
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
        mainStyle.fontSize = 13;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
