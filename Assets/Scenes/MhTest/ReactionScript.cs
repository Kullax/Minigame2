using UnityEngine;
using System.Collections;

public class ReactionScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Activate()
    {
        Debug.Log("I'm reacting");
        transform.Rotate(0,0,1);
    }
}
