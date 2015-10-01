using UnityEngine;
using System.Collections;

public class PipeTransportationSystem : MonoBehaviour {

 
    public GameObject ExitLocation;
    public float requiredsize = 1.0f;
    //public direction;

    private Rigidbody playerRB;
   // private GameObject player;

    // Use this for initialization
    void Start () {
        //GameObject player = GameObject.FindGameObjectWithTag("Player");

       // playerRB = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void OnTriggerStay(Collider cube) {
        if (cube.gameObject.tag != "Player")
            return;

        Rigidbody player = cube.attachedRigidbody;
        playerRB = player.GetComponent<Rigidbody>();


        if (cube.transform.localScale.x > requiredsize)
        {
            return;
        }

        //Do animation before changing the location and make sure the animation is complete.

        playerRB.velocity = new Vector3(0, 0, 0);
        cube.transform.position = ExitLocation.transform.position;
    }
}
