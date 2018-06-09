using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class GameManager : MonoBehaviour {

	public bool recording = true;

	private float initialFixedDeltaTime;
	private bool gamePaused = false;
	private bool manuallyPaused = false;
	private bool manuallyResumed = true;

	void Start(){
		initialFixedDeltaTime = Time.fixedDeltaTime;
	}

	void Update () {
		if (CrossPlatformInputManager.GetButton("Fire1")){
			recording = false;
		}else{
			recording = true;
		};

		if (Input.GetKeyDown(KeyCode.P) && !gamePaused){
			manuallyPaused = true;
			manuallyResumed = false;
			PauseGame();
		} else if(Input.GetKeyDown(KeyCode.P) && gamePaused){
			manuallyResumed = true;
			manuallyPaused = false;
			ResumeGame();

		} else if (gamePaused && !manuallyPaused){
			PauseGame();
		} else if (!gamePaused && !manuallyResumed){
			ResumeGame();
		}
	}

	void PauseGame(){
		gamePaused = true;
		Time.timeScale = 0;
		Time.fixedDeltaTime=0;
	}

	void ResumeGame(){
		gamePaused = false;
		Time.timeScale = 1f;
		Time.fixedDeltaTime = initialFixedDeltaTime;
	}

}
