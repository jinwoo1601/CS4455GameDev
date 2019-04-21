using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RespawnListener : StateMachineBehaviour
{
    bool triggered = false;
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!triggered)
        {
            triggered = UIGuideManager.instance.triggerStateEvent(animator, stateInfo, layerIndex);
        }
        
    }
}
