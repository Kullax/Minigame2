using UnityEngine;

public class CameraSoundScript : MonoBehaviour {

    public AudioClip FreezingMelody;
    public AudioClip MeltingMelody;
    public AudioClip RefreezeSound;
    private AudioSource freezing;
    private AudioSource melting;
    private AudioSource refreeze;
    public float volume = 1f;

	// Use this for initialization
	void Start () {
        freezing = gameObject.AddComponent<AudioSource>();
        freezing.clip = Instantiate (FreezingMelody);
        freezing.loop = true;
        freezing.volume = volume;
        melting = gameObject.AddComponent<AudioSource>();
        melting.clip = MeltingMelody;
        melting.volume = volume;
        freezing.Play();
        refreeze = gameObject.AddComponent<AudioSource>();
        refreeze.clip = Instantiate(RefreezeSound);
        refreeze.volume = volume;
    }

    public void meltingmelody()
    {
        freezing.Pause();
        melting.Play();
    }

    public void normalmelody()
    {
        if(!refreeze.isPlaying)
            refreeze.Play();
        melting.Stop();
        freezing.UnPause();
    }
}