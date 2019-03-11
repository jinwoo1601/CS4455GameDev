using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Animator), typeof(Rigidbody), typeof(CapsuleCollider))]
[RequireComponent(typeof(CharacterInputController))]
public class PlayerController : MonoBehaviour
{
    private CharacterInputController cinput;
    private Rigidbody rbody;

    //Useful if you implement jump in the future...
    //public float jumpableGroundNormalMaxAngle = 45f;
    //public bool closeToJumpableGround;

    public bool isGrounded;

    public float animationSpeed = 1f;
    public float rootMovementSpeed = 1f;
    public float rootTurnSpeed = 1f;

    public float forwardMaxSpeed = 1f;
    public float turnMaxSpeed = 2000f;
    public float jumpForce = 1f;

    public float detectableDistance = 2f;

    public GameObject buttonObject;

    private Animator anim;
    private Animation anim2;

    void Awake()
    {

        anim = GetComponent<Animator>();
        anim2 = GetComponent<Animation>();

        //if (anim == null)
        //    Debug.Log("Animator could not be found");

        rbody = GetComponent<Rigidbody>();

        if (rbody == null)
            Debug.Log("Rigid body could not be found");

        cinput = GetComponent<CharacterInputController>();
        if (cinput == null)
            Debug.Log("CharacterInput could not be found");
    }


    // Start is called before the first frame update
    void Start()
    {
        isGrounded = false;

        //never sleep so that OnCollisionStay() always reports for ground check
        rbody.sleepThreshold = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        //RaycastHit hit;
        //// Does the ray intersect any objects excluding the player layer
        //if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, detectableDistance, layerMask))
        //{
        //    promptText.text = "Press F to interact";
        //    if (Input.GetKeyDown("f"))
        //    {
        //        Debug.Log("lol");
        //        hit.collider.SendMessageUpwards("picked");
        //    }

        //}
        //else
        //{
        //    promptText.text = "";
        //}

        if(Input.GetKeyDown(KeyCode.J))
        {

            anim.SetTrigger("Attack");

        }
    }

    void FixedUpdate()
    {
        if (GameManager.GameEnd)
        {
            return;
        }

        float inputForward = 0f;
        float inputTurn = 0f;

        // input is polled in the Update() step, not FixedUpdate()
        // Therefore, you should ONLY use input state that is NOT event-based in FixedUpdate()
        // Input events should be handled in Update(), and possibly passed on to FixedUpdate() through 
        // the state of the MonoBehavior
        if (cinput.enabled)
        {
            inputForward = cinput.Forward;
            inputTurn = cinput.Turn;
        }

        //switch turn around if going backwards - From M1 BasicControlScript.cs
        if (inputForward < 0f)
            inputTurn = -inputTurn;

        //onCollisionStay() doesn't always work for checking if the character is grounded from a playability perspective
        //Uneven terrain can cause the player to become technically airborne, but so close the player thinks they're touching ground.
        //Therefore, an additional raycast approach is used to check for close ground
        //if (CharacterCommon.CheckGroundNear(this.transform.position, jumpableGroundNormalMaxAngle, 0.1f, 1f, out closeToJumpableGround))
        if (transform.position.y < 1.2f)
            isGrounded = true;

        rbody.MovePosition(rbody.position + this.transform.forward * inputForward * Time.deltaTime * forwardMaxSpeed);
        rbody.MoveRotation(rbody.rotation * Quaternion.AngleAxis(inputTurn * Time.deltaTime * turnMaxSpeed, Vector3.up));

        if (cinput.Jump && isGrounded)
        {
            rbody.AddForce(transform.up * jumpForce);
            isGrounded = false;
        }

        anim.SetFloat("velx", inputTurn);
        anim.SetFloat("vely", inputForward);
        //anim.SetBool("isFalling", !isGrounded);

    }

}
