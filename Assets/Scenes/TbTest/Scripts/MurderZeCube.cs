using UnityEngine;
using System.Collections;

public class MurderZeCube : MonoBehaviour {
    
	void Update () {
        if (!Input.GetMouseButtonDown(1))
            return;

        // GET ZE CUBE
        var cube = GameObject.FindGameObjectWithTag("Player");

        // MURDER ZE CUBE!!
        Destroy(cube);

        // Resurrect ze cube... AND KILL IT AGAIN!!! (later... maybe...)
        CheckpointManager.SpawnPlayer();
	}
}
