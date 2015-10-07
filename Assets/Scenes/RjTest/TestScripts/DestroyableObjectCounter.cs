using UnityEngine;
using System.Collections;
using System;

public class DestroyableObjectCounter : MonoBehaviour
{

    private int clickedCounter = 0;
    private int newCounter = 0;
    private int previousCounter = 0;
    // sound
    private AudioSource pipeHit;

    //shaking time
    private float ShakeTimeElapsed = 1.0f;
    private float ShakeTime = 1.0f;

    // Use this for initialization
    void Start()
    {
        pipeHit = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        newCounter = clickedCounter;

        // Did someone click to start shaking?
        if (newCounter != previousCounter)
        {
            ShakeTimeElapsed = 0;
        }

        //stop shaking? else shake!
        if (ShakeTimeElapsed > ShakeTime)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            shake();
        }
        ShakeTimeElapsed += Time.deltaTime;

        // shaking kills you, be carefull.
        if (clickedCounter > 4)
        {
            var delta = new Vector3(0, Mathf.Min(ShakeTimeElapsed, 3), 0);
            transform.localPosition -= delta;

            if (ShakeTimeElapsed > 100)
            {
                Destroy(gameObject);
            }
        }

        previousCounter = newCounter;
    }

    private void shake()
    {
        switch (GameManager.GameRotation)
        {
            case (GameRotation.NegativeZ):
            case (GameRotation.PositiveZ):
                transform.Rotate(0.0f, 0.0f, UnityEngine.Random.Range(-2.0f, 2.0f));
                var newZRotation = transform.rotation;
                transform.eulerAngles = new Vector3(0, 0, 0);
                transform.rotation = Quaternion.Lerp(transform.rotation, newZRotation, Time.time);

                break;

            case (GameRotation.NegativeX):
            case (GameRotation.PositiveX):
                transform.Rotate(UnityEngine.Random.Range(-2.0f, 2.0f), 0.0f, 0.0f);
                var newXRotation = transform.rotation;
                transform.eulerAngles = new Vector3(0, 0, 0);
                transform.rotation = Quaternion.Lerp(transform.rotation, newXRotation, Time.time);

                break;
        }
    }

    public int IncrementClicked()
    {
        return ++clickedCounter;
    }

    public void PlayNastySound()
    {
        pipeHit.Play();
    }
}
