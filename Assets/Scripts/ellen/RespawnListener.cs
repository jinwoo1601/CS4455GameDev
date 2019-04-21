﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RespawnListener : StateMachineBehaviour
{

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        UIGuideManager.instance.triggerStateEvent(animator, stateInfo, layerIndex);
        
    }
}
