using UnityEngine;

public enum GodPhase
{
    None,
    Began,
    Held,
    End
}

public class GodTouch : MonoBehaviour {

    public float DistanceFromCamera = 5;

    private static GodPhase _phase = GodPhase.None;
    public static GodPhase Phase { get { return _phase; } }

    private static Vector3 _worldPositionBegin = Vector3.zero;
    public static Vector3 WorldPositionBegin { get { return _worldPositionBegin; } }

    private static Vector3 _worldPositionHeld = Vector3.zero;
    public static Vector3 WorldPositionHeld { get { return _worldPositionHeld; } }

    private static Vector3 _worldPositionEnd = Vector3.zero;
    public static Vector3 WorldPositionEnd { get { return _worldPositionEnd; } }

    private Camera _camera;
    private Vector3 _screenPositionBegin = Vector3.zero;
    private Vector3 _screenPositionHeld = Vector3.zero;
    private Vector3 _screenPositionEnd = Vector3.zero;

    void Awake () {
        _camera = Camera.main;
	}
	
    private void SetScreenPosition ()
    {
        if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
        {
            var mousePos = Input.mousePosition;
            var screenPos = new Vector3(mousePos.x, mousePos.y, DistanceFromCamera);

            switch (Phase)
            {
                case GodPhase.None:
                    _phase = GodPhase.Began;
                    _screenPositionBegin = screenPos;
                    break;
                case GodPhase.Began:
                    _phase = GodPhase.Held;
                    _screenPositionHeld = screenPos;
                    break;
                case GodPhase.Held:
                    _screenPositionHeld = screenPos;
                    break;
                case GodPhase.End:
                    _phase = GodPhase.Began;
                    _screenPositionEnd = screenPos;
                    break;
                default:
                    Debug.LogError("Unexpected phase value in SetScreenPosition");
                    break;
            }
        }
        else {
            switch (Phase) {
                case GodPhase.None:
                    break;
                case GodPhase.Began:
                    _phase = GodPhase.End;
                    break;
                case GodPhase.Held:
                    _phase = GodPhase.End;
                    break;
                case GodPhase.End:
                    _phase = GodPhase.None;
                    break;
                default:
                    Debug.LogError("Unexpected phase value in SetScreenPosition");
                    break;
            }
        }
    }

    private void SetWorldPosition() {
        _worldPositionBegin = _camera.ScreenToWorldPoint(new Vector3(_screenPositionBegin.x, _screenPositionBegin.y, DistanceFromCamera));
        _worldPositionHeld = _camera.ScreenToWorldPoint(new Vector3(_screenPositionHeld.x, _screenPositionHeld.y, DistanceFromCamera));
        _worldPositionEnd = _camera.ScreenToWorldPoint(new Vector3(_screenPositionEnd.x, _screenPositionEnd.y, DistanceFromCamera));
    }

    private void Update() {
        SetScreenPosition();
        SetWorldPosition();
    }
}