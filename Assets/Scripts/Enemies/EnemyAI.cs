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

    private GameObject _player;

    public int damage = 5;

    public int EnemyHealth = 100;

    public int EnemyCurrentHealth = 100;

    private float coolDown = 3f; // 3 seconds cool down;

    int currWaypoint;

    private float nextActionTime = 0.0f;
    public float period = 0.2f;

    public enum AIState
    {
        Waypoints,
        Chasing,
        Attacking
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
        //if(Time.time < nextActionTime)
        //{
        //    return;
        //}
        //nextActionTime += period;

        coolDown -= Time.deltaTime;
        if(state == AIState.Waypoints)
        {
            if (meshAgent.remainingDistance < 0.1f && !meshAgent.pathPending)
            {
                setNextWaypoint();
            }
        }
        else if (state == AIState.Chasing)
        {
            chasePlayer();
        }
        else if (state == AIState.Attacking)
        {
            if (coolDown <= 0)
            {
                Attack();
            }
        }

        //animator.SetFloat("vely", meshAgent.velocity.magnitude / meshAgent.speed);
    }


    private void TakeDamage(int val)
    {
        //EnemyCurrentHealth -= val;
        //if(EnemyCurrentHealth == 0)
        //{
        //    GameManager.instance.EnemyDeath();
        //}
        gameObject.SetActive(false);

        GameManager.instance.EnemyDeath();
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

    private void chasePlayer()
    {
        Vector3 pos = Vector3.Lerp(_player.transform.position, transform.position, 0.1f);
        meshAgent.SetDestination(pos);
        animator.SetInteger("state", 1);
    }

    private void PlayerDetected(GameObject player)
    {
        _player = player;
        state = AIState.Chasing;
        chasePlayer();
    }

    private void PlayerLoss(GameObject player)
    {
        state = AIState.Waypoints;
        setNextWaypoint();
    }

    private void PlayerInAttackRange(GameObject player)
    {
        _player = player;
        state = AIState.Attacking;
        Attack();
        meshAgent.isStopped = true;
    }

    private void PlayerOutAttackRange(GameObject player)
    {
        _player = player;
        state = AIState.Chasing;
        chasePlayer();
        meshAgent.isStopped = false;
    }

    private void Attack()
    {
        coolDown = 1f;
        _player.SendMessage("TakeDamage", damage);
        animator.SetInteger("state", 2);
    }
}
