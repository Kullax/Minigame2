using UnityEngine;
using System.Collections;

public class CheckpointCollisionTrigger : MonoBehaviour {

    public GameObject checkpointToTrigger;

    private Checkpoint _checkpoint;

    private bool Sanity()
    {
        if (!checkpointToTrigger)
        {
            Debug.LogError("Checkpoint to trigger not set on checkpoint.");
            return false;
        }

        _checkpoint = checkpointToTrigger.GetComponent<Checkpoint>();
        if (!_checkpoint)
        {
            Debug.LogError("Invalid checkpoint to trigger set on checkpoint - no attached checkpoint script.");
            return false;
        }

        return true;
    }

    void Start()
    {
        if (!Sanity())
            enabled = false;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag != "Player")
            return;
        
        _checkpoint.SetThisAsActiveCheckpoint();
    }
}