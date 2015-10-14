using UnityEngine;
using System.Collections;
using System;

public class AudioManager : MonoBehaviour {
    private AudioSource[] allAudioSources;
    private AudioSource freezingmusic;
    private AudioSource meltingmusic;

	private bool mutedSet;
	private bool pausedMelting;

	void Update(){
		if (AudioSettings.GetInstance ().muted && !mutedSet) {

			allAudioSources = FindObjectsOfType<AudioSource>();
			var camera = FindObjectOfType<Camera>();
			CameraSoundScript camerasound = camera.GetComponent<CameraSoundScript>();
			freezingmusic = camerasound.getFreezingMelody();
			meltingmusic = camerasound.getMeltingMelody();

			MuteMusicSources ();
			MuteSoundSources();
			mutedSet = true;
		} else if(!mutedSet) {

			allAudioSources = FindObjectsOfType<AudioSource>();
			var camera = FindObjectOfType<Camera>();
			CameraSoundScript camerasound = camera.GetComponent<CameraSoundScript>();
			freezingmusic = camerasound.getFreezingMelody();
			meltingmusic = camerasound.getMeltingMelody();

			UnmuteMusicSources();
			UnmuteSoundSources();
			mutedSet = true;
		}
	}

    internal void Reset()
    {
        mutedSet = false;
        pausedMelting = false;
    }

    public void MuteMusicSources()
    {
        freezingmusic.mute = true;
		if (meltingmusic.isPlaying) {
			meltingmusic.Pause();
			pausedMelting = true;
		}
    }

    public void UnmuteMusicSources()
    {
		if(AudioSettings.GetInstance().muted)
			return;
        freezingmusic.mute = false;
        if (pausedMelting) {
			meltingmusic.UnPause();
			pausedMelting = false;
		}
    }

    public void MuteSoundSources()
    {
        foreach (AudioSource audio in allAudioSources)
        {
            audio.mute = true;
        }
    }

    public void UnmuteSoundSources()
    {
		if(AudioSettings.GetInstance().muted)
			return;
        foreach (AudioSource audio in allAudioSources)
        {
            audio.mute = false;
        }
    }

    public void SetMusicVolume(float volume)
    {
        freezingmusic.volume = volume;
        meltingmusic.volume = volume;
    }

    public void SetAudioVolume(float volume)
    {
        foreach (AudioSource audio in allAudioSources)
        {
            if (audio != freezingmusic && audio != meltingmusic)
                audio.volume = volume;
        }
    }
}
