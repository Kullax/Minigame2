using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class GodMovable : MonoBehaviour
{
    public float DistanceReact = 1;

    public float HeldForce = 1;
    public ForceMode HeldForceMode = ForceMode.Force;

    public float TapForce = 1;
    public ForceMode TapForceMode = ForceMode.Impulse;

    [Header("Debug options")]
    public bool ShowDistanceInEditMode = true;
    public bool ShowDistanceInPlayMode = false;
    public bool ShowTouch = true;

    private GodPhase _phase;
    private Rigidbody _rb;
    private Vector3 _relativePosition = Vector3.zero;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _phase = GodTouch.Phase;
    }

    void Update()
    {
        _phase = GodTouch.Phase;

        _relativePosition = _phase != GodPhase.None ?
            transform.position - GodTouch.WorldPositionBegin :
            Vector3.zero;
    }

    void FixedUpdate()
    {
        if (_relativePosition == Vector3.zero || _relativePosition.magnitude > DistanceReact + transform.localScale.x / 2)
            return;

        var distMod = 1 - Mathf.Sqrt(_relativePosition.magnitude / (DistanceReact + transform.localScale.x / 2));

        if (_phase == GodPhase.Began)
            _rb.AddForce(_relativePosition * distMod * TapForce, TapForceMode);

        if (_phase == GodPhase.Held)
            _rb.AddForce(_relativePosition * distMod * HeldForce, HeldForceMode);
    }

    private void DrawDebugDistance()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, DistanceReact + transform.localScale.x / 2);
    }

    private void DrawDebugTouch()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(GodTouch.WorldPositionBegin, DistanceReact);
    }

    void OnDrawGizmos()
    {
        if (!Application.isEditor)
            return;

        if (Application.isPlaying)
        {
            if (ShowTouch && _phase != GodPhase.None)
                DrawDebugTouch();

            if (ShowDistanceInPlayMode)
                DrawDebugDistance();
        }

        if (!Application.isPlaying)
        {
            if (ShowDistanceInEditMode)
                DrawDebugDistance();
        }
    }
}