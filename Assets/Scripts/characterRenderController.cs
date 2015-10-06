using UnityEngine;
using System.Collections;

public class characterRenderController : MonoBehaviour {

    private Camera MainCamera;
    private GameObject PlayerCube;

    private float xRotateVelocity = 0f;
    private float zRotateVelocity = 0f;
    private float rotateTime = 0.35f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate () {
        PlayerCube = GameManager.CurrentPlayer;
        MainCamera = Camera.main;

        //positions the rendered cube on the rigidbody.
        transform.localScale = PlayerCube.transform.localScale;
        
        //Makes the rendered image look into the camera.
        float xRot, yRot, zRot;
        yRot = MainCamera.transform.eulerAngles.y + 180;
        RaycastHit hit;
        if (Physics.SphereCast(PlayerCube.transform.position, 0.2f, Vector3.down, out hit)) {
            var quat = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            xRot = quat.eulerAngles.x;
            zRot = quat.eulerAngles.z;
        } else {
            xRot = 0;
            zRot = 0;
        }

        xRot = Mathf.SmoothDampAngle(transform.eulerAngles.x, xRot, ref xRotateVelocity, rotateTime);
        zRot = Mathf.SmoothDampAngle(transform.eulerAngles.z, zRot, ref zRotateVelocity, rotateTime);

        var euler = new Vector3(xRot, yRot, zRot);
        transform.eulerAngles = euler;

        // Correct pivot point to be center
        var pos = PlayerCube.transform.position;
        var offsetDistance = Quaternion.Euler(euler) * new Vector3(0, PlayerCube.transform.localScale.y / 2, 0);

        transform.position = pos - offsetDistance;
    }
}