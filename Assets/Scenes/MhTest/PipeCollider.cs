using UnityEngine;
using System.Collections;

public class PipeCollider : MonoBehaviour {

    public bool active;
    public CubeScale.Status effect;
    
    void OnTriggerStay(Collider cube)
    {
        if (!active)
            return;
        if (!cube.tag.Equals("Player"))
            return;
        cube.SendMessage("Effect", effect);
    }

    void Activate()
    {
        active = !active;
    }

    void MakeCold()
    {
        effect = CubeScale.Status.Freezing;
    }

    void MakeHot()
    {
        effect = CubeScale.Status.Melting;
    }

}
