using UnityEngine;
using System.Collections;

public class FindCamerScript : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        if (!GetComponent<Canvas>().worldCamera)
        {
            GetComponent<Canvas>().worldCamera = Camera.main;
            return;
        }
    }
}
