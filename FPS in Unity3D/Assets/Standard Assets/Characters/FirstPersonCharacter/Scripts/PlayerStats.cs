using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    private float maxHealth = 100;
    private float currentHealth = 100;
    private float maxArmour = 100;
    private float currentArmour = 100;
    private float maxStamina = 100;
    private float currentStamina = 100;
    private bool[] weapons = new bool[] { true, true, false, false, false, false, false, false, false, false };
    public Texture2D healthTexture;
    public Texture2D armourTexture;
    public Texture2D staminaTexture;
    public Texture2D FrameTexture;
    public Texture2D[] weaponsTextures = new Texture2D[5];
    public GameObject pausePanel;

    private float barWidth;
    private float barHeight;

    private CharacterController chCont;
    private UnityStandardAssets.Characters.FirstPerson.FirstPersonController fpsC;
    private Vector3 lastPosition;
    private float canRegenerate = 0.0f;
    private float canHeal = 0.0f;

    private GUIStyle statsStyle = new GUIStyle();
    public Font statsFont;
    static public int currentClip;
    static public int currentAmmo;
    public AudioClip hitSound;

	public float timeLeft;
	public float timer;


void Awake()
    {
        UnPause();
        barHeight = Screen.height * 0.04f;
        barWidth = barHeight * 10.0f;
        chCont = GetComponent<CharacterController>();
        fpsC = gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
        lastPosition = transform.position;

        statsStyle.font = statsFont;
        statsStyle.fontSize = 20;

    }

    void OnGUI()
    {
		timer = Time.time;
        if (Input.GetKeyDown(KeyCode.P) && !pausePanel.active)
        {
            //Application.LoadLevel("Menu");
            Pause();
        }
        if (Input.GetKeyDown(KeyCode.R) && pausePanel.active)
        {
            UnPause();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.LoadLevel("Menu");
        }


        DrawWeaponsBar();


        GUI.DrawTexture(new Rect(10,
                                 Screen.height - barHeight - 10,
                                 currentStamina * barWidth / maxStamina,
                                 barHeight),
                        staminaTexture);



        GUI.DrawTexture(new Rect(10,
                                 Screen.height - barHeight * 2 - 20,
                                 currentArmour * barWidth / maxArmour,
                                 barHeight),
                        armourTexture);

        if (currentArmour > 0)
        {
            GUI.TextField(new Rect(barWidth + 20,
                             Screen.height - barHeight * 2 - 20,
                             maxArmour,
                             barHeight*2), "ARM " + Mathf.Round(currentArmour / maxArmour * 100).ToString() + "/100", statsStyle);
        }
        GUI.DrawTexture(new Rect(10,
                                 Screen.height - barHeight * 3 - 30,
                                 currentHealth * barWidth / maxHealth,
                                 barHeight),
                        healthTexture);

        if (currentHealth > 0)
        {
            GUI.TextField(new Rect(barWidth + 20,
                             Screen.height - barHeight * 3 - 30,
                             maxArmour,
                             barHeight * 2), "HP   " + Mathf.Round(currentHealth / maxHealth * 100).ToString() + "/100", statsStyle);
        }
       
        GUI.TextField(new Rect(10,
                            Screen.height * 0.73f,
                            300,
                            barHeight * 3), "AMMUNITION: " + currentClip + " / " + currentAmmo, statsStyle);

		GUI.TextField(new Rect(10,
			Screen.height * 0.68f,
			300,
			barHeight * 3), "TIMER:  " + Mathf.Round(timeLeft - timer), statsStyle);
    	
		if ((timeLeft - timer) < 0) {
			GUI.TextField(new Rect(Screen.width *0.5f,
				Screen.height * 0.5f,
				500,
				barHeight * 3), "GAME OVER!!!", statsStyle);
		}
	}


    public void updateWeapons(int index)
    {
        if (index < 5)
            weapons[index] = true;
    }


    void takeHit(float damage)
    {
        if(damage>0.2f){
        GetComponent<AudioSource>().clip = hitSound;
        GetComponent<AudioSource>().Play();
        }

        if (currentHealth <= 0)
        {
            Destroy(fpsC);
            Application.LoadLevel("GameOver");
        }

        if (currentArmour > 0)
        {
            currentArmour -= damage;
            if (currentArmour < 0)
            {
                currentHealth += currentArmour;
                currentArmour = 0;
            }
        }
        else
        {
            currentHealth -= damage;
        }

        if (currentHealth < maxHealth)
        {
            canHeal = 5.0f;
        }

        currentArmour = Mathf.Clamp(currentArmour, 0, maxArmour);
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    public bool canGetHealth()
    {
        if (currentHealth == maxHealth)
        {
            return false;
        }
        return true;
    }

    void addHealth(int healthToAdd)
    {

        if (maxHealth - currentHealth >= healthToAdd)
        {
            currentHealth += healthToAdd;
        }
        else
        {
            currentHealth = maxHealth;
        }
    }

    void Start()
    {
		timeLeft = 100.0f;
    }

    void FixedUpdate()
    {

        if (chCont.isGrounded && Input.GetKey(KeyCode.LeftShift) && lastPosition != transform.position && currentStamina > 0)
        {
            lastPosition = transform.position;
            currentStamina -= 0.4f;
            currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
            canRegenerate = 5.0f;
        }

        if (currentStamina > 0)
        {
            fpsC.CanRun = true;
        }
        else
        {
            fpsC.CanRun = false;
        }
    }


    void regenerateStamina(ref float currentStat, float maxStat)
    {
        currentStat += maxStat * 0.005f;
        Mathf.Clamp(currentStat, 0, maxStat);
    }

    void regenerateHealth(ref float currentStat, float maxStat)
    {
        if (currentStat <= 10)
            currentStat += maxStat * 0.003f;

        currentStat += maxStat * 0.00003f;
        Mathf.Clamp(currentStat, 0, maxStat);
    }

    void Update()
    {
        if (canHeal > 0.0f)
        {
            canHeal -= Time.deltaTime;
        }
        if (canRegenerate > 0.0f)
        {
            canRegenerate -= Time.deltaTime;
        }


        if (canHeal <= 0.0f && currentHealth < maxHealth)
        {
            regenerateHealth(ref currentHealth, maxHealth);
        }
        if (canRegenerate <= 0.0f && currentStamina < maxStamina)
        {
            regenerateStamina(ref currentStamina, maxStamina);
        }
    }

    void DrawWeaponsBar()
    {
        float weaponBarHeight = barHeight * 2.5f;
        float weaponBarWidth = weaponBarHeight;
        float firstBarXPosition = Screen.width / 2.7f;

        for (int i = 0; i < 5; i++)
        {
            if (weapons[i])
            {
                GUI.DrawTexture(new Rect(firstBarXPosition + i * weaponBarWidth,
                    Screen.height * 0.87f,
                    weaponBarWidth,
                    weaponBarHeight),
                    weaponsTextures[i]);
            }
            else
            {
                GUI.DrawTexture(new Rect(firstBarXPosition + i * weaponBarWidth,
                     Screen.height * 0.87f,
                     weaponBarWidth,
                     weaponBarHeight),
                     FrameTexture);
            }
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void UnPause()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined;
    }
}
