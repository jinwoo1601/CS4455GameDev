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
    public int state = 0;
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
    public float disappear_speed = 5;

    public GameObject[] waypoints;
    public bool test = false;

    private Vector3 deathPosition;
    private Quaternion deathRotation;


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
    }

    void TakeDamage(float amount)
    {
        if (isDead)
            return;
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
            deathPosition = transform.position;
            deathRotation = transform.rotation;
            GameManager.instance.EnemyDeath(deathPosition, deathRotation);
            
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
            m_Animator.enabled = false;
            rgbody.isKinematic = true;
            rgbody.useGravity = true;
            rgbody.constraints = RigidbodyConstraints.None;
            test = false;
        }

        if (isDead)
        {
            
            if (Time.time - dead_time > 0.5f)
            {
                m_Animator.enabled = false;
                rgbody.isKinematic = true;
                rgbody.useGravity = true;
                rgbody.constraints = RigidbodyConstraints.None;
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
            EventManager.TriggerEvent<mDeathEvent, Vector3>(transform.position);
            return;
        }

        

            if (state == 0)
            {
                instance = targetScanner.Detect(transform);
            if (instance != null)
            {
                state = 2;
                target = instance;
                smallEnemyController.SetFollowNavmeshAgent(true);
                m_NavMeshAgent.SetDestination(instance.transform.position);
                m_Animator.SetBool("chasing_target", true);
            }
                

            }
            else if (state == 2)
            {
            instance = targetScanner.Detect(transform);
            if (instance != null)
            {
                if (Vector3.Distance(transform.position, instance.transform.position) < attack_range)
                {
                    attack_time = Time.time;
                    state = 3;
                    smallEnemyController.SetFollowNavmeshAgent(false);
                    m_Animator.SetTrigger("attack");


                }
                else if (m_NavMeshAgent.enabled)
                {
                    m_NavMeshAgent.SetDestination(target.transform.position);
                }
            }
            else {
                state = 0;
                smallEnemyController.SetFollowNavmeshAgent(false);
                m_Animator.SetBool("chasing_target", false);
            }
            


            }
            else if (state == 3)
            {
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
                        state = 2;
                        smallEnemyController.SetFollowNavmeshAgent(true);
                    }
                    else {
                        state = 0;
                        smallEnemyController.SetFollowNavmeshAgent(false);
                        m_Animator.SetBool("chasing_target", false);
                    }
                    
                    }
                }
            }

        

    }

    float CalculateHealth()
    {
        return healthPoint / maxHealthPoint;
    }

    public void OnDamage(Vector3 attackPoint, Vector3 attackForce, float AD)
    {
        if(healthPoint < 1)
        {
            return;
        }
        m_Animator.SetFloat("horizontalPoint", attackPoint.x);
        m_Animator.SetFloat("verticalPoint", attackPoint.y);
        m_Animator.ResetTrigger("hit");
        damaged_time = Time.time;
        TakeDamage(AD);
        
    }

    public bool canBeAttacked()
    {
        return Time.time > damaged_time + invulnerable_duration;
    }

    public Damageable getOwner()
    {
        return this;
    }


    public void attack_start() {
        trigger_state = true;
    }

    public void attack_end() {
        m_Animator.ResetTrigger("attack");
        transform.LookAt(instance.transform.position);
        trigger_state = false;
        
    }

    public void weapon_enable() {
        weapon.enbaleAttack();
    }

    public void weapon_disable() {
        weapon.disableAttack();
    }
}
