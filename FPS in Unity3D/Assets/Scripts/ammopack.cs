using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammopack : MonoBehaviour {


    public float ammunition = 25.0f;
    public float gunType = 1.0f;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            Transform gun = other.transform.Find("FPSCharacter/pistol");
            if (gun.GetComponent<Shooting>().canGetAmmo())
            {
                gun.SendMessage("addAmmo", new Vector2(ammunition, gunType));
                Destroy(gameObject);
            }
        }
    }

    void Start () {
		
	}
	
	
	void Update () {
        transform.Rotate(new Vector3(0, 0, 1));
    }
}
