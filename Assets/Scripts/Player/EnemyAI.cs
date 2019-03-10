using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent), typeof(Animator))]
public class EnemyAI : MonoBehaviour
{
    private Animator animator;

    private NavMeshAgent meshAgent;

    public GameObject[] waypoints;

    public VelocityReporter chasing;

    int currWaypoint;

    public enum AIState
    {
        Waypoints,
        Chasing
    };

    AIState state;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        meshAgent = GetComponent<NavMeshAgent>();
        currWaypoint = -1;
        state = AIState.Waypoints;
        setNextWaypoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (meshAgent.remainingDistance < 0.75f && !meshAgent.pathPending)
        {
            setNextWaypoint();
        }
        else if (state == AIState.Chasing)
        {
            float distance = Vector3.Distance(chasing.transform.position, this.transform.position);
            float time = distance / meshAgent.speed;
            Vector3 destination = chasing.transform.position + chasing.velocity * time * Time.deltaTime;
            meshAgent.SetDestination(destination);
        }
        animator.SetFloat("vely", meshAgent.velocity.magnitude / meshAgent.speed);
    }


    private void setNextWaypoint()
    {
        if (waypoints.Length == 0)
            return;


        if (state == AIState.Waypoints)
        {
            currWaypoint++;
            currWaypoint = currWaypoint % waypoints.Length;
            meshAgent.SetDestination(waypoints[currWaypoint].transform.position);
        }
        else
        {
            currWaypoint = 0;
            state = AIState.Waypoints;
            meshAgent.SetDestination(waypoints[currWaypoint].transform.position);
            return;
        }

    }
}
