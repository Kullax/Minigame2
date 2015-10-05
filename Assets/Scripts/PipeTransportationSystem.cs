using UnityEngine;
using System.Collections;

public class PipeTransportationSystem : MonoBehaviour {

 
    public GameObject ExitLocation;
    public float requiredsize = 1.0f;
    public GameRotation NewGameRotation = GameRotation.NegativeX;
    //public direction;

    //private Rigidbody playerRB;
    //private GameObject player;

    // Use this for initialization
    void Start () {
        //GameObject player = GameObject.FindGameObjectWithTag("Player");
        //playerRB = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void OnTriggerStay(Collider cube) {
        if (cube.gameObject.tag != "PlayerRB")
            return;

        Rigidbody player = cube.attachedRigidbody;


        if(cube.transform.localScale.x > requiredsize)
        {
            return;
        }

        //Do animation before changing the location and make sure the animation is complete.

        player.velocity = new Vector3(0, 0, 0);
        cube.transform.position = ExitLocation.transform.position;
        GameManager.SetGameRotation(NewGameRotation);
    }
}
