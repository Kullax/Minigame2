using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	void Start () {
		Instantiate(Resources.Load ("Pause Menu"));
		GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<Canvas>().enabled = false;
		Time.timeScale = 1;
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if(!GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<Canvas>().enabled){
				Time.timeScale = 0;
				GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<Canvas>().enabled = true;
				AudioManager am = FindObjectOfType<AudioManager>();
				am.MuteMusicSources();
				am.MuteSoundSources();
			} else {
				Time.timeScale = 1;
				GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<Canvas>().enabled = false;
				AudioManager am = FindObjectOfType<AudioManager>();
				am.UnmuteMusicSources();
				am.UnmuteSoundSources();
			}
		}
	}
}
