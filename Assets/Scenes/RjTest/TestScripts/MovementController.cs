using UnityEngine;
using System.Collections;
using System.Linq;

public class MovementController : MonoBehaviour {

    public bool useMouse = false;
    public bool hold = true;
    public float movementSpeed = 1.0f;

    private Rigidbody cubeRigitBody;
    private Camera playerScreen;
    private Vector2 goTolocation;

    void Start()
    {
        playerScreen = Camera.main;
        cubeRigitBody = GetComponent<Rigidbody>();
        goTolocation = new Vector2(transform.position.x, transform.position.y);
    }

    void Update() {

        var cubeScreenPosition = new Vector2(playerScreen.WorldToScreenPoint(transform.position).x, playerScreen.WorldToScreenPoint(transform.position).y);
        var screenDestination = TargetDestination() ?? cubeScreenPosition;
        Debug.Log(screenDestination);
        var worldDestination = playerScreen.ScreenToWorldPoint(new Vector3(screenDestination.x, screenDestination.y, transform.position.z));
        Debug.Log(worldDestination);

        if (!hold) {
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, worldDestination.x, Time.time), transform.position.y, 0);
        }
        if (hold) {
            if (!(screenDestination == cubeScreenPosition))
            {

                goTolocation = screenDestination;

                if (screenDestination.x > Screen.width / 2 + 40 && screenDestination.y < cubeScreenPosition.y + 40 && screenDestination.y > cubeScreenPosition.y - 40)
                {
                    SlideRight();
                }
                else if (screenDestination.x < Screen.width / 2 - 40 && screenDestination.y < cubeScreenPosition.y + 40 && screenDestination.y > cubeScreenPosition.y - 40)
                {
                    SlideLeft();
                }
            }
        }
    }

    private Vector2? TargetDestination()
    {
        if (useMouse){
           if (hold){
                return MouseHoldPosition();
            }
            return SetMouseClickPosition();
        }
        if (hold){
            return TapHoldPosition();
        }
        return TapPosition();
    }

    private Vector2? SetMouseClickPosition() {
        if (Input.GetMouseButtonDown(0)){
            return new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
        }
        return null;
    }

    private Vector2? MouseHoldPosition() {
        if (Input.GetMouseButton(0))
        {
            return new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        return null;
    }
    // Implement Tapping
    private Vector2? TapPosition () {
        /*
        var tapPosition = Input.GetTouch (0);
        return tapPosition.position;
        */
        return null;
    }

    private Vector2? TapHoldPosition() {
        if (Input.touchCount <= 0)
            return null;

        var touch = Input.touches.First();
        
        if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved )
        {
            return new Vector2(touch.deltaPosition.x, touch.deltaPosition.y);
        }
        return null;
        
        /*
        var tapHoldPosition = Input.GetTouch(0);
        return tapHoldPosition.position;
        */
        return null;
    }

    private void SlideRight() {
        var y = cubeRigitBody.velocity.y;
        cubeRigitBody.velocity = new Vector3(0, y, 0);
        cubeRigitBody.AddForce(movementSpeed * MoveDirection());

    }

    private void SlideLeft() {
        var y = cubeRigitBody.velocity.y;
        cubeRigitBody.velocity = new Vector3(0, y, 0);
        cubeRigitBody.AddForce(movementSpeed * -MoveDirection());
    }

    private Vector3 MoveDirection() {
        var direction = new Vector3(1, 0, 0);
        return direction;
    }
}
