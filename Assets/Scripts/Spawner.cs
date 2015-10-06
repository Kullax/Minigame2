using UnityEngine;
using System.Collections;
using System;

public class Spawner : ResettableMonoBehaviour
{
    public GameObject SpawnGO, StartGO, EndGO;
    public bool Active = true;

    private bool initiallyActive;
    
    void Start () {
        initiallyActive = Active;
    }    

    public override void ResetBehaviour() {
        Active = initiallyActive;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (!Active || collider.tag != "Player")
            return;

        Active = !Active;

        Instantiate(SpawnGO, StartGO.transform.position, StartGO.transform.rotation);

        SetTarget tmp = FindObjectOfType<SetTarget>();
        tmp.GetComponent<SetTarget>().EndTarget(EndGO.transform.position);
    }
}