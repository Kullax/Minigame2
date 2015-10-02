using UnityEngine;
using System.Collections;

public class characterRenderController : MonoBehaviour {

    private Camera MainCamera;
    private GameObject PlayerCube;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        PlayerCube = GameObject.FindWithTag("Player");
        MainCamera = Camera.main;
        transform.localScale = PlayerCube.transform.localScale;
        transform.position = PlayerCube.transform.position;
        //transform.rotation = 
        transform.rotation = MainCamera.transform.rotation;
        transform.Rotate(new Vector3 (0,180,0));
	}
}
