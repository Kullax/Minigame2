using UnityEngine;
using System.Collections;

public class CornerRotation : MonoBehaviour {

    public bool rightTurn;

    private Vector3 lastForward;

    void OnCollisionEnter ( Collision player) {
        lastForward = GameManager.GameRotation.GetForward();
        DeflectPlayer(player.rigidbody, player.impulse, lastForward);
        SetRotation();
    }

    void OnCollisionStay ( Collision player ) {
        DeflectPlayer(player.rigidbody, 25, lastForward, ForceMode.Acceleration);
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

    private void DeflectPlayer(Rigidbody rb, Vector3 strength, Vector3 direction, ForceMode mode = ForceMode.Impulse) {
        DeflectPlayer(rb, strength.magnitude, direction, mode);
    }

    private void DeflectPlayer(Rigidbody rb, float strength, Vector3 direction, ForceMode mode = ForceMode.Impulse)
    {
        rb.AddForce(strength * direction.normalized, mode);
    }
}
