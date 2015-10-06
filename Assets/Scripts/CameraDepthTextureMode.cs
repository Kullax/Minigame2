using UnityEngine;

public class CameraDepthTextureMode : MonoBehaviour
{
    public DepthTextureMode mode;
	void Start()
    {
        GetComponent<Camera>().depthTextureMode = mode;
	}
	
	void OnValidate()
    {
        GetComponent<Camera>().depthTextureMode = mode;
    }
}
