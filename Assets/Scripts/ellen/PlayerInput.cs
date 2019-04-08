using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    public static PlayerInput Instance
    {
        get { return s_Instance; }
    }


    protected Vector2 m_Movement;
    protected Vector2 m_Camera;
    protected bool m_Attack;
    protected bool m_Pause;
    protected bool m_ExternalInputBlocked;


    protected static PlayerInput s_Instance;


    WaitForSeconds m_AttachInputWait;
    Coroutine m_AttackWaitCoroutine;

    float k_AttackInputDuration = 0.03f;

    void Awake()
    {

        if (s_Instance == null)
            s_Instance = this;
        else if (s_Instance != this)
            throw new UnityException("There cannot be more than one PlayerInput script.  The instances are " + s_Instance.name + " and " + name + ".");

        m_AttachInputWait = new WaitForSeconds(k_AttackInputDuration);
    }

    // Update is called once per frame
    void Update()
    {
        m_Movement.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        m_Camera.Set(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));



        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("fire");
            if (m_AttackWaitCoroutine != null)
                StopCoroutine(m_AttackWaitCoroutine);

            m_AttackWaitCoroutine = StartCoroutine(AttackWait());
        }

        //m_Pause = Input.GetButtonDown("Pause");
    }

    IEnumerator AttackWait()
    {
        m_Attack = true;

        yield return m_AttachInputWait;

        m_Attack = false;
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
