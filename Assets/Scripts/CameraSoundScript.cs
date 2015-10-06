using UnityEngine;

public class CameraSoundScript : MonoBehaviour {

    public AudioClip FreezingMelody;
    public AudioClip MeltingMelody;
    public AudioClip RefreezeSound;
    public AudioClip DeathMelody;
    private AudioSource freezing;
    private AudioSource melting;
    private AudioSource refreeze;
    private AudioSource death;
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
        death = gameObject.AddComponent<AudioSource>();
        death.clip = DeathMelody;
        death.volume = volume;
    }

/*	void Update(){
		if (!freezing.isPlaying && !melting.isPlaying && !death.isPlaying) {
			freezing.Stop ();
			freezing.PlayDelayed (1);
		}
	}*/

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

    public void deathmelody()
    {
        if (!death.isPlaying)
            death.Play();
        freezing.Stop();
       melting.Stop();
    }

    public bool isIdle()
    {
        return !freezing.isPlaying && !melting.isPlaying && !death.isPlaying;
    }
}