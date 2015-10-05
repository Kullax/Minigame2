using UnityEngine;
using System.Collections;

public class CheckpointCollisionTrigger : MonoBehaviour {

    public GameObject checkpointToTrigger;
    public bool Activated = true;
    public GameObject ToggleLever;

    private Checkpoint _checkpoint;

    private bool Sanity() {
        if (!checkpointToTrigger) {
            Debug.LogError("Checkpoint to trigger not set on checkpoint.");
            return false;
        }

        _checkpoint = checkpointToTrigger.GetComponent<Checkpoint>();
        if (!_checkpoint) {
            Debug.LogError("Invalid checkpoint to trigger set on checkpoint - no attached checkpoint script.");
            return false;
        }

        if (ToggleLever) {
            var clickScript = ToggleLever.GetComponent<ClickScript>();
            if (!clickScript) {
                Debug.LogError("Invalid toggle lever set on checkpoint collision trigger - no attached click script.");
                return false;
            }
            clickScript.RegisterToggle(() => Activated = !Activated);
        }

        return true;
    }

    void Start() {
        if (!Sanity())
            enabled = false;
    }

    void OnTriggerEnter(Collider collider) {
        if (!Activated || collider.tag != "Player")
            return;
        
        _checkpoint.SetThisAsActiveCheckpoint();
    }
}