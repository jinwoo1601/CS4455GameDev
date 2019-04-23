// --------------------------------------
// This script is totally optional. It is an example of how you can use the
// destructible versions of the objects as demonstrated in my tutorial.
// Watch the tutorial over at http://youtube.com/brackeys/.
// --------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour, Damageable {

	public GameObject destroyedVersion; // Reference to the shattered version of the object

    public bool canBeAttacked()
    {
        return true;
    }

    public Damageable getOwner()
    {
        return this;
    }

    public void OnDamage(Vector3 attackPoint, Vector3 attackForce, float AD)
    {
        Debug.Log("on damage");
        destructed();
        EventManager.TriggerEvent<glassEvent, Vector3>(transform.position);
    }

    // If the player clicks on the object
    void destructed ()
	{
		// Spawn a shattered object
		Instantiate(destroyedVersion, transform.position, transform.rotation);
        // Remove the current object
        Destroy(gameObject);
	}



}
