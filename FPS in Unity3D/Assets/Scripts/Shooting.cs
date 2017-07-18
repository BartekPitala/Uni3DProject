using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Shooting : MonoBehaviour {

    public Texture2D crosshairTexture;
    public AudioClip pistolShot;
    public AudioClip reloadSound;
    public int maxAmmo = 200;
    public int clipSize = 10;
    public GUIText ammoText;
    public GUIText reloadText;
    public float reloadTime = 2.0f;

    private int currentAmmo = 30;
    private int currentClip;
    private Rect position;
    private float range = 10.0f;
    private GameObject pistolSparks;
    private Vector3 fwd;
    private RaycastHit hit;
    private bool isReloading = false;

    private float timer = 0.0f;


    void Start ()
    {
        Cursor.visible = false;
        position = new Rect((Screen.width - crosshairTexture.width) / 2,
                        (Screen.height - crosshairTexture.height) / 2,
                        crosshairTexture.width,
                        crosshairTexture.height);

        pistolSparks = GameObject.Find("Sparks");
        pistolSparks.GetComponent<ParticleEmitter>().emit = false;
        GetComponent<AudioSource>().clip = pistolShot;
        currentClip = clipSize;
    }
	
	
	void Update ()
    {
        fwd = transform.TransformDirection(Vector3.forward);

        if (Input.GetButtonDown("Fire1") && currentClip > 0 && !isReloading)
        {
            currentClip--;
            pistolSparks.GetComponent<ParticleEmitter>().Emit();
            GetComponent<AudioSource>().Play();

            if (Physics.Raycast(transform.position, fwd, out hit))
            {
                if (hit.transform.tag == "Enemy" && hit.distance < range)
                {
                    Debug.Log("Trafiony przeciwnik");

                }
                else if (hit.distance < range)
                {
                    Debug.Log("Trafiona Sciana");
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

    }

    public bool canGetAmmo()
    {
        if (currentAmmo == maxAmmo)
        {
            return false;
        }
        return true;
    }

    void addAmmo(Vector2 data)
    {
        int ammoToAdd = (int)data.x;

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
        ammoText.pixelOffset = new Vector2(-Screen.width / 2 + 100, -Screen.height / 2 + 30);
        ammoText.text = currentClip + " / " + currentAmmo;

        if (currentClip == 0)
        {
            reloadText.enabled = true;
        }
        else
        {
            reloadText.enabled = false;
        }
    }

}
