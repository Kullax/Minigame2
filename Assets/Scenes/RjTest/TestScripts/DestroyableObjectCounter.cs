using UnityEngine;
using System.Collections;
using System;

public class DestroyableObjectCounter : MonoBehaviour {

    private int clickedCounter = 0;
    private int newCounter;
    private int previousCounter;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        newCounter = clickedCounter;
        if (newCounter != previousCounter)
        {
            shake();
        }

        if (clickedCounter > 4)
        {
            Destroy(gameObject);
        }

        previousCounter = newCounter;
    }

    private void shake()
    {
        switch (GameManager.GameRotation)
        {
            case (GameRotation.NegativeX):
            case (GameRotation.PositiveX):

                transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);

                break;

            case (GameRotation.NegativeZ):
            case (GameRotation.PositiveZ):

                //transform.eulerAngles = new Vector3(Random.Range(-10.0f,10.0f), 0.0f, 0.0f);

                break;
        }
        transform.eulerAngles = new Vector3 (0,0,0);
    }

    public int IncrementClicked ()
    {
        return ++clickedCounter;
    }
}
