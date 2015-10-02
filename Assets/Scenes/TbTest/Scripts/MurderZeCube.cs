using UnityEngine;
using System.Collections;

public class MurderZeCube : MonoBehaviour {
    
	void Update () {

        if (Input.GetMouseButtonDown(1))
        {
            GameManager.RespawnPlayer();
            GameManager.ResetResettables();
        }
            
        if (Input.GetMouseButtonDown(2))
            GameManager.KillPlayer();
	}
}
