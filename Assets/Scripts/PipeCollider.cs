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

    public void Activate()
    {
        active = !active;
    }

    public void MakeCold()
    {
        effect = CubeScale.Status.Freezing;
    }

    public void MakeHot()
    {
        effect = CubeScale.Status.Melting;
    }

}
