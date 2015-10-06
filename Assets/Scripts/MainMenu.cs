using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour {

	public float panTimeStart;
	public float panTimeOptions;
	public Image backgroundImage;
	public Image shade;

	public GameObject animator;

	private Vector3 origPos;
	private float panStartTime;
	private bool option;
	private bool main;
	private bool start;
	private bool moving;
	private bool credit;
	private bool creditback;

    [Range(0,1)]
    public float buttonVolume = 1;

	public AudioClip[] buttonSounds;
	private AudioSource[] buttonSources;

	void Start(){
		origPos = backgroundImage.transform.localPosition;
		main = true;
		buttonSources = new AudioSource[buttonSounds.Length];
		for (int i = 0;i<buttonSounds.Length;i++){
			buttonSources[i] = Camera.main.gameObject.AddComponent<AudioSource>();
			buttonSources[i].clip = buttonSounds[i];
            buttonSources[i].volume = buttonVolume;
            buttonSources[i].playOnAwake = false;
		}
	}

	void Update(){
		if (start)
			scrollDown ();
		if (option)
			scrollLeft ();
		if (main)
			scrollRight ();
		if (credit)
			scrollCredit ();
		if (creditback)
			scrollCreditBack ();
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

	private void scrollCredit(){
		if (panStartTime != 0) {
			backgroundImage.transform.localPosition =  Vector3.Lerp(
				new Vector3(origPos.x + 800,origPos.y,origPos.z),
				new Vector3(origPos.x + 1600,origPos.y,origPos.z),
				(Time.time - panStartTime)/panTimeOptions);
			if (panStartTime + panTimeOptions < Time.time) {
				moving = false;
			}
		}
	}

	private void scrollCreditBack(){
		if (panStartTime != 0) {
			backgroundImage.transform.localPosition =  Vector3.Lerp(
				new Vector3(origPos.x + 1600,origPos.y,origPos.z),
				new Vector3(origPos.x + 800,origPos.y,origPos.z),
				(Time.time - panStartTime)/panTimeOptions);
			if (panStartTime + panTimeOptions < Time.time) {
				moving = false;
			}
		}
	}

	public void startAnimation(){
		playButtonSound ();
		if (!main || moving)
			return;
		animator.GetComponent<Animator> ().SetTrigger ("start");
	}

	public void startGame(){
		if (!main || moving)
			return;
		panStartTime = Time.time;
		start = true;
		moving = true;
		main = false;
		credit = false;
		creditback = false;
	}

	public void optionMenu(){
		playButtonSound ();
		if (!main || moving)
			return;
		panStartTime = Time.time;
		option = true;
		main = false;
		credit = false;
		moving = true;
		creditback = false;
	}

	public void creditMenu(){
		playButtonSound ();
		if ((!creditback && !option) || moving)
			return;
		panStartTime = Time.time;
		option = false;
		credit = true;
		main = false;
		creditback = false;
		moving = true;
	}

	public void creditBack(){
		playButtonSound ();
		if (!credit || moving)
			return;
		panStartTime = Time.time;
		option = false;
		credit = false;
		creditback = true;
		main = false;
		moving = true;
	}

	public void mainMenu(){
		playButtonSound ();
		if ((!creditback && !option) || moving)
			return;
		panStartTime = Time.time;
		option = false;
		moving = true;
		credit = false;
		main = true;
		creditback = false;
	}

	public void stopGame() {
		playButtonSound ();
		Application.Quit ();
	}

	public void english(){
		playButtonSound ();
		I18n.GetInstance ().LoadLanguage ("en");
		foreach (TextI18n textField in FindObjectsOfType<TextI18n> ()) {
			textField.updateField();
		}
	}

	public void danish(){
		playButtonSound ();
		I18n.GetInstance ().LoadLanguage ("da");
		foreach (TextI18n textField in FindObjectsOfType<TextI18n> ()) {
			textField.updateField();
		}
	}

	public void muteMusic(){
		Camera.main.GetComponent<AudioSource>().mute = true;
		AudioSettings.GetInstance ().muted = true;
	}

	public void unmuteMusic(){
		Camera.main.GetComponent<AudioSource>().mute = false;
		AudioSettings.GetInstance ().muted = false;
		playButtonSound ();
	}

	private void playButtonSound(){
		if (AudioSettings.GetInstance ().muted)
			return;
		buttonSources [Random.Range (0, 100) % buttonSounds.Length].Play ();
	}
}
