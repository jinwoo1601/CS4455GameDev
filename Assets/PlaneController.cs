using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
	public DialogueManager DM;
    // Start is called before the first frame update
    void OnTriggerStay(Collider other)
    {
        DM.DisplayMessage("I have no reason to go back this way. I should keep moving forward.");
    }

    // Update is called once per frame
    void OnTriggerExit(Collider other)
    {
        DM.HideMessage();
    }
}
