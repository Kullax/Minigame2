using UnityEngine;
using System.Collections;

public class PipeCollider : MonoBehaviour {

    public CubeScale.Status effect;
    
    void OnTriggerStay(Collider cube)
    {
        Debug.Log("ya");
        if (!cube.tag.Equals("Player"))
            return;
        cube.SendMessage("Effect", effect);
    }

}
