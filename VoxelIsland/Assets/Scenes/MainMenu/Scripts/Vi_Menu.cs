using UnityEngine;
using System.Collections;

public class Vi_Menu : MonoBehaviour {
	
	public AudioClip AcMouseOver; // Menu Sound
	public AudioClip AcMouseClick;
	static int iButtonWidth = 250;
	static int iButtonHeight = 30;
	static int iTooltipHeight = 60;
	static int iTooltipWidth = 250;
	static float fOffset = 1.1f;
	static float fMenuYPos;
	static float fMenuXPos;

	static bool toggle = false;
	
	private float fSoundVolume;
	
	private Rect rStartGame;
	private Rect rOptions;
	private Rect rAchievements;
	private Rect rProgress;

	public GUI selected;
	
	public bool bAchivements;
	public bool bProgress;
	
	private string sScore;
	
	public Texture2D T2padlockIcon;
	//public Texture2D T2Splash;
	public GUISkin DTH_MenuUISkin;
		
	void Start () {
		
		// Init Sounds
		fSoundVolume = 0.01f * PlayerPrefs.GetFloat("Sound Volume");	
		audio.volume = fSoundVolume;
	}
	
	void Update () {
		sScore = PlayerPrefs.GetString("GameOver");
		if(sScore == "true")
		{
			bProgress = true;
		}
	/*	if(!PlayerPrefs.HasKey("Player")) {	
			Application.LoadLevel("DtH_CreatePlayer");
		}	
		*/
	}

	void OnGUI () {	
		
		Event eMousePosition =  Event.current;
		GUI.depth = -2;
		GUI.skin = DTH_MenuUISkin;
		float scale = Mathf.Max( (float)Screen.width/200, (float)Screen.height/200 );
		float fMenuYPos = Screen.height*0.45f;
		float fMenuXPos = Screen.width*0.5f - iButtonWidth*0.5f;
		
		rStartGame = 		new Rect(fMenuXPos, fMenuYPos + iButtonHeight * 0 * fOffset, iButtonWidth, iButtonHeight);
		rOptions = 			new Rect(fMenuXPos, fMenuYPos + iButtonHeight * 1 * fOffset, iButtonWidth, iButtonHeight);
	 	rAchievements = 	new Rect(fMenuXPos, fMenuYPos + iButtonHeight * 2 * fOffset, iButtonWidth, iButtonHeight);
	 	rProgress = 		new Rect(fMenuXPos, fMenuYPos + iButtonHeight * 3 * fOffset, iButtonWidth, iButtonHeight);
		
		
		if (rStartGame.Contains(eMousePosition.mousePosition) && toggle == false) {
			audio.PlayOneShot(AcMouseOver);
			toggle = true;
		}
		if (rOptions.Contains(eMousePosition.mousePosition) && toggle == false) {
			audio.PlayOneShot(AcMouseOver);
			toggle = true;
		}
		if (rAchievements.Contains(eMousePosition.mousePosition) && bAchivements == true && toggle == false) {
			audio.PlayOneShot(AcMouseOver);
			toggle = true;
		}
		if (rProgress.Contains(eMousePosition.mousePosition) && bProgress == true && toggle == false) {
			audio.PlayOneShot(AcMouseOver);
			toggle = true;
		}
		else if (!rStartGame.Contains(eMousePosition.mousePosition) &&
			!rOptions.Contains(eMousePosition.mousePosition) &&
			!rAchievements.Contains(eMousePosition.mousePosition) &&
			!rProgress.Contains(eMousePosition.mousePosition)) {
			toggle = false;
		}
		
		//GUI.DrawTexture(new Rect(Screen.width*0.5f - 705*0.5f, Screen.height*0.5f  - 447*0.6f, 705, 447), T2Splash);

		if (GUI.Button (rStartGame, new GUIContent ("Start Game", "START A NEW GAME"), "styleMenu")) {
			audio.PlayOneShot(AcMouseClick);
			Application.LoadLevel("Game");
		}
		if (GUI.Button (rOptions , new GUIContent ("Options", "CHANGE YOUR GAME OPTIONS"), "styleMenu")) {
			audio.PlayOneShot(AcMouseClick);
			Application.LoadLevel("DtH_OptionsUI");
		}
		if (GUI.Button (rAchievements, new GUIContent ("Block Sets", "VIEW DIFFERENT BLOCK SETS"), "styleMenu")) {
				audio.PlayOneShot(AcMouseClick);
				Application.LoadLevel("BlockSetViewer");
		}
		if (bProgress == true)
		{
			if (GUI.Button (rProgress, new GUIContent ("Progress", "YOUR CURRENT GAME PROGRESS"), "styleMenu")) {
				audio.PlayOneShot(AcMouseClick);
				Application.LoadLevel("DtH_ScoreUI");
			}
		}
		else {
			if (GUI.Button (rProgress, new GUIContent ("Progress",T2padlockIcon, "LOCKED"), "styleLocked")) {}
		}
		
		GUI.Label (new Rect (Screen.width*0.5f - iTooltipWidth*0.5f, fMenuYPos + fOffset * iTooltipHeight /2 + fOffset * iButtonHeight * 3, iTooltipWidth, iTooltipHeight), GUI.tooltip, "styleTooltip");
	}
}
