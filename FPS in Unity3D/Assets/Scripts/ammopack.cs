using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ammopack : MonoBehaviour {


    public float ammunition = 25.0f;
    public float gunType = 1.0f;
    public AudioClip ammoCollect;
    public float destroyTime = 3.0f;

    private bool isCollected;
    private float timer = 0.0f;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            Transform gun = other.transform.Find("FPSCharacter/pistol");
            if (gun.GetComponent<Shooting>().canGetAmmo())
            {
                gun.SendMessage("addAmmo", new Vector2(ammunition, gunType));
                isCollected = true;
                transform.position = new Vector3(0, 0, 0);
                GetComponent<AudioSource>().Play();
            }
        }
    }

    void Start ()
    {
        isCollected = false;
        GetComponent<AudioSource>().clip = ammoCollect;
    }
	
	
	void Update ()
    {
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
