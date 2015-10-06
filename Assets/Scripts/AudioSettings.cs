using UnityEngine;
using System.Collections;

public class AudioSettings {

	public static AudioSettings instance;

	public bool muted { get; set;}
	
	private AudioSettings(){
		muted = false;
	}
	
	public static AudioSettings GetInstance(){
		if (instance == null){
			instance = new AudioSettings();
		}
		return instance;
	}
}
