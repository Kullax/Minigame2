using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class GodMovable : MonoBehaviour
{
    public float DistanceReact = 1;

    public float HeldForce = 1;
    public ForceMode HeldForceMode = ForceMode.Force;

    public float TapForce = 1;
    public ForceMode TapForceMode = ForceMode.Impulse;

    public float MaximalVelocity = 1;
    public float DampeningForce = 1;
    public ForceMode DampeningForceMode = ForceMode.Force;

    [Header("Debug options")]
    public bool ShowDistanceInEditMode = true;
    public bool ShowDistanceInPlayMode = false;
    public bool ShowTouch = true;

    private GodPhase _phase;
    private Rigidbody _rb;
    private Vector3 _relativePosition = Vector3.zero;
    private float _orgScale;
    private float _scaleMod { get { return DistanceReact + _orgScale / 2; } }
    private float _trueDistanceReact { get { return DistanceReact + transform.localScale.x / 2; } }

    void Awake() {
        _rb = GetComponent<Rigidbody>();
        _phase = GodTouch.Phase;

        // We set the scale mod based on the original localscale
        _orgScale = transform.localScale.x;
    }

    void Update() {
        _phase = GodTouch.Phase;

        _relativePosition = _phase != GodPhase.None ?
            transform.position - GodTouch.WorldPositionBegin :
            Vector3.zero;
    }

    void FixedUpdate() {
        ApplyForce();
        Damp();
    }

    void Damp() {
        var velocity = _rb.velocity;
        if (velocity.magnitude < MaximalVelocity)
            return;

        var direction = -velocity.normalized;
        _rb.AddForce(direction * DampeningForce, DampeningForceMode);
    }

    private void ApplyForce() {
        if (_relativePosition == Vector3.zero || _relativePosition.magnitude > _trueDistanceReact)
            return;
        
        var distMod = 1 - Mathf.Sqrt(_relativePosition.magnitude / _scaleMod);

        var forceDirection = RestrictDirection(_relativePosition);

        if (_phase == GodPhase.Began)
            _rb.AddForce(forceDirection * distMod * TapForce, TapForceMode);

        if (_phase == GodPhase.Held)
            _rb.AddForce(forceDirection * distMod * HeldForce, HeldForceMode);
    }

    private Vector3 RestrictDirection(Vector3 dir) {
        var velocity = _rb.velocity;
        if (velocity.magnitude < MaximalVelocity)
            return dir;

        var absX = Mathf.Abs(velocity.x);
        var absY = Mathf.Abs(velocity.y);
        var absZ = Mathf.Abs(velocity.z);

        // We're moving too fast, so we need to restrict an axis of movement.
        if (absX > absZ && absX > absY) {
            var x = CapAxis(velocity.x, dir.x);
            return new Vector3(x, dir.y, dir.z);
        } if (absZ > absY) {
            var z = CapAxis(velocity.z, dir.z);
            return new Vector3(dir.x, dir.y, z);
        } else {
            var y = CapAxis(velocity.y, dir.y);
            return new Vector3(dir.x, y, dir.z);
        }
    }

    private float CapAxis(float velocity, float direction) {
        return velocity * direction < 0 ? direction : 0;
    }

    private void DrawDebugDistance() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, DistanceReact + transform.localScale.x / 2);
    }

    private void DrawDebugTouch() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(GodTouch.WorldPositionBegin, DistanceReact);
    }

    void OnDrawGizmos() {
        if (!Application.isEditor)
            return;

        if (Application.isPlaying) {
            if (ShowTouch && _phase != GodPhase.None)
                DrawDebugTouch();

            if (ShowDistanceInPlayMode)
                DrawDebugDistance();
        }

        if (!Application.isPlaying) {
            if (ShowDistanceInEditMode)
                DrawDebugDistance();
        }
    }
}