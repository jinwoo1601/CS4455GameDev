using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEmitter : MonoBehaviour {



    public void ExecuteDeath() {

        EventManager.TriggerEvent<DeathEvent, Vector3>(transform.position);
    }
}
