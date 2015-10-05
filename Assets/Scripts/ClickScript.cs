using UnityEngine;
using System.Collections.Generic;

public class ClickScript : MonoBehaviour {
    public GameObject target;
    public IList<GameObject> targetList;
    private LayerMask mask;
    public CubeScale.Status status;
    private AudioSource audioSource;

    // Use this for initialization
    void Start () {
        mask |= (1 << LayerMask.NameToLayer("Clickable"));
		audioSource = GetComponent<AudioSource>();
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
                        // PLay Sound
						if (audioSource)
							audioSource.Play();
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
                        Pipe.Activate();

                    }
                    if (Door)
                    {
                        // PLay Sound
						if (audioSource)
							audioSource.Play();
                        Door.Activate();
                    }
                }
            }
        }
	}
}