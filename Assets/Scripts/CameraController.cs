using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    private GameObject Player;
    public int direction = 1;
    public float camHeight = 1.0f;
    public float camDist = 5.0f;
    public float movementSpeed = 1.0f;

    private Vector3 camPos;
    private Vector3 targetRotation;
    private float journeyLength;
    private int oldDiection;
    private float startTime;
    private float distCovered = 0;
    private Vector3 startPos, endPos;

    // Use this for initialization
    void Start () {
	    camPos = new Vector3(0.0f, camHeight, -camDist);
        startPos = transform.position;
        Player = GameObject.FindGameObjectWithTag("Player");
        if (Player == null) Debug.LogWarning("PlayerMissing from scene.");
        endPos = Player.transform.position + camPos;
    }
	
	// Update is called once per frame
	void Update () {
        if ( direction != oldDiection)
        {
            startTime = Time.time;
            distCovered = 0.0f;
            startPos = transform.position;
        }

        //delete these if-statements when we have real directions
        if (direction == 1) //pz
        {

            camPos = new Vector3(0.0f, camHeight, -camDist);
            endPos = Player.transform.position + camPos;
            targetRotation = new Vector3(0, 0, 1);

        }
        if (direction == 2) //nz
        {
            camPos = new Vector3(0.0f, camHeight, camDist);
            endPos = Player.transform.position + camPos;
            targetRotation = new Vector3(0, 0, -1);
        }
        if (direction == 3) // px
        {
            camPos = new Vector3(-camDist, camHeight, 0);
            targetRotation = new Vector3(1, 0, 0);
            endPos = Player.transform.position + camPos;
        }
        if (direction == 4) // nx
        {
            camPos = new Vector3(camDist, camHeight, 0);
            targetRotation = new Vector3(-1, 0, 0);
            endPos = Player.transform.position + camPos;
        }
        journeyLength = Vector3.Distance(startPos, endPos);

        distCovered = distCovered + Time.deltaTime * movementSpeed;

        var fracJourney = distCovered / journeyLength;

        transform.position = Vector3.Lerp(transform.position, endPos, fracJourney);

        //create rotation
        Quaternion wantedRotation = Quaternion.LookRotation(targetRotation);

        //then rotate
        transform.rotation = Quaternion.Lerp(transform.rotation, wantedRotation, fracJourney);

        oldDiection = direction;
    }
}
