using UnityEngine;
using System.Collections;

public class ClickScript : MonoBehaviour {
    public GameObject target;
    public LayerMask mask;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
            {
                if (target)
                {
                    target.SendMessage("Activate");
                    Debug.Log("Hitting " + hit.collider.name);
                }
            }
        }
	}

}
