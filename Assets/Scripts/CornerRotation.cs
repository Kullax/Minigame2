using UnityEngine;
using System.Collections;

public class CornerRotation : MonoBehaviour {

    public bool rightTurn;

    private bool reset = true;

    // Use this for initialization
    void Start () {
    }
	
    void OnCollisionEnter ( Collision player)
    {
        /* (player.tag != "Player")
        {
            return;
        }*/

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

	// Update is called once per frame
	void Update () {

	}
}
