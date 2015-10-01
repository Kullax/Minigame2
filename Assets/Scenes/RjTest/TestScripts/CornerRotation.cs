using UnityEngine;
using System.Collections;

public class CornerRotation : MonoBehaviour {

    public bool rightTurn;
    public int direction; // shoul be the direction in which the player is moving

    private bool reset = true;

    // Use this for initialization
    void Start () {
    }
	
    void OnTriggerExit ( Collider player)
    {
        if (player.tag != "Player")
        {
            return;
        }
        reset = true;
    }

    void OnTriggerStay ( Collider player)
    {


        if (player.tag != "Player")
        {
            return;
        }

        var rb = player.GetComponent<Rigidbody>();

        if (Vector3.Distance(player.transform.position, transform.position) < 0.05f && reset) {
            reset = false;
            if (direction == 1 || direction == 2) // px and nx
            {
                player.transform.Rotate(rightTurn ? new Vector3(0, -90, 0) : new Vector3(0, 90, 0));
                if (rb)
                {
                    rb.constraints = RigidbodyConstraints.None;
                    if (rightTurn)
                    {
                        rb.constraints = RigidbodyConstraints.FreezePositionZ;
                    } else {
                        rb.constraints = RigidbodyConstraints.FreezePositionX;
                    }
                }
            }
            if (direction == 3 || direction == 4) // pz and nz
            {
                player.transform.Rotate(rightTurn ? new Vector3(0, 90, 0) : new Vector3(0, -90, 0));
                if (rb)
                {
                    rb.constraints = RigidbodyConstraints.None;
                    if (rightTurn)
                    {
                        rb.constraints = RigidbodyConstraints.FreezePositionX;
                    } else {
                        rb.constraints = RigidbodyConstraints.FreezePositionZ;
                    }
                }
            }
            //player.transform.position = transform.position;
        }
    }

	// Update is called once per frame
	void Update () {

	}
}
