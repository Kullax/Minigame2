using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class PipeTransportationSystem : MonoBehaviour
{
    public GameObject ExitLocation;
    public float requiredsize = 1.0f;
    public GameRotation NewGameRotation = GameRotation.NegativeX;

    private AudioSource audioSourceTP;

    void Start()
    {
        audioSourceTP = GetComponent<AudioSource>();
    }

    void OnTriggerStay(Collider cube)
    {
        if (cube.gameObject.tag != "Player")
            return;

        audioSourceTP.Play();

        Rigidbody player = cube.attachedRigidbody;

        if (cube.transform.localScale.x > requiredsize)
        {
            return;
        }

        //Do animation before changing the location and make sure the animation is complete.

        player.velocity = new Vector3(0, 0, 0);
        cube.transform.position = ExitLocation.transform.position;
        GameManager.SetGameRotation(NewGameRotation);
    }
}
