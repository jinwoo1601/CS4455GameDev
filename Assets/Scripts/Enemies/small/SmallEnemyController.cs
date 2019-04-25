﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[DefaultExecutionOrder(-1)]
[RequireComponent(typeof(NavMeshAgent))]
public class SmallEnemyController : MonoBehaviour
{
    public Animator animator { get { return m_Animator; } }
    public Vector3 externalForce { get { return m_ExternalForce; } }
    public NavMeshAgent navmeshAgent { get { return m_NavMeshAgent; } }
    public bool followNavmeshAgent { get { return m_FollowNavmeshAgent; } }


    protected NavMeshAgent m_NavMeshAgent;
    protected bool m_FollowNavmeshAgent;
    protected Animator m_Animator;
    protected bool m_UnderExternalForce;
    protected bool m_ExternalForceAddGravity = false;
    protected Vector3 m_ExternalForce;

    protected Rigidbody m_Rigidbody;

    void OnEnable()
    {
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        m_Animator = GetComponent<Animator>();
        m_Animator.updateMode = AnimatorUpdateMode.AnimatePhysics;
        m_ExternalForceAddGravity = false;

        m_Rigidbody = GetComponentInChildren<Rigidbody>();
        if (m_Rigidbody == null)
            m_Rigidbody = gameObject.AddComponent<Rigidbody>();

        m_Rigidbody.isKinematic = false;
        m_Rigidbody.useGravity = true;
        m_Rigidbody.interpolation = RigidbodyInterpolation.Interpolate;

        m_FollowNavmeshAgent = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //animator.speed = 1;


        if (m_UnderExternalForce)
            ForceMovement();
    }

    void ForceMovement()
    {
        if (m_ExternalForceAddGravity)
            m_ExternalForce += Physics.gravity * Time.deltaTime;

        RaycastHit hit;
        Vector3 movement = m_ExternalForce * Time.deltaTime;
        if (!m_Rigidbody.SweepTest(movement.normalized, out hit, movement.sqrMagnitude))
        {
            movement.y = 0;
            m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
        }

        m_NavMeshAgent.Warp(m_Rigidbody.position);
    }

    private void OnAnimatorMove()
    {
        if (m_UnderExternalForce)
            return;

        if (m_FollowNavmeshAgent)
        {
            m_NavMeshAgent.speed = (m_Animator.deltaPosition / Time.deltaTime).magnitude;
            transform.position = m_NavMeshAgent.nextPosition;
        }
        else
        {
            RaycastHit hit;
            if (!m_Rigidbody.SweepTest(m_Animator.deltaPosition.normalized, out hit,
                m_Animator.deltaPosition.sqrMagnitude))
            {
                m_Rigidbody.MovePosition(m_Rigidbody.position + m_Animator.deltaPosition);
            }
        }
        transform.forward = m_Animator.deltaRotation * transform.forward;
    }

    // used to disable position being set by the navmesh agent, for case where we want the animation to move the enemy instead (e.g. Chomper attack)
    public void SetFollowNavmeshAgent(bool follow)
    {
        if (!follow && m_NavMeshAgent.enabled)
        {
            m_NavMeshAgent.ResetPath();
        }
        else if (follow && !m_NavMeshAgent.enabled)
        {
            m_NavMeshAgent.Warp(transform.position);
        }

        m_FollowNavmeshAgent = follow;
        m_NavMeshAgent.enabled = follow;
    }

    public void AddForce(Vector3 force, bool useGravity = true)
    {
        if (m_NavMeshAgent.enabled)
            m_NavMeshAgent.ResetPath();

        m_ExternalForce = force;
        m_NavMeshAgent.enabled = false;
        m_UnderExternalForce = true;
        m_ExternalForceAddGravity = useGravity;
    }

    public void ClearForce()
    {
        m_UnderExternalForce = false;
        m_NavMeshAgent.enabled = true;
    }

    public void SetForward(Vector3 forward)
    {
        Quaternion targetRotation = Quaternion.LookRotation(forward);

        transform.rotation = targetRotation;
    }

    public void SetTarget(Vector3 position)
    {
        m_NavMeshAgent.destination = position;
    }
}
