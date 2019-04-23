using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mDeathEmitter : MonoBehaviour {



    public void ExecutemDeath() {

        EventManager.TriggerEvent<mDeathEvent, Vector3>(transform.position);
    }
}
