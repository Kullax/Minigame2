using UnityEngine;
using System.Collections;

public class SecondReactionScript : MonoBehaviour {
    Vector3 start;
    bool active = false;
    public float hangtime;

	// Use this for initialization
	void Start () {
        start = transform.position;	

	}
	
	// Update is called once per frame
	void Update () {
        if (!active)
            transform.position = Vector3.MoveTowards(transform.position, start, Time.deltaTime * 0.1f);
        else {
            if (hangtime > 0)
                hangtime -= Time.deltaTime;
            else
                active = true;
            return;
        }
    }

    void Activate()
    {
        transform.position = Vector3.MoveTowards(transform.position,  transform.position + transform.up, Time.deltaTime);
    }
}
