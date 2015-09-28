using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClickScript : MonoBehaviour {
    public GameObject target;
    public IList<GameObject> targetList;
    private LayerMask mask;

    public enum Effect
    {
        Activate = 1 << 0,
        ActivateHold = 1 << 1,
        TurnCold = 1 << 2,
        TurnHot  = 1 << 3,
    }

    public Effect effect;

    // Use this for initialization
    void Start () {
        mask |= (1 << LayerMask.NameToLayer("Clickable"));
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0) && effect == Effect.ActivateHold)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask) && hit.transform.gameObject == gameObject)
            { 
                if (target)
                {
                    switch (effect)
                    {
                        case Effect.Activate:
                            target.SendMessage("Activate");
                            break;
                        case Effect.ActivateHold:
                            target.SendMessage("Activate");
                            break;
                        case Effect.TurnCold:
                            target.SendMessage("MakeCold");
                            break;
                        case Effect.TurnHot:
                            target.SendMessage("MakeHot");
                            break;
                        default:
                            break;
                    }
            }
            }
        }
	}


}
