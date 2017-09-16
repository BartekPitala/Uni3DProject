using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Shooting : MonoBehaviour {
	
    public Texture2D crosshairTexture;
    public AudioClip pistolShot;
    public AudioClip reloadSound;
    public int maxAmmo = 200;
    public int clipSize = 20;
    
    public float reloadTime = 2.0f;
	public bool automatic = false;
	public float shotDelay = 0.5f;
	public GameObject bulletHole;

    private int currentAmmo = 30;
    private int currentClip;
    private Rect position;
    private float range = 200.0f;
    private GameObject pistolSparks;
    private Vector3 fwd;
    private RaycastHit hit;
    private bool isReloading = false;
	private float shotDelayCounter = 0.0f;
	private float zoomFieldOfView = 40.0f;
	private float defaultFieldOfView = 60.0f;

    private float timer = 0.0f;
    private float barWidth;
    private float barHeight;

    public GameObject bloodParticles;
    public float demage = 5.0f;

    private GUIStyle statsStyle = new GUIStyle();
    public Font statsFont;

    void Awake()
    {
        barHeight = Screen.height * 0.04f;
        barWidth = barHeight * 10.0f;

        statsStyle.font = statsFont;
        statsStyle.fontSize = 12;
        
    }

    void Start ()
    {
        Cursor.visible = false;
        position = new Rect((Screen.width - crosshairTexture.width) / 2,
                        (Screen.height - crosshairTexture.height) / 2,
                        crosshairTexture.width,
                        crosshairTexture.height);
        pistolSparks = GameObject.Find("Sparks");
        GetComponent<AudioSource>().clip = pistolShot;
        currentClip = clipSize;
		pistolSparks.GetComponent<ParticleEmitter>().emit = false;

    }

	
	void Update ()
    {
        PlayerStats.currentAmmo = currentAmmo;
        PlayerStats.currentClip = currentClip;

        statsStyle.font = statsFont;
        statsStyle.fontSize = 12;

		if (shotDelayCounter > 0) {
			shotDelayCounter -= Time.deltaTime;
		}

		Transform tf = transform.parent.GetComponent<Transform> ();
		fwd = tf.TransformDirection (Vector3.forward);

		if (currentClip > 0 && !isReloading)
		{
			if((Input.GetButtonDown ("Fire1") || Input.GetButton("Fire1") && automatic) && shotDelayCounter <= 0)
			{
				shotDelayCounter = shotDelay;
				currentClip--;
				pistolSparks.GetComponent<ParticleEmitter>().Emit ();
				GetComponent<AudioSource> ().Play ();

				if (Physics.Raycast (tf.position, fwd, out hit)) 
				{
					if (hit.transform.tag == "Enemy" && hit.distance < range) {
                        hit.transform.gameObject.SendMessage("takeHit", demage);
                        GameObject go;
                        go = Instantiate(bloodParticles, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal)) as GameObject;
                        Destroy(go, 0.3f);
						Debug.Log ("ENEMY HIT");

                    } 
					else if (hit.distance < range) {
						GameObject go;
						go = Instantiate(bulletHole, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal)) as GameObject; 
						Destroy(go, 5);
						Debug.Log ("Trafiona Sciana");
					}
				}
			}
		}

        if (((Input.GetButtonDown("Fire1") && currentClip == 0) || Input.GetButtonDown("Reload")) && currentClip < clipSize)
        {
            if (currentAmmo > 0)
            {
                GetComponent<AudioSource>().clip = reloadSound;
                GetComponent<AudioSource>().Play();
                isReloading = true;
            }
        }
        if (isReloading)
        {
            timer += Time.deltaTime;
            if (timer >= reloadTime)
            {
                int needAmmo = clipSize - currentClip;

                if (currentAmmo >= needAmmo)
                {
                    currentClip = clipSize;
                    currentAmmo -= needAmmo;
                }
                else
                {
                    currentClip += currentAmmo;
                    currentAmmo = 0;
                }

                GetComponent<AudioSource>().clip = pistolShot;
                isReloading = false;
                timer = 0.0f;
            }
        }

		if(gameObject.GetComponentInParent<Camera>() is Camera) {
			Camera cam = gameObject.GetComponentInParent<Camera>();
			if(Input.GetButton("Fire2")) {
				if(cam.fieldOfView > zoomFieldOfView) {
					cam.fieldOfView--;
				}
			} else {
				if(cam.fieldOfView < defaultFieldOfView) {
					cam.fieldOfView++;
				}
			}
		}

        


    }

    public bool canGetAmmo()
    {
        if (currentAmmo == maxAmmo)
        {
            return false;
        }
        return true;
    }

    void addAmmo(int ammoToAdd)
    {
        if (maxAmmo - currentAmmo >= ammoToAdd)
        {
            currentAmmo += ammoToAdd;
        }
        else
        {
            currentAmmo = maxAmmo;
        }
    }

    void OnGUI()
    {   
        GUI.DrawTexture(position, crosshairTexture);    
    }

}
