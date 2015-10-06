using UnityEngine;
using System.Collections;

public class PipeTransportationSystem : MonoBehaviour {

 
    public GameObject ExitLocation;
    public float requiredsize = 1.0f;
    public GameRotation NewGameRotation = GameRotation.NegativeX;

    private Animator CAC; //Character Animation Controller

    // Use this for initialization
    void Start () {
        GameObject CharacterRenderer = GameObject.Find("iceCube_animation_control");
        CAC = CharacterRenderer.GetComponent<Animator>();
        //playerRB = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void OnTriggerStay(Collider cube) {
        if (cube.gameObject.tag != "Player")
            return;

        Rigidbody player = cube.attachedRigidbody;


        if(cube.transform.localScale.x > requiredsize)
        {
            return;
        }

        //Do animation before changing the location and make sure the animation is complete.
        CAC.Play("Melting Idle To enter Teleport");

        if (CAC.GetCurrentAnimatorStateInfo(0).IsName("Exit Teleport Pipe to idle"))
        {
            player.velocity = new Vector3(0, 0, 0);
            cube.transform.position = ExitLocation.transform.position;
            GameManager.SetGameRotation(NewGameRotation);
        }
    }
}
