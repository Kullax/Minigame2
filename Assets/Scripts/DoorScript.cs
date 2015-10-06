﻿using UnityEngine;

public class DoorScript : ResettableMonoBehaviour
{
    private Vector3 start;
    bool active = false;
    public float hangtime;
    public float distance = 1.5f;
    private float bck_hangtime;
    public bool closes = false;

    public AudioClip MovingSound;
    public AudioClip Click;
    public AudioClip Clunk;
    private AudioSource move;
    private AudioSource click;
    private AudioSource clunk;
    private Animator anm;

    // Use this for initialization
    void Start() {
        move = gameObject.AddComponent<AudioSource>();
        move.clip = Instantiate(MovingSound);
        move.loop = true;
        click = gameObject.AddComponent<AudioSource>();
        click.clip = Instantiate(Click);
        clunk = gameObject.AddComponent<AudioSource>();
        clunk.clip = Instantiate(Clunk);
        start = transform.position;
        bck_hangtime = hangtime;
        anm = GetComponentInParent<Animator>();
        anm.SetBool("Up", active);
    }

    // Update is called once per frame
    void Update() {
        if (!active)
            return;
        anm.SetBool("Up", active);
        if (!closes)
            return;
        if (hangtime > 0)
            hangtime -= Time.deltaTime;
        else {
            active = false;
            anm.SetBool("Up", active);
        }
    }

    public override void ResetBehaviour() {
        active = false;
        transform.position = start;
        hangtime = 0;
        anm.SetBool("Up", active);
    }

    public void Activate() {
        if (!active) {
            anm.SetBool("Up", active);
            active = true;
            hangtime = bck_hangtime;
        }
    }

    public void OnCollisionEnter(Collision c) {
        if (c.gameObject != GameManager.CurrentPlayer)
            return;

        foreach (var contact in c.contacts) {
            if (CloseToYAxis(contact.normal)) {
                GameManager.RespawnPlayer();
                GameManager.ResetResettables();
            }
        }
    }

    public bool CloseToYAxis (Vector3 vector) {
        return Vector3.Angle(vector, Vector3.up) < 15;
    }
}