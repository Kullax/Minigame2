using UnityEngine;

public class Checkpoint : MonoBehaviour {
    [Header("Debug options")]
    public bool DrawCheckpointInEditMode = true;
    public bool DrawCheckpointInPlayMode = false;

    public void Spawn(GameObject prefab)
    {
        var obj = Instantiate(prefab, transform.position, transform.rotation);
    }

    public void SetThisAsActiveCheckpoint()
    {
        CheckpointManager.SetActiveCheckpoint(this);
    }

    void OnDrawGizmos()
    {
        if (!Application.isEditor)
            return;

        if (DrawCheckpointInEditMode && !Application.isPlaying || DrawCheckpointInPlayMode && Application.isPlaying)
        {
            Gizmos.color = Color.red;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
            Gizmos.DrawFrustum(Vector3.zero, 15, 2, 1, 1);
        }
    }
}