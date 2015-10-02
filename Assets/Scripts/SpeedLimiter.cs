using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CubeScale))]

public class SpeedLimiter : MonoBehaviour
{
    public float MaxFreezeSpeed = 8f;
    public float MaxMeltSpeed = 10f;

    private Rigidbody _rb;
    private CubeScale _cs;

    // Use this for initialization
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _cs = GetComponent<CubeScale>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckRBMaxSpeed();
    }

    private void CheckRBMaxSpeed()
    {
        float tmpSpeed = 0.0f;

        if (_cs.status == CubeScale.Status.Melting)
            tmpSpeed = MaxMeltSpeed;
        else
            tmpSpeed = MaxFreezeSpeed;

        // Limiting the rigidbody speed
        if (_rb.velocity.magnitude > tmpSpeed)
        {
            //Debug.Log("Limiting max speed to: " + tmpSpeed);
            _rb.velocity = _rb.velocity.normalized * tmpSpeed;
        }
    }
}
