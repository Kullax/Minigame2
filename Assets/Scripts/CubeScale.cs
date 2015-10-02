﻿using UnityEngine;
using System.Collections;

public class CubeScale : MonoBehaviour {
    private float elapsedtime;
    private float epsilon = 0.05f;
    private float refreeze = 0.25f;
    private float speed;
    private Vector3 smelted = new Vector3(0, 0, 0);
    private Vector3 frozen = new Vector3(1, 1, 1);
    private Vector3 startstate;
    public Status status = Status.None;
    public float meltingspeed;
    public float lethallimit;
    private CameraSoundScript camerasound;

    public enum Status
    {
        None = 0,
        Melting = 1 << 1,
        Freezing = 1 << 2,
    }

    // Use this for initialization
    void Start () {
        elapsedtime = 0f;
        camerasound = Camera.main.GetComponent<CameraSoundScript>();
    }

    // Update is called once per frame
    void Update() {
        if (transform.localScale.x <= lethallimit)
        {
            GameManager.RespawnPlayer();
            GameManager.ResetResettables();
        }


        switch (status)
        {
            case Status.Melting:
                ScaleObject(smelted);
                break;
            case Status.Freezing:
                ScaleObject(frozen);
                break;
            default:
                break;
        }
    }

    void ScaleObject(Vector3 targetsize)
    {
        if (Vector3.Distance(transform.localScale, targetsize) > epsilon)
        {
            elapsedtime += Time.deltaTime;
            float frac = elapsedtime / speed;
            transform.localScale = Vector3.Lerp(startstate, targetsize, frac);
        }
        else
        {
            transform.localScale = targetsize;
            elapsedtime = 0f;
            status = Status.None;
        }
    }

    public void Effect(Status effect)
    {
        if (effect == status)
            return;
        switch (effect)
        {
            case Status.Melting:
                startstate = transform.localScale;
                status = Status.Melting;
                elapsedtime = 0;
                speed = meltingspeed;
                if(camerasound)
                    camerasound.meltingmelody();
                break;
            case Status.Freezing:
                if (status != Status.Melting)
                    return;
                startstate = transform.localScale;
                status = Status.Freezing;
                elapsedtime = 0;
                speed = refreeze;
                if(camerasound)
                    camerasound.normalmelody();
                break;
            default:
                break;
        }
    }

}