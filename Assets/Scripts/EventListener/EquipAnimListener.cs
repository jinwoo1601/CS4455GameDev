using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipAnimListener : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (animatorStateInfo.IsName("equip_arm"))
        {
            BarbPlayerController.instance.equip_arm();
        }

        if (animatorStateInfo.IsName("unequip_arm"))
        {
            BarbPlayerController.instance.unequip_arm();
        }
    }
}
