using UnityEngine;
using System.Collections;
using System.Net;

public class CubeScale : MonoBehaviour
{
    private float elapsedtime;
    private float epsilon = 0.05f;
    private float refreeze = 0.25f;
    private float speed;
    private Vector3 smelted;
    private readonly Vector3 frozen = new Vector3(1, 1, 1);
    private Vector3 startstate;
    public Status status = Status.None;
    public float meltingspeed;
    public float lethallimit;
    private CameraSoundScript camerasound;
    private characterRenderController _crc;
    private Animator anm;
    private bool alreadyplayed = false;
    //private Vector3 deathlocation;
    public float timeelapsed = 0;
    //private float timelimit = 4f;
    public bool dead = false;
    public bool door = false;

    public enum Status
    {
        None = 0,
        Melting = 1 << 1,
        Freezing = 1 << 2,
    }

    // Use this for initialization
    void Start()
    {
        //ResetCubeRBBehaviorAndStatus();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (dead)
        {
            DeathByAnimation();
            return;
        }

        if (transform.localScale.x <= lethallimit)
        {
            dead = true;
            return;
        }


        switch (status)
        {
            case Status.Melting:
                anm.SetBool("Melting", true);
                ScaleObject(smelted);
                break;
            case Status.Freezing:
                anm.SetBool("Melting", false);
                ScaleObject(frozen);
                break;
            default:
                break;
        }
    }

    public void DeathByAnimation()
    {
        GetComponent<GodMovable>().enabled = false;
        anm.SetBool("Dead", true);
        if (!anm.GetCurrentAnimatorStateInfo(0).IsName("Melting Idle To Death") || !anm.GetCurrentAnimatorStateInfo(0).IsName("Death by door"))
        {
            if (!door)
                anm.Play("Melting Idle To Death");
            else
                anm.Play("Death by door");

        }
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        //GetComponent<Collider>().enabled = false;
        //deathlocation = transform.position;
        timeelapsed += Time.deltaTime;
        if (anm.GetCurrentAnimatorStateInfo(0).IsName("Melting Idle To Death") || anm.GetCurrentAnimatorStateInfo(0).IsName("Death by door"))
        {
            if (camerasound && !alreadyplayed)
            {
                camerasound.deathmelody();
                alreadyplayed = true;
            }
            //            if (timelimit <= timeelapsed)
            if (camerasound.isIdle())
            {
                anm.SetBool("Melting", false);
                anm.SetBool("Dead", false);
                anm.Play("Idle Pose");
                GameManager.RespawnPlayer();
                GameManager.ResetResettables();
            }

        }
    }

    void ScaleObject(Vector3 targetsize)
    {
        if (Vector3.Distance(transform.localScale, targetsize) > epsilon)
        {
            elapsedtime += Time.deltaTime;
            float frac = elapsedtime / speed;
            transform.localScale = Vector3.Lerp(startstate, targetsize, frac);
        }
        else
        {
            transform.localScale = targetsize;
            elapsedtime = 0f;
            status = Status.None;
        }
    }

    public void PlayAnimation(Status effect)
    {
        if (anm != null)
            anm.Play("Ilde to Refreeze");
    }

    public void Effect(Status effect)
    {
        if (transform.localScale.x <= lethallimit)
            return;
        if (effect == status)
            return;
        switch (effect)
        {
            case Status.Melting:
                startstate = transform.localScale;
                status = Status.Melting;
                elapsedtime = 0;
                speed = meltingspeed;
                if (camerasound)
                    camerasound.meltingmelody();
                break;
            case Status.Freezing:
                if (status != Status.Melting)
                    return;
                startstate = transform.localScale;
                status = Status.Freezing;
                elapsedtime = 0;
                speed = refreeze;
                if (camerasound)
                    camerasound.normalmelody();
                break;
            default:
                break;
        }
    }

    public void ResetCubeRBBehaviorAndStatus()
    {
        camerasound = Camera.main.GetComponent<CameraSoundScript>();
        if (camerasound == null)
        {
            Debug.LogWarning("Camera sound missing!");
            return;
        }

        _crc = FindObjectOfType<characterRenderController>();
        if (_crc == null)
        {
            Debug.LogWarning("characterRenderController missing!");
            return;
        }
        _crc.transform.position = this.gameObject.transform.position;
        _crc.transform.rotation = this.gameObject.transform.rotation;

        anm = _crc.GetComponent<Animator>();
        if (anm == null)
        {
            Debug.LogWarning("Animator from characterRenderController missing!");
            return;
        }

        anm.SetBool("Melting", false);
        anm.SetBool("Dead", false);
        anm.Play("Idle Pose");

        status = Status.None;
        alreadyplayed = false;
        dead = false;
        door = false;
        speed = refreeze;
        elapsedtime = 0f;
        smelted = new Vector3(lethallimit, lethallimit, lethallimit);

        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        GetComponent<GodMovable>().enabled = true;
        ScaleObject(frozen);
    }
}