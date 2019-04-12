using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Damageable
{



    void OnDamage(Vector3 attackPoint, Vector3 attackForce);

    bool canBeAttacked();

    Damageable getOwner();


}
