using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{

    public GameObject SpawnGO,
        StartGO,
        EndGO;


    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag != "Player")
            return;

        Instantiate(SpawnGO, StartGO.transform.position, StartGO.transform.rotation);

        SetTarget tmp = FindObjectOfType<SetTarget>();
        tmp.GetComponent<SetTarget>().EndTarget(EndGO.transform.position);

        Destroy(this.gameObject);
    }
}
