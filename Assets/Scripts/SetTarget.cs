using UnityEngine;
using System.Collections;

public class SetTarget : MonoBehaviour
{
    public float speed = 20.0f;

    private Vector3 _target;

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target, speed * Time.deltaTime);
    }

    public void EndTarget(Vector3 tmp)
    {
        _target = tmp;
    }
}
