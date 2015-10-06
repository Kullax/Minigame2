using UnityEngine;
using System.Collections;
using System;

public class PlayIceClesAnimation : ResettableMonoBehaviour
{
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }
    
    public void PlayAnimation(bool toPlayOrNotToPlay)
    {
        if (toPlayOrNotToPlay)
            _animator.SetTrigger("Play");
        else
            _animator.SetTrigger("Reset");
    }

    public override void ResetBehaviour()
    {
        PlayAnimation(false);
    }
}
