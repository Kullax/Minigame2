using UnityEngine;
using System.Collections.Generic;
using System;

public class ClickScript : MonoBehaviour {
    public GameObject target;
    public IList<GameObject> targetList;
    private LayerMask mask;
    public CubeScale.Status status;
    private AudioSource audioSource;
    private Pipe Pipe;
    private DoorScript Door;

    // Use this for initialization
    void Start () {
        mask |= (1 << LayerMask.NameToLayer("Clickable"));
		audioSource = GetComponent<AudioSource>();
        if (target)
        {
            Pipe = target.GetComponent<Pipe>();
            Door = target.GetComponent<DoorScript>();
        }
    }

    public void RegisterToggle(Action toggle) {
        if (null == _registeredToggles)
            _registeredToggles = new LinkedList<Action>();

        _registeredToggles.AddLast(toggle);
    }

    private void ToggleEverything() {
        if (null != _registeredToggles)
            foreach (var toggle in _registeredToggles)
                toggle();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask) && hit.transform.gameObject == gameObject)
            {
                ToggleEverything();
                
                if (Pipe)
                {
                    if(Pipe.isIdle())
                    {
                        Pipe.Activate();
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
                    }
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