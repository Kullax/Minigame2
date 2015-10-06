using UnityEngine;
using System.Collections;

public class CubeScale : MonoBehaviour
{
    private float elapsedtime;
    private float epsilon = 0.05f;
    private float refreeze = 0.25f;
    private float speed;
    private Vector3 smelted;
    private Vector3 frozen = new Vector3(1, 1, 1);
    private Vector3 startstate;
    public Status status = Status.None;
    public float meltingspeed;
    public float lethallimit;
    private CameraSoundScript camerasound;
    private GameObject obj;
    private Animator anm;
    private bool alreadyplayed = false;

    public enum Status
    {
        None = 0,
        Melting = 1 << 1,
        Freezing = 1 << 2,
    }

    // Use this for initialization
    void Start()
    {
        elapsedtime = 0f;
        camerasound = Camera.main.GetComponent<CameraSoundScript>();
        smelted = new Vector3(lethallimit, lethallimit, lethallimit);
        obj = GameObject.Find("iceCube_animation_control");
        anm = obj.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.localScale.x <= lethallimit)
        {
            GetComponent<GodMovable>().enabled = false;
            anm.SetBool("Dead", true);
            if (anm.GetCurrentAnimatorStateInfo(0).IsName("Melting Idle To Death"))
            {
                if (camerasound && !alreadyplayed)
                {
                    camerasound.deathmelody();
                    alreadyplayed = true;
                }
                if (camerasound.isIdle())
                {
                    anm.SetBool("Melting", false);
                    anm.SetBool("Dead", false);
                    anm.Play("Idle Pose");
                    GameManager.RespawnPlayer();
                    GameManager.ResetResettables();
                }

            }
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
        switch (effect)
        {
            case Status.Freezing:
                if (status == Status.None)
                    anm.Play("Ilde to Refreeze");
                if (status == Status.Melting)
                    anm.Play("Melting Idle To Refreeze");
                break;
            default:
                break;
        }
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

}