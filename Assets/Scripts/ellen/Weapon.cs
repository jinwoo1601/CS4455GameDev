using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{


    public Collider weaponPoint;

    private Vector3 lastUpdate;

    private Vector3 velocity;

    public float AD = 1;

    public void enbaleAttack()
    {

        weaponPoint.enabled = true;
    }

    public void disableAttack()
    {
        weaponPoint.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        weaponPoint.enabled = false;
        lastUpdate = weaponPoint.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        velocity = weaponPoint.transform.position - lastUpdate;
        lastUpdate = weaponPoint.transform.position;
    }


    private void OnTriggerEnter(Collider other)
    {
        Damageable other_damageable = other.gameObject.GetComponent<Damageable>();
        if (other_damageable != null)
        {
            if (other_damageable.canBeAttacked() && other_damageable.getOwner() != this.gameObject.GetComponentInParent<Damageable>())
            {
                RaycastHit hit;
                Physics.Raycast(transform.position, transform.forward, out hit);
                other_damageable.OnDamage(hit.point, velocity, AD);
            }
        }

    }



}
