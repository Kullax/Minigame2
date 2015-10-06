using UnityEngine;
using System.Collections;

public class CornerRotation : MonoBehaviour {

    public bool rightTurn;

    void OnCollisionEnter ( Collision player)
    {
        DeflectPlayer(player.collider.attachedRigidbody, player.impulse);
        SetRotation();
    }

    private void SetRotation() {
        switch (GameManager.GameRotation) {
            case GameRotation.NegativeX:
                GameManager.SetGameRotation(rightTurn ? GameRotation.PositiveZ : GameRotation.NegativeZ);
                break;
            case GameRotation.PositiveX:
                GameManager.SetGameRotation(rightTurn ? GameRotation.NegativeZ : GameRotation.PositiveZ);
                break;
            case GameRotation.NegativeZ:
                GameManager.SetGameRotation(rightTurn ? GameRotation.PositiveX : GameRotation.NegativeX);
                break;
            case GameRotation.PositiveZ:
                GameManager.SetGameRotation(rightTurn ? GameRotation.NegativeX : GameRotation.PositiveX);
                break;
        }
    }

    private void DeflectPlayer(Rigidbody rb, Vector3 impulse) {
        // We are currently looking down the direction we want to push.
        var forward = GameManager.GameRotation.GetForward();
        // We redirect the full impulse towards our desired direction
        rb.AddForce(impulse.magnitude * forward, ForceMode.Impulse);
    }
}
