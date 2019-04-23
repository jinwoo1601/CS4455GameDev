using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(NavMeshAgent))]
public class SmallEnemyAI : MonoBehaviour, Damageable
{
    public SmallEnemyController smallEnemyController;

    protected NavMeshAgent m_NavMeshAgent;
    protected Animator m_Animator;
    protected Rigidbody rgbody;

    public volatile bool trigger_state = false;

    TargetScanner targetScanner;
    BarbPlayerController target;
    public BarbPlayerController instance;

    //0 - idle,  2-chasing, 3-attack
    public enemyState state;
    public enemyState defaultState = enemyState.idle;
    private float attack_time;
    public float attack_range = 1.0f;

    public GameObject healthUI;
    public Slider healthBar;
    public float maxHealthPoint = 5;
    public float healthPoint = 5;
    public bool isDead = false;
    public float dead_time;

    public bool damaged = false;
    public float damaged_time;
    public float invulnerable_duration = 1f;
    public float disappear_speed = 10f;

    public GameObject[] waypoints;
    public int curWP = -1;
    public bool test = false;

    private Vector3 deathPosition;
    private Quaternion deathRotation;

    public enum enemyState
    {
        idle,
        patrol,
        chasing,
        attacking
    }


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
        target = BarbPlayerController.instance;
        healthBar.value = CalculateHealth();
        state = defaultState;
        if (defaultState == enemyState.patrol)
        {
            m_Animator.SetBool("patrol", true);
        } 
        else
        {
            AnimatorStateInfo stateInfo = m_Animator.GetCurrentAnimatorStateInfo(0);
            m_Animator.Play(stateInfo.fullPathHash, -1, Random.Range(0f, 1f));
        }
    }

    void TakeDamage(float amount)
    {
        if (isDead)
            return;

        transform.LookAt(BarbPlayerController.instance.transform);
        if (damaged && Time.time - damaged_time < invulnerable_duration)
        {
            return;
        }
        m_Animator.SetTrigger("hit");
        healthPoint -= amount;
        if (healthPoint < 1)
        {
            isDead = true;
            dead_time = Time.time;
            m_Animator.SetBool("isDead", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (healthPoint < maxHealthPoint)
        {
            healthUI.SetActive(true);
        }

        healthBar.value = CalculateHealth();


    }

    void FixedUpdate()
    {
        if (test)
        {
            GameManager.instance.MoveToScene("l6");
            m_Animator.enabled = false;
            rgbody.isKinematic = true;
            rgbody.useGravity = true;
            rgbody.constraints = RigidbodyConstraints.None;
            test = false;
        }

        if (isDead)
        {
            if (Time.time - dead_time > disappear_speed)
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



        switch (state)
        {
            case enemyState.patrol:
            case enemyState.idle:
                if(state == enemyState.patrol)
                {
                    m_NavMeshAgent.enabled = true;
                    if (m_NavMeshAgent.remainingDistance < 0.1f && !m_NavMeshAgent.pathPending)
                    {
                        setNextWaypoint();
                    }
                }
                instance = targetScanner.Detect(transform);
                if (instance != null)
                {
                    state = enemyState.chasing;
                    target = instance;
                    smallEnemyController.SetFollowNavmeshAgent(true);
                    m_NavMeshAgent.SetDestination(instance.transform.position);
                    m_Animator.SetBool("chasing_target", true);
                }
                break;
            case enemyState.chasing:
                transform.LookAt(BarbPlayerController.instance.transform);
                instance = targetScanner.Detect(transform);
                if (instance != null)
                {
                    if (Vector3.Distance(transform.position, instance.transform.position) < attack_range)
                    {
                        attack_time = Time.time;
                        state = enemyState.attacking;
                        smallEnemyController.SetFollowNavmeshAgent(false);
                        m_Animator.SetTrigger("attack");


                    }
                    else if (m_NavMeshAgent.enabled)
                    {
                        m_NavMeshAgent.SetDestination(target.transform.position);
                    }
                }
                else
                {
                    state = defaultState;
                    smallEnemyController.SetFollowNavmeshAgent(false);
                    m_Animator.SetBool("chasing_target", false);
                }
                break;
            case enemyState.attacking:
                transform.LookAt(BarbPlayerController.instance.transform);
                if (!trigger_state)
                {
                    if (Vector3.Distance(transform.position, instance.transform.position) < attack_range)
                    {
                        //attack again
                        smallEnemyController.SetFollowNavmeshAgent(false);
                        m_Animator.SetTrigger("attack");
                    }
                    else
                    {
                        //back to chasing or detecting
                        instance = targetScanner.Detect(transform);
                        if (instance != null)
                        {
                            state = enemyState.chasing;
                            smallEnemyController.SetFollowNavmeshAgent(true);
                        }
                        else
                        {
                            state = defaultState;
                            smallEnemyController.SetFollowNavmeshAgent(false);
                            m_Animator.SetBool("chasing_target", false);
                        }
                    }
                }
                break;
        }
    }

    float CalculateHealth()
    {
        return healthPoint / maxHealthPoint;
    }

    void setNextWaypoint()
    {
        if (waypoints.Length == 0)
            return;

        m_NavMeshAgent.SetDestination(waypoints[++curWP % waypoints.Length].transform.position);
    }

    public void OnDamage(Vector3 attackPoint, Vector3 attackForce, float AD)
    {
        if (healthPoint < 1)
        {
            return;
        }
        m_Animator.SetFloat("horizontalPoint", attackPoint.x);
        m_Animator.SetFloat("verticalPoint", attackPoint.y);
        m_Animator.ResetTrigger("hit");
        damaged_time = Time.time;
        TakeDamage(AD);
    }

    public void died()
    {
        deathPosition = transform.position;
        deathRotation = transform.rotation;
        GameManager.instance.EnemyDeath(deathPosition, deathRotation);
        m_NavMeshAgent.enabled = false;
        m_Animator.enabled = false;
        rgbody.isKinematic = true;
        rgbody.useGravity = true;
        rgbody.constraints = RigidbodyConstraints.None;
        GetComponent<BoxCollider>().enabled = false;
    }

    public bool canBeAttacked()
    {
        return Time.time > damaged_time + invulnerable_duration;
    }

    public Damageable getOwner()
    {
        return this;
    }


    public void attack_start()
    {
        trigger_state = true;
    }

    public void attack_end()
    {
        m_Animator.ResetTrigger("attack");
        transform.LookAt(instance.transform.position);
        trigger_state = false;

    }

    public void weapon_enable()
    {
        weapon.enbaleAttack();
    }

    public void weapon_disable()
    {
        weapon.disableAttack();
    }
}
