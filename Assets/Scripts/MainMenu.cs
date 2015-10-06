using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public float panTimeStart;
	public float panTimeOptions;
	public Image backgroundImage;
	public Image shade;

	private Vector3 origPos;
	private float panStartTime;
	private bool option;
	private bool main;
	private bool start;
	private bool moving;

	void Start(){
		origPos = backgroundImage.transform.localPosition;
		main = true;
	}

	void Update(){
		if (start)
			scrollDown ();
		if (option)
			scrollLeft ();
		if (main)
			scrollRight ();
	}

	private void scrollDown(){
		if (panStartTime != 0) {
			backgroundImage.transform.localPosition =  Vector3.Lerp(
				origPos, 
				new Vector3(origPos.x,origPos.y + 800,origPos.z), 
				(Time.time - panStartTime)/panTimeStart);
			shade.color = new Color(0,0,0, 1-(panTimeStart-(Time.time - panStartTime))/panTimeStart);
			if (panStartTime + panTimeStart < Time.time) {
				Application.LoadLevel("Recombined_Level_with_correct_prefabs");
			}
		}
	}

	private void scrollLeft(){
		if (panStartTime != 0) {
			backgroundImage.transform.localPosition =  Vector3.Lerp(
				origPos, 
				new Vector3(origPos.x + 800,origPos.y,origPos.z), 
				(Time.time - panStartTime)/panTimeOptions);
			if (panStartTime + panTimeOptions < Time.time) {
				moving = false;
			}
		}
	}

	private void scrollRight(){
		if (panStartTime != 0) {
			backgroundImage.transform.localPosition =  Vector3.Lerp(
				new Vector3(origPos.x + 800,origPos.y,origPos.z),
				origPos, 
				(Time.time - panStartTime)/panTimeOptions);
			if (panStartTime + panTimeOptions < Time.time) {
				moving = false;
			}
		}
	}

	public void startGame(){
		if (!main || moving)
			return;
		panStartTime = Time.time;
		start = true;
		moving = true;
		main = false;
	}

	public void optionMenu(){
		if (!main || moving)
			return;
		panStartTime = Time.time;
		option = true;
		main = false;
		moving = true;
	}

	public void mainMenu(){
		if (!option || moving)
			return;
		panStartTime = Time.time;
		option = false;
		moving = true;
		main = true;
	}

	public void stopGame() {
		Application.Quit ();
	}

	public void english(){
		I18n.GetInstance ().LoadLanguage ("en");
		foreach (TextI18n textField in FindObjectsOfType<TextI18n> ()) {
			textField.updateField();
		}
	}

	public void danish(){
		I18n.GetInstance ().LoadLanguage ("da");
		foreach (TextI18n textField in FindObjectsOfType<TextI18n> ()) {
			textField.updateField();
		}
	}
}
