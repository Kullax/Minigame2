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
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = Instantiate(burst);
        audioSource.maxDistance = 8;
        audioSource.minDistance = 6;
        audioSource.volume = volume;
        last = Time.time;

        burstParticles = GetComponent<ParticleSystem>();
        if (burstParticles == null)
        {
            Debug.LogWarning("Particle system missing from: " + this.gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = GodTouch.WorldPositionBegin;

        //Makes sure that the burst has the circle fronting the camera and not turned sideways.
        if (GameManager.GameRotation == GameRotation.NegativeX || GameManager.GameRotation == GameRotation.NegativeZ)
        {
            transform.rotation = Quaternion.AngleAxis(90, Vector3.up);
        } else
        {
            transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
        }

        if (GodTouch.Phase == GodPhase.Began)
        {
            burstParticles.loop = false;
            PlayParticle();
        }
        else if (GodTouch.Phase == GodPhase.Held)
        {
                PlayParticle();
        }
        else if (GodTouch.Phase == GodPhase.End || GodTouch.Phase == GodPhase.None)
        {
            if (burstParticles.isPlaying)
                burstParticles.Stop();
        }
    }

    void PlayParticle()
    {
        if (Time.time - last > frequency)
        {
            burstParticles.Play();
            if (audioSource)
            {
                audioSource.Play();
            }
            last = Time.time;
        }

    }
}
