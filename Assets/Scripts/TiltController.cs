using UnityEngine;
using System.Collections;

public class TiltController : MonoBehaviour {

	public float Movement = 100;
	[Range(0f,1.0f)]
	public float Threshold = 0.2f;

	private Rigidbody rb;
	
	// Update is called once per frame
	void Update () {
		if(rb == null)
			rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
		if(Mathf.Abs(Input.acceleration.x) > Threshold){
			rb.AddForce(Camera.main.transform.right * Input.acceleration.x * Movement);
		}
	}
}
