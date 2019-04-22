using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnAnimListener : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if(BarbPlayerController.instance != null)
            BarbPlayerController.instance.respawned();
    }
}
