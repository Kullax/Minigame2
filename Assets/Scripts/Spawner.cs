using UnityEngine;
using System.Collections;
using System;

public class Spawner : ResettableMonoBehaviour
{
    public GameObject SpawnGO, StartGO, EndGO;
    public bool Active = true;
    public float ratSpeed = 10.0f;


    private bool initiallyActive;

    void Start()
    {
        initiallyActive = Active;
    }

    public override void ResetBehaviour()
    {
        Active = initiallyActive;
    }


    void OnTriggerEnter(Collider collider)
    {
        if (!Active || collider.tag != "Player")
            return;

        Active = !Active;

        Instantiate(SpawnGO, StartGO.transform.position, StartGO.transform.rotation);

        SetTarget tmp = FindObjectOfType<SetTarget>();
        tmp.EndTarget(EndGO.transform.position);
        tmp.speed = ratSpeed;
    }
}