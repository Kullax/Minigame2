using UnityEngine;

[RequireComponent(typeof(GodMovable))]
[RequireComponent(typeof(CubeScale))]

public class MeltSpeeder : MonoBehaviour
{
    public float meltMaximalVelocity;

    private GodMovable _gm;
    private CubeScale _cs;
    private float originalMaximalVelocity;

    private CubeScale.Status status = CubeScale.Status.None;
    // Use this for initialization
    void Awake()
    {
        _gm = GetComponent<GodMovable>();
        _cs = GetComponent<CubeScale>();

        originalMaximalVelocity = _gm.MaximalVelocity;
    }
    
    void Update() {
        if (_cs.status == status)
            return;

        status = _cs.status;
        if (status == CubeScale.Status.Melting)
            _gm.MaximalVelocity = meltMaximalVelocity;
        if (status == CubeScale.Status.None)
            _gm.MaximalVelocity = originalMaximalVelocity;
    }
}
