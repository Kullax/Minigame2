using UnityEngine;
using System.Collections;

public class PipeCollider : MonoBehaviour {

    private bool active;
    private CubeScale.Status effect;
    private MeshRenderer rend;
    public Material OnMaterial;
    public Material OffMaterial;
    private Material OldMaterial;
    public float speed = 1f;
    private float elapsedtime = 0;
    
    void Awake()
    {
        rend = GetComponent<MeshRenderer>();
    }
    void Start()
    {
        if (!active)
            OldMaterial = OffMaterial;
        else
            OldMaterial = OnMaterial;
    }

    void Update()
    {
        if(elapsedtime < speed)
            elapsedtime += Time.deltaTime;
        float diff = elapsedtime / speed;
        if (rend)
        {
            if (!active)
            {
                rend.material.Lerp(OldMaterial, OffMaterial, diff);
            }
            else
            {
                diff = 1 - diff;
                rend.material.Lerp(OnMaterial, OldMaterial, diff);
            }

        }
    }


    void OnTriggerStay(Collider cube)
    {
        if (!active)
            return;
        if (!cube.tag.Equals("Player"))
            return;
        var script = cube.GetComponent<CubeScale>();
        if (script)
            script.Effect(effect);
    }

    void OnTriggerEnter(Collider cube)
    {
        if (active)
        {
            if (effect == CubeScale.Status.Freezing)
            {
                var script = cube.GetComponent<CubeScale>();
                if (script)
                    script.PlayAnimation(effect);
            }
        }
    }

    public void Activate()
    {
        // Attempt to avoid instant max if spam clicked
        OldMaterial = rend.material;
        active = true;
        elapsedtime = 0;
    }

    public void Deactivate()
    {
        // Attempt to avoid instant max if spam clicked
        OldMaterial = rend.material;
        active = false;
        elapsedtime = 0;
    }

    public void MakeCold()
    {
       effect = CubeScale.Status.Freezing;
    }

    public void MakeHot()
    {
        effect = CubeScale.Status.Melting;
    }
}