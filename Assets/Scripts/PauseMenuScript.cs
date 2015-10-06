using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseMenuScript : MonoBehaviour {

	public void resetGame(){
		GameManager.RestartLevel ();
		GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<Canvas>().enabled = false;
		Time.timeScale = 1;
	}

	public void resumeGame(){
		GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<Canvas>().enabled = false;
		Time.timeScale = 1;
	}

	public void loadMainMenu(){
		Application.LoadLevel ("MainMenu");
	}

	public void showPauseMenu(){
		GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<Canvas>().enabled = true;
		Time.timeScale = 0;
	}

}
