using UnityEngine;
using System.Collections;

public class CubeParticleEffectsController : MonoBehaviour
{

    public ParticleSystem freezing,
        melting,
        death,
        speedMelt;

    public float DeathParticleSize = 1.5f;

    private bool once = true;
    private CubeScale _cs;

    void Start()
    {
        _cs = GetComponent<CubeScale>();
        once = true;
    }

    void Update()
    {
        if (_cs.status == CubeScale.Status.None)
        {
            melting.Stop();
            speedMelt.Stop();
        }

        if (_cs.status == CubeScale.Status.Freezing)
        {
            //Debug.Log("FREEZ");
            freezing.transform.position = this.transform.position;
            if (!freezing.isPlaying)
            {
                melting.Stop();
                speedMelt.Stop();
                freezing.Play();
            }
        }

        if (_cs.status == CubeScale.Status.Melting && this.GetComponent<Rigidbody>().velocity.magnitude < 0.3f)
        {
            //Debug.Log("MELTing particles");
            melting.transform.position = this.transform.position;
            if (!melting.isPlaying)
            {
                freezing.Stop();
                melting.Play();
            }

        }
        else if (_cs.status == CubeScale.Status.Melting && this.GetComponent<Rigidbody>().velocity.magnitude > 0.3f)
        {
            speedMelt.transform.position = this.transform.position;
            if (!speedMelt.isPlaying)
            {
                freezing.Stop();
                speedMelt.Play();
            }
        }

        if (transform.localScale.x <= _cs.lethallimit)
        {
            death.transform.position = this.transform.position;
            death.transform.localScale = new Vector3(DeathParticleSize, DeathParticleSize, DeathParticleSize);

            if (!death.isPlaying && once)
            {
                //Debug.Log("DIE");
                once = false;
                death.Play();
            }
        }

    }
}
