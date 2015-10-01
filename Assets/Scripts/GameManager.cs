using UnityEngine;

public enum GameRotation
{
    NegativeZ,
    PositiveZ,
    NegativeX,
    PositiveX
}

public static class GameRotationMethods {

    public static Vector3 GetForward(this GameRotation rotation) {
        switch (rotation) {
            case GameRotation.NegativeX:
                return Vector3.left;
            case GameRotation.PositiveX:
                return -Vector3.left;
            case GameRotation.NegativeZ:
                return -Vector3.forward;
            case GameRotation.PositiveZ:
                return Vector3.forward;
        }
        return Vector3.zero;
    }
    
    public static Quaternion AsWorldQuaternion(this GameRotation rotation) {
        return Quaternion.LookRotation(rotation.GetForward(), Vector3.up);
    }

    public static Quaternion AsReverseWorldQuaternion(this GameRotation rotation)
    {
        return Quaternion.LookRotation(-rotation.GetForward(), Vector3.up);
    }
}

public class GameManager : MonoBehaviour {

    public GameRotation InitialGameRotation = GameRotation.PositiveZ;

    public static GameObject CurrentPlayer { get { return _currentPlayer; } }
    public static GameRotation GameRotation { get { return _gameRotation; } }

    [Header("Debug options")]
    public bool DontSpawnPlayerOnStart = false;

    private static GameObject _currentPlayer;
    private static GameRotation _gameRotation;

    void Start() {
        if (!DontSpawnPlayerOnStart)
            _currentPlayer = CheckpointManager.SpawnPlayer();

        _gameRotation = InitialGameRotation;
	}

    /// <summary>
    /// Destroys the current player, if any, and spawns a new one from the currently active checkpoint.
    /// If a new player cannot be spawned, the current player will not be destroyed.
    /// </summary>
    /// <returns>True if a new player is spawned, otherwise false</returns>
    public static bool RespawnPlayer() {
        var newPlayer = CheckpointManager.SpawnPlayer();

        if (!newPlayer)
            return false;

        if (_currentPlayer)
            Destroy(_currentPlayer);

        _currentPlayer = newPlayer;

        return true;
    }

    /// <summary>
    /// Sets a new rotation for the world.
    /// </summary>
    /// <param name="newRotation">The new rotation</param>
    /// <returns>Returns true if the rotation was set</returns>
    public static bool SetGameRotation(GameRotation newRotation) {
        _gameRotation = newRotation;

        return true;
    }
    
    /// <summary>
    /// Destroys the current player. Note that the player may persist until the end of the current frame cycle.
    /// </summary>
    /// <returns>True if the current player is destroyed, false if there is no current player to destroy</returns>
    public static bool KillPlayer() {
        if (!_currentPlayer)
            return false;

        Destroy(_currentPlayer);
        return true;
    }
}