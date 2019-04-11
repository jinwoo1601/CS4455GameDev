using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SmallEnemyAI : MonoBehaviour
{
    public SmallEnemyController smallEnemyController;

    protected NavMeshAgent m_NavMeshAgent;
    protected Animator m_Animator;

    public bool trigger_state = false;

    TargetScanner targetScanner;

    EllenPlayerController target;

    public EllenPlayerController instance;

    //0 - idle,  2-chasing, 3-attack, 4-attack stop
    public int state = 0;
    private float attack_time;

    public float attack_range = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        smallEnemyController = GetComponent<SmallEnemyController>();
        targetScanner = new TargetScanner();
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        m_Animator = GetComponent<Animator>();
        smallEnemyController.SetFollowNavmeshAgent(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        instance = targetScanner.Detect(transform);
        
        if (instance != null)
        {
            if (state == 0)
            {
                state = 2;
                target = instance;
                smallEnemyController.SetFollowNavmeshAgent(true);
                m_NavMeshAgent.SetDestination(instance.transform.position);
                m_Animator.SetBool("chasing_target", true);
                
            }
            else if (state == 2)
            {

                if (Vector3.Distance(transform.position, instance.transform.position) < attack_range)
                {
                    attack_time = Time.time;
                    state = 3;
                    smallEnemyController.SetFollowNavmeshAgent(false);
                    m_Animator.SetTrigger("attack");
                    trigger_state = true;

                }
                else if (m_NavMeshAgent.enabled) {
                    m_NavMeshAgent.SetDestination(target.transform.position);
                }


            }
            else if (state == 3 && Time.time - attack_time > 1.2f)
            {

                if (Vector3.Distance(transform.position, instance.transform.position) < attack_range)
                {
                    
                    Quaternion m_TargetRotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(target.transform.position - transform.position), 1f * Time.deltaTime);

                    transform.rotation = m_TargetRotation;
                    if (trigger_state)
                    {
                        m_Animator.ResetTrigger("attack");
                        trigger_state = false;
                    }
                    else {
                        attack_time = Time.time;
                        m_Animator.SetTrigger("attack");
                        trigger_state = true;
                    }
                    

                    
                }
                else
                {

                    state = 2;
                    smallEnemyController.SetFollowNavmeshAgent(true);
                    m_Animator.ResetTrigger("attack");
                }
            }

        }
        if (instance == null || Vector3.Distance(transform.position, instance.transform.position) < attack_range)
        {
            Vector3 _direction = (target.transform.position - transform.position).normalized;
            Quaternion _lookRotation = Quaternion.LookRotation(_direction);
            Quaternion m_TargetRotation = Quaternion.RotateTowards(transform.rotation, _lookRotation, 1f * Time.deltaTime);

            transform.rotation = m_TargetRotation;
            if (state == 2) {
                m_NavMeshAgent.SetDestination(target.transform.position);
            }
        }

    }
}
