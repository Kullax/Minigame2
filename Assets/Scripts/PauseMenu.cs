using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	private GameObject pauseMenu;

	private Vector3 origPos;

	void Start () {
		pauseMenu  = (GameObject) Instantiate(Resources.Load("Pause Menu"));
		pauseMenu.SetActive (false);
		Time.timeScale = 1;
		pauseMenu.transform.SetParent(Camera.main.transform);
		pauseMenu.transform.localPosition = new Vector3 (5.1f,2.33f,5.75f);
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if(!pauseMenu.activeSelf){
				Time.timeScale = 0;
				pauseMenu.SetActive(true);
			} else {
				Time.timeScale = 1;
				pauseMenu.SetActive(false);
			}
		}
	}
}
