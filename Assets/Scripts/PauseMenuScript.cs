using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseMenuScript : MonoBehaviour {

	public void resetGame(){
		GameManager.RestartLevel ();
		GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<Canvas>().enabled = false;
		Time.timeScale = 1;
        AudioManager am = FindObjectOfType<AudioManager>();
        am.Reset();
    }

	public void resumeGame(){
		GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<Canvas>().enabled = false;
		Time.timeScale = 1;
		AudioManager am = FindObjectOfType<AudioManager>();
		am.UnmuteMusicSources();
		am.UnmuteSoundSources();
	}

	public void loadMainMenu(){
		Time.timeScale = 1;
		Application.LoadLevel ("MainMenu");
	}

	public void showPauseMenu(){
		GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<Canvas>().enabled = true;
		Time.timeScale = 0;
		AudioManager am = FindObjectOfType<AudioManager>();
		am.MuteMusicSources();
		am.MuteSoundSources();
	}

}
