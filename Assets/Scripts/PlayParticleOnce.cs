using UnityEngine;
using System.Collections;

public class PlayParticleOnce : MonoBehaviour
{

    public ParticleSystem ParticleToPlay;

    // Use this for initialization
    void Start()
    {
        ParticleToPlay.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
