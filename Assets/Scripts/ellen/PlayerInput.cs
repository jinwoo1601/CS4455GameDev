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



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_Movement.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        m_Camera.Set(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));


        //m_Pause = Input.GetButtonDown("Pause");
    }

    void Awake()
    {

        if (s_Instance == null)
            s_Instance = this;
        else if (s_Instance != this)
            throw new UnityException("There cannot be more than one PlayerInput script.  The instances are " + s_Instance.name + " and " + name + ".");
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
