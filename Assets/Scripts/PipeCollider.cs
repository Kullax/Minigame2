using UnityEngine;
using System.Collections;

public class PipeCollider : MonoBehaviour {

    private bool _active;
    private bool active {
        get
        {
            return _active;
        }
        set
        {
            _active = value;
            if (!_active)
                firstTime = false;
        }
    }
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

    private bool firstTime = false;
    void OnTriggerStay(Collider cube)
    {
        var script = cube.GetComponent<CubeScale>();

        if (!active)
            return;
        if (!cube.tag.Equals("Player"))
            return;
       // var script = cube.GetComponent<CubeScale>();
        if (script)
            script.Effect(effect);

        //Added by Rasmus
        if (active && !firstTime)
        {
            firstTime = true;
            if (!(cube.GetComponent<CubeScale>().status == CubeScale.Status.Freezing))
            {
                if (effect == CubeScale.Status.Freezing)
                {
                   // var script = cube.GetComponent<CubeScale>();
                    if (script)
                        script.PlayAnimation(effect);
                }
            }
        }
    }

    void OnTriggerExit(Collider whatever) {
        if (whatever.gameObject == GameManager.CurrentPlayer)
            firstTime = false;
    }

    /*void OnTriggerEnter(Collider cube)
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
    }*/

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