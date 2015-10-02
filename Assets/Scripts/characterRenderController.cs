using UnityEngine;
using System.Collections;

public class characterRenderController : MonoBehaviour {

    private Camera MainCamera;
    private GameObject PlayerCube;
    private Vector3 newAngle;
    private Vector3 oldAngle;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void LateUpdate () {
        PlayerCube = GameManager.CurrentPlayer;
        MainCamera = Camera.main;

        //positions the rendered cube on the rigidbody.
        transform.localScale = PlayerCube.transform.localScale;
        Vector3 tmpVector = new Vector3(PlayerCube.transform.position.x, PlayerCube.transform.position.y - (PlayerCube.transform.localScale.y/2), PlayerCube.transform.position.z);
        transform.position = tmpVector;
       
        //Makes the rendered image look into the camera. 
        transform.rotation = MainCamera.transform.rotation;
        transform.Rotate(new Vector3 (0,180,0));

        //Tild the rendered image on basis of the rigidbody
        
       // oldAngle = transform.localRotation.eulerAngles;
       // newAngle = PlayerRB.transform.localRotation.eulerAngles;

        /*if (GameManager.GameRotation == GameRotation.PositiveZ || GameManager.GameRotation == GameRotation.NegativeZ)
        {
            if (PlayerCube.GetComponent<Rigidbody>().velocity.y < -0.1)
            {
                transform.Rotate(new Vector3(0, 0, PlayerCube.transform.rotation.eulerAngles.z));
            }
        }
        else
        {
            transform.Rotate(new Vector3(PlayerCube.transform.rotation.eulerAngles.z, 0, 0));
        }

        oldAngle = newAngle;*/
    }
}
