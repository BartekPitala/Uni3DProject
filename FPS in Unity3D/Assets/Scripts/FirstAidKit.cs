using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FirstAidKit : MonoBehaviour {

    public AudioClip firstAidKitCollect;

    public float health = 25.0f;
    public float destroyTime = 3.0f;
    private bool isCollected;
    private float timer = 0.0f;

    void Start () {
        isCollected = false;
        GetComponent<AudioSource>().clip = firstAidKitCollect;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            
            if (other.GetComponent<PlayerStats>().canGetHealth())
            {
                other.SendMessage("addHealth", health);
                isCollected = true;
                transform.position = new Vector3(0, 0, 0);
                GetComponent<AudioSource>().Play();
            }
        }
    }

    void Update () {
        transform.Rotate(new Vector3(0, 0, 1));
        if (isCollected)
        {
            timer += Time.deltaTime;
            if (timer >= destroyTime)
            {
                Destroy(gameObject);
            }
        }
    }
}
