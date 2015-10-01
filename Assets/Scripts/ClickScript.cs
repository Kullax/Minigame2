using UnityEngine;
using System.Collections.Generic;

public class ClickScript : MonoBehaviour {
    public GameObject target;
    public IList<GameObject> targetList;
    private LayerMask mask;
    public CubeScale.Status status;


    // Use this for initialization
    void Start () {
        mask |= (1 << LayerMask.NameToLayer("Clickable"));
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask) && hit.transform.gameObject == gameObject)
            { 
                if (target)
                {
                    var Pipe = target.GetComponent<Pipe>();
                    var Door = target.GetComponent<DoorScript>();
                    if (Pipe)
                    {
                        Pipe.Activate();
                        switch (status)
                        {
                            case CubeScale.Status.Freezing:
                                Pipe.MakeCold();
                                break;
                            case CubeScale.Status.Melting:
                                Pipe.MakeHot();
                                break;
                            default:
                                break;
                        }
                    }
                    if (Door)
                    {
                        Door.Activate();
                    }
                }
            }
        }
	}
}