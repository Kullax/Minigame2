using UnityEngine;
using System.Collections;

public class characterRenderController : MonoBehaviour {

    public GameObject MainCamera;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.localScale = transform.parent.localScale;
        transform.rotation = MainCamera.transform.rotation;
        transform.Rotate(new Vector3 (0,180,0));
	}
}
