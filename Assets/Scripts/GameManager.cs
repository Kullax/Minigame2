using UnityEngine;
using System.Collections.Generic;

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

    public static Quaternion AsReverseWorldQuaternion(this GameRotation rotation) {
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
    private static Rigidbody _currentRigidbody;
    private static GameRotation _gameRotation;

    private static LinkedList<ResettableMonoBehaviour> _resettables;

    void Start() {
        if (!DontSpawnPlayerOnStart)
            RespawnPlayer();

        _gameRotation = InitialGameRotation;

        _resettables = new LinkedList<ResettableMonoBehaviour>();
        foreach (var resettable in FindObjectsOfType<ResettableMonoBehaviour>())
            _resettables.AddLast(resettable);
	}

    /// <summary>
    /// Reset all ResettableMonoBehaviours in the scene.
    /// </summary>
    /// <returns>Returns true if successful. If false the game manager is most likely not in the scene or otherwise faulty.</returns>
    public static bool ResetResettables () {
        if (_resettables == null)
            return false;

        foreach (var resettable in _resettables)
            resettable.ResetBehaviour();

        return true;
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

        var newRigidbody = newPlayer.GetComponent<Rigidbody>();
        if (!newRigidbody)
            return false;

        _currentRigidbody = newRigidbody;

        if (_currentPlayer)
            Destroy(_currentPlayer);

        _currentPlayer = newPlayer;
        EnforceRigidbodyConstraints();

        return true;
    }

    private static void EnforceRigidbodyConstraints() {
        switch (_gameRotation) {
            case GameRotation.PositiveX:
            case GameRotation.NegativeX:
                _currentRigidbody.constraints =
                    RigidbodyConstraints.FreezePositionX |
                    RigidbodyConstraints.FreezeRotationY |
                    RigidbodyConstraints.FreezeRotationZ;
                break;
            case GameRotation.PositiveZ:
            case GameRotation.NegativeZ:
                _currentRigidbody.constraints =
                    RigidbodyConstraints.FreezePositionZ |
                    RigidbodyConstraints.FreezeRotationY |
                    RigidbodyConstraints.FreezeRotationX;
                break;
        }
    }

    /// <summary>
    /// Sets a new rotation for the world.
    /// </summary>
    /// <param name="newRotation">The new rotation</param>
    /// <returns>Returns true if the rotation was set</returns>
    public static bool SetGameRotation(GameRotation newRotation) {
        _gameRotation = newRotation;

        if (_currentRigidbody)
            EnforceRigidbodyConstraints();

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