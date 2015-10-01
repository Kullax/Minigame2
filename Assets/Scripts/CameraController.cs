using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public float camHeight = 1.0f;
    public float camDist = 5.0f;
    public float movementSpeed = 1.0f;

    private Vector3 camPos;
    private float journeyLength;
    private GameRotation oldDirection;
    private float startTime;
    private float distCovered = 0;
    private Vector3 startPos, endPos;

    void Start () {
	    camPos = new Vector3(0.0f, camHeight, -camDist);
        startPos = transform.position;
    }

    private void StartNewJourney ()
    {
        startTime = Time.time;
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

    private void Travel() {
        var fracJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(transform.position, endPos, fracJourney);

        Quaternion wantedRotation = GameManager.GameRotation.AsWorldQuaternion();
        transform.rotation = Quaternion.Lerp(transform.rotation, wantedRotation, fracJourney);
    }
    
	void LateUpdate () {
        var currentPlayer = GameManager.CurrentPlayer;
        if (!currentPlayer)
            return;

        CalculateJourney(currentPlayer);
        Travel();
 
        oldDirection = GameManager.GameRotation;
    }
}