using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackEmitter : MonoBehaviour {

    public void ExecuteSheath()
    {

        EventManager.TriggerEvent<sheathEvent, Vector3>(transform.position);
    }

    public void ExecuteDraw()
    {

        EventManager.TriggerEvent<drawEvent, Vector3>(transform.position);
    }

    public void ExecuteStance()
    {

        EventManager.TriggerEvent<stanceEvent, Vector3>(transform.position);
    }

    public void ExecuteAttack() {

        EventManager.TriggerEvent<attackEvent, Vector3>(transform.position);
    }
}
