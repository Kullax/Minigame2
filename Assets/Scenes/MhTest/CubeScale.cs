using UnityEngine;
using System.Collections;

public class CubeScale : MonoBehaviour {
    private float elapsedtime;
    private float epsilon = 0.05f;
    private float refreeze = 0.5f;
    private float speed;
    private Vector3 smelted = new Vector3(0, 0, 0);
    private Vector3 frozen = new Vector3(1, 1, 1);

    //    public bool melting = false;
    public Status status = Status.None;

 public float meltingspeed;
// public float size;

    public enum Status
    {
        None = 0,
        Melting = 1 << 1,
        Freezing = 1 << 2,
    }

    // Use this for initialization
    void Start () {
        elapsedtime = 0f;
    }

    // Update is called once per frame
    void Update() {
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
            transform.localScale = Vector3.Lerp(transform.localScale, targetsize, frac);
        }
        else
        {
            transform.localScale = targetsize;
            elapsedtime = 0f;
            status = Status.None;
        }


    }

    void Effect(Status effect)
    {
        if (effect == status)
            return;
        switch (effect)
        {
            case Status.Melting:
                status = Status.Melting;
                elapsedtime = 0;
                speed = meltingspeed;
                break;
            case Status.Freezing:
                status = Status.Freezing;
                elapsedtime = 0;
                speed = refreeze;
                break;
            default:
                break;
        }
    }

}