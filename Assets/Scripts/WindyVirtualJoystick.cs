using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CubeScale))]

public class WindyVirtualJoystick : MonoBehaviour
{
    public float DeadZoneSize = 0.5f;

    public float WindStrength = 2000;

    public float MaximalVelocityWhileFrozen = 4.8f;
    public float MaximalVelocityWhileMelting = 12.4f;

    public float TurnSpeedFactor = 1f;

    private Rigidbody _rigidbody;
    private float _maximalVelocity;
    private CubeScale _cubeScale;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _cubeScale = GetComponent<CubeScale>();
    }
    private static Vector3 EliminateY (Vector3 v) { return new Vector3 (v.x, 0, v.z); }

    private Vector3 RestrictDirection(Vector3 dir)
    {
        var x = _rigidbody.velocity.x;
        var z = _rigidbody.velocity.z;
        var magnitude = (new Vector2(x, z)).magnitude;

        if (magnitude < _maximalVelocity)
            return dir;

        // We're moving too fast in the z direction, so we eliminate that axis
        if (Mathf.Abs(x) < Mathf.Abs(z) && dir.z * z > 0)
            return new Vector3(dir.x, dir.y, 0);

        // We're moving too fast in the x direction, so we eliminate that axis
        if (Mathf.Abs(z) < Mathf.Abs(x) && dir.x * x > 0)
            return new Vector3(0, dir.y, dir.z);

        // We're not moving fast, we're falling fast (or flying, but that shouldn't happen)!
        return dir;
    }

    private Vector3 TurnSpeedControl(Vector3 dir)
    {
        var xVel = _rigidbody.velocity.x;
        var zVel = _rigidbody.velocity.z;

        var x = dir.x;
        var z = dir.z;

        if (dir.x*xVel < 0)
            x *= TurnSpeedFactor;

        if (dir.z*zVel < 0)
            z *= TurnSpeedFactor;

        return new Vector3(x, dir.y, z);
    }

	void Update ()
	{
	    switch (_cubeScale.status)
	    {
            case CubeScale.Status.None:
	        case CubeScale.Status.Freezing:
	            _maximalVelocity = MaximalVelocityWhileFrozen;
	            break;
            case CubeScale.Status.Melting:
	            _maximalVelocity = MaximalVelocityWhileMelting;
	            break;
	    }

	    if (GodTouch.Phase != GodPhase.Held)
	        return;

	    var begin = EliminateY(GodTouch.WorldPositionBegin);
	    var held = EliminateY(GodTouch.WorldPositionHeld);
	    var relative = held - begin;

	    if (relative.magnitude < DeadZoneSize)
	        return;

	    var windDirection = RestrictDirection(relative).normalized;
	    var windForce = TurnSpeedControl(windDirection*WindStrength);

        _rigidbody.AddForce(windForce * Time.fixedDeltaTime, ForceMode.Force);
	}
}
