using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDamage : MonoBehaviour {

		private bool test=false;

	    void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
        		test=true;
                other.SendMessage("takeHit", 0.2f);

            
        }
    }
}
