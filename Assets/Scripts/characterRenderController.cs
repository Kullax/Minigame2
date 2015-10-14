using UnityEngine;
using System.Collections;

public class characterRenderController : MonoBehaviour
{

    public float rotateTime = 0.35f;
    public float faceRotateTime = 0.45f;

    private Camera MainCamera;
    private GameObject PlayerCube = null;

    private float xRotateVelocity = 0f;
    private float yRotateVelocity = 0f;
    private float zRotateVelocity = 0f;
    private float airDistance = 0.43f;

    private Animator CAC;

    void Start()
    {
        CAC = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (PlayerCube == null)
            PlayerCube = GameObject.FindGameObjectWithTag("Player"); //GameManager.CurrentPlayer;

        MainCamera = Camera.main;

        //positions the rendered cube on the rigidbody.
        transform.localScale = PlayerCube.transform.localScale;

        //Makes the rendered image look into the camera.
        float xRot, yRot, zRot;

        RaycastHit hit;
        if (Physics.SphereCast(PlayerCube.transform.position, 0.2f, Vector3.down, out hit))
        {
            var quat = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            xRot = quat.eulerAngles.x;
            zRot = quat.eulerAngles.z;

            //Debug.Log("RayDistance: " + hit.distance);

            if (hit.distance <= airDistance)
                CAC.SetBool("OnGround", true);

            if (hit.distance > airDistance)
            {
                CAC.Play("LookDownIdle");
                CAC.SetBool("OnGround", false);
            }
        }
        else
        {
            xRot = 0;
            zRot = 0;
        }

        yRot = Quaternion.LookRotation(-GameManager.GameRotation.GetForward()).eulerAngles.y;

        xRot = Mathf.SmoothDampAngle(transform.eulerAngles.x, xRot, ref xRotateVelocity, rotateTime);
        yRot = Mathf.SmoothDampAngle(transform.eulerAngles.y, yRot, ref yRotateVelocity, faceRotateTime);
        zRot = Mathf.SmoothDampAngle(transform.eulerAngles.z, zRot, ref zRotateVelocity, rotateTime);

        var euler = new Vector3(xRot, yRot, zRot);
        transform.eulerAngles = euler;

        // Correct pivot point to be center
        var pos = PlayerCube.transform.position;
        var offsetDistance = Quaternion.Euler(euler) * new Vector3(0, PlayerCube.transform.localScale.y / 2, 0);

        transform.position = pos - offsetDistance;
    }
}