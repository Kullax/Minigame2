using UnityEngine;

public class CheckpointManager : MonoBehaviour {

    public GameObject InitialPlayerPrefab;
    public GameObject InitialSpawnPoint;

    [Header("Debug options")]
    public bool ShowActiveCheckpoint = true;

    private static GameObject _prefab;
    private static Checkpoint _activeCheckpoint;

    private bool sanity ()
    {
        if (GameObject.FindObjectsOfType<CheckpointManager>().Length != 1)
        {
            Debug.LogError("Multiple checkpoint managers in scene.");
            return false;
        }
        
        _prefab = InitialPlayerPrefab;
        if (!_prefab)
        {
            Debug.LogError("No player prefab set on checkpoint manager.");
            return false;
        }
        
        if (!InitialSpawnPoint)
        {
            Debug.LogError("No spawnpoint set on checkpoint manager.");
            return false;
        }

        _activeCheckpoint = InitialSpawnPoint.GetComponent<Checkpoint> ();
        if (!_activeCheckpoint)
        {
            Debug.LogError("Invalid spawnpoint set on checkpoint manager - no attached Checkpoint script.");
            return false;
        }

        return true;
    }

	void Awake () {
        // If we can't pass sanity checks we disable ourselves and do nothing.
        if (!sanity())
        {
            enabled = false;
            return;
        }
	}

    public static GameObject SpawnPlayer()
    {
        if (!_activeCheckpoint)
        {
            Debug.LogError("Spawn failed! No active checkpoint. Also, this should not happen, are you sure you have a working Checkpoint Manager in the scene?");
            return null;
        }

        return _activeCheckpoint.Spawn(_prefab);
    }

    public static void SetActiveCheckpoint(Checkpoint checkpoint)
    {
        _activeCheckpoint = checkpoint;
    }

    public static void SetActiveCheckpoint(GameObject checkpoint)
    {
        var checkpointScript = checkpoint.GetComponent<Checkpoint>();
        if (!checkpointScript)
        {
            Debug.LogError("Invalid checkpoint - no attached Checkpoint script.");
            return;
        }

        SetActiveCheckpoint(checkpointScript);
    }

    void OnDrawGizmos()
    {
        if (ShowActiveCheckpoint && Application.isPlaying && _activeCheckpoint)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawSphere(_activeCheckpoint.transform.position, 0.25f);
        }
    }
}