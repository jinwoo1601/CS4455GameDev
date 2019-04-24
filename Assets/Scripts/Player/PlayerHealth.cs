using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthSlider;                                 // Reference to the UI's health bar.
    public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
    public AudioClip deathClip;                                 // The audio clip to play when the player dies.
    public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.


    public Color deathColour = new Color(1f, 0f, 0f, 0.5f);
    public Image deathImage;
    public Text deathText;


    Animator anim;                                              // Reference to the Animator component.
    //Rigidbody rig;
    //AudioSource playerAudio;                                    // Reference to the AudioSource component.
    bool isDead;                                                // Whether the player is dead.
    public bool damaged;                                               // True when the player gets damaged.


    GameObject target;

    void Awake()
    {
        // Setting up the references.
        anim = GetComponent<Animator>();
        isDead = false;
        deathText.text = "";
    }


    void Update()
    {
        if (isDead)
        {
            anim.enabled = false;
            return;
        }

        if (damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        healthSlider.value = PlayerData.curHealth;

        anim.enabled = true;
        damaged = false;
    }


    public void TakeDamage(float amount)
    {
        damaged = true;
        PlayerData.curHealth -= amount;
        healthSlider.value = PlayerData.curHealth;
        if (PlayerData.curHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;
        deathImage.color = Color.Lerp(deathImage.color, deathColour, 100f * Time.deltaTime);
        GameManager.GameEnd = true;
        EventManager.TriggerEvent<DeathEvent, Vector3>(transform.position);
        deathText.text = "   You are dead!\n Press ESC to restart!";
    }

}