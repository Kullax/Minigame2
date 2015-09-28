using UnityEngine;

public class GodMovable : MonoBehaviour
{
    public float DistanceReact = 1;

    public float HeldForce = 1;
    public ForceMode HeldForceMode = ForceMode.Force;

    public float TapForce = 1;
    public ForceMode TapForceMode = ForceMode.Impulse;

    private GodPhase _phase;
    private Rigidbody _rb;
    private Vector3 _relativePosition = Vector3.zero;

    void Awake ()
    {
        _rb = GetComponent<Rigidbody> ();
        _phase = GodTouch.Phase;
    }

    void Update ()
    {
        _phase = GodTouch.Phase;

        _relativePosition = _phase != GodPhase.None ?
            transform.position - GodTouch.WorldPositionBegin :
            Vector3.zero;
    }

	void FixedUpdate ()
    {
        if (_relativePosition == Vector3.zero || _relativePosition.magnitude > DistanceReact + transform.localScale.x)
            return;

        var distMod = 1 - Mathf.Sqrt(_relativePosition.magnitude / (DistanceReact + transform.localScale.x));
        
        if (_phase == GodPhase.Began)
            _rb.AddForce(_relativePosition * distMod * TapForce, TapForceMode);

        if (_phase == GodPhase.Held)
            _rb.AddForce(_relativePosition * distMod * HeldForce, HeldForceMode);
	}

    void OnDrawGizmos()
    {
        if (!Application.isPlaying || _phase == GodPhase.None)
            return;

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(GodTouch.WorldPositionBegin, DistanceReact);
    }
}