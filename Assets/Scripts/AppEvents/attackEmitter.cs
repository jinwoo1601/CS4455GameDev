using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackEmitter : MonoBehaviour {



    public void ExecuteAttack() {

        EventManager.TriggerEvent<attackEvent, Vector3>(transform.position);
    }
}
