using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CompanionController : MonoBehaviour
{
    public bool following = false;
    public GameObject ellen;

    NavMeshAgent navMeshAgent;
    DialogueTrigger dialogueTrigger;
    VendorTrigger vendorTrigger;

    // Inspector
    [SerializeField] private float m_WalkSpeed = 2.0f;
    [SerializeField] private float m_RunSpeed = 3.5f;
    [SerializeField] private float m_RotateSpeed = 8.0f;
    [SerializeField] private float m_JumpForce = 300.0f;
    [SerializeField] private float m_RunningStart = 1.0f;

    // member
    private Rigidbody m_RigidBody = null;
    private Animator m_Animator = null;
    private float m_MoveTime = 0;
    private float m_MoveSpeed = 0.0f;
    private bool m_IsGround = true;
    private bool isMove = false;
    private bool introduced = false;
    public static bool inConversation = false;

    private void Awake()
    {
        dialogueTrigger = GetComponent<DialogueTrigger>();
        vendorTrigger = GetComponent<VendorTrigger>();
        if (dialogueTrigger == null)
            Debug.Log("Couldn't find dialogue trigger");
        m_RigidBody = this.GetComponentInChildren<Rigidbody>();
        m_Animator = this.GetComponentInChildren<Animator>();
        m_MoveSpeed = m_WalkSpeed;


    }

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
            Debug.Log("Couldn't find NavMeshAgent");
    }

    void Update()
    {
        if (null == m_RigidBody) return;
        if (null == m_Animator) return;
        Vector3 newDes = transform.position;
        
        Vector3 playerPos = ellen.transform.position;
        if (following && Vector3.Distance(transform.position, playerPos) > 5)
        {
            Vector3 range = new Vector3(Random.Range(-3.0f, 3.0f), 0, Random.Range(-3.0f, 3.0f));
            newDes = ellen.transform.position + range;
            //navMeshAgent.SetDestination(newDes);
            isMove = true;
            m_Animator.SetBool("isMove", isMove);
        }
        else
        {
            isMove = false;
        }


        // check ground
        float rayDistance = 0.3f;
        Vector3 rayOrigin = (this.transform.position + (Vector3.up * rayDistance * 0.5f));
        bool ground = Physics.Raycast(rayOrigin, Vector3.down, rayDistance, LayerMask.GetMask("Default"));
        if (ground != m_IsGround)
        {
            m_IsGround = ground;

            // landing
            if (m_IsGround)
            {
                m_Animator.Play("landing");
            }
        }

        // input
        Vector3 vel = m_RigidBody.velocity;
       
        Vector3 moveDirection = newDes - transform.position;
        float h = moveDirection.x;
        float v = moveDirection.z;

        m_MoveTime = isMove ? (m_MoveTime + Time.deltaTime) : 0;
        bool isRun = (m_RunningStart <= m_MoveTime);

        // move speed (walk / run)
        float moveSpeed = isRun ? m_RunSpeed : m_WalkSpeed;
        m_MoveSpeed = isMove ? Mathf.Lerp(m_MoveSpeed, moveSpeed, (8.0f * Time.deltaTime)) : m_WalkSpeed;
        //		m_MoveSpeed = moveSpeed;

        Vector3 inputDir = new Vector3(h, 0, v);
        if (1.0f < inputDir.magnitude) inputDir.Normalize();

        if (0 != h) vel.x = (inputDir.x * m_MoveSpeed);
        if (0 != v) vel.z = (inputDir.z * m_MoveSpeed);

        m_RigidBody.velocity = vel;

        if (isMove)
        {
            // rotation
            float t = (m_RotateSpeed * Time.deltaTime);
            Vector3 forward = Vector3.Slerp(this.transform.forward, inputDir, t);
            this.transform.rotation = Quaternion.LookRotation(forward);
        }

        m_Animator.SetBool("isMove", isMove);
        m_Animator.SetBool("isRun", isRun);
    }


    public void Picked()
    {
        Debug.Log("picked");
        following = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!following && other.CompareTag("Player"))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            inConversation = true;
            Debug.Log("trigger enter");
            if (introduced)
            {
                vendorTrigger.TriggerVendorMenu();
            } else {
                dialogueTrigger.TriggerDialogue();
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (!following && other.CompareTag("Player"))
        {
            if (!inConversation)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                inConversation = false;
            }
            Debug.Log("trigger exit");
            if (introduced)
            {
                vendorTrigger.EndVendorMenu();
            }
            else
            {
                dialogueTrigger.EndDialogue();
                introduced = true;
            }
        }
    }
}
