using UnityEngine;

public class GodMovable : MonoBehaviour
{
    public float distanceReact = 1;

    public float heldForce = 1;
    public ForceMode heldForceMode = ForceMode.Force;

    public float tapForce = 1;
    public ForceMode tapForceMode = ForceMode.Impulse;

    private GodPhase phase;
    private Rigidbody rb;
    private Vector3 relativePosition = Vector3.zero;

    void Awake ()
    {
        rb = GetComponent<Rigidbody> ();
        phase = GodTouch.phase;
    }

    void Update ()
    {
        phase = GodTouch.phase;

        relativePosition = phase != GodPhase.None ?
            transform.position - GodTouch.worldPositionBegin :
            Vector3.zero;
    }

	void FixedUpdate ()
    {
        if (relativePosition == Vector3.zero || relativePosition.magnitude > distanceReact + transform.localScale.x)
            return;

        var distMod = 1 - Mathf.Sqrt(relativePosition.magnitude / (distanceReact + transform.localScale.x));
        
        if (phase == GodPhase.Began)
            rb.AddForce(relativePosition * distMod * tapForce, tapForceMode);

        if (phase == GodPhase.Held)
            rb.AddForce(relativePosition * distMod * heldForce, heldForceMode);
	}

    void OnDrawGizmos()
    {
        if (!Application.isPlaying || phase == GodPhase.None)
            return;

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(GodTouch.worldPositionBegin, distanceReact);
    }
}