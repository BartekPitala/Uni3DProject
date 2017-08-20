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
        GetComponent<AudioSource>().Play();
    }
	// Update is called once per frame
	void Update () {

        if (transform.position.y <= 14.245 && transform.position.y >= 14.190 && forward)
        {
            transform.Translate(0f, -0.4f, 0f);
            if(transform.position.y<=14.191)
            forward = false;

        }
        if (transform.position.y <= 14.245 && transform.position.y >= 14.184 && !forward)
        {
            transform.Translate(0f, 0.4f, 0f);
            if(transform.position.y>=14.243)
            forward = true;
        }

    }
}

