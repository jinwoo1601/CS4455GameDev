using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnAnimListener : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        BarbPlayerController.instance.respawned();
    }
}
