using UnityEngine;
using System.Collections;

public class DestroyableObjectCounter : MonoBehaviour {

    private int clickedCounter = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (clickedCounter > 4)
        {
            Destroy(gameObject);
        }
	}
}
