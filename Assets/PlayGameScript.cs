using UnityEngine;
using System.Collections;

public class PlayGameScript : MonoBehaviour {

	public GameObject mainMenuScript;

	void OnEnable(){
		mainMenuScript.GetComponent<MainMenu> ().startGame ();
	}
}
