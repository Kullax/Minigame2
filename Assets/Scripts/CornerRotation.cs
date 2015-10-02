using UnityEngine;
using System.Collections;

public class CornerRotation : MonoBehaviour {

    public bool rightTurn;

    void OnCollisionEnter ( Collision player)
    {
        switch (GameManager.GameRotation) {
            case GameRotation.NegativeX:
                if (rightTurn)
                {
                    GameManager.SetGameRotation(GameRotation.PositiveZ);
                }
                else
                {
                    GameManager.SetGameRotation(GameRotation.NegativeZ);
                }
                break;
            case GameRotation.PositiveX:
                if (rightTurn)
                {
                    GameManager.SetGameRotation(GameRotation.NegativeZ);
                }
                else
                {
                    GameManager.SetGameRotation(GameRotation.PositiveZ);
                }
                break;
            case GameRotation.NegativeZ:
                if (rightTurn)
                {
                    GameManager.SetGameRotation(GameRotation.PositiveX);
                }
                else
                {
                    GameManager.SetGameRotation(GameRotation.NegativeX);
                }
                break;
            case GameRotation.PositiveZ:
                if (rightTurn)
                {
                    GameManager.SetGameRotation(GameRotation.NegativeX);
                }
                else
                {
                    GameManager.SetGameRotation(GameRotation.PositiveX);
                }
                break;
        }
    }
}
