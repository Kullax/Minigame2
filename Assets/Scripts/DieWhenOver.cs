using UnityEngine;
using System.Collections;

public class DieWhenOver : MonoBehaviour
{
    private ParticleSystem tmpCurr;

    void Start()
    {
        tmpCurr = this.gameObject.GetComponent<ParticleSystem>();
        StartCoroutine(WaitToDie(tmpCurr.duration));
    }

    IEnumerator WaitToDie(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(this.gameObject);
    }
}
