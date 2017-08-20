using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainMovement : MonoBehaviour
{
    private bool forward;
    public AudioClip trainSound;
    // Use this for initialization
    void Start () {
        forward = true;
        GetComponent<AudioSource>().clip = trainSound;
    }
	// Update is called once per frame
	void Update () {


       // GetComponent<AudioSource>().Play();
        if (transform.position.y <= 14.24 && transform.position.y >= 14.19 && forward)
        {
            transform.Translate(0f, -0.4f, 0f);
            if(transform.position.y<=14.19)
            forward = false;

        }
        if (transform.position.y <= 14.24 && transform.position.y >= 14.18 && !forward)
        {
            transform.Translate(0f, 0.4f, 0f);
            if(transform.position.y>=14.235)
            forward = true;
        }

    }
}

