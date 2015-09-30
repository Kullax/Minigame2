using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour
{
    Vector3 start;
    bool active = false;
    public float hangtime;
    public float distance = 1.5f;
    private float bck_hangtime;
    public bool closes = false;
    private bool moving = false;

    // Use this for initialization
    void Start()
    {
        start = transform.position;
        bck_hangtime = hangtime;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.up, Time.deltaTime);
            if (distance <= Vector3.Distance(transform.position, start))
                moving = false;
            return;
        }
        if (!closes)
            return;
        if (hangtime > 0)
            hangtime -= Time.deltaTime;
        else
            transform.position = Vector3.MoveTowards(transform.position, start, Time.deltaTime);
        if (0 >= Vector3.Distance(transform.position, start))
            active = false;

    }

    public void Activate()
    {
        if (!active)
        {
            active = true;
            moving = true;
            hangtime = bck_hangtime;
        }
    }
}
