using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TargetScanner
{

    public float heightOffset = 0.0f;
    public float detectionRadius = 10;
    [Range(0.0f, 360.0f)]
    public float detectionAngle = 270;
    public float maxHeightDifference = 1.0f;
    public LayerMask viewBlockerLayerMask;

    public BarbPlayerController Detect(Transform detector, bool useHeightDifference = true)
    {
        //if either the player is not spwned or they are spawning, we do not target them
        if (BarbPlayerController.instance == null)
            return null;

        Vector3 eyePos = detector.position + Vector3.up * heightOffset;
        Vector3 toPlayer = BarbPlayerController.instance.transform.position - eyePos;
        Vector3 toPlayerTop = BarbPlayerController.instance.transform.position + Vector3.up * 1.5f - eyePos;

        if (useHeightDifference && Mathf.Abs(toPlayer.y + heightOffset) > maxHeightDifference)
        { //if the target is too high or too low no need to try to reach it, just abandon pursuit
           
            return null;
        }

        Vector3 toPlayerFlat = toPlayer;
        toPlayerFlat.y = 0;
        
        if (toPlayerFlat.sqrMagnitude <= detectionRadius * detectionRadius)
        {
            if (Vector3.Dot(toPlayerFlat.normalized, detector.forward) >
                Mathf.Cos(detectionAngle * 0.5f * Mathf.Deg2Rad))
            {

                bool canSee = false;

                Debug.DrawRay(eyePos, toPlayer, Color.blue);
                Debug.DrawRay(eyePos, toPlayerTop, Color.blue);

                canSee |= !Physics.Raycast(eyePos, toPlayer.normalized, detectionRadius,
                    viewBlockerLayerMask, QueryTriggerInteraction.Ignore);

                canSee |= !Physics.Raycast(eyePos, toPlayerTop.normalized, toPlayerTop.magnitude,
                    viewBlockerLayerMask, QueryTriggerInteraction.Ignore);

                if (canSee)
                    return BarbPlayerController.instance;
            }
        }

        return null;
    }
}
