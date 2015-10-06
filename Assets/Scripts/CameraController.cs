using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public float camHeight = 1.0f;
    public float camDist = 5.0f;
    public float movementSpeed = 1.0f;
    public float cameraTilt = 5.0f;

    //private Vector3 offset;
    private float journeyLength;
    private GameRotation oldDirection;
    //private float startTime;
    private float distCovered = 0;
    private Vector3 startPos, endPos;
    private Vector3 camPos;

    void Start () {
	    //offset = new Vector3(0.0f, camHeight, -camDist);
        startPos = transform.position;
    }

    
    private void StartNewJourney ()
    {
        //startTime = Time.time;
        distCovered = 0.0f;
        startPos = transform.position;
    }

    private void CalculateJourney (GameObject player) {
        if (GameManager.GameRotation != oldDirection)
            StartNewJourney();

        switch (GameManager.GameRotation) {
            case GameRotation.PositiveZ:
                camPos = new Vector3(0.0f, camHeight, -camDist);
                endPos = player.transform.position + camPos;
                break;
            case GameRotation.NegativeZ:
                camPos = new Vector3(0.0f, camHeight, camDist);
                endPos = player.transform.position + camPos;
                break;
            case GameRotation.PositiveX:
                camPos = new Vector3(-camDist, camHeight, 0);
                endPos = player.transform.position + camPos;
                break;
            case GameRotation.NegativeX:
                camPos = new Vector3(camDist, camHeight, 0);
                endPos = player.transform.position + camPos;
                break;
        }

        journeyLength = Vector3.Distance(startPos, endPos);

        distCovered = distCovered + Time.deltaTime * movementSpeed;
    }

    private void RotationTravel() {
        var fracJourney = distCovered / journeyLength;
        
        Quaternion wantedRotation = GameManager.GameRotation.AsWorldQuaternion();
        transform.rotation = Quaternion.Lerp(transform.rotation, wantedRotation, fracJourney);
    }

    private void PositionTravel()
    {
        var fracJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(transform.position, endPos, fracJourney);

    }

    void LateUpdate () {
        var currentPlayer = GameManager.CurrentPlayer;
        if (!currentPlayer)
            return;

        CalculateJourney(currentPlayer);
        if (distCovered / journeyLength < 0.9f)
        {
            PositionTravel();
        } else {
            transform.position = endPos;
        }
        RotationTravel();

        transform.eulerAngles = new Vector3(-cameraTilt, transform.eulerAngles.y, transform.eulerAngles.z);

        oldDirection = GameManager.GameRotation;
    }
}