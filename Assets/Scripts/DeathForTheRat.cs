using UnityEngine;
using System.Collections;

public class DeathForTheRat : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Rat")
        {
            //Debug.Log("Rat point reached and destroyed!");
            Destroy(col.gameObject);
            Destroy(this.gameObject);
        }
    }
}
