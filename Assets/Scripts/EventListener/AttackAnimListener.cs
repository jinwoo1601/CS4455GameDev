using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimListener : StateMachineBehaviour
{

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        // Reminder: handled by animation event;
        //PlayerInput.Instance.start_attack();
        //BarbPlayerController.instance.m_weapon.enbaleAttack();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        // Reminder: handled by animation event;
        //BarbPlayerController.instance.m_weapon.disableAttack();
    }
}
