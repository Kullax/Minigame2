using UnityEngine;
using System.Collections;

public class SecondReactionScript : MonoBehaviour {
    Vector3 start;
    bool active = false;
    public float hangtime;
    private float bck_hangtime;

	// Use this for initialization
	void Start () {
        start = transform.position;
        bck_hangtime = hangtime;
    }
	
	// Update is called once per frame
	void Update () {
        if (!active)
            transform.position = Vector3.MoveTowards(transform.position, start, Time.deltaTime);
        else {
            if (hangtime > 0)
                hangtime -= Time.deltaTime;
            else
                active = false;
        }
    }

    void Activate()
    {
        active = true;
        transform.position = Vector3.MoveTowards(transform.position,  transform.position + transform.up, Time.deltaTime);
        hangtime = bck_hangtime;
    }
}
