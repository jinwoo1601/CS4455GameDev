using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimListener : StateMachineBehaviour
{

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (animatorStateInfo.IsName("peaceAndLove"))
        {
            PlayerInput.Instance.stop_attack();
            return;
        }
        PlayerInput.Instance.start_attack();
    }
    
}
