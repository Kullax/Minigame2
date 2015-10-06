using UnityEngine;

public class BurstAnimator : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip burst;
    private ParticleSystem burstParticles;
    private float last;
    public float frequency = 0.25f;
    public float volume = 0.2f;

    // Use this for initialization
    void Start() {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = Instantiate(burst);
        audioSource.maxDistance = 8;
        audioSource.minDistance = 6;
        audioSource.volume = volume;
        last = Time.time;

        burstParticles = GetComponent<ParticleSystem>();
        if (burstParticles == null)
            Debug.LogWarning("Particle system missing from: " + this.gameObject.name);
    }

    // Update is called once per frame
    void Update() {
        transform.position = GodTouch.WorldPositionBegin;
        transform.rotation = Camera.main.transform.rotation;

        switch (GodTouch.Phase) {
            case GodPhase.Began:
                burstParticles.loop = false;
                PlayParticle();
                break;
            case GodPhase.Held:
                PlayParticle();
                break;
            default:
                if (burstParticles.isPlaying)
                    burstParticles.Stop();
                break;
        }
    }

    void PlayParticle() {
        if (Time.time - last > frequency) {
            burstParticles.Play();
            if (audioSource)
                audioSource.Play();
            last = Time.time;
        }
    }
}
