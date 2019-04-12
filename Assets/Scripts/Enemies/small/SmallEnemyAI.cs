using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SmallEnemyAI : MonoBehaviour, Damageable
{
    public SmallEnemyController smallEnemyController;

    protected NavMeshAgent m_NavMeshAgent;
    protected Animator m_Animator;
    protected Rigidbody rgbody;

    public bool trigger_state = false;

    TargetScanner targetScanner;
    EllenPlayerController target;
    public EllenPlayerController instance;

    //0 - idle,  2-chasing, 3-attack, 4-attack stop, 5-take damage.
    public int state = 0;
    private float attack_time;
    public float attack_range = 1.0f;

    public int healthPoint = 1;
    public bool isDead = false;
    public float dead_time;

    public bool damaged = false;
    public float damaged_time;
    public float invulnerable_duration = 2f;

    public float disappear_speed = 5;

    public GameObject[] waypoints;
    public bool test = false;

    public Weapon weapon;

    // Start is called before the first frame update
    void Start()
    {
        smallEnemyController = GetComponent<SmallEnemyController>();
        targetScanner = new TargetScanner();
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        m_Animator = GetComponent<Animator>();
        smallEnemyController.SetFollowNavmeshAgent(false);
        rgbody = GetComponent<Rigidbody>();
        target = EllenPlayerController.instance;
    }

    void TakeDamage(int amount)
    {
        if (isDead)
            return;
        if (damaged && Time.time - damaged_time < invulnerable_duration)
        {
            return;
        }
        healthPoint -= amount;
        if (healthPoint <= 0)
        {
            //GameManager.instance.EnemyDeath();
            state = 5;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (test)
        {
            TakeDamage(1);
            test = false;
        }

        if (isDead)
        {
            Debug.Log(Time.time);
            if (Time.time - dead_time > 1.6f)
            {
                m_Animator.enabled = false;
                rgbody.isKinematic = false;
            }
            if (Time.time - dead_time > 3f)
            {
                // TODO: not working. not sure why.
                SkinnedMeshRenderer[] rs = GetComponentsInChildren<SkinnedMeshRenderer>();
                foreach (SkinnedMeshRenderer rderer in rs)
                {
                    Color cur = rderer.material.color;
                    Color tar = cur;
                    tar.a = 0f;
                    rderer.material.color = tar; //Color.Lerp(cur, tar, disappear_speed * Time.deltaTime);
                }

                Destroy(gameObject);

            }
            return;
        }

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
                    weapon.enbaleAttack();
                }
                else if (m_NavMeshAgent.enabled)
                {
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
                    else
                    {
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
                    weapon.disableAttack();
                }
            }
            else if (state == 5)
            {
                state = 6;
                m_Animator.SetBool("hit", true);
                isDead = true;
                dead_time = Time.time;

            }

        }
        if (instance == null || Vector3.Distance(transform.position, instance.transform.position) < attack_range)
        {
            Vector3 _direction = (target.transform.position - transform.position).normalized;
            Quaternion _lookRotation = Quaternion.LookRotation(_direction);
            Quaternion m_TargetRotation = Quaternion.RotateTowards(transform.rotation, _lookRotation, 1f * Time.deltaTime);

            transform.rotation = m_TargetRotation;
            if (state == 2)
            {
                m_NavMeshAgent.SetDestination(target.transform.position);
            }
        }

    }

    public void OnDamage(Vector3 attackPoint, Vector3 attackForce)
    {
        Debug.Log("hit");
        TakeDamage(1);
    }

    public bool canBeAttacked()
    {
        return true;
    }

    public Damageable getOwner()
    {
        return this;
    }
}
