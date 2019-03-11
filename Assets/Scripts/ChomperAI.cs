using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent), typeof(Animator))]
public class ChomperAI : MonoBehaviour
{

    private Animator animator;

    private NavMeshAgent meshAgent;

    public GameObject chasing;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        meshAgent = GetComponent<NavMeshAgent>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (meshAgent.remainingDistance < 0.05f && !meshAgent.pathPending) {
            animator.SetInteger("state", 2);
            meshAgent.SetDestination(chasing.transform.position);
        }
        else
        {
           
            meshAgent.SetDestination(chasing.transform.position);
            animator.SetInteger("state", 1);
        }
        
    }
}
