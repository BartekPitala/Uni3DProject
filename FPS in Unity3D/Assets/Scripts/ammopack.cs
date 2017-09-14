using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ammopack : MonoBehaviour {


    public int ammunition = 25;
    public AudioClip ammoCollect;
    public float destroyTime = 3.0f;

    private bool isCollected;
    private float timer = 0.0f;

    void OnTriggerEnter(Collider other)
    {
        Transform gun;
        if (other.tag.Equals("Player"))
        {
            if (other.GetComponent<GunsInventory>().getCurrentGun() == 1)
            {
                gun = other.transform.Find("FPSCharacter/pistol");
                if (gun.GetComponent<Shooting>().canGetAmmo())
                {
                    gun.SendMessage("addAmmo", ammunition);
                    isCollected = true;
                    transform.position = new Vector3(0, 0, 0);
                    GetComponent<AudioSource>().Play();
                }
            }
            else if (other.GetComponent<GunsInventory>().getCurrentGun() == 2)
            {
                gun = other.transform.Find("FPSCharacter/MP5");
                if (gun.GetComponent<Shooting>().canGetAmmo())
                {
                    gun.SendMessage("addAmmo",ammunition);
                    isCollected = true;
                    transform.position = new Vector3(0, 0, 0);
                    GetComponent<AudioSource>().Play();
                }
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
        transform.Rotate(new Vector3(0, 1, 0));
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
