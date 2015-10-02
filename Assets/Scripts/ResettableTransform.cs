using UnityEngine;

/// <summary>
/// An example of a ResettableMonoBehaviour.
/// 
/// Saves the original transform of the game object it is attached to, and resets to it when required.
/// </summary>
public class ResettableTransform : ResettableMonoBehaviour {

    Vector3 position;
    Vector3 scale;
    Quaternion rotation;

	void Awake () {
        position = transform.position;
        scale = transform.localScale;
        rotation = transform.rotation;
	}
	
	public override void ResetBehaviour() {
        transform.position = position;
        transform.localScale = scale;
        transform.rotation = rotation;
    }
}
