using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gateEmitter : MonoBehaviour {

    public void ExecuteGate() {

        EventManager.TriggerEvent<gateEvent, Vector3>(transform.position);
    }
}
