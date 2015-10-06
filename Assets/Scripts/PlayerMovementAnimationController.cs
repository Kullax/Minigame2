using UnityEngine;
using System.Collections;

public class PlayerMovementAnimationController : MonoBehaviour {

    private Rigidbody _rb;
    private CubeScale _cs;
    // Use this for initialization
    void Start () {
        _rb = GetComponent<Rigidbody>(); //to know velocity
        _cs = GetComponent<CubeScale>(); //to now status of the cube
    }
	
	// Update is called once per frame
	void Update () {
	    if (_cs.status == CubeScale.Status.Melting)
        {

        }
        if (_cs.status == CubeScale.Status.None)
        {

        }
    }
}
