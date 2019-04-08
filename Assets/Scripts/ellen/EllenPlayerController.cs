using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class EllenPlayerController : MonoBehaviour
{

    protected static PlayerController s_Instance;
    public static PlayerController instance { get { return s_Instance; } }

    public float maxSpeed = 8f;
    public float minTurnSpeed = 400f;         // How fast Ellen turns when moving at maximum speed.
    public float maxTurnSpeed = 1200f;        // How fast Ellen turns when stationary.
    public float idleTimeout = 5f;            // How long before Ellen starts considering random idles.
    public bool canAttack;                    // Whether or not Ellen can swing her staff.
    protected float m_AngleDiff;
    protected float m_DesiredForwardSpeed;
    protected float m_ForwardSpeed;                // How fast Ellen is currently going along the ground.
    protected float m_VerticalSpeed;               // How fast Ellen is currently moving up or down.
    protected PlayerInput m_Input;
    protected Quaternion m_TargetRotation;
    protected CharacterController m_CharCtrl;      
    protected Animator m_Animator;
    public CameraSettings cameraSettings;

    private float m_IdleTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        m_Input = GetComponent<PlayerInput>();
        m_Animator = GetComponent<Animator>();
        m_CharCtrl = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CalculateForwardMovement();

        SetTargetRotation();
        if (IsMoveInput)
            UpdateOrientation();

        TimeoutToIdle();
        
    }

    void CalculateForwardMovement()
    {
        // Cache the move input and cap it's magnitude at 1.
        Vector2 moveInput = m_Input.MoveInput;
        if (moveInput.sqrMagnitude > 1f)
            moveInput.Normalize();

        // Calculate the speed intended by input.
        m_DesiredForwardSpeed = moveInput.magnitude * maxSpeed;

        // Determine change to speed based on whether there is currently any move input.
        float acceleration = 20f;

        // Adjust the forward speed towards the desired speed.
        m_ForwardSpeed = Mathf.MoveTowards(m_ForwardSpeed, m_DesiredForwardSpeed, acceleration * Time.deltaTime);

        // Set the animator parameter to control what animation is being played.
        if (Mathf.Abs(m_ForwardSpeed) < 0.01 && (m_ForwardSpeed < 0 || m_ForwardSpeed > 0)) {
            if (m_ForwardSpeed > 0)
                m_ForwardSpeed = 0.011f;
            else
                m_ForwardSpeed = -0.011f;
        }
        m_Animator.SetFloat("speed", m_ForwardSpeed);
    }

    void Reset()
    {
        cameraSettings = FindObjectOfType<CameraSettings>();

        if (cameraSettings != null)
        {
            if (cameraSettings.follow == null)
                cameraSettings.follow = transform;

            if (cameraSettings.lookAt == null)
                cameraSettings.follow = transform.Find("HeadTarget");
        }
    }

        void SetTargetRotation()
    {
        // Create three variables, move input local to the player, flattened forward direction of the camera and a local target rotation.
        Vector2 moveInput = m_Input.MoveInput;
        Vector3 localMovementDirection = new Vector3(moveInput.x, 0f, moveInput.y).normalized;

        Vector3 forward = Quaternion.Euler(0f, cameraSettings.Current.m_XAxis.Value, 0f) * Vector3.forward;
        forward.y = 0f;
        forward.Normalize();

        Quaternion targetRotation;

        // If the local movement direction is the opposite of forward then the target rotation should be towards the camera.
        if (Mathf.Approximately(Vector3.Dot(localMovementDirection, Vector3.forward), -1.0f))
        {
            targetRotation = Quaternion.LookRotation(-forward);
        }
        else
        {
            // Otherwise the rotation should be the offset of the input from the camera's forward.
            Quaternion cameraToInputOffset = Quaternion.FromToRotation(Vector3.forward, localMovementDirection);
            targetRotation = Quaternion.LookRotation(cameraToInputOffset * forward);
        }
        
        // The desired forward direction of Ellen.
        Vector3 resultingForward = targetRotation * Vector3.forward;


        // Find the difference between the current rotation of the player and the desired rotation of the player in radians.
        float angleCurrent = Mathf.Atan2(transform.forward.x, transform.forward.z) * Mathf.Rad2Deg;
        float targetAngle = Mathf.Atan2(resultingForward.x, resultingForward.z) * Mathf.Rad2Deg;

        m_AngleDiff = Mathf.DeltaAngle(angleCurrent, targetAngle);
        m_TargetRotation = targetRotation;
    }

    void TimeoutToIdle()
    {
        bool inputDetected = IsMoveInput || m_Input.Attack;
        if ( !inputDetected)
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

    void UpdateOrientation()
    {
        m_Animator.SetFloat("angle", m_AngleDiff * Mathf.Deg2Rad);

        Vector3 localInput = new Vector3(m_Input.MoveInput.x, 0f, m_Input.MoveInput.y);
        float groundedTurnSpeed = Mathf.Lerp(maxTurnSpeed, minTurnSpeed, m_ForwardSpeed / m_DesiredForwardSpeed);
        float actualTurnSpeed =  groundedTurnSpeed ;
        m_TargetRotation = Quaternion.RotateTowards(transform.rotation, m_TargetRotation, actualTurnSpeed * Time.deltaTime);

        transform.rotation = m_TargetRotation;
    }

    protected bool IsMoveInput
    {
        get { return !Mathf.Approximately(m_Input.MoveInput.sqrMagnitude, 0f); }
    }
}
