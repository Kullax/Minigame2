using UnityEngine;

public class CheckpointManager : MonoBehaviour {

    public GameObject InitialPlayerPrefab;
    public GameObject InitialSpawnPoint;

    [Header("Debug options")]
    public bool SpawnInitialPlayer = true;

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

	void Start () {
        // If we can't pass sanity checks we disable ourselves and do nothing.
        if (!sanity())
        {
            enabled = false;
            return;
        }

        // Spawn initial player
        if (SpawnInitialPlayer)
            SpawnPlayer();
	}

    public static void SpawnPlayer()
    {
        if (!_activeCheckpoint)
        {
            Debug.LogError("Spawn failed! No active checkpoint. Also, this should not happen.");
            return;
        }

        _activeCheckpoint.Spawn(_prefab);
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
}