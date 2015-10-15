using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameRotation SpawnRotation;

    [Header("Debug options")]
    public bool DrawCheckpointInEditMode = true;
    public bool DrawCheckpointInPlayMode = false;

    public GameObject Spawn(GameObject prefab)
    {
        GameManager.SetGameRotation(SpawnRotation);

        return Instantiate(prefab, transform.position, SpawnRotation.AsReverseWorldQuaternion()) as GameObject;
    }

    public GameObject PlaceCubeRB(GameObject prefab)
    {
        GameManager.SetGameRotation(SpawnRotation);
        
        prefab.transform.position = this.gameObject.transform.position;
        prefab.transform.rotation = SpawnRotation.AsReverseWorldQuaternion();
        return prefab;
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
            Gizmos.matrix = Matrix4x4.TRS(transform.position, SpawnRotation.AsReverseWorldQuaternion(), Vector3.one);
            Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
            Gizmos.DrawFrustum(Vector3.zero, 15, 2, 1, 1);
        }
    }
}