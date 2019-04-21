using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;


public interface StateMachineListener
{
    void triggerStateEvent(Animator animator, AnimatorStateInfo stateInfo, int layerIndex);
}
