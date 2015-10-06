using UnityEngine;
using System.Collections;

public class CubeParticleEffectsController : MonoBehaviour
{

    public ParticleSystem freezing;
    public ParticleSystem melting;
    public ParticleSystem death;

    private CubeScale _cs;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _cs = GetComponent<CubeScale>();
        /*if (_cs.status == CubeScale.Status.None)
        {
            melting.Stop();
           freezing.Stop();
        }*/

        if (_cs.status == CubeScale.Status.Freezing)
        {
            //Debug.Log("FREEZ");
            freezing.transform.position = this.transform.position;
            if (!freezing.isPlaying)
            {
                melting.Stop();
                freezing.Play();
            }

        }
        if (_cs.status == CubeScale.Status.Melting)
        {
            //Debug.Log("MELT");
            melting.transform.position = this.transform.position;
            if (!melting.isPlaying)
            {
                freezing.Stop();
                melting.Play();
            }

        }
        if (GameManager.CurrentPlayer.transform.localScale.x < _cs.lethallimit)
        {
            //Debug.Log("DIE");
            death.transform.position = this.transform.position;
            if (!death.isPlaying)
            {
                death.Play();
            }
            death.Stop();
        }

    }
}
