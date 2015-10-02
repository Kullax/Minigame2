using UnityEngine;
using System.Collections;

public class BurstAnimator : MonoBehaviour
{

    private ParticleSystem burstParticles;

    // Use this for initialization
    void Start()
    {
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

        if (GodTouch.Phase == GodPhase.Began)
        {
            //if (!burstParticles.isPlaying)
            //{
            burstParticles.loop = false;
            burstParticles.Play();
            //}
        }
        else if (GodTouch.Phase == GodPhase.Held)
        {
            if (!burstParticles.isPlaying)
            {
                burstParticles.loop = true;
                burstParticles.Play();
            }
        }
        else if (GodTouch.Phase == GodPhase.End || GodTouch.Phase == GodPhase.None)
        {
            if (burstParticles.isPlaying)
                burstParticles.Stop();
        }
    }
}
