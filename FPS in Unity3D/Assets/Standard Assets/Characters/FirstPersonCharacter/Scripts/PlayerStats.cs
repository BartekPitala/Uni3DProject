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

    public Texture2D healthTexture;
    public Texture2D armourTexture;
    public Texture2D staminaTexture;

    private float barWidth;
    private float barHeight;

    private CharacterController chCont;
    private UnityStandardAssets.Characters.FirstPerson.FirstPersonController fpsC;
    private Vector3 lastPosition;
    private float canRegenerate = 0.0f;
    private float canHeal = 0.0f;




    void Awake()
    {
        barHeight = Screen.height * 0.04f;
        barWidth = barHeight * 10.0f;
        chCont = GetComponent<CharacterController>();
        fpsC = gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
        lastPosition = transform.position;
    }

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(Screen.width - barWidth - 950,
                                 Screen.height - barHeight - 10,
                                 currentStamina * barWidth / maxStamina,
                                 barHeight),
                        staminaTexture);



        GUI.DrawTexture(new Rect(Screen.width - barWidth - 950,
                                 Screen.height - barHeight * 2 - 20,
                                 currentArmour * barWidth / maxArmour,
                                 barHeight),
                        armourTexture);

        if (currentArmour > 0)
        {
            GUI.Label(new Rect(Screen.width - barWidth - 700,
                             Screen.height - barHeight - 42,
                             maxArmour,
                             barHeight), (currentArmour / maxArmour * 100).ToString() + "/100");
        }

        GUI.DrawTexture(new Rect(Screen.width - barWidth - 950,
                                 Screen.height - barHeight * 3 - 30,
                                 currentHealth * barWidth / maxHealth,
                                 barHeight),
                        healthTexture);

        if (currentHealth > 0)
        {
            GUI.Label(new Rect(Screen.width - barWidth - 700,
                             Screen.height - barHeight - 77,
                             maxArmour,
                             barHeight), (int)(currentHealth / maxHealth * 100) + "/100");
        }
    }



    void takeHit(float damage)
    {
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


    void Start()
    {

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
        if(currentStat<=10)
         currentStat += maxStat * 0.003f;

        currentStat += maxStat * 0.00003f;
        Mathf.Clamp(currentStat, 0, maxStat);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            takeHit(30);
        }

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
            regenerateHealth(ref currentHealth,maxHealth);
        }
        if (canRegenerate <= 0.0f && currentStamina < maxStamina)
        {
            regenerateStamina(ref currentStamina, maxStamina);
        }
    }
}
