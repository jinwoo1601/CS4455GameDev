using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mFootstepEmitter : MonoBehaviour {



    public void ExecutemFootstep() {

        EventManager.TriggerEvent<mFootstepEvent, Vector3>(transform.position);
    }
}
