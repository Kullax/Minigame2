using UnityEngine;

public class DoorScript : ResettableMonoBehaviour
{
    private Vector3 start;
    bool active = false;
    public float hangtime;
    public float distance = 1.5f;
    private float bck_hangtime;
    public bool closes = false;
    private bool moving = false;
    public float upspeed;
    public float downspeed;
    private float uptime;
    private float downtime;

    public AudioClip MovingSound;
    public AudioClip Click;
    public AudioClip Clunk;
    private AudioSource move;
    private AudioSource click;
    private AudioSource clunk;

    // Use this for initialization
    void Start()
    {
        move = gameObject.AddComponent<AudioSource>();
        move.clip = Instantiate(MovingSound);
        move.loop = true;
        click = gameObject.AddComponent<AudioSource>();
        click.clip = Instantiate(Click);
        clunk = gameObject.AddComponent<AudioSource>();
        clunk.clip = Instantiate(Clunk);
        start = transform.position;
        bck_hangtime = hangtime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!active)
            return;
        if (moving)
        {
            uptime += Time.deltaTime;
            float lerp1 = uptime / upspeed;
            transform.position = Vector3.Lerp(start, start + transform.up * distance, lerp1);

            // MOVE UP SOUND
            if (!move.isPlaying)
            {
                move.Play();
                move.loop = true;
            }
            if (distance <= Vector3.Distance(transform.position, start))
            {
                moving = false;
                move.Stop();
                // STOP CLUNK SOUND
                if (!click.isPlaying)
                {
                    click.Play();
                    move.Stop();
                    move.loop = false;
                }
            }
            return;
        }
        if (!closes)
            return;
        if (hangtime > 0)
            hangtime -= Time.deltaTime;
        else
        {
            if (downtime < downspeed)
            {
                downtime += Time.deltaTime;
                float lerp2 = 1 - downtime / downspeed;
                transform.position = Vector3.Lerp(start, start + transform.up * distance, lerp2);
                // MOVE DOWN SOUND
                if (!move.isPlaying)
                    move.Play();
                if(lerp2 < 0)
                {
                    // TOUCHDOWN CLUNK SOUND
                    if (!clunk.isPlaying)
                        clunk.Play();
                    active = false;
                }
            }
        }

    }

    public override void ResetBehaviour() {
        active = false;
        moving = false;
        transform.position = start;
        hangtime = 0;
    }

    public void Activate()
    {
        if (!active)
        {
            uptime = 0;
            downtime = 0;
            active = true;
            moving = true;
            hangtime = bck_hangtime;
        }
    }
}