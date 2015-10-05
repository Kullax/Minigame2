using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public float panTime;
	public Image backgroundImage;
	public Image shade;

	private Vector3 origPos;
	private float panStartTime;

	void Start(){
		origPos = backgroundImage.transform.localPosition;
	}

	void Update(){
		if (panStartTime != 0) {
			backgroundImage.transform.localPosition =  Vector3.Lerp(
				origPos, 
				new Vector3(origPos.x,origPos.y + 800,origPos.z), 
				(Time.time - panStartTime)/panTime);
			shade.color = new Color(0,0,0, 1-(panTime-(Time.time - panStartTime))/panTime);
		}
		if (panStartTime + panTime < Time.time) {
			Application.LoadLevel("Recombined_Level_with_correct_prefabs");
		}
	}

	public void startGame(){
		panStartTime = Time.time;
	}

	public void stopGame() {
		Application.Quit ();
	}
}
