using UnityEngine;
using System.Collections;

public class PauseMenuScript : MonoBehaviour {

	public void resetGame(){
		GameManager.RestartLevel ();
		GameObject pauseMenu = GameObject.FindGameObjectWithTag ("PauseMenu");
		pauseMenu.SetActive (false);
		Time.timeScale = 1;
	}

	public void resumeGame(){
		GameObject pauseMenu = GameObject.FindGameObjectWithTag ("PauseMenu");
		pauseMenu.SetActive (false);
		Time.timeScale = 1;
	}

	public void loadMainMenu(){
		Application.LoadLevel ("MainMenu");
	}

}
