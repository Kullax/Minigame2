using UnityEngine;
using System.Collections;

public class LightController : MonoBehaviour {

    public Light lightComp;
    public Color color;
    public bool onoff = false;
    public bool changelight = true;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Activate()
    {
        if (!lightComp)
            return;
        if (onoff)
            lightComp.enabled = !lightComp.enabled;
        if(changelight)
            lightComp.color = color;

    }
}