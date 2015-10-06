using UnityEngine;
using System.Collections;

public class DestroyObjectOnClick : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    
	}

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray.origin, ray.direction,  out hit, 100))
            {
                if (hit.collider.gameObject.tag == "Destroyable")
                {
                    var destroyableCounter = hit.collider.gameObject.GetComponent<DestroyableObjectCounter>();
                    destroyableCounter.PlayNastySound();
                    destroyableCounter.IncrementClicked();
                    
                    //Destroy(hit.collider.gameObject);
                }
            }
        }
    }
}
