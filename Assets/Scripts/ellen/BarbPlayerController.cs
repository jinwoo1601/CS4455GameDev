using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator), typeof(Rigidbody), typeof(CapsuleCollider))]
public class BarbPlayerController : MonoBehaviour, Damageable
{

    protected static BarbPlayerController s_Instance;
    public static BarbPlayerController instance { get { return s_Instance; } }

    public float maxSpeed = 8f;
    public float rootMovementSpeed = 3f;
    public float rootTurnSpeed = 100f;
    public float armedRootMovementSpeed;
    public float armedRootTurnSpeed;
    public float disarmedRootMovementSpeed;
    public float disarmedRootTurnSpeed;
    public float idleTimeout = 5f;            // How long before Ellen starts considering random idles.

    public Text hintText;
    float hintExistTime;
    float hintDispayTime = 3f;
    float hintFadeOutSpeed = 3f;

    protected float m_AngleDiff;
    protected float m_DesiredForwardSpeed;
    protected float m_ForwardSpeed;                // How fast Ellen is currently going along the ground.
    protected float m_VerticalSpeed;               // How fast Ellen is currently moving up or down.
    protected PlayerInput m_Input;
    protected Quaternion rotation_speed = new Quaternion();
    protected Animator m_Animator;
    protected PlayerHealth m_PlayerHealth;

    public Transform locator1;
    public Transform locator2;
    FollowUpdate weapon_position;

    Rigidbody rbody;
    bool isGrounded = true;
    float damaged_time;
    float invulnerable_duration = 1f;
    bool armed = true;

    float m_speed;
    Vector3 curPosition;
    Vector3 lastPosition;

    //public List<GameObject> buffAuroras = new List<GameObject>();
    //public List<Buff.BuffType> buffs = new List<Buff.BuffType>();
    bool couldRevive = false;
    public float luckiness = 1f;

    private float m_IdleTimer = 0f;

    void Awake()
    {
        m_Input = GetComponent<PlayerInput>();
        m_Animator = GetComponent<Animator>();
        //m_CharCtrl = GetComponent<CharacterController>();
        m_PlayerHealth = GetComponent<PlayerHealth>();
        s_Instance = this;
        rbody = GetComponent<Rigidbody>();
        weapon_position = GetComponentInChildren<FollowUpdate>();
        PlayerData.coinCount = 1000;
        List<Buff.BuffType> list = PlayerData.buffs;
        PlayerData.buffs = new List<Buff.BuffType>();
        foreach (Buff.BuffType type in list)
        {
            Vendor.Instance.CreateBuff(type);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        curPosition = transform.position;
        curPosition.y = 0;
        m_speed = (((curPosition - lastPosition).magnitude) / Time.deltaTime);
        lastPosition = transform.position;
        lastPosition.y = 0;
        m_Animator.SetFloat("speed", m_speed);

        Vector2 moveInput = m_Input.MoveInput;
        m_Animator.SetFloat("velx", moveInput.x);
        m_Animator.SetFloat("vely", moveInput.y);

        if (!hintText.text.Equals("") && Time.time > hintExistTime)
        {
            hintText.text = "";
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("ragdoll");
            m_Animator.enabled = false;
            rbody.isKinematic = false;
        }

        if (moveInput.magnitude > 0.01 || (armed && m_Input.Attack))
        {
            m_Animator.SetBool("active", true);
        }
        else
        {
            m_Animator.SetBool("active", false);
        }

        rbody.MoveRotation(rbody.rotation * Quaternion.AngleAxis(m_Input.Turn * Time.deltaTime * rootTurnSpeed, Vector3.up));

        TimeoutToIdle();

        m_Animator.ResetTrigger("attack");
        if (m_Input.Attack)
        {
            if (armed)
            {
                m_Animator.SetTrigger("attack");
            }
            else
            {
                setHintText("need to pullup the weapon first!");
            }
        }

        if (m_Input.Equip)
        {
            if (armed)
            {
                m_Animator.SetTrigger("unequip");
            }
            else
            {
                m_Animator.SetTrigger("equip");
            }
        }

    }

    /*
    void Update()
    {
        if (m_Input.Attack)
        {
            if (armed)
            {
                EventManager.TriggerEvent<attackEvent, Vector3>(transform.position);
            }
        }
    }
    */
    public void addBuff(Buff.BuffType type)
    {
        switch (type)
        {
            case Buff.BuffType.attack:
                PlayerInput.Instance.weapon.AD *= 1.5f;
                break;
            case Buff.BuffType.speed:
                rootMovementSpeed *= 1.5f;
                break;
            case Buff.BuffType.revive:
                couldRevive = true;
                break;
            case Buff.BuffType.luck:
                luckiness *= 2;
                break;
        }
    }

    public void removeBuff(Buff.BuffType type)
    {
        switch (type)
        {
            case Buff.BuffType.attack:
                PlayerInput.Instance.weapon.AD /= 1.5f;
                break;
            case Buff.BuffType.speed:
                rootMovementSpeed /= 1.5f;
                break;
            case Buff.BuffType.revive:
                couldRevive = false;
                break;
            case Buff.BuffType.luck:
                luckiness /= 2;
                break;
        }
    }


    void TimeoutToIdle()
    {
        bool inputDetected = IsMoveInput || m_Input.Attack;
        if (!inputDetected)
        {
            m_IdleTimer += Time.deltaTime;

            if (m_IdleTimer >= idleTimeout)
            {
                m_IdleTimer = 0f;
                m_Animator.SetTrigger("to_idle");
            }
        }
        else
        {
            m_IdleTimer = 0f;
            m_Animator.ResetTrigger("to_idle");
        }

    }


    public void OnDamage(Vector3 attackPoint, Vector3 attackForce, float AD)
    {
        if (Time.time - damaged_time < invulnerable_duration)
            return;

        m_PlayerHealth.TakeDamage(10);
        damaged_time = Time.time;
    }

    void OnAnimatorMove()
    {

        Vector3 newRootPosition;
        Quaternion newRootRotation;

        if (isGrounded)
        {
            //use root motion as is if on the ground		
            newRootPosition = m_Animator.rootPosition;
        }
        else
        {
            //Simple trick to keep model from climbing other rigidbodies that aren't the ground
            newRootPosition = new Vector3(m_Animator.rootPosition.x, this.transform.position.y, m_Animator.rootPosition.z);
        }

        //use rotational root motion as is
        newRootRotation = m_Animator.rootRotation;

        //TODO Here, you could scale the difference in position and rotation to make the character go faster or slower

        this.transform.position = Vector3.LerpUnclamped(this.transform.position, newRootPosition, rootMovementSpeed);
        //this.transform.rotation = Quaternion.LerpUnclamped(this.transform.rotation, newRootRotation, rootTurnSpeed);


        //clear IsGrounded
        isGrounded = false;
    }

    public void equip_arm()
    {
        weapon_position.toFollow = locator1;
        rootTurnSpeed = armedRootTurnSpeed;
        rootMovementSpeed = armedRootMovementSpeed;
        armed = true;
        m_Animator.ResetTrigger("equip");
        m_Animator.SetBool("armed", true);
    }

    public void unequip_arm()
    {
        weapon_position.toFollow = locator2;
        rootTurnSpeed = disarmedRootTurnSpeed;
        rootMovementSpeed = disarmedRootMovementSpeed;
        armed = false;
        m_Animator.ResetTrigger("unequip");
        m_Animator.SetBool("armed", false);
    }

    public void respawned()
    {
        m_Animator.SetBool("respawn", true);
    }

    public void setHintText(string text)
    {
        hintText.text = text;
        hintExistTime = Time.time + 3f;
    }

    public bool canBeAttacked()
    {
        return true;
    }

    public Damageable getOwner()
    {
        return this;
    }

    protected bool IsMoveInput
    {
        get { return !Mathf.Approximately(m_Input.MoveInput.sqrMagnitude, 0f); }
    }

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "crate")
        {
            Rigidbody body = hit.collider.attachedRigidbody;
            Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
            body.velocity = pushDir * 2;
        }
    }
}
