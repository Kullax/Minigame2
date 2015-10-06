using UnityEngine;
using System.Collections;

public class PipeTransportationSystem : MonoBehaviour
{
    public GameObject ExitLocation;
    public float requiredsize = 1.0f;
    public GameRotation NewGameRotation = GameRotation.NegativeX;

    private Animator CAC; //Character Animation Controller
    private AudioSource audioSourceTP;

    void Start()
    {
        GameObject CharacterRenderer = GameObject.Find("iceCube_animation_control");
        CAC = CharacterRenderer.GetComponent<Animator>();
        audioSourceTP = GetComponent<AudioSource>();
    }

    void OnTriggerStay(Collider cube)
    {
        if (cube.gameObject.tag != "Player")
            return;

        Rigidbody playerRB = cube.attachedRigidbody;

        if (cube.transform.localScale.x > requiredsize)
        {
            return;
        }

        //Do animation before changing the location and make sure the animation is complete.
        CAC.Play("Melting Idle To enter Teleport");
        audioSourceTP.Play();
        cube.transform.position = this.transform.position;

        if (CAC.GetCurrentAnimatorStateInfo(0).IsName("Exit Teleport Pipe to idle"))
        {
            playerRB.velocity = new Vector3(0, 0, 0);
            cube.transform.position = ExitLocation.transform.position;
            GameManager.SetGameRotation(NewGameRotation);
        }
    }
}
