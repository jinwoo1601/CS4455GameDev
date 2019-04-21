using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    public static PlayerInput Instance
    {
        get { return s_Instance; }
    }

    protected static PlayerInput s_Instance;
    protected Vector2 m_Movement;
    protected Vector2 m_Camera;
    protected bool m_Attack;
    protected bool m_Pause;
    protected bool m_ExternalInputBlocked;

    private float filteredForwardInput = 0f;
    private float filteredTurnInput = 0f;

    public bool InputMapToCircular = true;

    public float forwardInputFilter = 5f;
    public float turnInputFilter = 5f;

    private float forwardSpeedLimit = 1f;

    public Animator animator;

    float start_attack_time;
    float attack_duration = 0.35f;

    WaitForSeconds m_AttachInputWait;
    Coroutine m_AttackWaitCoroutine;

    public Weapon weapon;

    public bool enable = true;

    float k_AttackInputDuration = 0.03f;

    public float Forward
    {
        get;
        private set;
    }

    public float Turn
    {
        get;
        private set;
    }

    public bool Action
    {
        get;
        private set;
    }

    public bool Jump
    {
        get;
        private set;
    }

    public bool Equip
    {
        get;
        private set;
    }

    void Awake()
    {

        if (s_Instance == null)
            s_Instance = this;
        else if (s_Instance != this)
            throw new UnityException("There cannot be more than one PlayerInput script.  The instances are " + s_Instance.name + " and " + name + ".");

        m_AttachInputWait = new WaitForSeconds(k_AttackInputDuration);
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enable)
        {
            m_Movement.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            m_Camera.Set(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            if (Input.GetKeyDown(KeyCode.J))
            {
                if (m_AttackWaitCoroutine != null)
                    StopCoroutine(m_AttackWaitCoroutine);
                m_AttackWaitCoroutine = StartCoroutine(AttackWait());
                animator.SetFloat("attack_time", Time.time - start_attack_time);
            }


            //m_Pause = Input.GetButtonDown("Pause");
            if (GameManager.GameEnd)
            {
                if (Input.GetKey(KeyCode.Escape))
                {
                    GameManager.instance.RestartGame();
                }
                return;
            }

            //GetAxisRaw() so we can do filtering here instead of the InputManager
            float h = Input.GetAxisRaw("Horizontal");// setup h variable as our horizontal input axis
            float v = Input.GetAxisRaw("Vertical"); // setup v variables as our vertical input axis


            if (InputMapToCircular)
            {
                // make coordinates circular
                //based on http://mathproofs.blogspot.com/2005/07/mapping-square-to-circle.html
                h = h * Mathf.Sqrt(1f - 0.5f * v * v);
                v = v * Mathf.Sqrt(1f - 0.5f * h * h);


                //BEGIN ANALOG ON KEYBOARD DEMO CODE
                if (Input.GetKey(KeyCode.Q))
                    h = -0.5f;
                else if (Input.GetKey(KeyCode.E))
                    h = 0.5f;

                //do some filtering of our input as well as clamp to a speed limit
                filteredForwardInput = Mathf.Clamp(Mathf.Lerp(filteredForwardInput, v,
                    Time.deltaTime * forwardInputFilter), -forwardSpeedLimit, forwardSpeedLimit);

                filteredTurnInput = Mathf.Lerp(filteredTurnInput, h,
                    Time.deltaTime * turnInputFilter);

                Forward = filteredForwardInput;
                Turn = filteredTurnInput;
                Action = Input.GetButtonDown("Fire1");
                Jump = Input.GetButtonDown("Jump");
                Equip = Input.GetKeyDown(KeyCode.K);
            }
        }
    }

    IEnumerator AttackWait()
    {
        
        m_Attack = true;

        yield return m_AttachInputWait;

        m_Attack = false;
        
    }

    public void start_attack()
    {
        start_attack_time = Time.time;
        weapon.enbaleAttack();
    }

    public void stop_attack()
    {
        weapon.disableAttack();
    }

    public Vector2 MoveInput
    {
        get
        {
            if ( m_ExternalInputBlocked)
                return Vector2.zero;
            return m_Movement;
        }
    }

    public void setMaxStraight()
    {
        m_Movement.x = 0;
        m_Movement.y = 1;
    }

    public Vector2 CameraInput
    {
        get
        {
            if ( m_ExternalInputBlocked)
                return Vector2.zero;
            return m_Camera;
        }
    }

    public bool Attack
    {
        get { return m_Attack && !m_ExternalInputBlocked; }
    }
}
