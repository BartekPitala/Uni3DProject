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



    void Awake()
    {
        barHeight = Screen.height * 0.02f;
        barWidth = barHeight * 10.0f;
        chCont = GetComponent<CharacterController>();
        fpsC = gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
        lastPosition = transform.position;
    }

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(Screen.width - barWidth - 10,
                                 Screen.height - barHeight - 10,
                                 currentStamina * barWidth / maxStamina,
                                 barHeight),
                        staminaTexture);
        GUI.DrawTexture(new Rect(Screen.width - barWidth - 10,
                                 Screen.height - barHeight * 2 - 20,
                                 currentArmour * barWidth / maxArmour,
                                 barHeight),
                        armourTexture);
        GUI.DrawTexture(new Rect(Screen.width - barWidth - 10,
                                 Screen.height - barHeight * 3 - 30,
                                 currentHealth * barWidth / maxHealth,
                                 barHeight),
                        healthTexture);
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

        currentArmour = Mathf.Clamp(currentArmour, 0, maxArmour);
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    // Use this for initialization
    void Start()
    {

    }

    void FixedUpdate()
    {
        float speed =fpsC.GetWalkSpeed();

        if (chCont.isGrounded && Input.GetKey(KeyCode.LeftShift) && lastPosition != transform.position && currentStamina > 0)
        {
            lastPosition = transform.position;
            speed = fpsC.GetRunSpeed();
            currentStamina -= 1;
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            takeHit(30);
        }
    }
}
