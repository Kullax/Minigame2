using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseMenuScript : MonoBehaviour {

    void Start()
    {
        float w = Screen.width;
        float h = Screen.height;
        RectTransform img = GameObject.FindGameObjectWithTag("PauseMenuImg").GetComponent<RectTransform>();
            img.transform.localScale = new Vector3((w/h) / (4f/3f), 1f, 1f);
    }

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
