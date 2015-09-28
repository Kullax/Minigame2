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

    public static GodPhase phase = GodPhase.None;
    public static Vector3 worldPositionBegin = Vector3.zero;
    public static Vector3 worldPositionHeld = Vector3.zero;
    public static Vector3 worldPositionEnd = Vector3.zero;
    
    new private Camera camera;
    private Vector3 screenPositionBegin = Vector3.zero;
    private Vector3 screenPositionHeld = Vector3.zero;
    private Vector3 screenPositionEnd = Vector3.zero;

    void Awake () {
        camera = Camera.main;
	}
	
    private void SetScreenPosition ()
    {
        if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
        {
            var mousePos = Input.mousePosition;
            var screenPos = new Vector3(mousePos.x, mousePos.y, DistanceFromCamera);

            switch (phase)
            {
                case GodPhase.None:
                    phase = GodPhase.Began;
                    screenPositionBegin = screenPos;
                    break;
                case GodPhase.Began:
                    phase = GodPhase.Held;
                    screenPositionHeld = screenPos;
                    break;
                case GodPhase.Held:
                    screenPositionHeld = screenPos;
                    break;
                case GodPhase.End:
                    phase = GodPhase.Began;
                    screenPositionEnd = screenPos;
                    break;
            }
        }
        else
        {
            switch (phase)
            {
                case GodPhase.None:
                    break;
                case GodPhase.Began:
                    phase = GodPhase.End;
                    break;
                case GodPhase.Held:
                    phase = GodPhase.End;
                    break;
                case GodPhase.End:
                    phase = GodPhase.None;
                    break;
            }
        }
    }

    private void SetWorldPosition ()
    {
        worldPositionBegin = camera.ScreenToWorldPoint(new Vector3(screenPositionBegin.x, screenPositionBegin.y, DistanceFromCamera));
        worldPositionHeld = camera.ScreenToWorldPoint(new Vector3(screenPositionHeld.x, screenPositionHeld.y, DistanceFromCamera));
        worldPositionEnd = camera.ScreenToWorldPoint(new Vector3(screenPositionEnd.x, screenPositionEnd.y, DistanceFromCamera));
    }

	void Update () {
        SetScreenPosition();
        SetWorldPosition();
	}
}