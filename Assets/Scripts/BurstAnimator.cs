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
