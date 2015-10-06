using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
    private AudioSource[] allAudioSources;
    private AudioSource freezingmusic;
    private AudioSource meltingmusic;

	private bool mutedSet;

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

    public void MuteMusicSources()
    {
        freezingmusic.mute = true;
        meltingmusic.mute = true;
    }

    public void UnmuteMusicSources()
    {
        freezingmusic.mute = false;
        meltingmusic.mute = false;
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
