using UnityEngine;

public class Pipe : ResettableMonoBehaviour {

    public GameObject target;
    public Material hot_material;
    public Material cold_material;
    public Material off_material;
    private PipeCollider pipe_collider;
    public CubeScale.Status effect;
    private Renderer rend;
    private float speed = 0.1f;
    private Material old_material;
    private Material active_material;
    public bool active;
    private AudioSource audioSource;
    private bool org_active;
    private CubeScale.Status org_effect;


    // Use this for initialization
    void Start () {
        org_active = active;
        org_effect = effect;
        rend = GetComponent<Renderer>();
        pipe_collider = target.GetComponent<PipeCollider>();
        switch (effect)
        { 
            case CubeScale.Status.Melting:
                active_material = hot_material;
                old_material = hot_material;
                break;
            case CubeScale.Status.Freezing:
                active_material = cold_material;
                old_material = cold_material;
                break;
            default:
                active_material = off_material;
                old_material = off_material;
                break;
        }
		audioSource = GetComponent<AudioSource>();
		audioSource.loop = true;
		audioSource.maxDistance = 15;
		audioSource.minDistance = 5;
		audioSource.spatialBlend = 1.0f;
		audioSource.rolloffMode = AudioRolloffMode.Linear;

        if (active)
        {
            pipe_collider.Activate();
            if (audioSource)
                audioSource.Play();
        }
        if (effect == CubeScale.Status.Freezing)
            MakeCold();
        if (effect == CubeScale.Status.Melting)
            MakeHot();
    }

    // Update is called once per frame
    void Update () {
        if (!active)
        {
            if (rend.material != off_material)
                rend.material.Lerp(old_material, off_material, speed);
        }
        else
        {
            if(rend.material != active_material)
                rend.material.Lerp(old_material, active_material, speed);
        }
    }

    public void Activate() {
        active = !active;
        if (active) {
			pipe_collider.Activate ();
			if (audioSource)
				audioSource.PlayDelayed (0.5f);
		} else {
			pipe_collider.Deactivate ();
			if(audioSource)
				audioSource.Stop();
		}
    }

    public void MakeHot()
    {
        old_material = rend.material;
        active_material = hot_material;
        pipe_collider.MakeHot();
    }

    public void MakeCold()
    {
        old_material = rend.material;
        active_material = cold_material;
        pipe_collider.MakeCold();
    }

    public override void ResetBehaviour()
    {
        switch (org_effect)
        {
            case CubeScale.Status.Melting:
                active_material = hot_material;
                old_material = hot_material;
                break;
            case CubeScale.Status.Freezing:
                active_material = cold_material;
                old_material = cold_material;
                break;
            default:
                active_material = off_material;
                old_material = off_material;
                break;
        }

        if (org_active)
        {
            pipe_collider.Activate();
            if (audioSource)
                audioSource.Play();
        }
        if (effect == CubeScale.Status.Freezing)
            MakeCold();
        if (effect == CubeScale.Status.Melting)
            MakeHot();
    }
}
