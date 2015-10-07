using UnityEngine;
using System.Collections;

public class PlayerMovementAnimationController : MonoBehaviour
{

    public float MovementThreshhold = 0.7f;

    private Rigidbody _rb;
    private CubeScale _cs;

    private float oldSize;
    private float newSize;

    private Animator CAC; //Character Animation Controller

    private float waitTimeElapsed = 0;
    private float waitTime = 2.5f;

    // Use this for initialization
    void Start()
    {
        _rb = GetComponent<Rigidbody>(); //to know velocity
        _cs = GetComponent<CubeScale>(); //to now status of the cube

        GameObject CharacterRenderer = GameObject.Find("iceCube_animation_control");
        CAC = CharacterRenderer.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!(Mathf.Abs(_rb.velocity.z) > MovementThreshhold || Mathf.Abs(_rb.velocity.x) > MovementThreshhold) && !CAC.GetBool("SpeedToLow"))
        {
            CAC.SetBool("SpeedToLow", false);
        }
        else
        {
            CAC.SetBool("SpeedToLow", true);
        }

        if (!(CAC.GetCurrentAnimatorStateInfo(0).IsName("Melting Idle Pose") || CAC.GetCurrentAnimatorStateInfo(0).IsName("Idle Pose") || CAC.GetCurrentAnimatorStateInfo(0).IsName("Idle Waiting"))) // Checking if the cube is ready to move
        {
            return;
        }

        if (Mathf.Abs(_rb.velocity.z) > MovementThreshhold || Mathf.Abs(_rb.velocity.x) > MovementThreshhold) // is the cube moving in any direction
        {
            switch (GameManager.GameRotation)
            {

                case (GameRotation.NegativeX):

                    if (_cs.status == CubeScale.Status.Melting)
                    {
                        if (_rb.velocity.z > 0)
                        {
                            CAC.Play("Melting Idle To Right");
                        }
                        else
                        {
                            CAC.Play("Melting Idle To Left");
                        }
                    }

                    if (_cs.status == CubeScale.Status.None)
                    {
                        if (_rb.velocity.z > 0)
                        {
                            CAC.Play("Idle to the right");
                        }
                        else
                        {
                            CAC.Play("Idle to the left");
                        }
                    }

                    break;

                case (GameRotation.PositiveX):

                    if (_cs.status == CubeScale.Status.Melting)
                    {
                        if (_rb.velocity.z < 0)
                        {
                            CAC.Play("Melting Idle To Right");
                        }
                        else
                        {
                            CAC.Play("Melting Idle To Left");
                        }
                    }

                    if (_cs.status == CubeScale.Status.None)
                    {
                        if (_rb.velocity.z < 0)
                        {
                            CAC.Play("Idle to the right");
                        }
                        else
                        {
                            CAC.Play("Idle to the left");
                        }
                    }

                    break;

                case (GameRotation.NegativeZ):

                    if (_cs.status == CubeScale.Status.Melting)
                    {
                        if (_rb.velocity.x < 0)
                        {
                            CAC.Play("Melting Idle To Right");
                        }
                        else
                        {
                            CAC.Play("Melting Idle To Left");
                        }
                    }

                    if (_cs.status == CubeScale.Status.None)
                    {
                        if (_rb.velocity.x < 0)
                        {
                            CAC.Play("Idle to the right");
                        }
                        else
                        {
                            CAC.Play("Idle to the left");
                        }
                    }
                    break;

                case (GameRotation.PositiveZ):

                    if (_cs.status == CubeScale.Status.Melting)
                    {
                        if (_rb.velocity.x < 0)
                        {
                            CAC.Play("Melting Idle To Left");
                        }
                        else
                        {
                            CAC.Play("Melting Idle To Right");
                        }
                    }

                    if (_cs.status == CubeScale.Status.None)
                    {
                        if (_rb.velocity.x < 0)
                        {
                            CAC.Play("Idle to the left");
                        }
                        else
                        {
                            CAC.Play("Idle to the right");
                        }
                    }

                    break;
            }
        }
        StartWaitAnimation();
    }

    private void StartWaitAnimation()
    {
        //This code gave me cancer and i would rather drag my private parts through 8 miles of broken glass than having to rewrite this.
        if (waitTimeElapsed >= waitTime+2 && CAC.GetBool("LookAround"))
        {
            CAC.SetBool("LookAround", false);
            waitTimeElapsed = 0;

        }

        if (_cs.status == CubeScale.Status.Melting)
        {
            return;
        }

        if (Mathf.Abs(_rb.velocity.z) > 0 || Mathf.Abs(_rb.velocity.x) > 0)
        {
            waitTimeElapsed = 0;
        }
        else
        {
            waitTimeElapsed += Time.deltaTime;
            if ((waitTimeElapsed >= waitTime) && !CAC.GetBool("LookAround"))
            {
                CAC.SetBool("LookAround", true);
            }
        }
    }
}
